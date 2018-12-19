
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using BeimingForce.Enum;
using BeimingForce.Helper;
using BeimingForce.Model;
using BeimingForce.ToolsKit;
using Fasterflect;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace BeimingForce.Engine
{
    public class CSharpScriptEngine : IDynamicScript, IAssemblyProcess<CSharpScriptEngine>
    {
        private bool _compiled;
        private bool _scriptCommon;
        private string _scriptHash;
        private readonly List<Assembly> _usingAssemblies;
        private readonly List<string> _usingNameSpaces;

        private string UsingNameSpaces
        {
            get
            {
                var namespaceStringBuilder = new StringBuilder();
                foreach (var @namespace in _usingNameSpaces)
                {
                    if (!String.IsNullOrEmpty(@namespace) && !@namespace.StartsWith("//"))
                    {
                        if (@namespace.StartsWith(Const.Using))
                            namespaceStringBuilder.AppendLine(@namespace);
                        else
                            namespaceStringBuilder.AppendLine(Const.Using + " " + @namespace + ";");
                    }
                }

                return namespaceStringBuilder.ToString();
            }
        }

        private readonly string _applicationName;
        private static readonly BeimingForceConfig _beimingForceConfig;

        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, List<MetadataReference>>
            _metadataReferences = new ConcurrentDictionary<string, List<MetadataReference>>();

        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, Type> _scriptTypeDict =
            new System.Collections.Concurrent.ConcurrentDictionary<string, Type>();

        public string Language => throw new NotImplementedException();
        public bool Compiled => _compiled;

        #region  static constructors

        static CSharpScriptEngine()
        {
            _beimingForceConfig = new BeimingForceConfig();
        }

        public CSharpScriptEngine InitMetadataReference()
        {
            List<MetadataReference> metadataReferences = new List<MetadataReference>();
            foreach (var assembly in _usingAssemblies)
            {
                var metadata = MetadataReference.CreateFromFile(assembly.Location);
                metadataReferences.Add(metadata);
            }

            _metadataReferences.TryAdd(_applicationName, metadataReferences);
            ReferenceNecessaryAssembly();
            return this;
        }

        private void ReferenceNecessaryAssembly()
        {
            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            var references = new List<MetadataReference>();
            {
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefMscorlib));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefSystem));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefSystemCore));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefMicrosoftCSharp));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefCollections));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefSystemRuntime));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefSystemConsole));
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, Const.RefNetstandard));
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);
            };

            foreach (var dllName in Const.RefSystemAll)
            {
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, dllName)));
            }
            _metadataReferences[_applicationName].AddRange(references);

        }

        #endregion

        public CSharpScriptEngine(string applicationName)
        {
            _applicationName = applicationName;
            _usingAssemblies = new List<Assembly>();
            _usingNameSpaces = new List<string>();

        }

        public CSharpScriptEngine LoadNameSpaces(string[] nameSpaces)
        {
            if (nameSpaces != null && nameSpaces.Length > 0)
            {
                foreach (var nameSpace in nameSpaces)
                {
                    _usingNameSpaces.Add(nameSpace);
                }
            }

            return this;
        }

        public CSharpScriptEngine LoadAssembly(Assembly[] assemblies)
        {
            if (assemblies != null && assemblies.Length > 0)
            {
                foreach (var assembly in assemblies)
                {
                    _usingAssemblies.Add(assembly);
                }
            }
            return this;
        }

        public CSharpScriptEngine LoadAssembly(string[] assemblyNames)
        {
            if (assemblyNames != null && assemblyNames.Length > 0)
            {
                foreach (var assemblyName in assemblyNames)
                {
                    var assemblies = RuntimeHelper.GetAssembliesByName(assemblyName);
                    _usingAssemblies.AddRange(assemblies.Where(x => x != null));
                }
            }
            return this;
        }


        public CSharpScriptEngine BuildDynamicScript(DynamicScriptCompileTime compileTime)
        {
            string errorMsg;
            var script = GetScript(compileTime.ScriptText.Trim());
            _scriptHash = GeneratedHash(script);
            BuildDynamicScript(script, out errorMsg);
            return this;
        }

        public string GeneratedHash(string scriptText)
        {
            return string.Format(Const.AssemblyScriptKey, DynamicScriptLanguageEnum.CSharp, _applicationName,
                scriptText.Md5());
        }

        private void BuildDynamicScript(string script, out string errorMsg)
        {
            string typeName = Const.MethodTypeName;
            var asm = CreateAsmExecutor(script, out errorMsg);
            if (asm != null)
            {
                if (!_scriptTypeDict.ContainsKey(_scriptHash))
                {
                    var type = asm.GetType(typeName);
                    _scriptTypeDict.TryAdd(_scriptHash, type);
                }
            }
        }

        private Assembly CreateAsmExecutor(string script, out string errorMsg)
        {
            errorMsg = null;
            var assemblyName = _scriptHash;
            var sourceTree = CSharpSyntaxTree.ParseText(script,
                path: Path.Combine(_beimingForceConfig.AppLibPath, assemblyName + ".cs"),
                encoding: Encoding.UTF8);

            var compilation = CSharpCompilation.Create(assemblyName,
                new[] { sourceTree }, _metadataReferences[_applicationName],
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithOptimizationLevel(
                    OptimizationLevel.Release));

            Assembly assembly;
            using (var assemblyStream = new MemoryStream())
            {
                using (var pdbStream = new MemoryStream())
                {
                    var emitResult = compilation.Emit(assemblyStream, pdbStream);
                    if (emitResult.Success)
                    {
                        var assemblyBytes = assemblyStream.GetBuffer();
                        var pdbBytes = pdbStream.GetBuffer();
                        assembly = Assembly.Load(assemblyBytes, pdbBytes);
                        if (_scriptCommon)
                            SaveAssembly(assemblyName, assemblyBytes);
                        _compiled = true;
                    }
                    else
                    {
                        foreach (var item in emitResult.Diagnostics)
                        {
                            Console.WriteLine(item);
                        }
                        _compiled = false;
                        return null;
                    }
                }
            }

            return assembly;
        }

        private void SaveAssembly(string assemblyName, byte[] assemblyBytes)
        {
            var path = FolderHelper.CreateFolder(_beimingForceConfig.AppLibPath);
            FolderHelper.CreateFile(Path.Combine(path, assemblyName), assemblyBytes);
        }

        private string GetScript(string scriptText)
        {
            scriptText = scriptText.ClearScript();
            if (scriptText.StartsWith(Const.Using))
            {
                _scriptCommon = true;
                return scriptText;
            }

            var script = Const.CSharpTemplate
                .Replace(Const.CSharpTemplateMainFunctionPlaceholder, scriptText)
                .Replace(Const.CSharpTemplateNameSpacesPlaceholder, UsingNameSpaces);
            return script.RemoveEmptyLines();
        }

        public CSharpScriptEngine BuildDynamicScript()
        {
            throw new NotImplementedException();
        }

        public dynamic Execute(string code)
        {
            throw new NotImplementedException();
        }

        public T Execute<T>(string code)
        {
            throw new NotImplementedException();
        }

        public bool HasCompileError(string code, out string errorMessage, string[] nameSpaces = null)
        {
            throw new NotImplementedException();
        }

        public T CallFunction<T>(string functionName, params object[] parameters)
        {
            if (_scriptHash != null && _scriptTypeDict.ContainsKey(_scriptHash))
            {
                var type = _scriptTypeDict[_scriptHash];
                if (true)
                {
                    var instance = Activator.CreateInstance(type);
                    var result = instance.TryCallMethodWithValues(functionName, parameters);
                    return (T)result;
                }
            }
            return default(T);
        }
    }
}
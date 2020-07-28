using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BeimingForce.Core;
using BeimingForce.Enum;
using BeimingForce.Helper;
using BeimingForce.Model;
using BeimingForce.ToolsKit;
using Fasterflect;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace BeimingForce.Engine.ScriptEngine
{
    public class CSharpScriptEngine : IDynamicScript
    {
        private readonly DynamicScriptDefinition _definition;
        private BeimingAssemblyLoadContext _context;
        private List<MetadataReference> _metadataReferences;
        private Type _type;

        private bool _compiled;
        private bool _scriptCommon;
        private string _scriptHash;

        private readonly List<Assembly> _usingAssemblies;
        private readonly List<string> _usingNameSpaces;

        public List<string> CompileErrorMessage { get; set; }

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

        private static readonly BeimingForceConfig _beimingForceConfig;

        public string Language => _definition.Script.Language.ToString();

        public bool Compiled => _compiled;

        public CSharpScriptEngine(DynamicScriptDefinition definition)
        {
            _definition = definition;
            CompileErrorMessage = new List<string>();
        }


        public void SetAssemblyLoadContext(BeimingAssemblyLoadContext context)
        {
            _context = context;
        }

        public void SetMetadataReferences(List<MetadataReference> metadataReferences)
        {
            _metadataReferences = metadataReferences;
        }

        public T CallFunction<T>(params object[] parameters)
        {
            var functionName = _definition.Script.FunctionName;

            var instance = Activator.CreateInstance(_type);

            return (T) instance.TryCallMethodWithValues(functionName, parameters);
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

        private string GeneratedHash(string scriptText)
        {
            return string.Format(Const.AssemblyScriptKey, DynamicScriptLanguageEnum.CSharp, "x", scriptText.Md5());
        }

        public IDynamicScript BuildDynamicScript()
        {
            var rawScript = _definition.Script.CompileTime.ScriptText.Trim();
            var script = GetScript(rawScript);
            _scriptHash = GeneratedHash(script);
            const string typeName = Const.MethodTypeName;
            var assembly = CreateAsmExecutor(script);
            if (assembly!=null)
            {
                _type = assembly.GetType(typeName);
            }
            return this;
        }


        private Assembly CreateAsmExecutor(string script)
        {
            var assemblyName = _scriptHash;
            // var sourceTree = CSharpSyntaxTree.ParseText(script,
            //     path: Path.Combine(_beimingForceConfig.AppLibPath, assemblyName + ".cs"),
            //     encoding: Encoding.UTF8);

            var sourceTree = CSharpSyntaxTree.ParseText(script);

            var compilation = CSharpCompilation.Create(assemblyName,
                new[] {sourceTree}, _metadataReferences,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithOptimizationLevel(
                    OptimizationLevel.Release));

            Assembly assembly = null;
            using var assemblyStream = new MemoryStream();
            using var pdbStream = new MemoryStream();
            var emitResult = compilation.Emit(assemblyStream, pdbStream);
            if (emitResult.Success)
            {
                assemblyStream.Position = 0;
                pdbStream.Position = 0;
                assembly = _context.LoadFromStream(assemblyStream, pdbStream);
                _compiled = true;
            }
            else
            {
                foreach (var item in emitResult.Diagnostics)
                {
                    CompileErrorMessage.Add(item.ToString());
                }

                _compiled = false;
            }

            return assembly;
        }

        private void SaveAssembly(string assemblyName, byte[] assemblyBytes)
        {
            var path = FolderHelper.CreateFolder(_beimingForceConfig.AppLibPath);
            FolderHelper.CreateFile(Path.Combine(path, assemblyName), assemblyBytes);
        }

        public bool HasCompileError(string code, out string errorMessage, string[] nameSpaces = null)
        {
            throw new NotImplementedException();
        }
    }
}
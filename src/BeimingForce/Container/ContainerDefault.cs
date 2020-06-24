using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using BeimingForce.Core;
using BeimingForce.Core.Runner;
using BeimingForce.Engine;
using BeimingForce.Helper;
using BeimingForce.Model;
using BeimingForce.ToolsKit;
using Fasterflect;
using Microsoft.CodeAnalysis;

namespace BeimingForce.Container
{
    internal class ContainerDefault : IContainer
    {
        private readonly BeimingAssemblyLoadContext _context;

        private readonly HashSet<Assembly> _usingAssemblies;

        private readonly List<MetadataReference> _metadataReferences;

        private readonly ConcurrentDictionary<string, IRunner> _runners;

        public string ContainerName { get; set; }

        public ContainerDefault(string containerName)
        {
            _context = new BeimingAssemblyLoadContext();
            _usingAssemblies = new HashSet<Assembly>();
            _metadataReferences = new List<MetadataReference>();
            _runners = new ConcurrentDictionary<string, IRunner>();
            ContainerName = containerName;
            Init();
        }

        private void Init()
        {
            LoadDefaultAssembly();
            InitMetadataReference();
        }

        private void LoadDefaultAssembly()
        {
            var allAssemblies = RuntimeHelper.GetAllAssemblies();
            foreach (var allAssembly in allAssemblies)
            {
                _usingAssemblies.Add(allAssembly);
            }
        }

        private void InitMetadataReference()
        {
            foreach (var assembly in _usingAssemblies)
            {
                var metadata = MetadataReference.CreateFromFile(assembly.Location);
                _metadataReferences.Add(metadata);
            }

            ReferenceNecessaryAssembly();
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
            }

            foreach (var dllName in Const.RefSystemAll)
            {
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, dllName)));
            }

            _metadataReferences.AddRange(references);
        }

        public IRunner RegisterScript(DynamicScriptDefinition definition)
        {
            var key = definition.Script.FunctionName;
            var runner = RunnerFactory.CreateRunner(definition, _context, _metadataReferences);
            return _runners.AddOrUpdate(key, runner, (s, runner1) => runner1);
        }

        public IRunner GetRunner(string functionName)
        {
            if (!_runners.ContainsKey(functionName)) return null;
            _runners.TryGetValue(functionName, out var runner);
            return runner;
        }

        public void Dispose()
        {
            _context.Unload();
        }

        // public IContainer LoadNameSpaces(string[] nameSpaces)
        // {
        //     if (nameSpaces != null && nameSpaces.Length > 0)
        //     {
        //         foreach (var nameSpace in nameSpaces)
        //         {
        //             _usingNameSpaces.Add(nameSpace);
        //         }
        //     }
        //
        //     return this;
        // }

        // public IContainer LoadAssembly(Assembly[] assemblies)
        // {
        //     if (assemblies != null && assemblies.Length > 0)
        //     {
        //         foreach (var assembly in assemblies)
        //         {
        //             _usingAssemblies.Add(assembly);
        //         }
        //     }
        //
        //     return this;
        // }

        // public IContainer LoadAssembly(string[] assemblyNames)
        // {
        //     if (assemblyNames != null && assemblyNames.Length > 0)
        //     {
        //         foreach (var assemblyName in assemblyNames)
        //         {
        //             var assemblies = RuntimeHelper.GetAssembliesByName(assemblyName);
        //             _usingAssemblies.AddRange(assemblies.Where(x => x != null));
        //         }
        //     }
        //
        //     return this;
        // }
    }
}
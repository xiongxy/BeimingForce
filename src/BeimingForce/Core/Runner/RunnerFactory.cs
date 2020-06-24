using System;
using System.Collections.Generic;
using BeimingForce.Container;
using BeimingForce.Engine.ScriptEngine;
using BeimingForce.Model;
using Microsoft.CodeAnalysis;

namespace BeimingForce.Core.Runner
{
    public static class RunnerFactory
    {
        public static IRunner CreateRunner(DynamicScriptDefinition definition, BeimingAssemblyLoadContext context,
            List<MetadataReference> metadataReferences)
        {
            var dynamicScript = ScriptEngineFactory.CreateDynamicScript(definition);
            IRunner runner = new DefaultRunner(dynamicScript, context, metadataReferences);
            return runner;
        }
    }
}


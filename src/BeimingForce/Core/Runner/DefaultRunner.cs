using System.Collections.Generic;
using System.Diagnostics;
using BeimingForce.Engine;
using BeimingForce.Engine.ScriptEngine;
using BeimingForce.Model;
using Microsoft.CodeAnalysis;

namespace BeimingForce.Core.Runner
{
    public class DefaultRunner : IRunner
    {
        private readonly IDynamicScript _dynamicScript;
        private readonly BeimingAssemblyLoadContext _context;
        private readonly List<MetadataReference> _metadataReferences;
        private bool _isInit;

        public DefaultRunner(
            IDynamicScript dynamicScript,
            BeimingAssemblyLoadContext context,
            List<MetadataReference> metadataReferences)
        {
            _dynamicScript = dynamicScript;
            _context = context;
            _metadataReferences = metadataReferences;
        }

        public IRunner Pretreatment()
        {
            _dynamicScript.SetAssemblyLoadContext(this._context);
            _dynamicScript.SetMetadataReferences(this._metadataReferences);
            _dynamicScript.BuildDynamicScript();
            _isInit = true;
            return this;
        }

        public List<string> GetCompileErrorMessage()
        {
            return _dynamicScript.CompileErrorMessage;
        }

        public DynamicScriptResult<T> RunDynamicScript<T>(params object[] args)
        {
            var watch = Stopwatch.StartNew();

            if (!_isInit)
            {
                Pretreatment();
            }

            var errorMessage = new List<string>();

            if (!_dynamicScript.Compiled)
            {
                errorMessage.AddRange(_dynamicScript.CompileErrorMessage);
                return new DynamicScriptResult<T>()
                {
                    ErrorMessage = errorMessage,
                    Result = default,
                    TimeConsumedMS = watch.ElapsedMilliseconds
                };
            }

            var scriptResult = _dynamicScript.CallFunction<T>(args);
            return new DynamicScriptResult<T>()
            {
                ErrorMessage = errorMessage,
                Result = scriptResult,
                TimeConsumedMS = watch.ElapsedMilliseconds
            };
        }
    }
}
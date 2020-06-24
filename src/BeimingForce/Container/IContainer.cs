using System;
using System.Collections;
using System.Collections.Generic;
using BeimingForce.Core;
using BeimingForce.Core.Runner;
using BeimingForce.Engine;
using BeimingForce.Model;

namespace BeimingForce.Container
{
    public interface IContainer : IDisposable
    {
        public string ContainerName { get; set; }

        public IRunner RegisterScript(DynamicScriptDefinition definition);

        public IRunner GetRunner(string functionName);
    }
}
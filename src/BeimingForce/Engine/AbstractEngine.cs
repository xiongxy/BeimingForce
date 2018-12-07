using System;
using System.Collections.Generic;
using System.Reflection;
using BeimingForce.Model;

namespace BeimingForce.Engine
{
    public abstract class AbstractEngine
    {
        public List<Assembly> Assemblies = new List<Assembly>();
     
        public virtual dynamic Run(string code)
        {
            throw new System.NotImplementedException();
        }
    }
}

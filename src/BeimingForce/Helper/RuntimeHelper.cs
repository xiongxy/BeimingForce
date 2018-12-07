using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace BeimingForce.Helper
{
    public class RuntimeHelper
    {
        //public static IList<Assembly> GetAllAssemblies()
        //{
        //    List<Assembly> list = new List<Assembly>();
        //    var deps = DependencyContext.Default;
        //    //排除所有的系统程序集、Nuget下载包
        //    var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");
        //    foreach (var lib in libs)
        //    {
        //        try
        //        {
        //            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
        //            list.Add(assembly);
        //        }
        //        catch (Exception ex)
        //        {
        //            //
        //        }
        //    }
        //    return list;
        //}
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace BeimingForce.Engine
{
    public class CSharpScriptEngine : AbstractEngine
    {
        private MetadataReference _mscorlib;
        private MetadataReference Mscorlib
        {
            get
            {
                if (_mscorlib == null)
                {
                    _mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
                }
                return _mscorlib;
            }
        }
        public override dynamic Run(string code)
        {

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(@"
    using System;
    namespace RoslynCompileSample
    {
        public class Writer
        {
            public void Write(string message)
            {
               System.Console.WriteLine(message);
            }
        }
    }");
            string assemblyName = Path.GetRandomFileName();



            var vv = typeof(object).Assembly.Location;


            var vvv = new System.IO.DirectoryInfo(vv);


            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Console").Location),
                MetadataReference.CreateFromFile(System.Reflection.Assembly.Load("System.Runtime").Location)

            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                ms.Seek(0, SeekOrigin.Begin);
                Assembly assembly = Assembly.Load(ms.ToArray());
            }

            return 1;





            //var syntaxTree = CSharpSyntaxTree.ParseText(code);


            //var metadataReferences = new List<MetadataReference>();
            //metadataReferences.Add(Mscorlib);
            //Assemblies.Add(System.Reflection.Assembly.Load("System"));

            //  System.Console

            //Assemblies.ForEach(x =>
            //{
            //    PortableExecutableReference portableExecutableReference = MetadataReference.CreateFromFile(x.Location);
            //    metadataReferences.Add(portableExecutableReference);
            //});

            //var compilation = CSharpCompilation.Create("calc", new[] { syntaxTree },
            //    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithOptimizationLevel(OptimizationLevel.Debug),
            //    references: metadataReferences );

            //Assembly compiledAssembly;

            //using (var stream = new MemoryStream())
            //{
            //    var compileResult = compilation.Emit(stream);
            //    compiledAssembly = Assembly.Load(stream.GetBuffer());
            //}

            //var calculatorClass = compiledAssembly.GetType("Calculator");
            //var evaluateMethod = calculatorClass.GetMethod("Evaluate");
            //var result = evaluateMethod.Invoke(null, null).ToString();

            //return result;
        }
    }
}
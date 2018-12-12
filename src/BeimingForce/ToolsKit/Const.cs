﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BeimingForce.ToolsKit
{
    public class Const
    {
        public const string Using = "using";
        public const string Class = "class";
        public const string Methods = "methods";
        public const string AssemblyScriptKey = "GeneratedAssembly_{0}_{1}_{2}";
        public const string MethodTypeName = "BeimingForce.Currency";

        public const string RefMscorlib = "mscorlib.dll";
        public const string RefSystem = "System.dll";
        public const string RefSystemCore = "System.Core.dll";
        public const string RefMicrosoftCSharp = "Microsoft.CSharp.dll";
        public const string RefCollections = "System.Collections.dll";
        public const string RefSystemRuntime = "System.Runtime.dll";
        public const string RefSystemConsole = "System.Console.dll";
        public const string RefNetstandard = "netstandard.dll";


        public static readonly List<string> RefSystemAll = new List<string>()
        {
"Microsoft.CSharp.dll",
"Microsoft.VisualBasic.dll",
"Microsoft.Win32.Primitives.dll",
"Microsoft.Win32.Registry.dll",
"netstandard.dll",
"System.AppContext.dll",
"System.Buffers.dll",
"System.Collections.Concurrent.dll",
"System.Collections.dll",
"System.Collections.Immutable.dll",
"System.Collections.NonGeneric.dll",
"System.Collections.Specialized.dll",
"System.ComponentModel.Annotations.dll",
"System.ComponentModel.DataAnnotations.dll",
"System.ComponentModel.dll",
"System.ComponentModel.EventBasedAsync.dll",
"System.ComponentModel.Primitives.dll",
"System.ComponentModel.TypeConverter.dll",
"System.Configuration.dll",
"System.Console.dll",
"System.Core.dll",
"System.Data.Common.dll",
"System.Data.dll",
"System.Diagnostics.Contracts.dll",
"System.Diagnostics.Debug.dll",
"System.Diagnostics.DiagnosticSource.dll",
"System.Diagnostics.FileVersionInfo.dll",
"System.Diagnostics.Process.dll",
"System.Diagnostics.StackTrace.dll",
"System.Diagnostics.TextWriterTraceListener.dll",
"System.Diagnostics.Tools.dll",
"System.Diagnostics.TraceSource.dll",
"System.Diagnostics.Tracing.dll",
"System.dll",
"System.Drawing.dll",
"System.Drawing.Primitives.dll",
"System.Dynamic.Runtime.dll",
"System.Globalization.Calendars.dll",
"System.Globalization.dll",
"System.Globalization.Extensions.dll",
"System.IO.Compression.Brotli.dll",
"System.IO.Compression.dll",
"System.IO.Compression.FileSystem.dll",
"System.IO.Compression.ZipFile.dll",
"System.IO.dll",
"System.IO.FileSystem.AccessControl.dll",
"System.IO.FileSystem.dll",
"System.IO.FileSystem.DriveInfo.dll",
"System.IO.FileSystem.Primitives.dll",
"System.IO.FileSystem.Watcher.dll",
"System.IO.IsolatedStorage.dll",
"System.IO.MemoryMappedFiles.dll",
"System.IO.Pipes.AccessControl.dll",
"System.IO.Pipes.dll",
"System.IO.UnmanagedMemoryStream.dll",
"System.Linq.dll",
"System.Linq.Expressions.dll",
"System.Linq.Parallel.dll",
"System.Linq.Queryable.dll",
"System.Memory.dll",
"System.Net.dll",
"System.Net.Http.dll",
"System.Net.HttpListener.dll",
"System.Net.Mail.dll",
"System.Net.NameResolution.dll",
"System.Net.NetworkInformation.dll",
"System.Net.Ping.dll",
"System.Net.Primitives.dll",
"System.Net.Requests.dll",
"System.Net.Security.dll",
"System.Net.ServicePoint.dll",
"System.Net.Sockets.dll",
"System.Net.WebClient.dll",
"System.Net.WebHeaderCollection.dll",
"System.Net.WebProxy.dll",
"System.Net.WebSockets.Client.dll",
"System.Net.WebSockets.dll",
"System.Numerics.dll",
"System.Numerics.Vectors.dll",
"System.ObjectModel.dll",
"System.Private.CoreLib.dll",
"System.Private.DataContractSerialization.dll",
"System.Private.Uri.dll",
"System.Private.Xml.dll",
"System.Private.Xml.Linq.dll",
"System.Reflection.DispatchProxy.dll",
"System.Reflection.dll",
"System.Reflection.Emit.dll",
"System.Reflection.Emit.ILGeneration.dll",
"System.Reflection.Emit.Lightweight.dll",
"System.Reflection.Extensions.dll",
"System.Reflection.Metadata.dll",
"System.Reflection.Primitives.dll",
"System.Reflection.TypeExtensions.dll",
"System.Resources.Reader.dll",
"System.Resources.ResourceManager.dll",
"System.Resources.Writer.dll",
"System.Runtime.CompilerServices.VisualC.dll",
"System.Runtime.dll",
"System.Runtime.Extensions.dll",
"System.Runtime.Handles.dll",
"System.Runtime.InteropServices.dll",
"System.Runtime.InteropServices.RuntimeInformation.dll",
"System.Runtime.InteropServices.WindowsRuntime.dll",
"System.Runtime.Loader.dll",
"System.Runtime.Numerics.dll",
"System.Runtime.Serialization.dll",
"System.Runtime.Serialization.Formatters.dll",
"System.Runtime.Serialization.Json.dll",
"System.Runtime.Serialization.Primitives.dll",
"System.Runtime.Serialization.Xml.dll",
"System.Security.AccessControl.dll",
"System.Security.Claims.dll",
"System.Security.Cryptography.Algorithms.dll",
"System.Security.Cryptography.Cng.dll",
"System.Security.Cryptography.Csp.dll",
"System.Security.Cryptography.Encoding.dll",
"System.Security.Cryptography.OpenSsl.dll",
"System.Security.Cryptography.Primitives.dll",
"System.Security.Cryptography.X509Certificates.dll",
"System.Security.dll",
"System.Security.Principal.dll",
"System.Security.Principal.Windows.dll",
"System.Security.SecureString.dll",
"System.ServiceModel.Web.dll",
"System.ServiceProcess.dll",
"System.Text.Encoding.dll",
"System.Text.Encoding.Extensions.dll",
"System.Text.RegularExpressions.dll",
"System.Threading.dll",
"System.Threading.Overlapped.dll",
"System.Threading.Tasks.Dataflow.dll",
"System.Threading.Tasks.dll",
"System.Threading.Tasks.Extensions.dll",
"System.Threading.Tasks.Parallel.dll",
"System.Threading.Thread.dll",
"System.Threading.ThreadPool.dll",
"System.Threading.Timer.dll",
"System.Transactions.dll",
"System.Transactions.Local.dll",
"System.ValueTuple.dll",
"System.Web.dll",
"System.Web.HttpUtility.dll",
"System.Windows.dll",
"System.Xml.dll",
"System.Xml.Linq.dll",
"System.Xml.ReaderWriter.dll",
"System.Xml.Serialization.dll",
"System.Xml.XDocument.dll",
"System.Xml.XmlDocument.dll",
"System.Xml.XmlSerializer.dll",
"System.Xml.XPath.dll",
"System.Xml.XPath.XDocument.dll",
        };

        public const string CSharpTemplateNameSpacesPlaceholder = "[$NameSpaces]";
        public const string CSharpTemplateMainFunctionPlaceholder = "[$MainFunction]";
        public const string CSharpTemplate = @"
using System;
[$NameSpaces]
namespace BeimingForce
{
    public class Currency
    {
        public object Main(){
            [$MainFunction]
        }
    }
}
";
    }
}

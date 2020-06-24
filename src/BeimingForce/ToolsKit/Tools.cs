using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BeimingForce.ToolsKit
{
    public static class Tools
    {
        private static string emptyLinePattern = @"^\s*$\n|\r";
        private static string commentPattern = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
        private static readonly Regex CommentRgx = new Regex(commentPattern);
        private static readonly Regex EmptyLineRgx = new Regex(emptyLinePattern, RegexOptions.Multiline);

        public static string ClearScript(this string script)
        {
            return RemoveEmptyLines(StripComments(script).Trim()).Trim();
        }

        public static string RemoveEmptyLines(this string script)
        {
            return EmptyLineRgx.Replace(script, "");
        }
        
        public static string StripComments(this string script)
        {
            return CommentRgx.Replace(script, "$1");
        }

        public static string Md5(this string content)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            return BitConverter.ToString(bytes).Replace("-", "");
        }
       
        public static string ToByteSizeString(this long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };

            var order = 0;
            while (bytes >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                bytes = bytes / 1024;
            }
            return String.Format("{0:0.##} {1}", bytes, sizes[order]);
        }
    }
}

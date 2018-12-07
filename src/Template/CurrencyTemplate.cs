namespace Template
{
    public class CurrencyTemplate
    {
        public static string Template = @"
using System;

namespace Template
{
    public class $Namespace
    {
        public void Main(){
            $MainFunction
        }
    }
}
";
    }
}

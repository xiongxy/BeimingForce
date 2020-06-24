using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BeimingForce.Model
{
    public class DynamicScriptResult<T>
    {
        public long TimeConsumedMS { get; set; }
        public List<string> ErrorMessage { get; set; }
        public T Result { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"当前执行用时：${TimeConsumedMS}");
            stringBuilder.AppendLine($"当前错误信息：");
            if (ErrorMessage != null && ErrorMessage.Any())
            {
                ErrorMessage.ForEach(x => { stringBuilder.AppendLine(x); });
            }
            stringBuilder.AppendLine(JsonConvert.SerializeObject(Result));
            return stringBuilder.ToString();
        }
    }
}
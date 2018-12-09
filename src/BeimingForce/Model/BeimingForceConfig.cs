using System;
using System.Collections.Generic;
using System.Text;
using BeimingForce.ToolsKit;

namespace BeimingForce.Model
{
    public class BeimingForceConfig
    {
        public string AppLibPath => ConfigHelper.GetSectionValue("AppLibPath");
    }
}

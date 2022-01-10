using System;
using System.IO;
using LowerInputOutputSystem.ScriptAddons;
using Microsoft.ClearScript.V8;

namespace LowerInputOutputSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string filePath = FileOperation.BaseDirectory + "\\main.js";
            if (! new FileInfo(filePath).Exists)
            {
                FileOperation.WriteFile(filePath, "");
            }

            ScriptContainer container = new ScriptContainer("catilgrass", 1.0f);
            string content = FileOperation.ReadFile(filePath);
            container.Execute(content);
        }
    }
}

/*
 
// 初始化阶段
function initState ()
{
    self.addType('System.Console');
    
    *self.IS_SERVICE = true;

    *self.addProxy("commandName", "proxyMethod1")
}

function proxyMethod1 (param1, param2, ...)
{
    // TODO: Execute when this method has proxied
}

*function service ()
{
    // TODO: Execute when IS_SERVICE is enabled
}

*/
function init ()
{
    // 引入控制台
    self.AddType(engine, "System.Console");
    self.AddType(engine, "LowerInputOutputSystem.ScriptAddons.FileOperation");
}
init();

Console.WriteLine("Hello World !");
Console.ReadKey();

FileOperation.WriteFile(
    FileOperation.BaseDirectory + "\\" + "catilgrass.js", ""
);
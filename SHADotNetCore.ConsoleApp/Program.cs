using SHADotNetCore.ConsoleApp;

AdoDotNet adoDotNet = new AdoDotNet();

//adoDotNet.Read();
//adoDotNet.Create();
//adoDotNet.Edit();
//adoDotNet.Update();
//adoDotNet.Delete();

DapperExample dapper = new DapperExample();

//dapper.Read();
//dapper.Create("MyTitle2", "Rahulladk", "this is my content2");
//dapper.Update(11, "Hello", "Rahull", "Yes");
dapper.Delete(11);
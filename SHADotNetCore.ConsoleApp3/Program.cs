using SHADotNetCore.ConsoleApp3;

HttpClientExample httpClientExample = new HttpClientExample();

//await httpClientExample.Read();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(101);
//await httpClientExample.Create(1, "test title", "test body");
//await httpClientExample.Update(1, 1, "title", "body");
await httpClientExample.Delete(1);
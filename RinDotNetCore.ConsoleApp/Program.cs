

// See https://aka.ms/new-console-template for more information
using MTZMDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotNetExample a = new AdoDotNetExample();
//a.Create("title", "author", "content");
//a.Update(1, "test title", "test author", "test content");
//a.Delete(1);
a.Edit(2);


Console.ReadKey();



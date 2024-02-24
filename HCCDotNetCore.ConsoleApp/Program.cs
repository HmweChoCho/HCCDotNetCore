// See https://aka.ms/new-console-template for more information
using HCCDotNetCore.ConsoleApp.AdoDotNetExamples;
using HCCDotNetCore.ConsoleApp.DapperExamples;
using HCCDotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
//sqlConnectionStringBuilder.DataSource = ".";
//sqlConnectionStringBuilder.InitialCatalog = "TestDb";
//sqlConnectionStringBuilder.UserID = "sa";
//sqlConnectionStringBuilder.Password = "sa@123";

//SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
//connection.Open();

//string query=@"SELECT * FROM Tbl_Blog";
//DataTable dt = new DataTable();

//SqlCommand command=new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(command);
//adapter.Fill(dt);
//connection.Close();

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Title...." + dr["BlogTitle"]);
//    Console.WriteLine("Author....." +dr["BlogAuthor"]);
//    Console.WriteLine("Content...." + dr["BlogContent"]);
//}

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Read();
adoDotNetExample.Edit(1);
adoDotNetExample.Create("title1", "author1", "test content");
adoDotNetExample.Update(2, "title2", "author2", "test content2");
adoDotNetExample.Delete(2);

DapperExample dapperExample = new DapperExample();
dapperExample.Read();
dapperExample.Edit(2);
dapperExample.Create("title2", "author2", "content test 2");
dapperExample.Update(3, "title3", "author3", "content test 3");
dapperExample.Delete(1);


EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Read();
eFCoreExample.Edit(4);
eFCoreExample.Create("title4", "author4", "content test 4");
eFCoreExample.Update(4, "title test 4", "author test 4", "content test 4");
eFCoreExample.Delete(3);
Console.ReadKey();

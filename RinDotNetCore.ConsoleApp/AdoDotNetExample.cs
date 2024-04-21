using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace MTZMDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-2HI448L\\MSSQLSERVER33",
            InitialCatalog = "MTZMDotNetCore",
            UserID = "sasa",
            Password = "12345",
        };
        public void Read()
        {
          
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            string query = "select * from Tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Close");

            //dataset
            //datatable
            //datarow

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog Id ==> " + dr["BlogId"]);
                Console.WriteLine("Blog Title ==> " + dr["BlogTitle"]);
                Console.WriteLine("Blog Author ==> " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content ==> " + dr["BlogContent"]);

            }
        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
           @BlogAuthor,
           @BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle" , title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Saving successful." : "Saving Failed.";
            Console.WriteLine(message);
        }
        public void Update(int id , string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                               WHERE [BlogId]=@BlogId";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@BlogId", id);

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            Console.WriteLine(message);
        }
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("connection open.");

            string query = "select* from tbl_Blog where BlogId=@BlogId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            Console.WriteLine("connection close.");

            //dataSet=> datatable
            //datatable=>dataRow
            //dataRow=> datacolumn
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine("BlogId =>" + dr["BlogId"]);
            Console.WriteLine("BlogAuthor =>" + dr["BlogAuthor"]);
            Console.WriteLine("BlogTitle =>" + dr["BlogTitle"]);
            Console.WriteLine("BlogContent =>" + dr["BlogContent"]);
            Console.WriteLine("___________");

        }
    }
}

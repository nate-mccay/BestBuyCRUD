using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerMessage("Begin Running");
            var departments = GetAllDepartments();
            foreach(var department in departments)
            {
                Console.WriteLine(department);
            }
            try
            {
                Console.WriteLine(departments[8]);
            }
            catch(Exception e)
            {
                LoggerError(e);
            }
        }
        static void LoggerMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Environment.NewLine}-------------------{Environment.NewLine}");
            sb.Append($"{message} {DateTime.Now}");
            sb.Append($"{Environment.NewLine}-------------------{Environment.NewLine}");
            var filePath = "";
            File.AppendAllText(filePath + "Log.txt", sb.ToString());
        }
        static void LoggerError(Exception error)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Environment.NewLine}-------------------{Environment.NewLine}");
            sb.Append($"{error.Message} {DateTime.Now}");
            sb.Append($"{Environment.NewLine}-------------------{Environment.NewLine}");
            var filePath = "";
            File.AppendAllText(filePath + "Log.txt", sb.ToString());
        }
        static List<string> GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                LoggerMessage("Accessing Connection File");
                conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");
            }
            catch(Exception e)
            {
                LoggerError(e);
            }
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Departments;";
            using (conn)                                                      
            {                                                                 
                conn.Open();                                                  
                List<string> allDepartments = new List<string>();             
                MySqlDataReader reader = cmd.ExecuteReader();                 
                while(reader.Read() == true)
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }
                return allDepartments;
            }
        }
        static void InsertNewDepartment(string newDepartmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.Txt");
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO departments (Name) VALUES (@deptname);";
            cmd.Parameters.AddWithValue("deptname", newDepartmentName);
            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }
        static void DeleteDepartment(string departmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.Txt");
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM departments WHERE Name = @departmentName;";
            cmd.Parameters.AddWithValue("departmentname", departmentName);
            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static void UpdateDepartment(string dept, string newName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.Txt");
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE departments SET Name = @newName WHERE Name = @dept;";
            cmd.Parameters.AddWithValue("dept", dept);
            cmd.Parameters.AddWithValue("newName", newName);
            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

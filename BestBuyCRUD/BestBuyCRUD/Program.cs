using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var departments = GetAllDepartments();
            foreach(var department in departments)
            {
                Console.WriteLine(department);
            }
        }
        static List<string> GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection();
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
    }
}

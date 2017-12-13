using System;
using System.IO;
using System.Data.SQLite;

namespace MYGOD539.Helper
{
    public class ConnectionHelper{
        public static string SQLiteConnectionString(){
            string dbStr = string.Concat("Data Source=",CheckDirectory(),"\\539db.db;Version=3;");

            return dbStr;
        }

        private static string CheckDirectory(){
            string path = Path.Combine(System.Environment.CurrentDirectory,"DB");
            if(Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            return path;
        }

    }

}
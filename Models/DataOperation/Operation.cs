using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace MYGOD539.Models.DataOperation
{
    public class Operation : IOperation
    {
        private string ConnectionString {get;set;}

        public Operation(){
            
            this.ConnectionString = 
                Helper.ConnectionHelper.SQLiteConnectionString();
        }


        public int Insert(string sql , SQLiteParameter[] collection)
        {
            int affectedRows = 0;
            using(SQLiteConnection conn = new SQLiteConnection(this.ConnectionString)){
                using(SQLiteCommand cmd = new SQLiteCommand(sql,conn)){

                    cmd.Parameters.AddRange(collection);

                    try{
                        conn.Open();
                        affectedRows = cmd.ExecuteNonQuery();

                        return affectedRows;
                    }
                    catch(Exception err){
                        throw err;
                    }
                }
            }
        }

        public DataTable Select(string sql, SQLiteParameter[] collection)
        {
            DataTable table = new DataTable();
            using(SQLiteConnection conn = new SQLiteConnection(this.ConnectionString)){
                using(SQLiteCommand cmd = new SQLiteCommand(sql,conn)){
                    
                    cmd.Parameters.AddRange(collection);
                    SQLiteDataReader reader;
                    
                    try{
                        conn.Open();
                        reader = cmd.ExecuteReader();
                        table.Load(reader);

                        return table;
                    }
                    catch(Exception err){
                        throw err;
                    }
                }
            }

        }

        public string SelectMax(string sql, SQLiteParameter[] collection) {
            string result = string.Empty;

            using(SQLiteConnection conn = new SQLiteConnection(this.ConnectionString)){
                using(SQLiteCommand cmd = new SQLiteCommand(sql,conn)){
                    
                    cmd.Parameters.AddRange(collection);
                    SQLiteDataReader reader;
                    
                    try{
                        conn.Open();
                        reader = cmd.ExecuteReader();

                        while(reader.Read()){
                            result = reader[0].ToString();
                        }

                        return result;
                    }
                    catch(Exception err){
                        throw err;
                    }
                }
            }
        }

        public void ClearTable(string tableName)
        {
            using(SQLiteConnection conn = new SQLiteConnection(this.ConnectionString)){
                using(SQLiteCommand cmd = new SQLiteCommand(string.Concat("DELETE FROM ",tableName,";"),conn)){
                    try{
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception err){
                        throw err;
                    }
                }

            }
        }
    }


}
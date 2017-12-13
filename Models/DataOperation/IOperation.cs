using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace MYGOD539.Models.DataOperation
{
    public interface IOperation
    {
        int Insert(string sql , SQLiteParameter[] collection);

        DataTable Select(string sql, SQLiteParameter[] collection);

        string SelectMax(string sql, SQLiteParameter[] collection);


        void ClearTable(string tableName);
    }
}
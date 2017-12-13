using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using MYGOD539.Helper;

namespace MYGOD539.Models.DataOperation
{
    public class SQLOperation
    {
        private IOperation operation;
        private SQLiteParameter[] parameters;

        public SQLOperation(){
            operation = new Operation();
        }

        public int InsertHistory(Number num){
            string insertSQL = "INSERT INTO History (open_no,open_date,open_num_1,open_num_2,open_num_3,open_num_4,open_num_5)";
            insertSQL = string.Concat(insertSQL,
                " VALUES(@OpenNum , @OpenDate , @OpenNum1 , @OpenNum2 , @OpenNum3 , @OpenNum4 , @OpenNum5);");
            
            parameters = new SQLiteParameter[7];
            var p = new SQLiteParameter();
            p.ParameterName = "@OpenNum";
            p.DbType = System.Data.DbType.String;
            p.Value = num.Periods;
            parameters[0] = p;

            p = new SQLiteParameter();
            p.ParameterName = "@OpenDate";
            p.DbType = System.Data.DbType.Date;
            p.Value = num.NumberDate;
            parameters[1] = p;

            p = new SQLiteParameter();
            p.ParameterName = "@OpenNum1";
            p.DbType = System.Data.DbType.Int32;
            p.Value = num.num1;
            parameters[2] = p;

            p = new SQLiteParameter();
            p.ParameterName = "@OpenNum2";
            p.DbType = System.Data.DbType.Int32;
            p.Value = num.num2;
            parameters[3] = p;

            p = new SQLiteParameter();
            p.ParameterName = "@OpenNum3";
            p.DbType = System.Data.DbType.Int32;
            p.Value = num.num3;
            parameters[4] = p;

            p = new SQLiteParameter();
            p.ParameterName = "@OpenNum4";
            p.DbType = System.Data.DbType.Int32;
            p.Value = num.num4;
            parameters[5] = p;

            p = new SQLiteParameter();
            p.ParameterName = "@OpenNum5";
            p.DbType = System.Data.DbType.Int32;
            p.Value = num.num5;
            parameters[6] = p;

            return operation.Insert(insertSQL,parameters);
        }

        public int InsertProbability(DataTable table,int year){
            int result = 0;
            // var table = this.SelectPopularNum(year);

            var parameters = new SQLiteParameter[3];
            SQLiteParameter p;

            foreach (DataRow row in table.Rows)
            {
                p = new SQLiteParameter();
                p.ParameterName = "@Num";
                p.DbType = System.Data.DbType.Int32;
                p.Value = int.Parse(row["NUM"].ToString());
                parameters[0] = p;

                p = new SQLiteParameter();
                p.ParameterName = "@Frq";
                p.DbType = System.Data.DbType.Int32;
                p.Value = int.Parse(row["COUNT1"].ToString());
                parameters[1] = p;

                p = new SQLiteParameter();
                p.ParameterName = "@Per";
                p.DbType = System.Data.DbType.Double;
                p.Value = double.Parse(row["Percent"].ToString());
                parameters[2] = p;
                
                result = operation.Insert(ScriptHelper.GetScriptFromFile("InsertNumProbability"),parameters);
            }

            return result;
        }

        public DataTable SelectPopularNum(int year) {
            const int numbers = 39;
            int numTimes = 0;
            double data = 0.0;
            DataTable table = new DataTable();
            parameters = new SQLiteParameter[1];

            var p = new SQLiteParameter();
            p.ParameterName = "@Year";
            p.DbType = System.Data.DbType.String;
            p.Value = year - 1911;
            parameters[0] = p;

            table = operation.Select(ScriptHelper.GetScriptFromFile("GetPopularNumber"),parameters);
            int maxPeriods = this.GetMaxPeriods(year);
            DataColumn column = table.Columns.Add("Percent", typeof(Double));
            column.Unique = false;
            
            for (int idx = 0; idx < numbers; idx++)
            {
                numTimes = int.Parse(table.Rows[idx]["COUNT1"].ToString());
                data = ((double)numTimes / (double)maxPeriods) * 100;
                table.Rows[idx]["Percent"] = Math.Round(data, 2);
            }

            return table;
        }


        public int GetMaxPeriods(int year){
            string result;
            DataTable table = new DataTable();
            parameters = new SQLiteParameter[1];

            var p = new SQLiteParameter();
            p.ParameterName = "@Year";
            p.DbType = System.Data.DbType.String;
            p.Value = year - 1911;
            parameters[0] = p;

            result = operation.SelectMax(
                "SELECT MAX(open_no) from History WHERE substr(open_no,1,3) = @Year;",parameters);

            return int.Parse(result.Substring(result.Length - 3,3));
        }

    }

}
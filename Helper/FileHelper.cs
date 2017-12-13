using System;
using System.IO;
using System.Data;

namespace MYGOD539.Helper
{
    public class FileHelper{
        public static void ExportCSV(DataTable table,  string file){
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            // builder.Append();

            if(table.Rows.Count > 0){
                foreach (DataColumn col in table.Columns)
                {
                    builder.Append(string.Format("{0},",col.ColumnName));
                }

                builder.Append(System.Environment.NewLine);

                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        builder.Append(string.Format("{0},",item));
                    }
                    
                    builder.Append(System.Environment.NewLine);
                }
                
                try{
                    if(File.Exists(file)) {
                        File.Delete(file);
                    }
                    File.WriteAllText(file , builder.ToString());
                }catch(Exception err){
                    throw err;
                }
            }


        }

    }
}
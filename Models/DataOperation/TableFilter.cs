using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MYGOD539.Helper;

namespace MYGOD539.Models.DataOperation
{
    public class TableFilter
    {
        public TableFilter()
        {
        }

        public List<int> Filter(DataTable table , string searchStr){
            StringBuilder builder = new StringBuilder();
            List<int> popNumList = new List<int>();
            //Datatable Number to List
            var result = table.Select(searchStr);
            foreach (var item in result)
            {
                popNumList.Add(Convert.ToInt32(item[0]));
            }
            
            return popNumList.OrderBy(x => x).ToList();
        }
    }



}
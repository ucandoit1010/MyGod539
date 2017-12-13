using System;
using System.Data;
using MYGOD539.Models.DataOperation;

namespace MYGOD539.Models.NumberCalculation
{
    public class PopularNumber : Calculator{

        SQLOperation operation;

        public PopularNumber() {
            operation = new SQLOperation();
        }

        public override DataTable CalcMethod(int year)
        {
            var table = operation.SelectPopularNum(year);
            operation.InsertProbability(table,year);

            return table;
        }
    }



}
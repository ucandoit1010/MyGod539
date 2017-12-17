using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MYGOD539.Models.DataOperation;

namespace MYGOD539.Models.NumberCalculation
{
    public class OddOrEvenNumber : Calculator{

        SQLOperation operation;

        public OddOrEvenNumber()
        {
            operation = new SQLOperation();
        }

        public override DataTable CalcMethod(int year)
        {
            return operation.SelectOddEvenNum(year);
        }


    }

}
using System;

namespace MYGOD539.Models
{
    public class Number {
        
        public string GetOddOrEven(){
            string result = string.Empty;
            int [] box = new int [5];
            int count = 0;

            if(this.num1 > 0 && this.num2 > 0 && this.num3 > 0 && 
                this.num4 > 0 && this.num5 > 0) {
                
                box[0] = this.num1;
                box[1] = this.num2;
                box[2] = this.num3;
                box[3] = this.num4;
                box[4] = this.num5;

                foreach (int data in box)
                {
                    if(data % 2 == 0){
                        count++;
                    }
                }

                result = count >= 3 ? "EVEN" : "ODD";

            }else{
                throw new ArgumentOutOfRangeException();
            }

            return result;
        }


        public int num1 { get; set; }
        public int num2 { get; set; }
        public int num3 { get; set; }
        public int num4 { get; set; }
        public int num5 { get; set; }


        public DateTime NumberDate { get; set; }
        public string Periods { get; set; }
    }

}
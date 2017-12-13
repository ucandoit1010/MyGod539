using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MYGOD539.Models.DataOperation;
using MYGOD539.Models.NumberCalculation;
using MYGOD539.Helper;

namespace MYGOD539
{
    class Program
    {
        static void Main(string[] args)
        {
            PopularNumber popular = new PopularNumber();
            var result = popular.CalcMethod(2017);
            // FileHelper.ExportCSV(result,"D:\Output.csv");

            var popularList = popular.GetFilterNumber(result,"Percent >= 12.5");
            var unPopularList = popular.GetFilterNumber(result,"Percent < 12.5");

            Console.WriteLine("Popular=" + string.Join(",",popularList));
            Console.WriteLine("UnPopular=" + string.Join(",",unPopularList));

            Console.WriteLine("done");
        }

        static void LoadHTML(){
            #region 物件
            DateTime theDate = new DateTime(2014,7,28);
            int count = 1;
            // string dir = System.Environment.CurrentDirectory;
            // string url = "http://www.nfd.com.tw/lottery/39-year/39-f2017.htm";
            // string url = "http://www.nfd.com.tw/lottery/39-year/39-f2016.htm";
            // string url = "http://www.nfd.com.tw/lottery/39-year/39-f2015.htm";
            // string url = "http://www.nfd.com.tw/lottery/39-year/39-f2014.htm";
            string url = "http://www.nfd.com.tw/lottery/39-year/39-f2013.htm";
            int year = 2013;
            HttpClient client;
            HttpContent content;
            HttpResponseMessage message;
            HtmlDocument document = new HtmlDocument();
            Models.Number numberData;
            SQLOperation operation = new SQLOperation();
            #endregion

            using(client = new HttpClient()){
                using(message = client.GetAsync(url).Result){
                    content = message.Content;
                    if(content != null){
                        document.LoadHtml(content.ReadAsStringAsync().Result);
                        var table = document.DocumentNode.SelectSingleNode("//table");
                        var trList = table.SelectNodes("//tr");//.Skip(0);
                        foreach(var item in trList){
                            if(count <= 1){
                                count++;
                                continue;
                            }
                            numberData = new Models.Number();
                            numberData.NumberDate = DateTime.Parse(
                                String.Concat(year,"/",RemoveTag(item.SelectSingleNode("td[2]").InnerText)));


                            numberData.num1 = int.Parse(RemoveTag(item.SelectSingleNode("td[4]").InnerText));
                            numberData.num2 = int.Parse(RemoveTag(item.SelectSingleNode("td[5]").InnerText));
                            numberData.num3 = int.Parse(RemoveTag(item.SelectSingleNode("td[6]").InnerText));
                            numberData.num4 = int.Parse(RemoveTag(item.SelectSingleNode("td[7]").InnerText));
                            numberData.num5 = int.Parse(RemoveTag(item.SelectSingleNode("td[8]").InnerText));

                            numberData.Periods = PaddingZero(
                                (year - 1911).ToString(),RemoveTag(item.SelectSingleNode("td[3]").InnerText));

                            Console.WriteLine(numberData.NumberDate.ToShortDateString() + " " + numberData.num1 + " " +
                            numberData.num2 + " " + numberData.num3 + " " + numberData.num4 + " " + numberData.num5 + " " +
                            numberData.Periods);
                        
                            Console.WriteLine(operation.InsertHistory(numberData));
                            
                        }
                    }
                }
            }
        }


        static string PaddingZero(string year , string periodsVal){
            int len = periodsVal.Length;
            int paddingLen = 9 - 3 - periodsVal.Length;
            string zero = string.Empty;

            for(int idx = 1 ; idx <= paddingLen ; idx++){
                zero += "0";
            }

            return String.Concat(year,zero,periodsVal);
        }

        static string RemoveTag(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty).
                Replace("\r\n","").Replace(" ","").Replace("\t","");
        }
    }
}

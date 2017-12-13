using System;
using System.IO;

namespace MYGOD539.Helper
{
    public class ScriptHelper{
        public static string GetScriptFromFile(string fileName){
            string path = Path.Combine(System.Environment.CurrentDirectory,"Script",string.Concat(fileName,".sql"));
            
            return File.ReadAllText(path);
        }

    }
}
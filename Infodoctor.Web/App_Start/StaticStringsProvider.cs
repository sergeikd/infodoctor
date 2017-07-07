using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.VisualBasic.FileIO;

namespace Infodoctor.Web
{
    public static class StaticStringsProvider
    {
        private static string PathToStaticStringsFile => ConfigurationManager.AppSettings["PathToStaticStrings"];
        private static string SupportedLanguages => ConfigurationManager.AppSettings["LangCodes"];

        public static void ReadStaticStrings()
        {
            StaticStrings.LanguageNames = SupportedLanguages.Split(',').ToArray();
            StaticStrings.StringValues = ReadFile(StaticStrings.LanguageNames.Length);
        }

        private static List<string[]> ReadFile(int languagesQuantity)
        {
            var staticStringValues = new List<string[]>();
            using (var parser = new TextFieldParser(GetPathtoCsvFile(), Encoding.GetEncoding(1251)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(new string[] {","});
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    var readLanguagesLine = new string[languagesQuantity + 1];
                    for (var i = 0; i <= languagesQuantity; i++)
                    {
                        readLanguagesLine[i] = fields[i];
                    }
                    staticStringValues.Add(readLanguagesLine);
                }
            }
            return staticStringValues;
        }

        public static string GetPathtoCsvFile()
        {
            return HttpContext.Current.Server.MapPath("~") + PathToStaticStringsFile;
        }
    }

    public static class StaticStrings
    {
        public static string[] LanguageNames { get; set; }
        public static List<string[]> StringValues { get; set; }
    }
}
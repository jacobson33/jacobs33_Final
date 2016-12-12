using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using jacobs33_Final.Models;


namespace jacobs33_Final.DAL
{
    public class BudgetDataServiceXML
    {
        public static List<BudgetSheet> ReadObjectFromXml(string file)
        {
            List<BudgetSheet> sheets = new List<BudgetSheet>();

            var serializer = new XmlSerializer(typeof(BudgetSheet));
            serializer.Deserialize(new StreamReader(file));


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using jacobs33_Final.Models;
using System.Reflection;
using System.IO;

namespace jacobs33_Final.DAL
{
    public class DataServiceXML
    {
        public BudgetSheet LoadBudgetSheetFromFile(string filename)
        {
            BudgetSheet sheet = new BudgetSheet();

            XmlSerializer serializer = new XmlSerializer(typeof(BudgetSheet));
            string basedir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            StreamReader sr = new StreamReader(basedir + @"\Data\" + filename);

            sheet = (BudgetSheet)serializer.Deserialize(sr);

            sr.Close();

            return sheet;
        }

        /// <summary>
        /// Serialize budget sheet to file
        /// </summary>
        /// <param name="sheet">Object</param>
        public bool SaveToFile(BudgetSheet sheet)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BudgetSheet));
            string basedir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (TextWriter tw = new StreamWriter($"{basedir}\\Data\\{sheet.Name}.xml"))
            {
                serializer.Serialize(tw, sheet);
            }

            return true;
        }
    }
}

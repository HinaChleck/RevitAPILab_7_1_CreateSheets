using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPILab_7_1_CreateSheets
{
    internal class ViewSheetsUtils
    {
        public static List<ViewSheet> CreateViewSheets(Document doc, ElementId titleBlockTypeId, int quantity)
        {
            var sheets = new List<ViewSheet>();
            for (int i= 0; i<quantity; i++ )
            {
                ViewSheet sheet = ViewSheet.Create(doc, titleBlockTypeId);
                sheets.Add(sheet);
            }
           
            return sheets;
        }
    }
}

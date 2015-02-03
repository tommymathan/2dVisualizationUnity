using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Office.Tools.Excel;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;


namespace DataVisualizationPlugin
{
    public partial class Sheet1
    {
        List<string> exportList = new List<string>();

        private void Sheet1_Startup(object sender, System.EventArgs e)
        {
            //MessageBox.Show("NULL VALUE");
        }

        private void Sheet1_Shutdown(object sender, System.EventArgs e)
        {

        }

        public void GatherRange(){
            exportList = new List<string>();
            MessageBox.Show("Monitoring Values within selected Range");
            Excel.Range selection = Globals.ThisWorkbook.ThisApplication.Selection as Excel.Range;
            //MessageBox.Show("Number of Selected Columns: " + selection.Cells.Columns.Count);
            //MessageBox.Show("Number of Selected Rows: " + selection.Cells.Rows.Count);
            int numCols = selection.Cells.Columns.Count;
            int numRows = selection.Cells.Rows.Count;
            int counter = 0;
            foreach(object cell in ((object[,])selection.Value)){
                if ((counter % numCols == 0) &&(counter!=0))
                {
                    exportList.Add("%Break%");
                }
                exportList.Add(cell.ToString());
                exportList.Add(",");
                counter++;
            }

            for (int i = 0; i < exportList.Count; i++)
            {
                if (exportList[i].Equals("%Break%"))
                {
                    exportList.RemoveAt(i-1);
                }
                else if (i == exportList.Count - 1)
                {
                    exportList.RemoveAt(i);
                }
            }
           //for(int i = 0; i<exportList.Count; i++)
           //{
           //    //MessageBox.Show("Selected Value is: " + s);
           //}
            
            //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", exportList);
            System.IO.File.Delete(@"C:\Users\Public\Documents\WriteLines.txt");
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\Documents\unity.txt", false);
            
            foreach (string s in exportList)
            {
                if (s.Equals("%Break%"))
                {
                    file.WriteLine();
                }
                else
                {
                    file.Write(s);
                }
            }
            
            file.Close();
        }
        
        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet1_Startup);
            this.Shutdown += new System.EventHandler(Sheet1_Shutdown);
        }

        #endregion

    }
}

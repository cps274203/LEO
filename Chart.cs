using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Load_Effort_Optimisation
{
    public partial class Chart : Form
    {

        List<string> final = new List<string>();
        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
           
            var usedarray = Main.used.ToArray();
            var cachearry = Main.cache.ToArray();
            var buffers = Main.buffers.ToArray();
            var pagetablearry = Main.pagetables.ToArray();
            var slabarray = Main.slab.ToArray();
            var vmallocusedarray = Main.vmallocused.ToArray();
            //List<string> final = new List<string>();
            //List<int> index= new List<int>();
                       
            //int i=0;
            for (int i = 0; i < usedarray.Length; i++)
            {
                try
                {
                    if (usedarray[i].Contains(".") || buffers[i].Contains(".") || cachearry[i].Contains("."))
                    {
                        double value = Convert.ToDouble(usedarray[i]) - Convert.ToDouble(buffers[i]) - Convert.ToDouble(cachearry[i]) - Convert.ToDouble(pagetablearry[i]) - Convert.ToDouble(slabarray[i]) - Convert.ToDouble(vmallocusedarray[i]);
                        final.Add(value.ToString());

                    }
                    else
                    {
                        int value = Convert.ToInt32(usedarray[i]) - Convert.ToInt32(buffers[i]) - Convert.ToInt32(cachearry[i]) - Convert.ToInt32(pagetablearry[i]) - Convert.ToInt32(slabarray[i]) - Convert.ToInt32(vmallocusedarray[i]);
                        final.Add(value.ToString());
                    }
                }
                catch(SystemException se) 
                {
                    MessageBox.Show(se.Message);
                }
                //index.Add(i);
                //MessageBox.Show(value.ToString());
            }
            var finalarry = final.ToArray();
            //double dif = (Convert.ToDouble(finalarry[finalarry.Length - 1]) - Convert.ToDouble(finalarry[0]));
            //MessageBox.Show(dif.ToString());
            string dif1 = Main.dif.ToString() + "KB";
            Series series = this.chart1.Series.Add(dif1.ToString());
            // Data arrays.
	        string[] seriesArray = final.ToArray();
	        //int[] pointsArray = index.ToArray();

	        // Set palette.
	        this.chart1.Palette = ChartColorPalette.SeaGreen;

    	    // Set title.
	        this.chart1.Titles.Add("Overall Memery graph");
            var chartArea = new ChartArea("chart1");


    	    // Add series.
	        for (int i = 0; i < seriesArray.Length; i++)
	        {

                this.chart1.Series["Memory Consumption"].Points.AddXY(i,seriesArray[i]);
		    
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pngpath = Main.currentdir + "\\" + "Overall_memory_leak.jpeg";
            try
            {
                this.chart1.SaveImage(pngpath, ChartImageFormat.Jpeg);
                MessageBox.Show("Image Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveexcel();
        }
        public void saveexcel()
        {
            try
            {

                saveFileDialog1.InitialDirectory = Main.currentdir;
                saveFileDialog1.Title = "Save as Excel file";
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "Excel Files (2003)|*.xls|Excel Files (2007)|*.xlsx";
                if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {

                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);

                    ExcelApp.Columns.ColumnWidth = 6;
                    ExcelApp.Cells[1, 1] = "Used";
                    ExcelApp.Cells[1, 2] = "Cache";
                    ExcelApp.Cells[1, 3] = "Buffers";
                    ExcelApp.Cells[1, 4] = "Page tables";
                    ExcelApp.Cells[1, 5] = "slab";
                    ExcelApp.Cells[1, 6] = "vmallocused";
                    ExcelApp.Cells[1, 7] = "Final";

                    for (int i = 1; i < final.ToArray().Length; i++)
                    {
                        ExcelApp.Cells[i + 1, 1] = Main.used[i - 1];
                        ExcelApp.Cells[i + 1, 2] = Main.cache[i - 1];
                        ExcelApp.Cells[i + 1, 3] = Main.buffers[i - 1];
                        ExcelApp.Cells[i + 1, 4] = Main.pagetables[i - 1];
                        ExcelApp.Cells[i + 1, 5] = Main.slab[i - 1];
                        ExcelApp.Cells[i + 1, 6] = Main.vmallocused[i - 1];
                        ExcelApp.Cells[i + 1, 7] = final[i - 1];


                    }
                    ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                    ExcelApp.ActiveWorkbook.Saved = true;
                    MessageBox.Show("Excel Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExcelApp.Quit();
                }
            }

            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //        //double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            //        //double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            //        //double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            //        //double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
            //        //
            //        //double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(xMax - xMin) / 4;
            //        //double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(xMax - xMin) / 4;
            //        //double posYStart = chart1.ChartAreas[0].AxisY.PixelPositionToValue (yMax - yMin) / 4;
            //        //double posYFinish = chart1.ChartAreas[0].AxisY.PixelPositionToValue(yMax - yMin) / 4;
            //
            //        chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0,100);
            //        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100);
            //    
            //}
            //catch (SystemException se)
            //{
            //    MessageBox.Show(se.Message);
            //}            
        }
       
    }
}

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
    public partial class VXMLChart : Form
    {
        public VXMLChart()
        {
            InitializeComponent();
        }

        private void VXMLChart_Load(object sender, EventArgs e)
        {
            string[] seriesvszArray = Main.vsz.ToArray();
            string[] seriesrssArray = Main.rss.ToArray();

            int dif1 = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
            int dif2 = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
            
            string finaldif1 = dif1.ToString() + "KB";
            Series series1 = this.chart1.Series.Add(finaldif1);
            string finaldif2 = dif2.ToString() + "KB";
            Series series2 = this.chart2.Series.Add(finaldif2);
            this.chart1.Palette = ChartColorPalette.SeaGreen;
            this.chart2.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            this.chart1.Titles.Add("VSZ Memory graph");
            this.chart2.Titles.Add("RSS Memory graph");

            for (int i = 0; i < seriesvszArray.Length; i++)
            {

                this.chart1.Series["VSZ Usage"].Points.AddXY(i, seriesvszArray[i]);
                this.chart2.Series["RSS Usage"].Points.AddXY(i, seriesrssArray[i]);
                // Add series.

                //Series series= this.chart1.Series.Add(dif.ToString());
                // Add point.
                //series.Points.Add(Convert.ToInt32(seriesArray[i]));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vszpath = Main.currentdir + "\\" + "VXML_Chart_VSZ.jpeg";
            string rsspath = Main.currentdir + "\\" + "VXML_Chart_RSS.jpeg";
            try
            {
                this.chart1.SaveImage(vszpath, ChartImageFormat.Jpeg);
                this.chart2.SaveImage(rsspath, ChartImageFormat.Jpeg);
                MessageBox.Show("VXML Chart Image Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}

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
    public partial class Statistics_Analysis : Form
    {
        public Statistics_Analysis()
        {
            InitializeComponent();
        }

        private void Statistics_Analysis_Load(object sender, EventArgs e)
        {
           
            richTextBox1.AppendText("RTP Statistics:-");
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("\tRTP Packets TX =");
            richTextBox1.AppendText(Main.rtppackettxttotal);
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("\tRTP Packets RX =");
            richTextBox1.AppendText(Main.rtppacketrxtotal);
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("\tRTP Packets Lost =");
            richTextBox1.AppendText(Main.rtppacketlosttotal);
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("DSP Utilization:-");
            richTextBox1.AppendText(Main.dspstable);
            richTextBox1.AppendText("\n");
            //For graph plotting

            string[] avgdsputilization = Main.AvgDSPUtilization.ToArray();
            this.chart1.Palette = ChartColorPalette.Fire;
            this.chart1.Titles.Add("DSP Utilization Graph");
            string s = Main.dspstable;
            //Series series = this.chart1.Series.Add(s);
            int len = Main.AvgDSPUtilizationlen;
            for (int i = 0; i < len; i++)
            {

                this.chart1.Series["DSP Utilization"].Points.AddXY(i, avgdsputilization[i]);

            }

           
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dsppath = Main.currentdir + "\\" + "DSP_Chart.Jpeg";
            try
            {
                this.chart1.SaveImage(dsppath, ChartImageFormat.Jpeg);
                MessageBox.Show("DSP Utilization Chart Image Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}

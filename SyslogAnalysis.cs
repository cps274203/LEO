using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Load_Effort_Optimisation
{
    public partial class SyslogAnalysis : Form
    {
        public static string logtype = "";
        public SyslogAnalysis()
        {
            InitializeComponent();
        }

        private void SyslogAnalysis_Load(object sender, EventArgs e)
        {

            string[] warningArray = Main.warning.ToArray();
            string[] errorArray = Main.error.ToArray();
            string[] alertArray = Main.alert.ToArray();
            string[] criticalArray = Main.critical.ToArray();
          
            
            this.chart1.Titles.Add("Syslog Statistics graph");
            //this.chart1.Titles
            double totollogscount = Main.syslogcount;
            double addall = criticalArray.Length + errorArray.Length + alertArray.Length + warningArray.Length;
            //other logs means notice/warning
            double otherlogs = totollogscount - addall;
            double criticalpercentage = (Convert.ToDouble(criticalArray.Length) * 100) / totollogscount;
            criticalpercentage = Math.Round(criticalpercentage, 2);
            double errorpercentage = (Convert.ToDouble(errorArray.Length) * 100) / totollogscount;
            errorpercentage = Math.Round(errorpercentage, 2);
            double alertpercentage = (Convert.ToDouble(alertArray.Length)) * 100 / totollogscount;
            alertpercentage = Math.Round(alertpercentage, 2);
            double warningpercentage = (Convert.ToDouble(warningArray.Length)) * 100 / totollogscount;
            warningpercentage = Math.Round(warningpercentage, 2);

            double otherpercentage = otherlogs * 100 / totollogscount;
            otherpercentage = Math.Round(otherpercentage, 2);

            this.chart1.Series["Logs Graph"].Points.AddXY("Critical "+ criticalpercentage +" %", criticalArray.Length);
            //this.chart1.Ser
            this.chart1.Series["Logs Graph"].Points.AddXY("Error " + errorpercentage + " %", errorArray.Length);
            this.chart1.Series["Logs Graph"].Points.AddXY("Alert " + alertpercentage + " %", alertArray.Length);
            this.chart1.Series["Logs Graph"].Points.AddXY("Warning " + warningpercentage + " %", warningArray.Length);
            this.chart1.Series["Logs Graph"].Points.AddXY("Others-Notice/Information/Debug " + otherpercentage + " %", otherlogs);
           
                            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            logtype = "critical";
            LogDisplay ld = new LogDisplay();
            ld.Show();
            
           //nt len = criticalArray.Length;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            logtype = "alert";
            LogDisplay ld = new LogDisplay();
            ld.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            logtype = "error";
            LogDisplay ld = new LogDisplay();
            ld.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            logtype = "warning";
            LogDisplay ld = new LogDisplay();
            ld.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            logtype = "all";
            LogDisplay ld = new LogDisplay();
            ld.Show();
        }
    }
}

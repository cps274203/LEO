using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Load_Effort_Optimisation
{
    public partial class Overall_report : Form
    {
        double otherpercentage;
        double criticalpercentage;
        double errorpercentage;
        double alertpercentage;
        double warningpercentage;
        double a;
        double b;
        double c;
        double d;
        double e;
        double f;
        double callfailed;
        double conffailed;
        RichTextBox rtb = new RichTextBox();
        public Overall_report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBoxoverall.Text);
                MessageBox.Show("Copied");
            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception at button1_click-Overall_report", se.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void syslogerrors()
        {
            try
            {
                string[] warningArray = Main.warning.ToArray();
                string[] errorArray = Main.error.ToArray();
                string[] alertArray = Main.alert.ToArray();
                string[] criticalArray = Main.critical.ToArray();
                double totollogscount = Main.syslogcount;
                double addall = criticalArray.Length + errorArray.Length + alertArray.Length + warningArray.Length;
                //other logs means notice/warning
                double otherlogs = totollogscount - addall;
                criticalpercentage = (Convert.ToDouble(criticalArray.Length) * 100) / totollogscount;
                criticalpercentage = Math.Round(criticalpercentage, 2);
                errorpercentage = (Convert.ToDouble(errorArray.Length) * 100) / totollogscount;
                errorpercentage = Math.Round(errorpercentage, 2);
                alertpercentage = (Convert.ToDouble(alertArray.Length)) * 100 / totollogscount;
                alertpercentage = Math.Round(alertpercentage, 2);
                warningpercentage = (Convert.ToDouble(warningArray.Length)) * 100 / totollogscount;
                warningpercentage = Math.Round(warningpercentage, 2);
                otherpercentage = otherlogs * 100 / totollogscount;
                otherpercentage = Math.Round(otherpercentage, 2);

                 a=Convert.ToDouble(Main.atstats[0]);
                 b = Convert.ToDouble(Main.atstats[1]);
                 c = Convert.ToDouble(Main.atstats[2]);
                 d = Convert.ToDouble(Main.atstats[3]);
                 e = Convert.ToDouble(Main.atstats[4]);
                 f = Convert.ToDouble(Main.atstats[5]);
                 callfailed = Math.Round((c / a)*100,3);
                 conffailed = Math.Round((f / d) * 100, 3);
            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception at Overall_report-syslogerrors", se.Message);
            }

        }
        private void Overall_report_Load(object sender, EventArgs e)
        {
            syslogerrors();
            try
            {
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.AppendText("\n");
                if (Main.crashfound != "no")
                {
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("Note:-Crash found during the load test!!");
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText(Main.crashfound);
                    richTextBoxoverall.AppendText("\n");

                }
                if (Main.currentportactive != "unknown")
                {
                    if (Convert.ToInt32(Main.currentportactive) != 0)
                    {
                        richTextBoxoverall.SelectionColor = Color.Red;
                        richTextBoxoverall.AppendText("Note:-Ports are not cleared properly!! Total active ports found=");
                        richTextBoxoverall.SelectionColor = Color.Red;
                        richTextBoxoverall.AppendText(Main.currentportactive);
                        richTextBoxoverall.AppendText("\n");

                    }
                }
                richTextBoxoverall.AppendText("1-Total Duration[Hours/days] of Model Run:-");
                richTextBoxoverall.AppendText(Main.totatrunningtime.ToString());
                if (Main.TestToolProtocol == "SIP")
                {
                    richTextBoxoverall.AppendText("/");
                    double days = Convert.ToDouble(Main.totatrunningtime) / 24;
                    days = Math.Round(days, 2);
                    richTextBoxoverall.AppendText(days.ToString());
                }
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.AppendText("2-Issue Observed as below:-");
                richTextBoxoverall.AppendText("\n");
                string topfilepath = Main.currentdir + "\\" + "top.txt";
                if (File.Exists(topfilepath))
                {
                    richTextBoxoverall.AppendText("\tNote-Top Interval[hh:mm:ss]=");
                    richTextBoxoverall.AppendText(Main.topinterval);
                    richTextBoxoverall.AppendText("\n");
                    if (Main.dif >= 1000)
                    {
                        richTextBoxoverall.SelectionColor = Color.Red;
                        richTextBoxoverall.AppendText("\tA-Overall Memory Consumption=  ");
                        richTextBoxoverall.AppendText(Main.dif.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        if (Main.cpdifvsz >= 5000)
                        {
                            richTextBoxoverall.SelectionColor = Color.Red;
                            richTextBoxoverall.AppendText("\t1-Cp/SCRM Process Memory Consumption for VSZ=  ");
                            richTextBoxoverall.AppendText(Main.cpdifvsz.ToString());
                            richTextBoxoverall.AppendText("KB Consumption found!!!");
                        }
                        else
                        {
                            richTextBoxoverall.AppendText("\t1-Cp/SCRM Process Memory Consumption for VSZ=  ");
                            richTextBoxoverall.AppendText(Main.cpdifvsz.ToString());
                            richTextBoxoverall.AppendText("KB Consumption found!!!");
                        }
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t2-Cp/SCRM Process Memory Consumption for RSS=  ");
                        richTextBoxoverall.AppendText(Main.cpdifrss.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t3-SE Process Memory Consumption for VSZ=  ");
                        richTextBoxoverall.AppendText(Main.sedifvsz.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t4-OAMP Process Memory Consumption for VSZ= ");
                        richTextBoxoverall.AppendText(Main.oampdifvsz.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        if (Main.MpcMp1vsz != 0)
                        {
                        richTextBoxoverall.AppendText("\t5-DSP Memory Consumption=  ");
                        richTextBoxoverall.AppendText("\n");
                       
                            if (Main.MpcMp1vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp1 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp1vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");

                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp1 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp1vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp2vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp2 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp2vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp2 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp2vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");

                            if (Main.MpcMp3vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp3 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp3vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp3 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp3vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");

                            if (Main.MpcMp4vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp4 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp4vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp4 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp4vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");

                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp5vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp5 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp5vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp5 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp5vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp6vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp6 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp6vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp6 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp6vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp7vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp7 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp7vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp7 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp7vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp8vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp8 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp8vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp8 Memory Consumption=  ");

                                richTextBoxoverall.AppendText(Main.MpcMp8vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp9vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp9 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp9vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp9 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp9vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp10vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp10 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp10vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp10 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp10vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp11vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp11 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp11vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp11 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp11vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp12vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp12 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp12vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp12 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp12vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");

                            }
                        }


                    }
                    else
                    {
                        richTextBoxoverall.AppendText("\tA-Memory Consumption= No!!!\t");
                        richTextBoxoverall.AppendText(Main.dif.ToString());
                        richTextBoxoverall.AppendText("KB found");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t1-Cp/SCRM Process Memory Consumption for VSZ=  ");
                        richTextBoxoverall.AppendText(Main.cpdifvsz.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t2-Cp/SCRM Process Memory Consumption for RSS=  ");
                        richTextBoxoverall.AppendText(Main.cpdifrss.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t3-SE Process Memory Consumption for VSZ=  ");
                        richTextBoxoverall.AppendText(Main.sedifvsz.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\t4-OAMP Process Memory Consumption for VSZ=  ");
                        richTextBoxoverall.AppendText(Main.oampdifvsz.ToString());
                        richTextBoxoverall.AppendText("KB Consumption found!!!");
                        richTextBoxoverall.AppendText("\n");
                        if (Main.MpcMp1vsz != 0)
                        {
                            richTextBoxoverall.AppendText("\t5-DSP Memory Consumption= ");
                            richTextBoxoverall.AppendText("\n");
                             if (Main.MpcMp1vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp1 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp1vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");

                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp1 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp1vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp2vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp2 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp2vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp2 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp2vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");

                            if (Main.MpcMp3vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp3 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp3vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp3 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp3vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");

                            if (Main.MpcMp4vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp4 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp4vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp4 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp4vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");

                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp5vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp5 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp5vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp5 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp5vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp6vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp6 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp6vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp6 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp6vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp7vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp7 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp7vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp7 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp7vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp8vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp8 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp8vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp8 Memory Consumption=  ");

                                richTextBoxoverall.AppendText(Main.MpcMp8vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp9vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp9 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp9vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp9 Memory Consumption= ");
                                richTextBoxoverall.AppendText(Main.MpcMp9vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp10vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp10 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp10vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp10 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp10vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp11vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp11 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp11vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp11 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp11vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MpcMp12vsz >= 5000)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("\t\tMpcMp12 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp12vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                            else
                            {
                                richTextBoxoverall.AppendText("\t\tMpcMp12 Memory Consumption=  ");
                                richTextBoxoverall.AppendText(Main.MpcMp12vsz.ToString());
                                richTextBoxoverall.AppendText("KB Consumption found!!!");
                            }
                        }
                    }
                }
                else
                {
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("\tA-top output not found!!!  ");
                }
            }
            
            catch (SystemException se)
            {
                MessageBox.Show("Exception at Overall_report-memory reporting",se.Message);
            }
        
                        
            richTextBoxoverall.AppendText("\n");
            richTextBoxoverall.AppendText("\tB-Syslog Analysis:-");
            richTextBoxoverall.AppendText("\n");
          
            if (otherpercentage < 90)
            {
                
                richTextBoxoverall.SelectionColor = Color.Red;
                richTextBoxoverall.AppendText("\tCritical logs =" + criticalpercentage +"%");
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.SelectionColor = Color.Red;
                richTextBoxoverall.AppendText("\tAlerts logs =" + alertpercentage+"%");
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.SelectionColor = Color.Red;
                richTextBoxoverall.AppendText("\tWarning logs =" + warningpercentage + "%");
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.SelectionColor = Color.Red;
                richTextBoxoverall.AppendText("\tError logs =" + errorpercentage + "%");
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.SelectionColor = Color.Red;
                richTextBoxoverall.AppendText("\tNotice,Info and Debugs logs =" + otherpercentage + "%");
                richTextBoxoverall.AppendText("\n");
            }
            try
            {
                //For Critical logs

                if (Main.criticaldist.Count >= 20)
                {
                    richTextBoxoverall.AppendText("\tCritical Log-Only Distinct");
                    richTextBoxoverall.AppendText("\n");
                    for (int i = Main.criticaldist.Count - 10; i < Main.criticaldist.Count; i++)
                    {
                        richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.SelectionColor = Color.Red;
                        richTextBoxoverall.AppendText(Main.criticaldist[i]);
                        richTextBoxoverall.AppendText("\n");
                   

                    }
                }
                else
                {
                    if (Main.criticaldist.Count == 0)
                    {
                        richTextBoxoverall.AppendText("\tCritical Log:-None");
                        richTextBoxoverall.AppendText("\n");

                    }
                    else
                    {
                        //richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.AppendText("\tCritical Log");
                        richTextBoxoverall.AppendText("\n");
                        for (int i = 0; i < Main.criticaldist.Count; i++)
                        {
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.SelectionColor = Color.Red;
                            richTextBoxoverall.AppendText(Main.criticaldist[i]);
                            richTextBoxoverall.AppendText("\n");

                        }
                    }
                }
                //For Alert logs


                if (Main.alertdist.Count >= 20)
                {
                    richTextBoxoverall.AppendText("\tAlert Log-Only Distinct");
                    richTextBoxoverall.AppendText("\n");
                    for (int i = Main.alertdist.Count - 10; i < Main.alertdist.Count; i++)
                    {
                        richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.SelectionColor = Color.Red;
                        richTextBoxoverall.AppendText(Main.alertdist[i]);
                        richTextBoxoverall.AppendText("\n");

                    }
                }
                else
                {
                    if (Main.alertdist.Count == 0)
                    {
                        richTextBoxoverall.AppendText("\tAlert Log:-None");
                        richTextBoxoverall.AppendText("\n");

                    }
                    else
                    {
                        //richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.AppendText("\tAlert Log");
                        richTextBoxoverall.AppendText("\n");
                        for (int i = 0; i < Main.alertdist.Count; i++)
                        {
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.SelectionColor = Color.Red;
                            richTextBoxoverall.AppendText(Main.alertdist[i]);
                            richTextBoxoverall.AppendText("\n");

                        }
                    }
                }



                //For Error Logs

                if (Main.errordist.Count >= 20)
                {
                    richTextBoxoverall.AppendText("\tError Log-Only Distinct");
                    richTextBoxoverall.AppendText("\n");
                    for (int i = Main.errordist.Count - 10; i < Main.errordist.Count; i++)
                    {
                        richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.AppendText(Main.errordist[i]);
                        richTextBoxoverall.AppendText("\n");

                    }
                }
                else
                {
                    if (Main.errordist.Count == 0)
                    {
                        richTextBoxoverall.AppendText("\tError Log:-None");
                        richTextBoxoverall.AppendText("\n");

                    }
                    else
                    {
                        //richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.AppendText("\tError Log");
                        richTextBoxoverall.AppendText("\n");
                        for (int i = 0; i < Main.errordist.Count; i++)
                        {
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.AppendText(Main.errordist[i]);
                            richTextBoxoverall.AppendText("\n");

                        }
                    }
                }


                //For Warning logs

                if (Main.warningdist.Count >= 20)
                {
                    richTextBoxoverall.AppendText("\tWarning Log:-Only Distinct");
                    richTextBoxoverall.AppendText("\n");
                    for (int i = Main.warningdist.Count - 10; i < Main.warningdist.Count; i++)
                    {
                        richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.AppendText(Main.warningdist[i]);
                        richTextBoxoverall.AppendText("\n");

                    }
                }
                else
                {
                    if (Main.warningdist.Count == 0)
                    {
                        richTextBoxoverall.AppendText("\tWarning Log:-None");
                        richTextBoxoverall.AppendText("\n");

                    }
                    else
                    {
                        //richTextBoxoverall.AppendText("\t");
                        richTextBoxoverall.AppendText("\tWarning Log");
                        richTextBoxoverall.AppendText("\n");
                        for (int i = 0; i < Main.warningdist.Count; i++)
                        {
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.AppendText(Main.warningdist[i]);
                            richTextBoxoverall.AppendText("\n");

                        }
                    }
                }

            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception at Overall_report-Syslog % reporting", se.Message);
            }
            richTextBoxoverall.AppendText("\n");
            richTextBoxoverall.AppendText("\tC- Slipped/Super/Burst cycles Per DSP");
            richTextBoxoverall.AppendText("\n");
            try
            {
                if (Main.LoadModellingtext == "OK")
                {
                    int checkslip = 0;
                    int checkburst = 0;
                    int checksuper = 0;
                    for (int i = 1; i < Main.numtotaldsp + 1; i++)
                    {
                        if (null != Main.slip[i])
                        {
                            checkslip++;
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.SelectionColor = Color.Red;
                            richTextBoxoverall.AppendText(Main.slip[i]);
                            richTextBoxoverall.AppendText("\n");
                        }
                        if (null != Main.super[i])
                        {
                            checksuper++;
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.SelectionColor = Color.Red;
                            richTextBoxoverall.AppendText(Main.super[i]);
                            richTextBoxoverall.AppendText("\n");
                        }
                        if (null != Main.burst[i])
                        {
                            checkburst++;
                            richTextBoxoverall.AppendText("\t");
                            richTextBoxoverall.SelectionColor = Color.Red;
                            richTextBoxoverall.AppendText(Main.burst[i]);
                            richTextBoxoverall.AppendText("\n");
                        }
                    }
                  
                   if (checkburst == 0)
                   {
                       richTextBoxoverall.AppendText("\tNo Burst Slip cycles found for Any Audio , Video and HC DSP!!");
                       richTextBoxoverall.AppendText("\n");
                   }
                   if (checksuper == 0)
                   {
                       richTextBoxoverall.AppendText("\tNo Super Slip cycles found for Any Audio and Video DSP!!");
                       richTextBoxoverall.AppendText("\n");
                   }
                   if (checkslip == 0)
                   {
                       richTextBoxoverall.AppendText("\tNo Slip cycles found for Any Audio DSP!!");
                       richTextBoxoverall.AppendText("\n");
                   }

                }
                else
                {
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("\tSyslog does not have notice level log so slip cycles can not be calculated !!");
                    richTextBoxoverall.AppendText("\n");
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception at Overall_report-Slipped/Super/Burst reporting", se.Message);
            }
            try
            {
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.AppendText("\tC- Other DSP details Per DSP");
                richTextBoxoverall.AppendText("\n");
                if (Main.LoadModellingtext == "OK")
                {
                    for (int i = 1; i < Main.numtotaldsp + 1; i++)
                    {
                        if (null != Main.audioscrm[i])
                        {
                            //LostCmds(CP->DSP):
                            if (Main.LostCmdsCPDSP[i].Sum() != 0)
                            {
                                if ((Main.LostCmdsCPDSP[i].Sum()-Main.LostCmdsCPDSP[i][0]) >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total LostCmds(CP->DSP)=" + Main.LostCmdsCPDSP[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total LostCmds(CP->DSP)=" + Main.LostCmdsCPDSP[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            //LostCmds(DSP->CP):
                            if (Main.LostCmdsDSPCP[i].Sum() != 0)
                            {
                                if ((Main.LostCmdsDSPCP[i].Sum()-Main.LostCmdsDSPCP[i][0]) >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total LostCmds(DSP->CP)=" + Main.LostCmdsDSPCP[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total LostCmds(DSP->CP)=" + Main.LostCmdsDSPCP[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            //NonDecodedAudioFrames
                            if (Main.sysNonDecodedAudioFrames[i].Sum() != 0)
                            {
                                if (Main.sysNonDecodedAudioFrames[i].Sum()>= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total NonDecodedAudioFrames=" + Main.sysNonDecodedAudioFrames[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total NonDecodedAudioFrames=" + Main.sysNonDecodedAudioFrames[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysJitterBufFramesDropped[i].Sum() != 0)
                            {
                                if (Main.sysJitterBufFramesDropped[i].Sum() >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total JitterBufFramesDropped=" + Main.sysJitterBufFramesDropped[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total JitterBufFramesDropped=" + Main.sysJitterBufFramesDropped[i].Sum().ToString() + " found for Audio DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                
                
                        }
                
                        //Video DSP
                        if (null != Main.videoscrm[i])
                        {
                            //LostCmds(CP->DSP):
                            if (Main.LostCmdsCPDSP[i].Sum() != 0)
                            {
                                if ((Main.LostCmdsCPDSP[i].Sum()-Main.LostCmdsCPDSP[i][0]) >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total LostCmds(CP->DSP)=" + Main.LostCmdsCPDSP[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total LostCmds(CP->DSP)=" + Main.LostCmdsCPDSP[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            //LostCmds(DSP->CP):
                            if (Main.LostCmdsDSPCP[i].Sum() != 0)
                            {
                                if ((Main.LostCmdsDSPCP[i].Sum()-Main.LostCmdsCPDSP[i][0]) >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total LostCmds(DSP->CP)=" + Main.LostCmdsDSPCP[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total LostCmds(DSP->CP)=" + Main.LostCmdsDSPCP[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            //sysNonDecodedVideoFrames
                            if (Main.sysNonDecodedVideoFrames[i].Sum() != 0)
                            {
                                if (Main.sysNonDecodedVideoFrames[i].Sum() >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total NonDecodedVideoFrames=" + Main.sysNonDecodedVideoFrames[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total NonDecodedVideoFrames=" + Main.sysNonDecodedVideoFrames[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysJitterBufFramesDropped[i].Sum() != 0)
                            {
                                if (Main.sysJitterBufFramesDropped[i].Sum() >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total JitterBufFramesDropped=" + Main.sysJitterBufFramesDropped[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total JitterBufFramesDropped=" + Main.sysJitterBufFramesDropped[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysMaxVideoFrameDrop[i].Sum() != 0)
                            {
                                if (Main.sysMaxVideoFrameDrop[i].Average() >= 5)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total Avg. MaxVideoFrameDrop=" + Main.sysMaxVideoFrameDrop[i].Average().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total MaxVideoFrameDrop=" + Main.sysMaxVideoFrameDrop[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysAvgVideoFrameDrop[i].Sum() != 0)
                            {
                                if (Main.sysAvgVideoFrameDrop[i].Average() >= 5)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total Avg. AvgVideoFrameDrop=" + Main.sysAvgVideoFrameDrop[i].Average().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total AvgVideoFrameDrop=" + Main.sysAvgVideoFrameDrop[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysDroppedVideoPacket[i].Sum() != 0)
                            {
                                if (Main.sysDroppedVideoPacket[i].Average() >= 5)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total Avg. DroppedVideoPacket=" + Main.sysDroppedVideoPacket[i].Average().ToString() + " found for Video DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total DroppedVideoPacket=" + Main.sysDroppedVideoPacket[i].Sum().ToString() + " found for Video DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                
                        }
                
                
                        //HC DSP
                        if (null != Main.hdscrm[i])
                        {
                            //LostCmds(CP->DSP):
                            if (Main.LostCmdsCPDSP[i].Sum() != 0)
                            {
                                if ((Main.LostCmdsCPDSP[i].Sum()-Main.LostCmdsCPDSP[i][0]) >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total LostCmds(CP->DSP)=" + Main.LostCmdsCPDSP[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total LostCmds(CP->DSP)=" + Main.LostCmdsCPDSP[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            //LostCmds(DSP->CP):
                            if (Main.LostCmdsDSPCP[i].Sum() != 0)
                            {
                                if ((Main.LostCmdsDSPCP[i].Sum()-Main.LostCmdsDSPCP[i][0]) >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total LostCmds(DSP->CP)=" + Main.LostCmdsDSPCP[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total LostCmds(DSP->CP)=" + Main.LostCmdsDSPCP[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            //sysNonDecodedVideoFrames
                            if (Main.sysNonDecodedVideoFrames[i].Sum() != 0)
                            {
                                if (Main.sysNonDecodedVideoFrames[i].Sum() >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total NonDecodedVideoFrames=" + Main.sysNonDecodedVideoFrames[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total NonDecodedVideoFrames=" + Main.sysNonDecodedVideoFrames[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysJitterBufFramesDropped[i].Sum() != 0)
                            {
                                if (Main.sysJitterBufFramesDropped[i].Sum() >= 100)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total JitterBufFramesDropped=" + Main.sysJitterBufFramesDropped[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total JitterBufFramesDropped=" + Main.sysJitterBufFramesDropped[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysMaxVideoFrameDrop[i].Sum() != 0)
                            {
                                if (Main.sysMaxVideoFrameDrop[i].Average() >= 5)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total Avg. MaxVideoFrameDrop=" + Main.sysMaxVideoFrameDrop[i].Average().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total MaxVideoFrameDrop=" + Main.sysMaxVideoFrameDrop[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysAvgVideoFrameDrop[i].Sum() != 0)
                            {
                                if (Main.sysAvgVideoFrameDrop[i].Average() >= 5)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total Avg. AvgVideoFrameDrop=" + Main.sysAvgVideoFrameDrop[i].Average().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total AvgVideoFrameDrop=" + Main.sysAvgVideoFrameDrop[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                            if (Main.sysDroppedVideoPacket[i].Sum() != 0)
                            {
                                if (Main.sysDroppedVideoPacket[i].Average() >= 5)
                                {
                                    richTextBoxoverall.SelectionColor = Color.Violet;
                                    richTextBoxoverall.AppendText("\t Total Avg. DroppedVideoPacket=" + Main.sysDroppedVideoPacket[i].Average().ToString() + " found for HC DSP" + i.ToString());
                                    richTextBoxoverall.AppendText("\n");
                                }
                                //else
                                //{
                                //    richTextBoxoverall.AppendText("\t Total DroppedVideoPacket=" + Main.sysDroppedVideoPacket[i].Sum().ToString() + " found for HC DSP" + i.ToString());
                                //    richTextBoxoverall.AppendText("\n");
                                //}
                            }
                
                        }
                    }
                                       
                
                }
                else
                {
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("\tSyslog does not have notice level logs so other DSP details can not be calculated !!");
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception at Overall_report-Other DSP reporting", se.Message);
            }
            try
            {
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.AppendText("\n");
                richTextBoxoverall.AppendText("3-Load Model Details:-");
                richTextBoxoverall.AppendText("\n");
                //check for AT_output status
                if (Main.RTPGstats[4] != null)
                {
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tProtocol=");
                    richTextBoxoverall.AppendText(Main.TestToolProtocol);
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tLoad Model Name=");
                    richTextBoxoverall.AppendText(Main.RTPGstats[4]);
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tNumberOfConnections=");
                    richTextBoxoverall.AppendText(Main.NumberOfConnections.ToString());
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tNumberOfConfenrece=");
                    richTextBoxoverall.AppendText(Main.NumberOfConferences.ToString());
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tCalls Created =");
                    richTextBoxoverall.AppendText(Main.atstats[0].ToString());
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tCalls Deleted =");
                    richTextBoxoverall.AppendText(Main.atstats[1].ToString());
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tFailed Scenarios =");
                    richTextBoxoverall.AppendText(Main.atstats[2].ToString());
                    richTextBoxoverall.AppendText("\tCalls failed");
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tFailed Percentage =");
                    richTextBoxoverall.AppendText(callfailed + "\t% calls failed");
                    richTextBoxoverall.AppendText("\n");
                    if (Convert.ToInt32(Main.atstats[3]) != 0)
                    {
                        richTextBoxoverall.AppendText("\tNumber of Conferences Created =");
                        richTextBoxoverall.AppendText(Main.atstats[3].ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Conferences Deleted =");
                        richTextBoxoverall.AppendText(Main.atstats[4].ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tConference Failures =");
                        richTextBoxoverall.AppendText(Main.atstats[5].ToString());
                        richTextBoxoverall.AppendText("\t" + conffailed + "% Conference failed");
                        richTextBoxoverall.AppendText("\n");
                    }
                    //add SDP detail here.



                    richTextBoxoverall.AppendText("4-RTPG Statistic from AT output:-");
                    richTextBoxoverall.AppendText("\n\t");
                    richTextBoxoverall.AppendText(Main.RTPGstats[0]);
                    richTextBoxoverall.AppendText("\n\t");
                    richTextBoxoverall.AppendText(Main.RTPGstats[1]);
                    richTextBoxoverall.AppendText("\n\t");
                    richTextBoxoverall.AppendText(Main.RTPGstats[2]);
                    richTextBoxoverall.AppendText("\n\t");
                    richTextBoxoverall.AppendText(Main.RTPGstats[3]);
                    richTextBoxoverall.AppendText("\n");

                    if (Convert.ToInt32(Main.numtotaldsp) != 0)
                    {
                        richTextBoxoverall.AppendText("5-Core Allocation:-");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Media Processing Cores =");
                        richTextBoxoverall.AppendText(Main.numtotaldsp.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Video Processing Cores =");
                        richTextBoxoverall.AppendText(Main.numvideodsp.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Audio Processing Cores = ");
                        richTextBoxoverall.AppendText(Main.numaudiodsp.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of HD Video Processing Cores = ");
                        richTextBoxoverall.AppendText(Main.numhddsp.ToString());
                        richTextBoxoverall.AppendText("\n");
                    }
                    else
                    {
                        richTextBoxoverall.AppendText("5-Core Allocation using SNMP :-");
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Media Processing Cores =");
                        richTextBoxoverall.AppendText(Main.numtotaldspsnmp.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Video Processing Cores =");
                        richTextBoxoverall.AppendText(Main.numvideodspsnmp.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of Audio Processing Cores = ");
                        richTextBoxoverall.AppendText(Main.numaudiodspsnmp.ToString());
                        richTextBoxoverall.AppendText("\n");
                        richTextBoxoverall.AppendText("\tNumber of HD Video Processing Cores = ");
                        richTextBoxoverall.AppendText(Main.numhddspsnmp.ToString());
                        richTextBoxoverall.AppendText("\n");
                    }
                }
                else
                {
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("Model name not found");
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("AT_output is missing in current dir");
                    richTextBoxoverall.AppendText("\n");
                }
                //From Statistics
                richTextBoxoverall.AppendText("6-Statistics Analysis from MS");
                richTextBoxoverall.AppendText("\n");
                string[] statfilename = Directory.GetFiles(Main.currentdir, "statistics*");
                if (statfilename.Length != 0)
                {
                    richTextBoxoverall.AppendText("\tNote:-Statistics reporting interval [In Minutes]:-");
                    richTextBoxoverall.AppendText(Main.statisticsinterval.ToString());
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\tA-RTP Statistics:-");
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\t\tRTP Packets TX =");
                    richTextBoxoverall.AppendText(Main.rtppackettxttotal);
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\t\tRTP Packets RX =");
                    richTextBoxoverall.AppendText(Main.rtppacketrxtotal);
                    richTextBoxoverall.AppendText("\n");
                    richTextBoxoverall.AppendText("\t\tRTP Packets Lost =");
                    richTextBoxoverall.AppendText(Main.rtppacketlosttotal);
                    richTextBoxoverall.AppendText("\n");
                    try
                    {
                        double totalpacket = Convert.ToDouble(Main.rtppackettxttotal) + Convert.ToDouble(Main.rtppacketrxtotal);
                        double per = (Convert.ToDouble(Main.rtppacketlosttotal) * 100) / totalpacket;
                        per = Math.Round(per, 4);


                        if (per > 2)
                        {
                            richTextBoxoverall.SelectionColor = Color.Red;
                        }
                        richTextBoxoverall.AppendText("\t\tRTP Packets Lost in % =");
                        richTextBoxoverall.AppendText(per.ToString());
                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show("% calcualtion failed for RTP Packets loss -Overall Reprot", se.Message);
                    }
                    richTextBoxoverall.AppendText("\n");
                    try
                    {
                        if (Main.superslipaudio.Count >= 10)
                        {
                            if (Main.superslipaudio.Average() > 3)
                            {
                                //richTextBoxoverall.SelectionColor = Color.Red;
                            }
                            richTextBoxoverall.AppendText("\t\tSuper Slipped Cycle Count (Audio)[Highest Top 10] =");

                            for (int size = Main.superslipaudio.Count - 1; size > Main.superslipaudio.Count - 10; size--)
                            {
                                richTextBoxoverall.AppendText(Main.superslipaudio[size].ToString());
                                richTextBoxoverall.AppendText(",");
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.superslipvideo.Count != 0)
                            {
                                if (Main.superslipvideo.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tSuper Slipped Cycle Count (Video)[Highest Top 10] =");
                                for (int size = Main.superslipvideo.Count - 1; size > Main.superslipvideo.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.superslipvideo[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                            richTextBoxoverall.AppendText("\n");
                            if (Main.burstslipaudio.Count != 0)
                            {
                                if (Main.burstslipaudio.Average() > 1)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tBurst Slipped Cycle Count (Audio)[Highest Top 10] =");
                                for (int size = Main.burstslipaudio.Count - 1; size > Main.burstslipaudio.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.burstslipaudio[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }

                            //richTextBoxoverall.AppendText(Main.burstslipaudio.ToString());
                            richTextBoxoverall.AppendText("\n");
                            if (Main.burstslipvideo.Count != 0)
                            {
                                if (Main.burstslipvideo.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tBurst Slipped Cycle Count (Video)[Highest Top 10] =");
                                for (int size = Main.burstslipvideo.Count - 1; size > Main.burstslipvideo.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.burstslipvideo[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                            //richTextBoxoverall.AppendText(Main.burstslipvideo.ToString());
                            richTextBoxoverall.AppendText("\n");
                            if (Main.burstslipHC.Count != 0)
                            {
                                if (Main.burstslipHC.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tBurst Slipped Cycle Count (HC Video)[Highest Top 10] =");
                                for (int size = Main.burstslipHC.Count - 1; size > Main.burstslipHC.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.burstslipHC[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                            //richTextBoxoverall.AppendText(Main.burstslipHC.ToString());
                            richTextBoxoverall.AppendText("\n");


                            if (Convert.ToInt32(Main.currentportactive) > 0)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                            }
                            richTextBoxoverall.AppendText("\t\tCurrent Ports Active =");
                            richTextBoxoverall.AppendText(Main.currentportactive.ToString());
                            richTextBoxoverall.AppendText("\n");
                            if (Main.NonDecodedVideoFrames.Count != 0)
                            {
                                if (Main.NonDecodedVideoFrames.Average() > 30)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tNon Decoded Video Frames [Highest Top 10] =");
                                for (int size = Main.NonDecodedVideoFrames.Count - 1; size > Main.NonDecodedVideoFrames.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.NonDecodedVideoFrames[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }

                            //richTextBoxoverall.AppendText(Main.NonDecodedVideoFrames.ToString());
                            richTextBoxoverall.AppendText("\n");

                            if (Main.NonDecodedAudioFrames.Count != 0)
                            {
                                if (Main.NonDecodedAudioFrames.Average() > 15)
                                {
                                    // richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tNon Decoded Audio Frames [Highest Top 10] =");
                                for (int size = Main.NonDecodedAudioFrames.Count - 1; size > Main.NonDecodedAudioFrames.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.NonDecodedAudioFrames[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }

                            //richTextBoxoverall.AppendText(Main.NonDecodedAudioFrames.ToString());
                            richTextBoxoverall.AppendText("\n");

                            if (Main.JitterBufferFramesDropped.Count != 0)
                            {
                                if (Main.JitterBufferFramesDropped.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tJitter Buffer Frames Dropped [Highest Top 10] =");
                                for (int size = Main.JitterBufferFramesDropped.Count - 1; size > Main.JitterBufferFramesDropped.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.JitterBufferFramesDropped[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                            }
                            //richTextBoxoverall.AppendText(Main.JitterBufferFramesDropped.ToString());
                            richTextBoxoverall.AppendText("\n");

                            if (Main.DroppedVideoPackets.Count != 0)
                            {
                                if (Main.DroppedVideoPackets.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tDropped Video Packets [Highest Top 10] =");
                                for (int size = Main.DroppedVideoPackets.Count - 1; size > Main.DroppedVideoPackets.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.DroppedVideoPackets[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                            //richTextBoxoverall.AppendText(Main.DroppedVideoPackets.ToString());
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MaxFramesDroppedHCVideo.Count != 0)
                            {
                                if (Main.MaxFramesDroppedHCVideo.Average() > 10)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tMax Frames Dropped HC Video[Highest Top 10]  =");
                                for (int size = Main.MaxFramesDroppedHCVideo.Count - 1; size > Main.MaxFramesDroppedHCVideo.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.MaxFramesDroppedHCVideo[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                            //richTextBoxoverall.AppendText(Main.MaxFramesDroppedHCVideo.ToString());
                            richTextBoxoverall.AppendText("\n");
                            if (Main.MaxVideoEncoderFrameRateDropRate.Count != 0)
                            {
                                if (Main.MaxVideoEncoderFrameRateDropRate.Average() > 10)
                                {
                                    // richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tMax Video Encoder Frame Rate Drop Rate[Highest Top 10]  =");
                                for (int size = Main.MaxVideoEncoderFrameRateDropRate.Count - 1; size > Main.MaxVideoEncoderFrameRateDropRate.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.MaxVideoEncoderFrameRateDropRate[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                        }
                        else 
                        {
                            richTextBoxoverall.AppendText("\t\tSuper Slipped Cycle Count (Audio)[Highest Top 10] =");
                            if (Main.superslipaudio.Count != 0)
                            {
                                if (Main.superslipaudio.Average() > 3)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                

                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.superslipaudio[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");
                            }
                            richTextBoxoverall.AppendText("\t\tSuper Slipped Cycle Count (Video)[Highest Top 10] =");
                            if (Main.superslipvideo.Count != 0)
                            {
                                if (Main.superslipvideo.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.superslipvideo[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");
                            }
                            richTextBoxoverall.AppendText("\t\tBurst Slipped Cycle Count (Audio)[Highest Top 10] =");
                            if (Main.burstslipaudio.Count != 0)
                            {
                                if (Main.burstslipaudio.Average() > 1)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.burstslipaudio[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                //richTextBoxoverall.AppendText(Main.burstslipaudio.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");
                            }
                            richTextBoxoverall.AppendText("\t\tBurst Slipped Cycle Count (Video)[Highest Top 10] =");
                            if (Main.burstslipvideo.Count != 0)
                            {
                                if (Main.burstslipvideo.Average() > 15)
                                {
                                   // richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.burstslipvideo[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                //richTextBoxoverall.AppendText(Main.burstslipvideo.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else 
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");
                            
                            }
                            richTextBoxoverall.AppendText("\t\tBurst Slipped Cycle Count (HC Video)[Highest Top 10] =");
                            if (Main.burstslipHC.Count != 0)
                            {

                                if (Main.burstslipHC.Average() > 15)
                                {
                                   // richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.burstslipHC[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                                //richTextBoxoverall.AppendText(Main.burstslipHC.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                            if (Convert.ToInt32(Main.currentportactive) > 0)
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                            }
                            richTextBoxoverall.AppendText("\t\tCurrent Ports Active =");
                            richTextBoxoverall.AppendText(Main.currentportactive.ToString());
                            richTextBoxoverall.AppendText("\n");
                            richTextBoxoverall.AppendText("\t\tNon Decoded Video Frames [Highest Top 10] =");
                            if (Main.NonDecodedVideoFrames.Count != 0)
                            {
                                if (Main.NonDecodedVideoFrames.Average() > 30)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.NonDecodedVideoFrames[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                //richTextBoxoverall.AppendText(Main.NonDecodedVideoFrames.ToString());
                                richTextBoxoverall.AppendText("\n");

                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                            richTextBoxoverall.AppendText("\t\tNon Decoded Audio Frames [Highest Top 10] =");
                            if (Main.NonDecodedAudioFrames.Count != 0)
                            {
                                if (Main.NonDecodedAudioFrames.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.NonDecodedAudioFrames[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                //richTextBoxoverall.AppendText(Main.NonDecodedAudioFrames.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                            richTextBoxoverall.AppendText("\t\tJitter Buffer Frames Dropped [Highest Top 10] =");
                            if (Main.JitterBufferFramesDropped.Count != 0)
                            {
                                if (Main.JitterBufferFramesDropped.Average() > 15)
                                {
                                   // richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.JitterBufferFramesDropped[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                //richTextBoxoverall.AppendText(Main.JitterBufferFramesDropped.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                            richTextBoxoverall.AppendText("\t\tDropped Video Packets [Highest Top 10] =");
                            if (Main.DroppedVideoPackets.Count != 0)
                            {

                                if (Main.DroppedVideoPackets.Average() > 15)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                richTextBoxoverall.AppendText("\t\tDropped Video Packets [Highest Top 10] =");
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.DroppedVideoPackets[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                                //richTextBoxoverall.AppendText(Main.DroppedVideoPackets.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                            richTextBoxoverall.AppendText("\t\tMax Frames Dropped HC Video[Highest Top 10]  =");
                            if (Main.MaxFramesDroppedHCVideo.Count != 0)
                            {
                                if (Main.MaxFramesDroppedHCVideo.Average() > 10)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }
                                
                                for (int size = 0; size < Main.superslipaudio.Count; size++)
                                {
                                    richTextBoxoverall.AppendText(Main.MaxFramesDroppedHCVideo[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }

                                //richTextBoxoverall.AppendText(Main.MaxFramesDroppedHCVideo.ToString());
                                richTextBoxoverall.AppendText("\n");
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                            richTextBoxoverall.AppendText("\t\tMax Video Encoder Frame Rate Drop Rate[Highest Top 10]  =");
                            if (Main.MaxVideoEncoderFrameRateDropRate.Count != 0)
                            {
                                if (Main.MaxVideoEncoderFrameRateDropRate.Average() > 10)
                                {
                                    //richTextBoxoverall.SelectionColor = Color.Red;
                                }

                                for (int size = Main.MaxVideoEncoderFrameRateDropRate.Count - 1; size > Main.MaxVideoEncoderFrameRateDropRate.Count - 10; size--)
                                {
                                    richTextBoxoverall.AppendText(Main.MaxVideoEncoderFrameRateDropRate[size].ToString());
                                    richTextBoxoverall.AppendText(",");
                                }
                            }
                            else
                            {
                                richTextBoxoverall.SelectionColor = Color.Red;
                                richTextBoxoverall.AppendText("Not Found!!!!");
                                richTextBoxoverall.AppendText("\n");

                            }
                        }
                    }
                    catch(SystemException se)
                    {
                        MessageBox.Show("Exception at Overall_report-reporting statistics", se.Message);
                    }
                    //richTextBoxoverall.AppendText(Main.MaxVideoEncoderFrameRateDropRate.ToString());
                    richTextBoxoverall.AppendText("\n");

                    richTextBoxoverall.AppendText("\tB-DSP Utilization:-");
                    richTextBoxoverall.AppendText(Main.dspstable);
                    richTextBoxoverall.AppendText("\n");




                }
                else
                {
                    richTextBoxoverall.SelectionColor = Color.Red;
                    richTextBoxoverall.AppendText("\tA-Statistics file not found");
                    richTextBoxoverall.AppendText("\n");
                }
                richTextBoxoverall.AppendText("6-Attachments:-");
                richTextBoxoverall.AppendText("\n");
                string[] filenames = Directory.GetFiles(Main.currentdir);
                foreach (string filename in filenames)
                {
                    if (filename.Contains("archive.tar.gz") || filename.Contains("Load_Effort_Optimisation.exe") || filename.Contains("ICSharpCode.SharpZipLib.dll") || filename.Contains("SnmpSharpNet.dll"))
                    {
                        //noaction
                    }
                    else 
                    {
                        string s = filename.Replace(Main.currentdir+"\\", String.Empty);
                    
                        richTextBoxoverall.AppendText("\t"+s);
                        richTextBoxoverall.AppendText("\n");
                                         
                    }
                }
                richTextBoxoverall.AppendText("########################################################################\n");
                richTextBoxoverall.AppendText("###################Generated and Uploaded By Codeathon-2014 LEO Tool ##################\n");
                richTextBoxoverall.AppendText("########################################################################\n");
            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception at Overall_report_Load",se.Message);
            }
            //richTextBoxoverall.AppendText("###########Total Time Taken=");
            //richTextBoxoverall.AppendText(Main.totaltime);
           // richTextBoxoverall.AppendText("##################################\n");
    
            string path = Main.currentdir + "\\" + "comment.txt";
            //writeTextFile(path, richTextBoxoverall.Text);
            try
            {
                if (!File.Exists(path))
                {

                    //File.Create(path);
                    TextWriter tw = new StreamWriter(path);
                    tw.WriteLine(richTextBoxoverall.Text);
                    tw.Close();
                }
                else if (File.Exists(path))
                {
                    File.Delete(path);
                    TextWriter tw = new StreamWriter(path);
                    tw.WriteLine(richTextBoxoverall.Text);
                    tw.Close();
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
                Console.Write(se.Message);
            }
        }
        public void snmpgetbulk()
        {
            this.Cursor = Cursors.WaitCursor;
            OctetString community = new OctetString("CONV");

            // Define agent parameters class
            AgentParameters param = new AgentParameters(community);
            // Set SNMP version to 1 (or 2)
            param.Version = SnmpVersion.Ver2;
            try
            {
                // Construct the agent address object
                // IpAddress class is easy to use here because
                //  it will try to resolve constructor parameter if it doesn't
                //  parse to an IP address

                IpAddress agent = new IpAddress(Main.SipMSIPAddressSCC);

                // Construct target
                UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);


                //  tree you wish to retrieve
                Oid rootOid = new Oid("1.3.6.1.4.1.7569"); // ifDescr
                // This Oid represents last Oid returned by
                //  the SNMP agent
                Oid lastOid = (Oid)rootOid.Clone();
                // Pdu class used for all requests
                Pdu pdubulk = new Pdu(PduType.GetBulk);
               
                // Make SNMP request
                if (Main.PingHost(Main.SipMSIPAddressSCC))
                {
                    try
                    {

                        while (lastOid != null)
                        {
                            // When Pdu class is first constructed, RequestId is set to 0
                            // and during encoding id will be set to the random value
                            // for subsequent requests, id will be set to a value that
                            // needs to be incremented to have unique request ids for each
                            // packet
                            if (pdubulk.RequestId != 0)
                            {
                                pdubulk.RequestId += 1;
                            }
                            // Clear Oids from the Pdu class.
                            pdubulk.VbList.Clear();
                            // Initialize request PDU with the last retrieved Oid
                            pdubulk.VbList.Add(lastOid);
                            SnmpV2Packet resultbulk = (SnmpV2Packet)target.Request(pdubulk, param);
                            if (resultbulk != null)
                            {
                                // ErrorStatus other then 0 is an error returned by 
                                // the Agent - see SnmpConstants for error definitions
                                if (resultbulk.Pdu.ErrorStatus != 0)
                                {
                                    // agent reported an error with the request
                                    Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                                        resultbulk.Pdu.ErrorStatus,
                                        resultbulk.Pdu.ErrorIndex);
                                    lastOid = null;
                                    break;
                                }
                                else
                                {
                                    // Walk through returned variable bindings
                                    foreach (Vb v in resultbulk.Pdu.VbList)
                                    {
                                        // Check that retrieved Oid is "child" of the root OID
                                        if (rootOid.IsRootOf(v.Oid))
                                        {
                                            rtb.AppendText(v.Oid.ToString()+"\t"+"("+SnmpConstants.GetTypeName(v.Value.Type)+")"+"\t"+v.Value.ToString());
                                            rtb.AppendText("\n");
                                            //Console.WriteLine("{0} ({1}): {2}",
                                            //    v.Oid.ToString(),
                                            //    SnmpConstants.GetTypeName(v.Value.Type),
                                            //    v.Value.ToString());
                                            if (v.Value.Type == SnmpConstants.SMI_ENDOFMIBVIEW)
                                                lastOid = null;
                                            else
                                                lastOid = v.Oid;
                                        }
                                        else
                                        {
                                            // we have reached the end of the requested
                                            // MIB tree. Set lastOid to null and exit loop
                                            lastOid = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No response received from SNMP agent.");
                            }

                        }
                        string path = Main.currentdir + "\\" + "SNMPWALK.txt";
                        //writeTextFile(path, richTextBoxoverall.Text);
                        try
                        {
                            if (!File.Exists(path))
                            {

                                TextWriter tw = new StreamWriter(path);
                                tw.WriteLine(rtb.Text);
                                tw.Close();
                            }
                            else if (File.Exists(path))
                            {
                                File.Delete(path);
                                TextWriter tw = new StreamWriter(path);
                                tw.WriteLine(rtb.Text);
                                tw.Close();
                            }
                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show(se.Message);
                            Console.Write(se.Message);
                        }
                    }
                    catch (SnmpNetworkException se)
                    {
                        MessageBox.Show(se.Message);
                    }
                }
                else
                {
                    Console.WriteLine(Main.SipMSIPAddressSCC + " is not pingble");
                    MessageBox.Show(Main.SipMSIPAddressSCC + " is not pingble. Exception while doing SNMPWALK");
                }

                target.Close();
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
            this.Cursor = Cursors.Default;
        }

   
    

        private void button3_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Upload_to_Load_DB")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Upload_Load_DB obj = new Upload_Load_DB();
                obj.MdiParent = this.MdiParent;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            snmpgetbulk();
        }
    }
}

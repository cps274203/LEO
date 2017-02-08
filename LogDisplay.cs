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
    public partial class LogDisplay : Form
    {
       
        public LogDisplay()
        {
            InitializeComponent();
        }

        private void LogDisplay_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string s = SyslogAnalysis.logtype;
            int len = 0;
            string[] warningArray = Main.warning.ToArray();
            string[] errorArray = Main.error.ToArray();
            string[] alertArray = Main.alert.ToArray();
            string[] criticalArray = Main.critical.ToArray();
            switch (s)
            {
                case "critical":
                    len = criticalArray.Length;
                    try
                    {
                        if (len <= 500)
                        {
                            for (int i = 0; i < len; i++)
                            {
                                richTextBox1.AppendText(criticalArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                        else
                        {
                            for (int i = len - 500; i < len; i++)
                            {
                                richTextBox1.AppendText(criticalArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show(se.Message);
                    }
                    break;
                case "alert":
                    len = alertArray.Length;
                    try
                    {
                        if (len <= 500)
                        {
                            for (int i = 0; i < len; i++)
                            {
                                richTextBox1.AppendText(alertArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                        else
                        {
                            for (int i = len - 500; i < len; i++)
                            {
                                richTextBox1.AppendText(alertArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show(se.Message);
                    }
                    break;
                case "error":
                    len = errorArray.Length;
                    try
                    {
                        if (len <= 500)
                        {
                            for (int i = 0; i < len; i++)
                            {
                                richTextBox1.AppendText(errorArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                        else
                        {
                            for (int i = len - 500; i < len; i++)
                            {
                                richTextBox1.AppendText(errorArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show(se.Message);
                    }
                    break;
                case "warning":
                    len = warningArray.Length;
                    try
                    {
                        if (len <= 500)
                        {
                            for (int i = 0; i < len; i++)
                            {
                                richTextBox1.AppendText(warningArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                        else
                        {
                            for (int i = len - 500; i < len; i++)
                            {
                                richTextBox1.AppendText(warningArray[i]);
                                richTextBox1.AppendText("\n");
                            }
                        }
                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show(se.Message);
                    }
                    break;
                case "all":
                default:
                    //
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

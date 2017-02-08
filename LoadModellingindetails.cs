using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Load_Effort_Optimisation
{
    public partial class LoadModellingindetails : Form
    {
        public static int value = 0;
       
        //public static int i = 0;
        //public static int k = 0;
        //public static int l = 1;
        //modified
       
        public static int k = 1;
        public static int j = 1;

        //for datagrid2
        public static int value1 = 0;
        public static int value2 = 0;
        public static int ii = 0;
        public static int kk = 0;
        public static int ll = 1;

        public LoadModellingindetails()
        {
            InitializeComponent();
        }

        private void LoadModellingindetails_Load(object sender, EventArgs e)
        {
            //ThreadStart slipth = new ThreadStart(slip_super);
            //Thread slipthreaf = new Thread(slipth);
            //slipthreaf.Start();
            this.chart1.Visible = false;
            
            //new codec to save time:-
            //this.dataGridView1.Rows.Add();
            //this.dataGridView1.Rows[l-1].Cells[0].Value = l;
            try
            {
                for (k = 1; k < Main.sysscrmload[1].Count; k++)
                {

                    this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[k - 1].Cells[0].Value = k;
                    for (j = 1; j < 161; j++)
                    {
                        //string s=Main.sysscrmload[j][k].ToString();
                        if (Main.sysscrmload[j].Count==0)
                        {
                            this.dataGridView1.Rows[k - 1].Cells[j].Value = "";
                            this.dataGridView1.Rows[k - 1].Cells[160 + j].Value = "";
                        }
                        else
                        {
                            if ((Main.sysscrmload[j].Count == k) || (Main.sysdspload[j].Count == k))
                            {
                                MessageBox.Show("Load modeling logs are missing hence filling Datagrid with empty value");
                                k = Main.sysscrmload[1].Count;
                                break;
                            }
                            else
                            {
                                this.dataGridView1.Rows[k - 1].Cells[j].Value = Main.sysscrmload[j][k-1].ToString();
                                this.dataGridView1.Rows[k - 1].Cells[160 + j].Value = Main.sysdspload[j][k-1].ToString();
                            }
                        }
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }

        
        }

        
               



        private void dspradioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            this.chart1.Visible = false;
            bool ischecked = dspradioButton1.Checked;
            try
            {
                if (ischecked)
                {
                    //
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //enable DSP1
                    for (int h = 1; h <= 8; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 161; h <= 168; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
         
        }

        private void button1_Click_1(object sender, EventArgs e)
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

                    ExcelApp.Columns.ColumnWidth = 15;
                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {

                        ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }
                    for (int i = 1; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridView1.Columns.Count + 1; j++)
                        {
                            ExcelApp.Cells[i + 1, j] = dataGridView1.Rows[i - 1].Cells[j - 1].Value.ToString();

                        }
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

        private void dspradioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton2.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 9; h <= 16; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 169; h <= 176; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton3.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 16; h <= 24; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 176; h <= 184; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton4.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 24; h <= 32; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 184; h <= 192; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton5_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton5.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 32; h <= 40; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 192; h <= 200; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton6_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton6.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 40; h <= 48; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 200; h <= 208; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton7_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton7.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 48; h <= 56; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 208; h <= 216; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton8_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton8.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 56; h <= 64; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 216; h <= 224; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton9_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton9.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 64; h <= 72; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 224; h <= 232; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton10_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton10.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 72; h <= 80; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 232; h <= 240; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton11_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton11.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 80; h <= 88; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 240; h <= 248; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton12_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton12.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 88; h <= 96; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 248; h <= 256; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton13_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton13.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 96; h <= 104; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 256; h <= 264; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton14_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton14.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 104; h <= 112; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 264; h <= 272; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton15_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton15.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 112; h <= 120; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 272; h <= 280; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton16_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton16.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 120; h <= 128; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 280; h <= 288; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton17_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton17.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 128; h <= 136; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 288; h <= 296; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton18_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton18.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 136; h <= 144; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 288; h <= 304; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton19_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton19.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 144; h <= 152; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 304; h <= 312; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButton20_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButton20.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                    //Enable dsp2
                    for (int h = 152; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 312; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void dspradioButtonall_CheckedChanged_1(object sender, EventArgs e)
        {
            bool ischecked = dspradioButtonall.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                   
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(textBox1.Text);
                //enable all
                for (int h = 1; h <= 160; h++)
                {
                    string text = "Column" + h;
                    dataGridView1.Columns[text].Visible = true;
                }
                for (int h = 161; h <= 320; h++)
                {
                    string text = "Column" + h;
                    dataGridView1.Columns[text].Visible = true;
                }
                //Enable only selected
                for (int h = 1; h <= 160; h++)
                {
                    if (h != value)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                }
                for (int h = 161; h <= 320; h++)
                {
                    int temp = 160 + value;
                    if (temp != h)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = false;
                    }
                }
                this.chart1.Visible = true;
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
                //this.chart1.Series.Add("MM");
                //this.chart1.Series.Add("MP");
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    this.chart1.Series["MM"].Points.AddXY( i, Convert.ToDouble(dataGridView1.Rows[i].Cells[value].Value));
                    this.chart1.Series["MP"].Points.AddXY( i, Convert.ToDouble(dataGridView1.Rows[i].Cells[value + 160].Value));


                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            this.chart1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
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

                    ExcelApp.Columns.ColumnWidth = 15;
                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {

                        ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }
                    for (int i = 1; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridView1.Columns.Count + 1; j++)
                        {
                            ExcelApp.Cells[i + 1, j] = dataGridView1.Rows[i - 1].Cells[j - 1].Value.ToString();

                        }
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

        private void dspradioButtonall_CheckedChanged(object sender, EventArgs e)
        {
            bool ischecked = dspradioButtonall.Checked;
            try
            {
                if (ischecked)
                {
                    //Disable all
                    for (int h = 1; h <= 160; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }
                    for (int h = 161; h <= 320; h++)
                    {
                        string text = "Column" + h;
                        dataGridView1.Columns[text].Visible = true;
                    }

                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pngpath = Main.currentdir + "\\" + "LoadModelingforDSP"+textBox1.Text +".jpeg";
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }


        //public void slip_super()
        //{
        //    //Main.sysslip.Clear();
        //    //Main.syssuper.Clear();
        //    //Main.LostCmdsCPDSP.Clear();
        //    //Main.LostCmdsDSPCP.Clear();
        //    //Main.sysNonDecodedAudioFrames.Clear();
        //    //Main.sysJitterBufFramesDropped.Clear();
        //    //Main.sysburst.Clear();
        //    //Main.sysMaxVideoFrameDrop.Clear();
        //    //Main.sysBurstSlippedExceedLimit.Clear();
        //    //Main.sysNonDecodedVideoFrames.Clear();
        //    //Main.sysDroppedVideoPacket.Clear();
        //    //Main.sysAvgVideoFrameDrop.Clear();
        //    for (int i = 1; i <= 160; i++)
        //    {
        //        Main.sysslip[i] = new List<int>();
        //        Main.sysslip[i]= new List<int>();
        //        Main.syssuper[i]= new List<int>();
        //        Main.LostCmdsCPDSP[i]= new List<int>();
        //        Main.LostCmdsDSPCP[i]= new List<int>();
        //        Main.sysNonDecodedAudioFrames[i]= new List<int>();
        //        Main.sysJitterBufFramesDropped[i]= new List<int>();
        //        Main.sysburst[i] = new List<int>();
        //        Main.sysMaxVideoFrameDrop[i] = new List<int>();
        //        Main.sysBurstSlippedExceedLimit[i] = new List<int>();
        //        Main.sysNonDecodedVideoFrames[i] = new List<int>();
        //        Main.sysDroppedVideoPacket[i] = new List<int>();
        //        Main.sysAvgVideoFrameDrop[i] = new List<int>();
        //    }
        //    string[] logfile = Directory.GetFiles(Main.currentdir, "*.log");
        //    if (logfile.Length != 0)
        //    {
        //
        //       
        //            int i = logfile.Length;
        //            IEnumerable<string> lines = File.ReadLines(logfile[i - 1]);
        //            foreach (string line in lines)
        //            {
        //                string syslogline = line;
        //                try
        //                {
        //                  
        //
        //
        //                    if (syslogline.Contains(", Slipped:"))
        //                    {
        //                        string[] words = syslogline.Split(',');
        //                        for (int j = 1; j < 100; j++)
        //                        {
        //                            if (words[0].Contains("( " + j + ")"))
        //                            {
        //                                //Main.sysslip[j].Add(1);
        //                                //Main.sysslip[j].Add(2);
        //                                //Main.sysslip[j].Add(3);
        //                                //Main.sysslip[j].Add(4);
        //                                Main.sysslip[j].Add(Convert.ToInt32(words[2].Replace(" Slipped:", String.Empty)));
        //                                Main.syssuper[j].Add(Convert.ToInt32(words[3].Replace(" Super:", String.Empty)));
        //                                Main.LostCmdsCPDSP[j].Add(Convert.ToInt32(words[6].Replace(" LostCmds(CP->DSP):", String.Empty)));
        //                                Main.LostCmdsDSPCP[j].Add(Convert.ToInt32(words[7].Replace(" LostCmds(DSP->CP):", String.Empty)));
        //                                Main.sysNonDecodedAudioFrames[j].Add(Convert.ToInt32(words[9].Replace(" NonDecodedAudioFrames:  ", String.Empty)));
        //                                Main.sysJitterBufFramesDropped[j].Add(Convert.ToInt32(words[10].Replace(" JitterBufFramesDropped:  ", String.Empty)));
        //
        //
        //                            }
        //                            else if (words[0].Contains("(" + j + ")"))
        //                            {
        //                                Main.sysslip[j].Add(Convert.ToInt32(words[2].Replace(" Slipped:", String.Empty)));
        //                                Main.syssuper[j].Add(Convert.ToInt32(words[3].Replace(" Super:", String.Empty)));
        //                                Main.LostCmdsCPDSP[j].Add(Convert.ToInt32(words[6].Replace(" LostCmds(CP->DSP):", String.Empty)));
        //                                Main.LostCmdsDSPCP[j].Add(Convert.ToInt32(words[7].Replace(" LostCmds(DSP->CP):", String.Empty)));
        //                                Main.sysNonDecodedAudioFrames[j].Add(Convert.ToInt32(words[9].Replace(" NonDecodedAudioFrames:  ", String.Empty)));
        //                                Main.sysJitterBufFramesDropped[j].Add(Convert.ToInt32(words[10].Replace(" JitterBufFramesDropped:  ", String.Empty)));
        //
        //                            }
        //                        }
        //                        for (int j = 100; j < 161; j++)
        //                        {
        //                            if (words[0].Contains("(" + j + ")"))
        //                            {
        //
        //                                Main.sysslip[j].Add(Convert.ToInt32(words[2].Replace(" Slipped:", String.Empty)));
        //                                Main.syssuper[j].Add(Convert.ToInt32(words[3].Replace(" Super:", String.Empty)));
        //                                Main.LostCmdsCPDSP[j].Add(Convert.ToInt32(words[6].Replace(" LostCmds(CP->DSP):", String.Empty)));
        //                                Main.LostCmdsDSPCP[j].Add(Convert.ToInt32(words[7].Replace(" LostCmds(DSP->CP):", String.Empty)));
        //                                Main.sysNonDecodedAudioFrames[j].Add(Convert.ToInt32(words[9].Replace(" NonDecodedAudioFrames:  ", String.Empty)));
        //                                Main.sysJitterBufFramesDropped[j].Add(Convert.ToInt32(words[10].Replace(" JitterBufFramesDropped:  ", String.Empty)));
        //                            }
        //                        }
        //                    }
        //
        //                    if (syslogline.Contains("MaxBurstSlipped:"))
        //                    {
        //                        string[] xBurst = syslogline.Split(',');
        //                        for (int j = 1; j < 100; j++)
        //                        {
        //                            if (xBurst[0].Contains("( " + j + ")"))
        //                            {
        //                                Main.sysburst[j].Add(Convert.ToInt32(xBurst[3].Replace(" MaxBurstSlipped:", String.Empty)));
        //                                string s = xBurst[2].Replace(" MaxVideoFrameDrop:", String.Empty);
        //                                s = s.Replace("%", String.Empty);
        //                                Main.sysMaxVideoFrameDrop[j].Add(Convert.ToInt32(s));
        //                                Main.sysBurstSlippedExceedLimit[j].Add(Convert.ToInt32(xBurst[4].Replace(" BurstSlippedExceedLimit:", String.Empty)));
        //                                Main.sysNonDecodedVideoFrames[j].Add(Convert.ToInt32(xBurst[5].Replace(" NonDecodedVideoFrames:", String.Empty)));
        //                                Main.sysDroppedVideoPacket[j].Add(Convert.ToInt32(xBurst[6].Replace(" DroppedVideoPacket:", String.Empty)));
        //                                string w = xBurst[7].Replace(" AvgVideoFrameDrop:", String.Empty);
        //                                w = w.Replace("%", String.Empty);
        //                                Main.sysAvgVideoFrameDrop[j].Add(Convert.ToInt32(w));
        //
        //
        //                            }
        //                            else if (xBurst[0].Contains("(" + j + ")"))
        //                            {
        //                                Main.sysburst[j].Add(Convert.ToInt32(xBurst[3].Replace(" MaxBurstSlipped:", String.Empty)));
        //                                string s = xBurst[2].Replace(" MaxVideoFrameDrop:", String.Empty);
        //                                s = s.Replace("%", String.Empty);
        //                                Main.sysMaxVideoFrameDrop[j].Add(Convert.ToInt32(s));
        //                                Main.sysBurstSlippedExceedLimit[j].Add(Convert.ToInt32(xBurst[4].Replace(" BurstSlippedExceedLimit:", String.Empty)));
        //                                Main.sysNonDecodedVideoFrames[j].Add(Convert.ToInt32(xBurst[5].Replace(" NonDecodedVideoFrames:", String.Empty)));
        //                                Main.sysDroppedVideoPacket[j].Add(Convert.ToInt32(xBurst[6].Replace(" DroppedVideoPacket:", String.Empty)));
        //                                string w = xBurst[7].Replace(" AvgVideoFrameDrop:", String.Empty);
        //                                w = w.Replace("%", String.Empty);
        //                                Main.sysAvgVideoFrameDrop[j].Add(Convert.ToInt32(w));
        //                            }
        //                        }
        //                        for (int j = 100; j < 161; j++)
        //                        {
        //                            if (xBurst[0].Contains("(" + j + ")"))
        //                            {
        //                                Main.sysburst[j].Add(Convert.ToInt32(xBurst[3].Replace(" MaxBurstSlipped:", String.Empty)));
        //                                string s = xBurst[2].Replace(" MaxVideoFrameDrop:", String.Empty);
        //                                s = s.Replace("%", String.Empty);
        //                                Main.sysMaxVideoFrameDrop[j].Add(Convert.ToInt32(s));
        //                                Main.sysBurstSlippedExceedLimit[j].Add(Convert.ToInt32(xBurst[4].Replace(" BurstSlippedExceedLimit:", String.Empty)));
        //                                Main.sysNonDecodedVideoFrames[j].Add(Convert.ToInt32(xBurst[5].Replace(" NonDecodedVideoFrames:", String.Empty)));
        //                                Main.sysDroppedVideoPacket[j].Add(Convert.ToInt32(xBurst[6].Replace(" DroppedVideoPacket:", String.Empty)));
        //                                string w = xBurst[7].Replace(" AvgVideoFrameDrop:", String.Empty);
        //                                w = w.Replace("%", String.Empty);
        //                                Main.sysAvgVideoFrameDrop[j].Add(Convert.ToInt32(w));
        //
        //                            }
        //                        }
        //
        //
        //
        //                    }
        //                }
        //                catch (SystemException se)
        //                {
        //                    MessageBox.Show("Exception in slipcycle function",se.Message);
        //                }
        //            }
        //       
        //
        //
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {

            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            try
            {
                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "")
                    {
                        value1 = Convert.ToInt32(textBox2.Text);
                        value2 = Convert.ToInt32(textBox3.Text);
                        int v12 = value2 - value1;
                        if (v12 <= 9)
                        {
                            dataGridView2.ColumnCount = 141;
                            dataGridView2.Columns[0].Name = "Index";
                            int temp = value1;
                            int j = 1;
                            for (int a = temp; a <= value2; a++)
                            {
                                string junk = a.ToString();
                                dataGridView2.Columns[j].Name = "MM Load " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "MP Load " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "Slipped " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "Super " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "LostCmdsCPDSP " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "LostCmdsDSPCP " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "NonDecodedAudioFrames " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "JitterBufFramesDropped " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "MaxVideoFrameDrop " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "MaxBurstSlipped " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "BurstSlippedExceedLimit " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "NonDecodedVideoFrames " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "DroppedVideoPacket " + junk;
                                j++;
                                dataGridView2.Columns[j].Name = "AvgVideoFrameDrop " + junk;
                                j++;

                            }
                            //slip_super();
                            int g;
                            try
                            {
                                for (int k = 0; k < Main.sysslip[1].Count; k++)
                                {
                                    
                                    dataGridView2.Rows.Add();
                                    g = 0;
                                    this.dataGridView2.Rows[k].Cells[0].Value = k.ToString();
                                    for (int a = value1; a <= value2; a++)
                                    {

                                        g++;
                                        if (Main.sysscrmload[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysscrmload[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysdspload[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysdspload[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysslip[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysslip[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.syssuper[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.syssuper[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.LostCmdsCPDSP[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.LostCmdsCPDSP[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.LostCmdsDSPCP[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.LostCmdsDSPCP[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysNonDecodedAudioFrames[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysNonDecodedAudioFrames[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysJitterBufFramesDropped[a].Count >k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysJitterBufFramesDropped[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysburst[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysburst[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysMaxVideoFrameDrop[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysMaxVideoFrameDrop[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysBurstSlippedExceedLimit[a].Count >k )
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysBurstSlippedExceedLimit[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysNonDecodedVideoFrames[a].Count > k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysNonDecodedVideoFrames[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysDroppedVideoPacket[a].Count >k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysDroppedVideoPacket[a][k].ToString();
                                        }
                                        g++;
                                        if (Main.sysAvgVideoFrameDrop[a].Count >k)
                                        {
                                            this.dataGridView2.Rows[k].Cells[g].Value = Main.sysAvgVideoFrameDrop[a][k].ToString();
                                        }

                                    }
                                   

                                }

                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("dataGridView2 Add value failed" + se.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Either textbox difference is greater than 10 or you have entered unsupported value. please retry!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("END DSP value is Empty, Please enter to see DSP Data");
                    }
                }
                else
                {
                    MessageBox.Show("START DSP value is Empty, Please enter to see DSP Data");
                }
            }

            catch (SystemException se)
            {
                MessageBox.Show("dataGridView2 Add Columns failed" + se.Message);
            }
            //ThreadStart slipth = new ThreadStart(slip_super);
            //Thread slipthreaf = new Thread(slipth);
            //slipthreaf.Start();
        }
     
 

    }
}

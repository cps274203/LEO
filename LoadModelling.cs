using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Load_Effort_Optimisation
{
    public partial class LoadModelling : Form
    {
        //string[] audiodsp = new string[161];
        //List<string> videodsplist = new List<string>();
        //List<string> audiodsplist = new List<string>();
        //
        //string[] audioscrm = new string[161];
        //string[] videoscrm = new string[161];
        //string[] slip = new string[161];
        //string[] hdscrm = new string[161];
        //string[] super = new string[161];
        //string[] burst = new string[161];
        //int numaudiodsp = 0;
        //int numvideodsp = 0;
        //int numhddsp = 0;
        //int numofiteration = 0;
        //int avginterval = 0;
        //int numtotaldsp = 0;

        public LoadModelling()
        {
            InitializeComponent();
        }

        private void LoadModelling_Load(object sender, EventArgs e)
        {
                     datagridadd();
          

        }


  
        public void datagridadd()
        {
            try
            {
                
                for (int i = 1; i < Main.numtotaldsp + 1; i++)
                {
                    if (null != Main.audioscrm[i])
                    {
                        this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[i - 1].Cells[0].Value = i;
                        this.dataGridView1.Rows[i - 1].Cells[1].Value = Main.audioscrm[0];
                        this.dataGridView1.Rows[i - 1].Cells[2].Value = Main.audioscrm[i];
                        this.dataGridView1.Rows[i - 1].Cells[3].Value = Main.audiodsp[i];
                        this.dataGridView1.Rows[i - 1].Cells[4].Value = (Convert.ToInt32(Main.audioscrm[i]) - Convert.ToInt32(Main.audiodsp[i]));
                        
                        this.dataGridView2.Rows.Add();
                        this.dataGridView2.Rows[i - 1].Cells[0].Value = i;
                        this.dataGridView2.Rows[i - 1].Cells[1].Value = Main.audioscrm[0];
                        this.dataGridView2.Rows[i - 1].Cells[2].Value = Main.sysslip[i].Sum();
                        this.dataGridView2.Rows[i - 1].Cells[3].Value = Main.syssuper[i].Sum();
                        this.dataGridView2.Rows[i - 1].Cells[4].Value = Main.sysburst[i].Sum();
                        //this.dataGridView2.Rows[i - 1].Cells[5].Value = 0;

                    }
                    if (null != Main.videoscrm[i])
                    {
                        this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[i - 1].Cells[0].Value = i;
                        this.dataGridView1.Rows[i - 1].Cells[1].Value = Main.videoscrm[0];
                        this.dataGridView1.Rows[i - 1].Cells[2].Value = Main.videoscrm[i];
                        this.dataGridView1.Rows[i - 1].Cells[3].Value = Main.audiodsp[i];
                        this.dataGridView1.Rows[i - 1].Cells[4].Value = (Convert.ToInt32(Main.videoscrm[i]) - Convert.ToInt32(Main.audiodsp[i]));
                        this.dataGridView2.Rows.Add();
                        this.dataGridView2.Rows[i - 1].Cells[0].Value = i;
                        this.dataGridView2.Rows[i - 1].Cells[1].Value = Main.videoscrm[0];
                        this.dataGridView2.Rows[i - 1].Cells[2].Value = Main.sysslip[i].Sum();
                        this.dataGridView2.Rows[i - 1].Cells[3].Value = Main.syssuper[i].Sum();
                        this.dataGridView2.Rows[i - 1].Cells[4].Value = Main.sysburst[i].Sum();
                        //this.dataGridView2.Rows[i - 1].Cells[5].Value = 0;

                    }
                    if (null != Main.hdscrm[i])
                    {
                        this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[i - 1].Cells[0].Value = i;
                        this.dataGridView1.Rows[i - 1].Cells[1].Value = Main.hdscrm[0];
                        this.dataGridView1.Rows[i - 1].Cells[2].Value = Main.hdscrm[i];
                        this.dataGridView1.Rows[i - 1].Cells[3].Value = Main.audiodsp[i];
                        this.dataGridView1.Rows[i - 1].Cells[4].Value = (Convert.ToInt32(Main.hdscrm[i]) - Convert.ToInt32(Main.audiodsp[i]));
                        this.dataGridView2.Rows.Add();
                        this.dataGridView2.Rows[i - 1].Cells[0].Value = i;
                        this.dataGridView2.Rows[i - 1].Cells[1].Value = Main.hdscrm[0];
                        this.dataGridView2.Rows[i - 1].Cells[2].Value = Main.sysslip[i].Sum();
                        this.dataGridView2.Rows[i - 1].Cells[3].Value = Main.syssuper[i].Sum();
                        this.dataGridView2.Rows[i - 1].Cells[4].Value = Main.sysburst[i].Sum();
                        this.dataGridView1.Rows.Add();
                        if (Main.numtotaldsp > 16)
                        {
                             this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[i] .Cells[0].Value = i+1;
                            this.dataGridView1.Rows[i].Cells[1].Value = Main.hdscrm[0];
                            this.dataGridView1.Rows[i].Cells[2].Value = Main.hdscrm[i];
                            this.dataGridView1.Rows[i ].Cells[3].Value = Main.audiodsp[i];
                            this.dataGridView1.Rows[i].Cells[4].Value = (Convert.ToInt32(Main.hdscrm[i]) - Convert.ToInt32(Main.audiodsp[i]));
                            this.dataGridView2.Rows.Add();
                            this.dataGridView2.Rows[i ].Cells[0].Value = i+1;
                            this.dataGridView2.Rows[i ].Cells[1].Value = Main.hdscrm[0];
                            this.dataGridView2.Rows[i].Cells[2].Value = Main.sysslip[i].Sum();
                            this.dataGridView2.Rows[i].Cells[3].Value = Main.syssuper[i].Sum();
                            this.dataGridView2.Rows[i].Cells[4].Value = Main.sysburst[i].Sum();
                            this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[i + 1].Cells[0].Value = i+2;
                            this.dataGridView1.Rows[i + 1].Cells[1].Value = Main.hdscrm[0];
                            this.dataGridView1.Rows[i + 1].Cells[2].Value = Main.hdscrm[i];
                            this.dataGridView1.Rows[i + 1].Cells[3].Value = Main.audiodsp[i];
                            this.dataGridView1.Rows[i + 1].Cells[4].Value = (Convert.ToInt32(Main.hdscrm[i]) - Convert.ToInt32(Main.audiodsp[i]));
                            this.dataGridView2.Rows.Add();
                            this.dataGridView2.Rows[i + 1].Cells[0].Value = i+2;
                            this.dataGridView2.Rows[i + 1].Cells[1].Value = Main.hdscrm[0];
                            this.dataGridView2.Rows[i + 1].Cells[2].Value = Main.sysslip[i].Sum();
                            this.dataGridView2.Rows[i + 1].Cells[3].Value = Main.syssuper[i].Sum();
                            this.dataGridView2.Rows[i + 1].Cells[4].Value = Main.sysburst[i].Sum();
                        }
                        //this.dataGridView2.Rows[i - 1].Cells[5].Value = 0;

                    }

                }
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }



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

       
        private void button2_Click(object sender, EventArgs e)
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
                    for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                    {

                        ExcelApp.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                    }
                    for (int i = 1; i < dataGridView2.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridView2.Columns.Count + 1; j++)
                        {
                            ExcelApp.Cells[i + 1, j] = dataGridView2.Rows[i - 1].Cells[j - 1].Value.ToString();

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
    }

    
}

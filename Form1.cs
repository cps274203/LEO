using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Load_Effort_Optimisation
{
    public partial class Login : Form
    {
      
        public Login()
        {
            InitializeComponent();
        }
        public static string l1, l2;
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\db\Load_DB.accdb");
       // OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);

            
        public void login()
        {
            
            l1 = usernamechkbox.Text;
           
            try
            {
                //String qry = "SELECT * FROM Login WHERE UserName = 'admin') AND ([Password] = 'admin')";
                String qry = "Select Userid,pass from Login where Userid='" + usernamechkbox.Text + "' and pass='" + passchkbox.Text + "'";
                OleDbCommand cmd = new OleDbCommand(qry, con);
              
                cmd.Connection.Open();
              
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                   
                    cmd.Connection.Close();
                    Main obj = new Main();
                   
                    this.Hide();
                    obj.Show();

               }
                else
                {
                    MessageBox.Show("Invalid User or Password");
                    passchkbox.Text = "";
                    cmd.Connection.Close();
                }

            }
            catch (SystemException e)
            {
                MessageBox.Show(e.Message);
                //con.Close();
            }

        }
      
             

        private void resetbtn_Click(object sender, EventArgs e)
        {
            //clear the txt field
            usernamechkbox.Clear();
            passchkbox.Clear();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            //cheking of validation,,,
            login();
        

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
        
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // resetbtn.Enabled = false;
            //string user = usernamechkbox.Text;

        }

        private void usernamechkbox_TextChanged(object sender, EventArgs e)
        {
            //if (usernamechkbox.TextLength == 0)
            //{
            //  resetbtn.Enabled = true;
            //}
        }

        private void passchkbox_Enter(object sender, EventArgs e)
        {
            //this.submitbtn.Click+=new EventHandler(this.submitbtn_Click);
        }

        private void passchkbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { //} if (e.KeyValue.ToString() == "Enter")
                //{
                login();
            }
        }

     
    }
}

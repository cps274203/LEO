using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System.Threading;
using System.Data.OleDb;
using System.Configuration;



namespace Load_Effort_Optimisation
{
    public partial class Upload_Load_DB : Form
    {
        RichTextBox richTextBoxoverall = new RichTextBox();
        
        public Upload_Load_DB()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Upload_Load_DB_Load(object sender, EventArgs e)
        {
            comboBox4.Hide();
           
            comboBox6.Hide();
            textBox1.Text="";
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            textBox1.Text = userName.Replace("RADISYS\\", String.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                if (comboBox2.Text != "")
                {
                    if (comboBox1.Text != "")
                    {
                        this.Cursor = Cursors.WaitCursor;
                        statusStrip1.Text = "Changing IP. Please Wait!!!!!";
                        platformIPmapping();
                        statusStrip1.Text = "Archiving Current Directory. Please Wait!!!!!";
                        gzip();
                        statusStrip1.Text = "Uploading into LOAD DB. Please Wait!!!!!";
                        uploadtoDB();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Select Human Verdict", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (comboBox4.Text != "")
                    {
                        if (comboBox1.Text != "")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            statusStrip1.Text = "Changing IP. Please Wait!!!!!";
                            platformIPmapping();
                            statusStrip1.Text = "Archiving Current Directory. Please Wait!!!!!";
                            gzip();
                            statusStrip1.Text = "Uploading into LOAD DB. Please Wait!!!!!";
                            uploadtoDB();
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            MessageBox.Show("Select Human Verdict", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Select Project", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            else
            {
                MessageBox.Show("Enter User Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
        }

        //public void submit()
        //{
        //    try
        //    {
        //
        //        string comments = "comment.txt";
        //        string ex1 = Main.currentdir + "\\" + "submit.pl";
        //        string hostip = "10.1.60.104";
        //        string projectname = comboBox2.Text;
        //        string verdict = "";
        //        if (comboBox1.Text == "Pass")
        //        {
        //            verdict = "p";
        //        }
        //        if (comboBox1.Text == "Fail")
        //        {
        //            verdict = "f";
        //        }
        //        if (comboBox1.Text == "Invalid")
        //        {
        //            verdict = "i";
        //        }
        //        string space = " ";
        //        string[] atfile = Directory.GetFiles(Main.currentdir, "*.dat");
        //        string[] log = Directory.GetFiles(Main.currentdir, "*.rar");
        //        string augument = ex1 + space + hostip + space + projectname + space + verdict + space + atfile[0] + space + log[0] + space + comments + space + textBox1.Text;
        //        ProcessStartInfo perlStart = new ProcessStartInfo("perl");
        //        //perlStart.FileName = "C:\\Perl64\\bin\\perl.exe";
        //        perlStart.Arguments = augument;
        //            //.Replace("\\",@"\");
        //        perlStart.UseShellExecute = false;
        //        perlStart.CreateNoWindow = true;
        //        perlStart.RedirectStandardOutput = true;
        //        perlStart.WindowStyle = ProcessWindowStyle.Hidden;
        //        Process perl = new Process();
        //        perl.StartInfo = perlStart;
        //        perl.Start();
        //   
        //        //perl.BeginOutputReadLine();
        //        perl.WaitForExit(999);
        //        if (!perl.HasExited)
        //        {
        //            perl.Kill();
        //            MessageBox.Show("Time Out Retry!!!!");
        //
        //        }
        //        else
        //        {
        //            string output = perl.StandardOutput.ReadToEnd();
        //            MessageBox.Show(output);
        //        }
        //    }
        //    catch (SystemException se)
        //    {
        //        MessageBox.Show(se.Message);
        //    }
        //       
        //}
        //

        public void platformIPmapping()
        {
            string ip="";
            if (comboBox3.Visible == false)
            {
                try
                {
                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\bcnas02\RAD\RAD\DraftDocs\DV\Users\Chandra\LEO_DB\Load_DB.accdb");
                    string qry = "SELECT IP FROM Platform_IP_mapping WHERE (Platform_name = '" + comboBox6.Text + "')";
                    Console.WriteLine("Qry=" + qry);
                    OleDbCommand cmd = new OleDbCommand(qry, con);
                    cmd.CommandText = qry;
                    cmd.Connection.Open();
                    ip = cmd.ExecuteScalar().ToString();
                    cmd.Connection.Close();
                    Console.WriteLine("Got IP="+ip);
                    MessageBox.Show("Changing IP to"+ip);
                }
                catch (SystemException se)
                {
                    MessageBox.Show("platformIPmapping() exception" + se.Message);
                }

            }
            else
            {
                if (comboBox3.Text != "")
                {
                    if (comboBox3.Text == "Dell R610")
                    {
                        ip = "10.211.1.01";
                    }
                    if (comboBox3.Text == "Dell R620")
                    {
                        ip = "10.211.1.11";
                    }
                    if (comboBox3.Text == "Dell R710")
                    {
                        ip = "10.211.1.21";
                    }
                    if (comboBox3.Text == "Kontron TIGH2U")
                    {
                        ip = "10.211.1.31";
                    }
                    if (comboBox3.Text == "Kontron SB")
                    {
                        ip = "10.211.1.36";
                    }
                    if (comboBox3.Text == "ComE")
                    {
                        ip = "10.211.1.41";
                    }
                    if (comboBox3.Text == "MPX50")
                    {
                        ip = "10.211.1.46";
                    }
                    if (comboBox3.Text == "RMS220")
                    {
                        ip = "10.211.1.51";
                    }

                    if (comboBox3.Text == "RHKVM-HP G8")
                    {
                        ip = "10.211.5.31";
                    }
                    if (comboBox3.Text == "RHKVM-CISCO UCS")
                    {
                        ip = "10.211.5.51";
                    }
                    if (comboBox3.Text == "RHKVM-Dell 620")
                    {
                        ip = "10.211.5.41";
                    }
                    if (comboBox3.Text == "HP DL360 G7")
                    {
                        ip = "10.211.5.11";
                    }
                    if (comboBox3.Text == "RHKVM-Kontron")
                    {
                        ip = "10.211.0.1";
                    }
                    if (comboBox3.Text == "KVM")
                    {
                        ip = "10.211.5.61";
                    }

                    if (comboBox3.Text == "VMware-CISCO UCS")
                    {
                        ip = "10.211.1.60";
                    }
                    if (comboBox3.Text == "VMware-HP G8")
                    {
                        ip = "10.211.1.90";
                    }
                    if (comboBox3.Text == "VMware-Dell 620")
                    {
                        ip = "10.211.1.80";
                    }
                    if (comboBox3.Text == "CentOS")
                    {
                        ip = "10.211.1.100";
                    }
                    if (comboBox3.Text == "CISCO UCS")
                    {
                        ip = "10.211.2.69";
                    }
                    if (comboBox3.Text == "DellR630")
                    {
                        ip = "10.211.1.120";
                    }
                    if (comboBox3.Text == "VMware-Dell 630")
                    {
                        ip = "10.211.1.130";
                    }
                    if (comboBox3.Text == "RHKVM-Dell 630")
                    {
                        ip = "10.211.1.140";
                    }
                    if (comboBox3.Text == "HP DL460 G8")
                    {
                        ip = "10.211.1.150";
                    }
                    if (comboBox3.Text == "VMware-HP DL460 G8")
                    {
                        ip = "10.211.1.160";
                    }
                    if (comboBox3.Text == "DellR720")
                    {
                        ip = "10.211.1.170";
                    }
                    if (comboBox3.Text == "VMware-Dell 720")
                    {
                        ip = "10.211.1.180";
                    }
                    if (comboBox3.Text == "HP DL360 G8")
                    {
                        ip = "10.211.5.20";
                    }
                    if (comboBox3.Text == "DELL620-OEL5.7")
                    {
                        ip = "10.211.1.70";
                    }
                    if (comboBox3.Text == "Amazon c3.2x large")
                    {
                        ip = "10.211.1.110";
                    }
                }
            }
            if (ip != "")
            {
                try
                {
                    string[] atfile = Directory.GetFiles(Main.currentdir, "at_output*");
                    string[] text = File.ReadAllLines(atfile[0]);
                    string temp = "";
                    if (Main.TestToolProtocol == "SIP")
                    {
                        //string text = File.ReadAllText(atfile[0]);
                        temp = "SipMSIPAddress=" + ip;
                        for (int i = 0; i < text.Length - 1; i++)
                        {
                            if (text[i].Contains("SipMSIPAddress="))
                            {
                                var writer = new StreamWriter(atfile[0]);
                                text[i] = temp;
                                for (int j = 0; j < text.Length - 1; j++)
                                {
                                    writer.WriteLine(text[j]);

                                }
                                writer.Close();
                                //File.WriteAllText("test.txt", text);
                            }
                          
                        }
                    }
                    else
                    {
                        temp = "MSName : " + ip;
                        for (int i = 0; i < text.Length - 1; i++)
                        {
                            if (text[i].Contains("MSName :"))
                            {
                                var writer = new StreamWriter(atfile[0]);
                                text[i] = temp;
                                //MessageBox.Show(temp);
                                for (int j = 0; j < text.Length - 1; j++)
                                {
                                    writer.WriteLine(text[j]);

                                }
                                writer.Close();
                                //File.WriteAllText("test.txt", text);
                            }
                        }
                    }
                }
                catch (SystemException se)
                {
                    MessageBox.Show("Exception at function platformIPmapping()", se.Message);
                }
                
            }
        }

        public void uploadtoDB()
        {
            try
            {
                string boundary = "--" + DateTime.Now.Ticks.ToString("x");
                 byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                 byte[] boundarybytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                 string newline = "\n";
                 byte[] newlinebyte = System.Text.Encoding.ASCII.GetBytes(newline);
                string user = textBox1.Text;
                string prject = comboBox2.Text;
                if (comboBox4.Text != "")
                {
                    prject = comboBox4.Text;
                }
                
                // string verdict = comboBox2.Text;
                string hostip = "flibust.radisys.com";
                string url = "http://" + hostip + "/testcase_db/submitAndLoadOneResult.php?submitter=" + user + "&db=" + prject + "&cmdline=true";
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary="+boundary;
               
               // Content-Type: multipart/form-data; boundary=xYzZY

                ((HttpWebRequest)request).UserAgent = "LEO tool";
               // ((HttpWebRequest)request).TransferEncoding = "deflate,gzip;q=0.3";
               // ((HttpWebRequest)request).Connection = "keep-alive";
                ((HttpWebRequest)request).KeepAlive = true;
                //((HttpWebRequest)request).Accept="text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //request.Timeout
                string[] atfile = Directory.GetFiles(Main.currentdir, "at_output*");
                string logfile = Main.currentdir + "\\" + "archive.tar.gz";
                string verdict = comboBox1.Text;
                string commet = Main.currentdir + "\\" + "comment.txt";
         

                string[] fat = File.ReadAllLines(atfile[0]);
                FileStream archive = File.OpenRead(logfile);
                
                FileStream comments = File.OpenRead(commet);
                byte[] buffer = new byte[1024];
                Stream streamall;
                //For AT upload//
//                Content-Disposition: form-data; name="userfile[]"; filename="at_output.SipTransCodingModel_2014.03.05_11%3A08%3A43.dat"

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = string.Format(headerTemplate, "userfile[]", atfile[0], "text/plain");
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                streamall = request.GetRequestStream();
                streamall.Write(boundarybytesF, 0, boundarybytesF.Length);
                streamall.Write(headerbytes, 0, headerbytes.Length);
                foreach (string line in fat)
                {
                    
                    Byte[] data = Encoding.UTF8.GetBytes(line);
                    streamall.Write(data, 0, data.Length);
                    streamall.Write(newlinebyte, 0, newlinebyte.Length);
                } 

                //Archiev upload
                streamall.Write(boundarybytes, 0, boundarybytes.Length);
                string headerarchive = string.Format(headerTemplate, "userfile[]", "archive.tar.gz", "application/x-tar");
                byte[] headerbytesarchive = System.Text.Encoding.UTF8.GetBytes(headerarchive);
                streamall.Write(headerbytesarchive, 0, headerbytesarchive.Length);
                int bytearchiveread = 0;
                do
                {
                    bytearchiveread = archive.Read(buffer, 0, 1024);
                     streamall.Write(buffer, 0, bytearchiveread);
                } while (bytearchiveread != 0);

                
                //For verdict
                streamall.Write(boundarybytes, 0, boundarybytes.Length);
                string verdictcomment = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n";
                string verdictheader = string.Format(verdictcomment, "result");
                byte[] verdictbytes = System.Text.Encoding.UTF8.GetBytes(verdictheader);
                streamall.Write(verdictbytes, 0, verdictbytes.Length);
                string postData = verdict;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                streamall.Write(byteArray, 0, byteArray.Length);

                //For comment upload
                streamall.Write(boundarybytes, 0, boundarybytes.Length);
                string commentheader = string.Format(verdictcomment, "comment");
                byte[] commentbytes = System.Text.Encoding.UTF8.GetBytes(commentheader);
                streamall.Write(commentbytes, 0, commentbytes.Length);

                int bytecommentread = 0;
                do
                {
                    bytecommentread = comments.Read(buffer, 0, 1024);
                    streamall.Write(buffer, 0, bytecommentread);
                } while (bytecommentread != 0);

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                streamall.Write(trailer, 0, trailer.Length);
              
                // Close the Stream object.
                //dataStream.Close();
                // Get the response.
                System.Threading.Thread.Sleep(3000);
                WebResponse response = request.GetResponse();
                // Display the status.
               // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                streamall = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(streamall);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                MessageBox.Show(responseFromServer, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if(responseFromServer.Contains("test case submitted successfully"))
                {
                    MessageBox.Show("Load Test submitted successfully!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    statusStrip1.Text = "Load Test submitted successfully!!!";
                }
                else
                {
                    MessageBox.Show("Load Test submission failed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Please rety after 1 min", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    statusStrip1.Text = "Load Test submission failed!! Please rety after 1 min!!";
                }

                // Clean up the streams.
                reader.Close();
                streamall.Close();
                response.Close();
            }
            catch (SystemException se)
            {
                MessageBox.Show("Load Test upload exception", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Please rety after 5 min" + se.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("Please Retry!!!!"+se.Message);
            }
        }

        public void gzip()
        {
            
            string startPath = Main.currentdir;
            string zipPath = Main.currentdir + "\\" + "archive.tar.gz";

            CreateTarGZ(zipPath,startPath);
          

        }
        
        private void CreateTarGZ(string tgzFilename, string sourceDirectory)
        {
            try
            {
                Stream outStream = File.Create(tgzFilename);
                Stream gzoStream = new GZipOutputStream(outStream);
                TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzoStream);

                // Note that the RootPath is currently case sensitive and must be forward slashes e.g. "c:/temp"
                // and must not end with a slash, otherwise cuts off first char of filename
                // This is scheduled for fix in next release
                tarArchive.RootPath = sourceDirectory.Replace('\\', '/');
                if (tarArchive.RootPath.EndsWith("/"))
                    tarArchive.RootPath = tarArchive.RootPath.Remove(tarArchive.RootPath.Length - 1);

                AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);

                tarArchive.Close();
            }
            catch (SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }
        private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
        {
            try
            {
                // Optionally, write an entry for the directory itself.
                // Specify false for recursion here if we will add the directory's files individually.
                //
                TarEntry tarEntry = TarEntry.CreateEntryFromFile(sourceDirectory);
                tarArchive.WriteEntry(tarEntry, false);

                // Write each file to the tar.
                //
                string[] filenames = Directory.GetFiles(sourceDirectory);
                //removing archive.tar.gz from array

                foreach (string filename in filenames)
                {
                    if (filename.Contains("archive.tar.gz") || filename.Contains("Load_Effort_Optimisation.exe") || filename.Contains("ICSharpCode.SharpZipLib.dll") || filename.Contains("SnmpSharpNet.dll"))
                    {
                        //noaction
                    }
                    else
                    {
                        tarEntry = TarEntry.CreateEntryFromFile(filename);
                        tarArchive.WriteEntry(tarEntry, true);
                    }
                }

                if (recurse)
                {
                    string[] directories = Directory.GetDirectories(sourceDirectory);
                    foreach (string directory in directories)
                        AddDirectoryFilesToTar(tarArchive, directory, recurse);
                }
            }
            catch(SystemException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex =-1;
            try
            {
                
                //comboBox1.Hide();
                comboBox2.Hide();
                comboBox2.Visible=false;
                comboBox4.Show();
                comboBox4.Visible = true;
                // TODO: This line of code loads data into the 'load_DBDataSet.Project_list' table. You can move, or remove it, as needed.
                this.project_listTableAdapter.Fill(this.load_DBDataSet.Project_list);
            }
            catch (SystemException se)
            {
                MessageBox.Show("Loading Project_name failed"+se.Message);
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = -1;
             try
            {
                comboBox3.Hide();
                comboBox3.Visible = false;
                comboBox6.Show();
                comboBox6.Visible = true;
            // TODO: This line of code loads data into the 'load_DBDataSet.Platform_IP_mapping' table. You can move, or remove it, as needed.
            this.platform_IP_mappingTableAdapter.Fill(this.load_DBDataSet.Platform_IP_mapping);
            }
             catch (SystemException se)
             {
                 MessageBox.Show("Loading platform_IP_mapping failed" + se.Message);

             }
        }
        
    }
}

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
using System.Threading;
using System.Net;
using System.Diagnostics;
using SnmpSharpNet;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace Load_Effort_Optimisation
{
    public partial class Main : Form
    {
        //global path=current dir
        public static string currentdir = Path.GetDirectoryName(Application.ExecutablePath);
        //For memeory leak
        public static List<string> used = new List<string>();
        public static List<string> buffers = new List<string>();
        public static List<string> cache = new List<string>();
        public static List<string> pagetables = new List<string>();
        public static List<string> slab = new List<string>();
        public static List<string> vmallocused = new List<string>();
        public static List<string> vsz = new List<string>();
        public static List<string> rss = new List<string>();
        //Log related declation
        public static List<string> warning = new List<string>();
        public static List<string> error = new List<string>();
        public static List<string> alert = new List<string>();
        //public static List<string> emergency = new List<string>();
        public static List<string> critical = new List<string>();

        //Distict log related declaration
        public static List<string> warningdist = new List<string>();
        public static List<string> errordist = new List<string>();
        public static List<string> alertdist = new List<string>();
        //public static List<string> emergency = new List<string>();
        public static List<string> criticaldist = new List<string>();

        //const char space = ' ';

        //string topfilepath = currentdir + "\\" + "top.txt";
        static string usedbufferstring = "Mem:";
        static string cachestring = "Swap:";
        static string pagetablestring = "PageTables:";
        static string slabstring = "Slab:";
        static string vmallocusedstring = "VmallocUsed:";
        StringBuilder sb = new StringBuilder();
        //For Report
        public static string TestToolProtocol = "";
        public static string totatrunningtime = "";
        public static int NumberOfConferences = 0;
        public static int NumberOfConnections = 0;
        public static String[] RTPGstats = new String[5];
        public static int[] atstats = new int[6];
        //For SNMP query
        public static string mpcslot = "";
        public static string mscardtype = "";
        public static string SipMSIPAddressSCC = "";

        //Overall Memory Leakage
        public static int dif = 0;
        public static string topinterval = "";

        //For slip cyle
        public static string[] audiodsp = new string[161];
        public static List<string> videodsplist = new List<string>();
        public static List<string> audiodsplist = new List<string>();
        //For load modeling
        public static string[] audioscrm = new string[161];
        public static string[] videoscrm = new string[161];
        public static string[] hdscrm = new string[161];
        public static string[] slip = new string[161];
        public static string[] super = new string[161];
        public static string[] burst = new string[161];

        public static List<int>[] sysscrmload = new List<int>[161];
        public static List<int>[] sysdspload = new List<int>[161];
        public static List<int>[] sysslip = new List<int>[161];
        public static List<int>[] syssuper = new List<int>[161];
        public static List<int>[] sysburst = new List<int>[161];
        public static List<int>[] LostCmdsCPDSP = new List<int>[161];
        public static List<int>[] LostCmdsDSPCP = new List<int>[161];
        public static List<int>[] sysNonDecodedAudioFrames = new List<int>[161];
        public static List<int>[] sysJitterBufFramesDropped = new List<int>[161];
        public static List<int>[] sysMaxVideoFrameDrop = new List<int>[161];
        public static List<int>[] sysMaxBurstSlipped = new List<int>[161];
        public static List<int>[] sysBurstSlippedExceedLimit = new List<int>[161];
        public static List<int>[] sysNonDecodedVideoFrames = new List<int>[161];
        public static List<int>[] sysDroppedVideoPacket = new List<int>[161];
        public static List<int>[] sysAvgVideoFrameDrop = new List<int>[161];
       

        public static int numaudiodsp = 0;
        public static int numvideodsp = 0;
        public static int numhddsp = 0;
        //SNMP
        public static string numaudiodspsnmp = "";
        public static string numvideodspsnmp = "";
        public static string numhddspsnmp = "";
        public static string numtotaldspsnmp = "";
        //
        public static int numofiteration = 0;
        public static int avginterval = 0;
        public static int numtotaldsp = 0;
        //toolStripStatusLabel1.Text = "";
        public static string LoadModellingtext = "";
        public static string avgcaltxt = "";
        //For Statistics
        public static string rtppacketlosttotal = "unknown";
        public static string rtppackettxttotal = "unknown";
        public static string rtppacketrxtotal = "unknown";
        public static string dspstable = "unknown";
        public static string crashfound = "no";
        public static List<string> AvgDSPUtilization = new List<string>();
        public static int AvgDSPUtilizationlen = 0;
        public static List<string> frameratedrop = new List<string>();
        public static string Slipcycle = "";
        public static int syslogcount = 0;
        public static string totaltime = "";
        //Add few more for pluto2
        public static List<int> superslipaudio = new List<int>();
        public static List<int> burstslipaudio = new List<int>();
        public static List<int> superslipvideo = new List<int>();
        public static List<int> burstslipvideo = new List<int>();
        public static List<int> burstslipHC = new List<int>();
        public static string currentportactive = "unknown";
        public static int statisticsinterval = 0;
        public static List<int> NonDecodedVideoFrames = new List<int>();
        public static List<int> NonDecodedAudioFrames = new List<int>();
        public static List<int> JitterBufferFramesDropped = new List<int>();
        public static List<int> DroppedVideoPackets = new List<int>();
        public static List<int> MaxFramesDroppedHCVideo = new List<int>();
        public static List<int> MaxVideoEncoderFrameRateDropRate = new List<int>();
        // Memory variable declaration
        //CP
        public static int cpdifvsz = 0;
        public static int cpdifrss = 0;
        //SE
        public static int sedifvsz = 0;
        public static int sedifrss = 0;
        //OAMP
        public static int oampdifvsz = 0;
        public static int oampdifrss = 0;
        //VXML
        public static int vxmldifvsz = 0;
        public static int vxmldifrss = 0;
        //DSP
        //MpcMp1
        public static int MpcMp1vsz = 0;
        public static int MpcMp1rss = 0;
        //MpcMp2
        public static int MpcMp2vsz = 0;
        public static int MpcMp2rss = 0;
        //3
        public static int MpcMp3vsz = 0;
        public static int MpcMp3rss = 0;
        //4
        public static int MpcMp4vsz = 0;
        public static int MpcMp4rss = 0;
        //5
        public static int MpcMp5vsz = 0;
        public static int MpcMp5rss = 0;
        //6
        public static int MpcMp6vsz = 0;
        public static int MpcMp6rss = 0;
        //7
        public static int MpcMp7vsz = 0;
        public static int MpcMp7rss = 0;
        //8
        public static int MpcMp8vsz = 0;
        public static int MpcMp8rss = 0;
        //9
        public static int MpcMp9vsz = 0;
        public static int MpcMp9rss = 0;
        //10
        public static int MpcMp10vsz = 0;
        public static int MpcMp10rss = 0;
        //11
        public static int MpcMp11vsz = 0;
        public static int MpcMp11rss = 0;
        //12
        public static int MpcMp12vsz = 0;
        public static int MpcMp12rss = 0;

        public Main()
        {
            InitializeComponent();


        }


        private void Main_Load(object sender, EventArgs e)
        {

            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            load();

            stopWatch.Stop();
            totaltime = stopWatch.Elapsed.ToString();
            MessageBox.Show("Total Analysis time taken=" + totaltime, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void MDI(string StatusbarText)
        {
            toolStripStatusLabel1.Text = StatusbarText;
        }
        public void load()
        {


            this.Cursor = Cursors.WaitCursor;
            string topfilepath = currentdir + "\\" + "top.txt";
            if (File.Exists(topfilepath))
            {
                toolStripStatusLabel1.Text = "Calculating Memory leak";
                overallmemoryleak();

                ThreadJobbuffer();

                ThreadJobcache();

                ThreadJobpagetables();

                ThreadJobslab();

                ThreadJobvmallocused();

                overallmemleakdif();
                cpoverallmemoryleakdif();
                dspoverallmemoryleakdif();
                oampoverallmemoryleakdif();
                seoverallmemoryleakdif();
                vxmloverallmemoryleakdif();
            }
            else
            {
                MessageBox.Show("top.txt is not available at current location!! Click ok for syslog anyalysis!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string[] logfile = Directory.GetFiles(currentdir, "*.log");
            List<Thread> threads = new List<Thread>();
            if (logfile.Length != 0)
            {
                toolStripStatusLabel1.Text = "Syslog Analysis Now!!!";



                //Thread 1
                //ThreadStart syslogthred = new ThreadStart(syslog);
                //Thread syslogd = new Thread(syslogthred);
                //syslogd.Name = "syslog Thread2";
                //syslogd.Start();
                //threads.Add(syslogd);
                //Console.WriteLine("Adding and starting syslog thread to MainThread");

               
                //Thread 2
                ThreadStart l = new ThreadStart(loadmodeling);
                Thread lth = new Thread(l);
                lth.Name = "loadmodeling Thread1";
                lth.Start();
                threads.Add(lth);
                Console.WriteLine("Adding and starting loadmodeling thread");
                //Thread 3
                ThreadStart SAT_HAT = new ThreadStart(AT_OUTPUT);
                Thread sat = new Thread(SAT_HAT);
                sat.Name = "AT_OUTPUT Thread3";
                sat.Start();
                threads.Add(sat);
                Console.WriteLine("Adding and starting AT_OUTPUT thread to MainThread");
                //Thread 4
                ThreadStart stat = new ThreadStart(statistic_analysis);
                Thread stath = new Thread(stat);
                stath.Name = "statistic_analysis Thread4";
                stath.Start();
                threads.Add(stath);
                Console.WriteLine("Adding and starting statistic_analysis thread to MainThread");
                //Thread 5
                ThreadStart snmpcoreget = new ThreadStart(snmpget);
                Thread snmpcore = new Thread(snmpcoreget);
                snmpcore.Name = "snmpget Thread5";
                snmpcore.Start();
                threads.Add(snmpcore);
                Console.WriteLine("Adding and starting snmpget thread to MainThread");

                //Thread 6
                //ThreadStart dsputil = new ThreadStart(dsputilizatio);
                //Thread dsputilth = new Thread(dsputil);
                //dsputilth.Start();
                //threads.Add(dsputilth);
                //Thread 7
                ThreadStart slipth = new ThreadStart(slip_super);
                Thread slipthreaf = new Thread(slipth);
                slipthreaf.Name = "slip_super Thread5";
                slipthreaf.Start();
                threads.Add(slipthreaf);
                toolStripStatusLabel1.Text = "Load Modeling Analysis Now!!!!";
                Console.WriteLine("Joining All Thread");
                Console.Write("Total number of Threads = ");
                Console.WriteLine(threads.Count.ToString());
       

                if (threads != null)
                {
                    foreach (Thread thread in threads)
                    { thread.Join(); }
                }
                foreach (var thread in threads)
                    thread.Abort();

               
            }
            else
            {
                MessageBox.Show("*.log is not available at current location!! Click ok for Statistics anyalysis!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Console.WriteLine("Calling function syslog()");
            syslog();
            Console.WriteLine("syslog() function finished");
            Console.WriteLine("Calling function dsputilizatio()");
            dsputilizatio();
            Console.WriteLine("dsputilizatio() function finished");
            Console.WriteLine("Calling function finalavg()");
            finalavg();
            Console.WriteLine("finalavg() function finished");
            Console.WriteLine("Calling slip_super_burst_percore() function");
            slip_super_burst_percore();
            Console.WriteLine("slip_super_burst_percore() function finished");
            Console.WriteLine("Calling function Overall_Report()");
            Overall_Report();
            Console.WriteLine("Overall_Report() function finished");
            
            this.Cursor = Cursors.Default;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //startScan();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void overallmemoryleak()
        {


            string topfilepath = currentdir + "\\" + "top.txt";

            //check for top output availebe
            try
            {
                used.Clear();
                cache.Clear();
                buffers.Clear();
                pagetables.Clear();
                slab.Clear();
                vmallocused.Clear();
                string topinvervallocal1="";
                string topinvervallocal2="";

                IEnumerable<string> lines = File.ReadLines(topfilepath);

                foreach (string line in lines)
                {

                    string usedbuffer = line;
                    if (topinterval == "")
                    {
                        if (line.Contains("top -"))
                        {
                            try
                            {
                                string[] words = line.Split(' ');
                                if (topinvervallocal1 == "")
                                {
                                    topinvervallocal1 = words[2];
                                }
                                else
                                {
                                    topinvervallocal2 = words[2];
                                    DateTime d1 = new DateTime();
                                    DateTime d2 = new DateTime();
                                    d1 = Convert.ToDateTime(topinvervallocal1);
                                    d2 = Convert.ToDateTime(topinvervallocal2);
                                    topinterval = d2.Subtract(d1).ToString();
                                    //MessageBox.Show(topinterval);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("topinterval calculation exception");
                            }

                        }
                    }
                    //if (usedbuffer.Contains(usedbufferstring))
                    if (usedbuffer.Contains(usedbufferstring) && usedbuffer.Contains("buffers"))
                    {
                        try
                        {
                            string[] words = usedbuffer.Split(' ');
                            //

                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                //removing spaces
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());

                                }
                            }

                            if (txt[3].Contains('k'))
                            {
                                used.Add(txt[3].ToString().Replace("k", String.Empty));
                            }
                            if (txt[3].Contains('M'))
                            {
                                Double j = Convert.ToDouble(txt[3].Replace("M", String.Empty));
                                Double k = j * 1024;
                                used.Add(k.ToString());
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Used memory reading exception");
                            break;
                        }

                    }
                    
                }



            }
            catch
            {
                MessageBox.Show(" Not able to read Top output");

            }


        }
        private static void ThreadJobbuffer()
        {
            string topfilepath = currentdir + "\\" + "top.txt";
            IEnumerable<string> lines = File.ReadLines(topfilepath);
            foreach (string line in lines)
            {
                string usedbuffer = line;
                if (usedbuffer.Contains(usedbufferstring) && usedbuffer.Contains("buffers"))
                {
                    try
                    {
                        string[] words = usedbuffer.Split(' ');

                        List<string> txt = new List<string>();
                        foreach (string word in words)
                        {
                            if (word.Equals(""))
                            {
                                //no action
                            }
                            else
                            {
                                txt.Add(word.ToString());
                                //MessageBox.Show(word.ToString());
                            }
                        }

                        if (txt[7].Contains('k'))
                        {

                            buffers.Add(txt[7].Replace("k", String.Empty));

                        }
                        if (txt[7].Contains('M'))
                        {
                            Double j = Convert.ToDouble(txt[7].Replace("M", String.Empty));
                            Double k = j * 1024;
                            buffers.Add(k.ToString());

                        }


                    }
                    catch
                    {

                        MessageBox.Show("Buffer memory reading exception");
                        break;
                    }

                }
            }
        }
        private static void ThreadJobcache()
        {
            string topfilepath = currentdir + "\\" + "top.txt";
            IEnumerable<string> lines = File.ReadLines(topfilepath);

            foreach (string line in lines)
            {
                string cacheline = line;
                if (cacheline.Contains(cachestring) && cacheline.Contains("cached"))
                {
                    try
                    {
                        string[] words = cacheline.Split(' ');

                        List<string> txt = new List<string>();
                        foreach (string word in words)
                        {
                            if (word.Equals(""))
                            {
                                //no action
                            }
                            else
                            {
                                txt.Add(word.ToString());
                                //MessageBox.Show(word.ToString());
                            }
                        }

                        if (txt[7].Contains('k'))
                        {
                            cache.Add(txt[7].Replace("k", String.Empty));
                        }
                        if (txt[7].Contains('M'))
                        {
                            Double j = Convert.ToDouble(txt[7].Replace("M", String.Empty));
                            Double k = j * 1024;
                            cache.Add(k.ToString());

                        }



                    }
                    catch
                    {
                        MessageBox.Show("Cache Memory reading exception");
                        break;
                    }


                }
            }
        }

        private static void ThreadJobpagetables()
        {
            string topfilepath = currentdir + "\\" + "top.txt";
            IEnumerable<string> lines = File.ReadLines(topfilepath);

            foreach (string line in lines)
            {
                string cacheline = line;
                if (cacheline.Contains(pagetablestring))
                {
                    try
                    {
                        string[] words = cacheline.Split(' ');

                        List<string> txt = new List<string>();
                        foreach (string word in words)
                        {
                            if (word.Equals(""))
                            {
                                //no action
                            }
                            else
                            {
                                txt.Add(word.ToString());
                                //MessageBox.Show(word.ToString());
                            }
                        }

                        pagetables.Add(txt[1]);

                    }
                    catch
                    {
                        MessageBox.Show("Page table reading exception");
                        break;
                    }


                }
            }
        }

        private static void ThreadJobslab()
        {
            string topfilepath = currentdir + "\\" + "top.txt";
            IEnumerable<string> lines = File.ReadLines(topfilepath);

            foreach (string line in lines)
            {
                string cacheline = line;
                if (cacheline.Contains(slabstring))
                {
                    try
                    {
                        string[] words = cacheline.Split(' ');
                        List<string> txt = new List<string>();
                        foreach (string word in words)
                        {
                            if (word.Equals(""))
                            {
                                //no action
                            }
                            else
                            {
                                txt.Add(word.ToString());
                                //MessageBox.Show(word.ToString());
                            }
                        }

                        slab.Add(txt[1]);

                    }
                    catch
                    {
                        MessageBox.Show("Slab reading exception");
                        break;
                    }


                }
            }
        }

        private static void ThreadJobvmallocused()
        {
            string topfilepath = currentdir + "\\" + "top.txt";
            IEnumerable<string> lines = File.ReadLines(topfilepath);

            foreach (string line in lines)
            {
                string cacheline = line;
                if (cacheline.Contains(vmallocusedstring))
                {
                    try
                    {
                        string[] words = cacheline.Split(' ');

                        List<string> txt = new List<string>();
                        foreach (string word in words)
                        {
                            if (word.Equals(""))
                            {
                                //no action
                            }
                            else
                            {
                                txt.Add(word.ToString());
                                //MessageBox.Show(word.ToString());
                            }
                        }

                        vmallocused.Add(txt[1]);

                    }
                    catch
                    {
                        MessageBox.Show("Vmallocused exception");
                        break;
                    }


                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void memoryUsageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void overallGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //overallmemoryleak();
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Memory Usage Graph")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Memory Usage Graph";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Chart obj = new Chart();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                toolStripStatusLabel1.Text = "Memory Usage Graph";
                obj.Show();

                obj.Width = this.Width;
                obj.Height = this.Height;
            }

        }

        private void overallReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Overall_Report();
        }
        public void Overall_Report()
        {

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Overall_report")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Overall Report";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Overall_report obj = new Overall_report();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                toolStripStatusLabel1.Text = "Overall Report";
                obj.Show();

                obj.Width = this.Width;
                obj.Height = this.Height;
            }

        }
        public void AT_OUTPUT()
        {
            
            RTPGstats[0] = "Packets sent=unknown";
            RTPGstats[1] = "Packets received=unknown";
            RTPGstats[2] = "Packets lost=unknown";
            RTPGstats[3] = "Packets dropped=unknown";
            RTPGstats[4] = "Model Name = Unknown";
            atstats[0] = 0;
            atstats[1] = 0;
            atstats[2] = 0;
            atstats[3] = 0;
            atstats[4] = 0;
            atstats[5] = 0;
            //string[] atfile = Directory.GetFiles(currentdir, "*.dat");
            string[] atfile = Directory.GetFiles(currentdir, "*at_output*");

            if (atfile.Length != 0)
            {
                if (atfile.Length >= 2)
                {
                    MessageBox.Show("Multiple AT_output file is present in currecnt dir", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Chosing \t" + atfile[0].Replace(currentdir, String.Empty), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                IEnumerable<string> liness = File.ReadLines(atfile[0]);
                foreach (string line in liness)
                {
                    if (line.Contains("_TestToolProtocol=SIP"))
                    {
                        //case sip
                        TestToolProtocol = "SIP";
                        break;
                    }
                    if (line.Contains("_TestToolProtocol : H248"))
                    {
                        //case Megaco
                        TestToolProtocol = "H248";
                        break;
                    }
                }
                //case of SAT
                if (TestToolProtocol == "SIP")
                {
                    IEnumerable<string> lines = File.ReadLines(atfile[0]);
                    //condition for SAT/HAT
                    int sendcheck = 0;
                    int recvcheck = 0;
                    int losscheck = 0;
                    int dropcheck = 0;
                    foreach (string line in lines)
                    {
                        string durationline = line;
                        if (durationline.Contains("Total Running time for this model in seconds ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');
                                string tmp = words[1].Replace(".*", String.Empty);
                                totatrunningtime = (Convert.ToSingle(tmp)/ 3600).ToString();

                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for duration",se.Message);
                            }


                        }

                        if (durationline.Contains("Calls Created ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                atstats[0] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Calls Created", se.Message);
                            }

                        }

                        if (durationline.Contains("Calls Deleted ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                atstats[1] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Calls Deleted", se.Message);
                            }

                        }


                        if (durationline.Contains("Failed Scenarios ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                atstats[2] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Failed Scenarios", se.Message);
                            }

                        }

                        if (durationline.Contains("Number of Conferences Created ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                atstats[3] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Number of Conferences Created", se.Message);
                            }

                        }

                        if (durationline.Contains("Number of Conferences Deleted ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                atstats[4] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Number of Conferences Deleted", se.Message);
                            }

                        }

                        if (durationline.Contains("Conference Failures ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                atstats[5] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Conference Failures", se.Message);
                            }

                        }
                        if (durationline.Contains("Model Name ="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                RTPGstats[4] = words[1];


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for Model Name", se.Message);
                            }

                        }

                        if (durationline.Contains("NumberOfConnections="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');

                                NumberOfConnections = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for NumberOfConnections", se.Message);
                            }

                        }
                        if (durationline.Contains("NumberOfConferences="))
                        {
                            try
                            {
                                string[] words = durationline.Split('=');
                                // string tmp = words[1].Replace(".*", String.Empty);
                                NumberOfConferences = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for NumberOfConferences", se.Message);
                            }

                        }
                        if (line.Contains("Packets sent") && sendcheck == 0)
                        {
                            sendcheck = 1;
                            RTPGstats[0] = line.ToString();
                        }
                        if (line.Contains("Packets received") && recvcheck == 0)
                        {
                            recvcheck = 1;
                            RTPGstats[1] = line.ToString();
                        }
                        if (line.Contains("Packets lost") && losscheck == 0)
                        {
                            losscheck = 1;
                            RTPGstats[2] = line.ToString();
                        }
                        if (line.Contains("Packets dropped") && dropcheck == 0)
                        {
                            dropcheck = 1;
                            RTPGstats[3] = line.ToString();
                        }
                        string atline = line;
                        //getting the IP address
                        if (atline.Contains("SipMSIPAddressSCC="))
                        {
                            try
                            {
                                string[] words = atline.Split('=');
                                SipMSIPAddressSCC = words[1];
                                Console.WriteLine("SipMSIPAddressSCC={0}", SipMSIPAddressSCC);
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for SipMSIPAddressSCC", se.Message);
                            }
                        }
                        //getting the slot
                        if (atline.Contains("_Mpc_Card_1_Slot="))
                        {
                            try
                            {
                                string[] words = atline.Split('=');
                                mpcslot = words[1];
                                Console.WriteLine("mpcslot={0}", mpcslot);
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for _Mpc_Card_1_Slot", se.Message);
                            }
                        }
                        //getting CardType
                        if (atline.Contains("_RetrievedFromMS_MPC1_CardType="))
                        {
                            try
                            {
                                string[] words = atline.Split('=');
                                mscardtype = words[1];
                                Console.WriteLine("mscardtype={0}", mscardtype);
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for _RetrievedFromMS_MPC1_CardType", se.Message);
                            }
                        }



                    }
                }
                //case of HAT
                if (TestToolProtocol == "H248")
                {

                    IEnumerable<string> lines = File.ReadLines(atfile[0]);
                    int sendcheck = 0;
                    int recvcheck = 0;
                    int losscheck = 0;
                    int dropcheck = 0;
                    int confcheck = 0;
                    //int annccheck = 0;
                    foreach (string line in lines)
                    {
                        string durationline = line;
                        if (durationline.Contains("Total Running Time"))
                        {
                            
                            try
                            {
                                string[] words = durationline.Split('=');
                                totatrunningtime = words[1];

                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Total Running time", se.Message);
                            }


                        }

                        if (durationline.Contains("Transaction Added"))
                        {
                            try
                            {
                                string[] words = durationline.Split(' ');
                                atstats[0] = Convert.ToInt32(words[2]);
                                atstats[1] = Convert.ToInt32(words[4]);
                                atstats[2] = Convert.ToInt32(words[6]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Transaction Added", se.Message);
                            }

                        }
                        if (durationline.Contains("Conference Statistics"))
                        {
                            confcheck = 1;
                        }

                        if (durationline.Contains("Created:")&& confcheck==1)
                        {
                            try
                            {
                                string[] words = durationline.Split(':');

                                atstats[3] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Conference Statistics Created", se.Message);
                            }

                        }

                        if (durationline.Contains("Deleted:") && confcheck == 1)
                        {
                            try
                            {
                                string[] words = durationline.Split(':');

                                atstats[4] = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Conference Statistics Deleted", se.Message);
                            }

                        }

                        //if (durationline.Contains("Active:") && confcheck == 1)
                        //{
                        //    try
                        //    {
                        //        string[] words = durationline.Split('=');
                        //
                        //        atstats[5] = Convert.ToInt32(words[1]);
                        //
                        //
                        //    }
                        //    catch (SystemException se)
                        //    {
                        //        MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Conference Statistics Active", se.Message);
                        //    }
                        //
                        //}
                        if (durationline.Contains("Model:"))
                        {
                            try
                            {
                                string[] words = durationline.Split(' ');

                                RTPGstats[4] = words[1];


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Modelname", se.Message);
                            }

                        }

                        if (durationline.Contains("Connections :"))
                        {
                            try
                            {
                                string[] words = durationline.Split(':');

                                NumberOfConnections = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Calls Connections count", se.Message);
                            }

                        }
                        if (durationline.Contains("Conferences :"))
                        {
                            try
                            {
                                string[] words = durationline.Split(':');
                                // string tmp = words[1].Replace(".*", String.Empty);
                                NumberOfConferences = Convert.ToInt32(words[1]);


                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 Calls Conferences count", se.Message);
                            }

                        }
                        if (line.Contains("Min/Max Octets Sent:") && sendcheck == 0)
                        {
                            sendcheck = 1;
                            RTPGstats[0] = line.ToString();
                        }
                        if (line.Contains("Min/Max Octets Received: ") && recvcheck == 0)
                        {
                            recvcheck = 1;
                            RTPGstats[1] = line.ToString();
                        }
                        if (line.Contains("Min/Max Packets Sent:") && losscheck == 0)
                        {
                            losscheck = 1;
                            RTPGstats[2] = line.ToString();
                        }
                        if (line.Contains("Min/Max Packets Received:") && dropcheck == 0)
                        {
                            dropcheck = 1;
                            RTPGstats[3] = line.ToString();
                        }

                        string atline = line;
                        //getting the IP address
                        if (atline.Contains("MSName :"))
                        {
                            try
                            {
                                string[] words = atline.Split(':');
                                SipMSIPAddressSCC = words[1].Replace(" ",String.Empty);
                                Console.WriteLine("MSName :={0}", SipMSIPAddressSCC);
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 MSName IP", se.Message);
                            }
                        }
                        //getting the slot
                        if (atline.Contains("_RetrievedFromMS_MSPlatform"))
                        {
                            try
                            {
                                string[] words = atline.Split(':');
                                if (words[1].Contains("SWMS"))
                                {
                                    mpcslot = Convert.ToString(2);
                                    Console.WriteLine("mpcslot={0}", mpcslot);
                                }
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for h.248 _RetrievedFromMS_MSPlatform", se.Message);
                            }
                        }
                        //getting CardType
                        if (atline.Contains("_RetrievedFromMS_MPCCardType"))
                        {
                            try
                            {
                                string[] words = atline.Split(':');
                                mscardtype = words[1];
                                Console.WriteLine("mscardtype={0}", mscardtype);
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in function AT_OUTPUT() for _RetrievedFromMS_MPCCardType", se.Message);
                            }
                        }

                    }


                }

            }
            else
            {
                MessageBox.Show("SAS/HAT output is not available at current locatoin");
            }

        }

        private void cPUsageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cpoverallmemoryleak();

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "CPChart")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "CP/SCRM Memory Usage Graph";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                CPChart obj = new CPChart();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                toolStripStatusLabel1.Text = "CP/SCRM Memory Usage Graph";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }
        public static void cpoverallmemoryleak()
        {

            vsz.Clear();
            rss.Clear();

            string checkprocess = "";
            string topfilepath = Main.currentdir + "\\" + "top.txt";
            if (File.Exists(topfilepath))
            {
                IEnumerable<string> lines = File.ReadLines(topfilepath);

                foreach (string line in lines)
                {
                    //i++;
                    string usedvsz = line;
                    if (usedvsz.Contains("/dmf/bin/mpccp.out"))
                    {
                        checkprocess = "OK";
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }


                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }

                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at cpoverallmemoryleak() MRF",se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                    if (usedvsz.Contains("/opt/swms/bin/mpccp.out"))
                    {
                        checkprocess = "OK";
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }


                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }

                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at cpoverallmemoryleak() SWMS", se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                }
                if (checkprocess == "")
                {
                    MessageBox.Show("mpccp.out process is not found in top.txt");
                }
            }
            else
            {
                MessageBox.Show("top.txt file is missing in current directory");
            }

        }

        public static void dspoverallmemoryleak(int i)
        {
            try
            {
                vsz.Clear();
                rss.Clear();


                string topfilepath = Main.currentdir + "\\" + "top.txt";
                if (File.Exists(topfilepath))
                {
                    IEnumerable<string> lines = File.ReadLines(topfilepath);

                    foreach (string line in lines)
                    {
                        //i++;
                        string usedvsz = line;
                        if (usedvsz.Contains("/dmf/bin/MpcMp " + i.ToString()))
                        {
                            try
                            {
                                // MessageBox.Show("used");
                                string[] words = usedvsz.Split(' ');
                                List<string> txt = new List<string>();
                                foreach (string word in words)
                                {
                                    if (word.Equals(""))
                                    {
                                        //no action
                                    }
                                    else
                                    {
                                        txt.Add(word.ToString());
                                        //MessageBox.Show(word.ToString());
                                    }
                                }


                                vsz.Add(txt[4].ToString());
                                rss.Add(txt[5].ToString());

                            }

                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in dspoverallmemoryleak() MRF", se.Message);
                            }

                            //MessageBox.Show(usedbuffer.ToString());
                        }
                        if (usedvsz.Contains("/opt/swms/bin/MpcMp " + i.ToString()))
                        {
                            try
                            {
                                // MessageBox.Show("used");
                                string[] words = usedvsz.Split(' ');
                                List<string> txt = new List<string>();
                                foreach (string word in words)
                                {
                                    if (word.Equals(""))
                                    {
                                        //no action
                                    }
                                    else
                                    {
                                        txt.Add(word.ToString());
                                        //MessageBox.Show(word.ToString());
                                    }
                                }


                                vsz.Add(txt[4].ToString());
                                rss.Add(txt[5].ToString());

                            }

                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception in dspoverallmemoryleak() SWMS", se.Message);
                            }

                            //MessageBox.Show(usedbuffer.ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("top.txt file is missing in current directory");
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception in dspoverallmemoryleak() Main",se.Message);
            }

        }

        private void sEUsageGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seoverallmemoryleak();

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "SEChart")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "SE Memory Usage Graph";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                SEChart obj = new SEChart();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                toolStripStatusLabel1.Text = "SE Memory Usage Graph";
                obj.Show();

                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        public static void seoverallmemoryleak()
        {

            vsz.Clear();
            rss.Clear();

           string topfilepath = Main.currentdir + "\\" + "top.txt";
            if (File.Exists(topfilepath))
            {
                IEnumerable<string> lines = File.ReadLines(topfilepath);

                foreach (string line in lines)
                {
                    //i++;
                    string usedvsz = line;
                    if (usedvsz.Contains("/dmf/bin/CmnSe.out"))
                    {
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }

                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at seoverallmemoryleak() MRF",se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                    if (usedvsz.Contains("/opt/swms/bin/CmnSe.out"))
                    {
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }

                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at seoverallmemoryleak() SWMS", se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                }
            }

            else
            {
                MessageBox.Show("top.txt file is missing in current directory");
            }


        }

        private void oAMPUsageGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oampoverallmemoryleak();
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "OAMPChart")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "OAMP Memory Usage Graph";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                OAMP obj = new OAMP();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                toolStripStatusLabel1.Text = "OAMP Memory Usage Graph";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        public static void oampoverallmemoryleak()
        {

            vsz.Clear();
            rss.Clear();

            //ThreadStart jobrss = new ThreadStart(ThreadJobrss);
            //Thread threadrss = new Thread(jobrss);
            // threadrss.Start();


            string topfilepath = Main.currentdir + "\\" + "top.txt";
            if (File.Exists(topfilepath))
            {

                IEnumerable<string> lines = File.ReadLines(topfilepath);

                foreach (string line in lines)
                {
                    //i++;
                    string usedvsz = line;
                    if (usedvsz.Contains("/dmf/bin/mpcoamp"))
                    {
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }


                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at oampoverallmemoryleak() MRF", se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                    if (usedvsz.Contains("/opt/swms/bin/mpcoamp"))
                    {
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }


                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at oampoverallmemoryleak() SWMS", se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                }
            }

            else
            {
                MessageBox.Show("top.txt file is missing in current directory");
            }


        }

        private void vXMLUsageGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vxmloverallmemoryleak();
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "VXMLChart")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "VXI Memory Usage Graph";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                VXMLChart obj = new VXMLChart();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                toolStripStatusLabel1.Text = "VXI Memory Usage Graph";
                obj.Show();

                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }
        public static void vxmloverallmemoryleak()
        {

            vsz.Clear();
            rss.Clear();

            //ThreadStart jobrss = new ThreadStart(ThreadJobrss);
            //Thread threadrss = new Thread(jobrss);
            // threadrss.Start();


            string topfilepath = Main.currentdir + "\\" + "top.txt";
            if (File.Exists(topfilepath))
            {

                IEnumerable<string> lines = File.ReadLines(topfilepath);

                foreach (string line in lines)
                {
                    //i++;
                    string usedvsz = line;
                    if (usedvsz.Contains("/dmf/bin/cmsvxi"))
                    {
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }


                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at vxmloverallmemoryleak() MRF", se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                    if (usedvsz.Contains("/opt/swms/bin/cmsvxi"))
                    {
                        try
                        {
                            // MessageBox.Show("used");
                            string[] words = usedvsz.Split(' ');
                            List<string> txt = new List<string>();
                            foreach (string word in words)
                            {
                                if (word.Equals(""))
                                {
                                    //no action
                                }
                                else
                                {
                                    txt.Add(word.ToString());
                                    //MessageBox.Show(word.ToString());
                                }
                            }


                            vsz.Add(txt[4].ToString());
                            rss.Add(txt[5].ToString());

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at vxmloverallmemoryleak() SWMS", se.Message);
                        }

                        //MessageBox.Show(usedbuffer.ToString());
                    }
                }
            }

            else
            {
                MessageBox.Show("top.txt file is missing in current directory");
            }

        }

        private void loadModellingAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModeling")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Load Modeling Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                if (LoadModellingtext != null && avgcaltxt != "OK")
                {
                    finalavg();
                }
                LoadModelling obj = new LoadModelling();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                toolStripStatusLabel1.Text = "Load Modeling Analysis";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }

        }

        private void errorWarningAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "SyslogAnalysis")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Syslog Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                SyslogAnalysis obj = new SyslogAnalysis();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                toolStripStatusLabel1.Text = "Syslog Analysis";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        public void syslog()
        {
            error.Clear();
            warning.Clear();
            alert.Clear();
            critical.Clear();
            errordist.Clear();
            warningdist.Clear();
            alertdist.Clear();
            criticaldist.Clear();
            string[] logfile = Directory.GetFiles(currentdir, "*.log");

            if (logfile.Length != 0)
            {
                for (int i = 0; i < logfile.Length; i++)
                {
                    IEnumerable<string> lines = File.ReadLines(logfile[i]);

                    foreach (string line in lines)
                    {

                        syslogcount++;
                        string syslogline = line;
                        if (syslogline.Contains("C:reg.c"))
                        {
                            string[] words = syslogline.Split('>');
                            crashfound = words[1];
                        }
                        if (syslogline.Contains("app_exception"))
                        {
                            string[] words = syslogline.Split('>');
                            crashfound = words[1];
                        }
                        if (syslogline.Contains(": E:"))
                        {
                            try
                            {
                                if (error.Count < 50000)
                                {

                                    error.Add(syslogline);
                                    string[] words = syslogline.Split('>');
                                    if (words.Length > 2)
                                    {
                                        string cat = "";
                                        for (i = 1; i < words.Length; i++)
                                        {
                                            cat = cat + words[i];
                                        }
                                        errordist.Add(cat);
                                    }
                                    else
                                    {
                                        errordist.Add(words[1]);
                                    }
                                }
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Length of errorlist" + error.Count + se.Message);
                                break;
                            }

                        }
                        if (syslogline.Contains(": A:"))
                        {
                            try
                            {
                                if (alert.Count < 100000)
                                {
                                    alert.Add(syslogline);
                                    string[] words = syslogline.Split('>');
                                    if (words.Length > 2)
                                    {
                                        string cat = "";
                                        for (i = 1; i < words.Length; i++)
                                        {
                                            cat = cat + words[i];
                                        }
                                        alertdist.Add(cat);
                                    }
                                    else
                                    {
                                        alertdist.Add(words[1]);

                                    }
                                }
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Length of alertist" + alert.Count + se.Message);
                                break;
                            }

                        }
                        if (syslogline.Contains(": C:"))
                        {
                            try
                            {
                                if (critical.Count < 100000)
                                {
                                    critical.Add(syslogline);
                                    string[] words = syslogline.Split('>');
                                    if (words.Length > 2)
                                    {
                                        string cat = "";
                                        for (i = 1; i < words.Length; i++)
                                        {
                                            cat = cat + words[i];
                                        }
                                        criticaldist.Add(cat);
                                    }
                                    else
                                    {
                                        criticaldist.Add(words[1]);
                                    }
                                }
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Length of criticaltist" + critical.Count + se.Message);
                                break;
                            }

                        }
                        if (syslogline.Contains(": W:"))
                        {
                            try
                            {
                                if (warning.Count < 50000)
                                {
                                    warning.Add(syslogline);
                                    string[] words = syslogline.Split('>');
                                    if (words.Length > 2)
                                    {
                                        string cat = "";
                                        for (i = 1; i < words.Length; i++)
                                        {
                                            cat = cat + words[i];
                                        }
                                        warningdist.Add(cat);
                                    }
                                    else
                                    {

                                        warningdist.Add(words[1]);
                                    }
                                }
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Length of warningtist" + warning.Count + se.Message);
                                break;
                            }

                        }

                    }
                }
                var w = warningdist.Distinct().ToArray();
                warningdist.Clear();
                for (int i = 0; i < w.Length; i++)
                {
                    warningdist.Add(w[i]);
                }

                var a = alertdist.Distinct().ToArray();
                alertdist.Clear();
                for (int i = 0; i < a.Length; i++)
                {
                    alertdist.Add(a[i]);
                }
                var e = errordist.Distinct().ToArray();
                errordist.Clear();
                for (int i = 0; i < e.Length; i++)
                {
                    errordist.Add(e[i]);
                }
                var c = criticaldist.Distinct().ToArray();
                criticaldist.Clear();
                for (int i = 0; i < c.Length; i++)
                {
                    criticaldist.Add(c[i]);
                }
            }
            else
            {
                MessageBox.Show("No Syslog *.log file found in current directory");
            }


        }

        private void aTAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void audioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModeling")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Load Modeling Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                LoadModelling obj = new LoadModelling();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TabControl tb = new TabControl();
                tb.TabIndex = 1;
                toolStripStatusLabel1.Text = "Load Modeling Analysis";
                obj.Show();

                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModeling")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Load Modeling Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                LoadModelling obj = new LoadModelling();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TabControl tb = new TabControl();
                tb.TabIndex = 1;
                obj.Show();
                toolStripStatusLabel1.Text = "Load Modeling Analysis";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }

        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sumbitLoadResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Upload_to_Load_DB")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Upload to LOAD DB";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Upload_Load_DB obj = new Upload_Load_DB();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                toolStripStatusLabel1.Text = "Upload to LOAD DB";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        public void overallmemleakdif()
        {

            var usedarray = Main.used.ToArray();
            var cachearry = Main.cache.ToArray();
            var bufferarray = Main.buffers.ToArray();
            var pagetablearry = Main.pagetables.ToArray();
            var slabarray = Main.slab.ToArray();
            var vmallocusedarray = Main.vmallocused.ToArray();
            List<string> final = new List<string>();
            int check = 1;
            final.Clear();
            //

            for (int i = 0; i < usedarray.Length-1; i++)
            {
                try
                {
                    if (usedarray[i].Contains(".") || bufferarray[i].Contains(".") || cachearry[i].Contains("."))
                    {
                        double value = Convert.ToDouble(usedarray[i]) - Convert.ToDouble(bufferarray[i]) - Convert.ToDouble(cachearry[i]) - Convert.ToDouble(pagetablearry[i]) - Convert.ToDouble(slabarray[i]) - Convert.ToDouble(vmallocusedarray[i]);
                        final.Add(value.ToString());
                        check = 0;

                    }
                    else
                    {
                        int value = Convert.ToInt32(usedarray[i]) - Convert.ToInt32(bufferarray[i]) - Convert.ToInt32(cachearry[i]) - Convert.ToInt32(pagetablearry[i]) - Convert.ToInt32(slabarray[i]) - Convert.ToInt32(vmallocusedarray[i]);
                        final.Add(value.ToString());
                        var finalarry = final.ToArray();

                    }
                }
                catch (SystemException se)
                {
                    MessageBox.Show("Overall memory leak calculation exception" + se.Message);
                    Console.WriteLine("Overall memory leak calculation exception" + se.Message);
                    break;
                }

            }
            if (check == 0)
            {
                try
                {
                    var finalarry = final.ToArray();
                    int len = finalarry.Length;
                    int a = len / 5;
                    int b = Convert.ToInt32(Math.Round(Convert.ToDouble(a), 0));
                    int k = b;
                    double dif1 = (Convert.ToDouble(finalarry[b]) - Convert.ToDouble(finalarry[0]));
                    b = b + k;
                    double dif2 = (Convert.ToDouble(finalarry[b]) - Convert.ToDouble(finalarry[0]));
                    b = b + k;
                    double dif3 = (Convert.ToDouble(finalarry[b]) - Convert.ToDouble(finalarry[0]));
                    b = b + k;
                    double dif4 = (Convert.ToDouble(finalarry[b]) - Convert.ToDouble(finalarry[0]));
                    double dif5 = (Convert.ToDouble(finalarry[len - 1]) - Convert.ToDouble(finalarry[0]));
                    //double dif12 = dif1 - dif2;
                    //double dif23 = dif2 - dif3;//Main
                    //double dif34 = dif3 - dif4;//Main
                    //double dif45 = dif4 - dif5;
                    //dif = Convert.ToInt32(dif34 - dif23);
                    dif = Convert.ToInt32(dif4 - dif1);
                }
                catch (SystemException se)
                {
                    MessageBox.Show("Exception at overallmemleakdif() while calculating dif case 1",se.Message);
                    Console.WriteLine("Exception at overallmemleakdif() while calculating dif case 1", se.Message);
                }

            }
            else
            {
                try
                {

                    var finalarry = final.ToArray();
                    int len = finalarry.Length;
                    int a = len / 5;
                    int b = Convert.ToInt32(Math.Round(Convert.ToDouble(a), 0));
                    int k = b;
                    double dif1 = (Convert.ToInt32(finalarry[b]) - Convert.ToInt32(finalarry[0]));
                    b = b + k; ;
                    double dif2 = (Convert.ToInt32(finalarry[b]) - Convert.ToInt32(finalarry[0]));
                    b = b + k; ;
                    double dif3 = (Convert.ToInt32(finalarry[b]) - Convert.ToInt32(finalarry[0]));
                    b = b + k; ;
                    double dif4 = (Convert.ToInt32(finalarry[b]) - Convert.ToInt32(finalarry[0]));
                    double dif5 = (Convert.ToInt32(finalarry[len - 1]) - Convert.ToInt32(finalarry[0]));
                    //double dif12 = dif1 - dif2;
                    //double dif23 = dif2 - dif3;//Main
                    //double dif34 = dif3 - dif4;//Main
                    //double dif45 = dif4 - dif5;
                    //dif = Convert.ToInt32(dif34 - dif23);
                    dif = Convert.ToInt32(dif4 - dif1);
                }
                catch (SystemException se)
                {
                    MessageBox.Show("Exception at overallmemleakdif() while calculating dif case 2", se.Message);
                    Console.WriteLine("Exception at overallmemleakdif() while calculating dif case 2", se.Message);
                }
            }

        }

        public void cpoverallmemoryleakdif()
        {
            try
            {
                cpoverallmemoryleak();
                string[] seriesvszArray = vsz.ToArray();
                string[] seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    cpdifvsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    cpdifrss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("cpoverallmemoryleakdif()",se.Message);
                Console.WriteLine("cpoverallmemoryleakdif()"+se.Message);
            }


        }
        public void dspoverallmemoryleakdif()
        {
            try
            {
                //1
                dspoverallmemoryleak(1);
                string[] seriesvszArray = vsz.ToArray();
                string[] seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp1vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp1rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //2
                dspoverallmemoryleak(2);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp2vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp2rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //3
                dspoverallmemoryleak(3);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp3vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp3rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //4
                dspoverallmemoryleak(4);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp4vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp4rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //5
                dspoverallmemoryleak(5);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp5vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp5rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //6
                dspoverallmemoryleak(6);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp6vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp6rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //7
                dspoverallmemoryleak(7);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp7vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp7rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //8
                dspoverallmemoryleak(8);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp8vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp8rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //9
                dspoverallmemoryleak(9);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp9vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp9rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //10
                dspoverallmemoryleak(10);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp10vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp10rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //11
                dspoverallmemoryleak(11);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp11vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp11rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
                //12
                dspoverallmemoryleak(12);
                seriesvszArray = vsz.ToArray();
                seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    MpcMp12vsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    MpcMp12rss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("dspoverallmemoryleak()",se.Message);
                Console.WriteLine("dspoverallmemoryleak()" + se.Message);
            }

        }

        public void oampoverallmemoryleakdif()
        {
            try
            {
                oampoverallmemoryleak();
                string[] seriesvszArray = vsz.ToArray();
                string[] seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    oampdifvsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    oampdifrss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
            }
            catch (SystemException se)
            {
                
                MessageBox.Show("oampoverallmemoryleakdif()", se.Message);
                Console.WriteLine("oampoverallmemoryleakdif()" + se.Message);
            }
        }

        public void seoverallmemoryleakdif()
        {
            try
            {
                seoverallmemoryleak();
                string[] seriesvszArray = vsz.ToArray();
                string[] seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    sedifvsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    sedifrss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("seoverallmemoryleakdif()", se.Message);
                Console.WriteLine("seoverallmemoryleakdif()" + se.Message);
              
            }
        }
        public void vxmloverallmemoryleakdif()
        {
            try
            {
                vxmloverallmemoryleak();
                string[] seriesvszArray = vsz.ToArray();
                string[] seriesrssArray = rss.ToArray();
                if (seriesvszArray.Length != 0)
                {
                    vxmldifvsz = (Convert.ToInt32(seriesvszArray[seriesvszArray.Length - 1]) - Convert.ToInt32(seriesvszArray[0]));
                    vxmldifrss = (Convert.ToInt32(seriesrssArray[seriesrssArray.Length - 1]) - Convert.ToInt32(seriesrssArray[0]));
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("vxmloverallmemoryleakdif()", se.Message);
                Console.WriteLine("vxmloverallmemoryleakdif()" + se.Message);
                
            }
        }


        private void videoToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void slipCycleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModeling")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Load Modeling Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                LoadModelling obj = new LoadModelling();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TabControl tb = new TabControl();
                tb.TabIndex = 1;
                obj.Show();
                toolStripStatusLabel1.Text = "Load Modeling Analysis";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        //For slip cyle

        public void loadmodeling()
        {

            //string syslogline = "";
            Console.WriteLine("Starting loadmodeling");
            audioscrm[0] = "Audio";
            videoscrm[0] = "Video";
            hdscrm[0] = "HC";
            string[] logfile = Directory.GetFiles(Main.currentdir, "*.log");

            try
            {
                if (logfile.Length != 0)
                {
                    if (logfile.Length == 1)
                    {
                        //Thread mynewThread = new Thread(() => loadmodelingthread(logfile[0]));
                        loadmodelingthread(logfile[0]);
                        Console.WriteLine("Calling function() loadmodelingthread");
                        //Thread 2
                        ThreadStart syslogthred = new ThreadStart(syslog);
                        Thread syslogd = new Thread(syslogthred);
                        syslogd.Name = "syslog Thread2";
                        syslogd.Start();
                       
                        //mynewThread.Name = "loadmodelingthread Parent=loadmodeling";
                        //mynewThread.Start();
                    }
                    else
                    {
                        for (int i = 0; i <= logfile.Length - 1; i++)
                        {
                            Console.WriteLine("Calling function() loadmodelingthread");
                            loadmodelingthread(logfile[i]);
                            ThreadStart syslogthred = new ThreadStart(syslog);
                            Thread syslogd = new Thread(syslogthred);
                            syslogd.Name = "syslog Thread2";
                            syslogd.Start();

                            //Thread mynewThread = new Thread(() => loadmodelingthread(logfile[i]));
                            //mynewThread.Start();
                            //Thread.Sleep(3000);

                        }
                    }



                }
                else
                {
                    MessageBox.Show("No syslog found in current dir!! Load modeling can not calculated ");
                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("Loadmodeling class Exception" + se.Message);

            }

            Console.WriteLine("Finished loadmodeling");


        }

        public void loadmodelingthread(string logfile)
        {
            Console.WriteLine("Start loadmodelingthread");
            string syslogline = "";
            try
            {
                //added for more load modeling details
                for (int iiii = 1; iiii <= 160; iiii++)
                {
                    Main.sysdspload[iiii] = new List<int>();
                    Main.sysscrmload[iiii] = new List<int>();
                    //Main.sysscrmload[j].Add(value);
                }

                IEnumerable<string> lines = File.ReadLines(logfile);
                
                foreach (string line in lines)
                {
                    syslogline = line;
                 
                    //Audio Load
                    if (syslogline.Contains("Audio Load:"))
                    {

                        string[] words = syslogline.Split('>');
                        for (int j = 1; j < 100; j++)
                        {
                            if (words[1].Contains("( " + j + ")"))
                            {
                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                                double value = Convert.ToDouble(audioscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                audioscrm[j] = final.ToString();
                                //added for more load modeling details
                                Main.sysscrmload[j].Add(Convert.ToInt32(wordSearched[4]));
                                //numaudiodsp = j;
                            }

                            else if (words[1].Contains("(" + j + ")"))
                            {


                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                                double value = Convert.ToDouble(audioscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[3]), 2);
                                audioscrm[j] = final.ToString();
                                //added for more load modeling details
                                Main.sysscrmload[j].Add(Convert.ToInt32(wordSearched[3]));
                                //numaudiodsp = j;
                            }
                        }
                        for (int j = 100; j < 161; j++)
                        {
                            if (words[1].Contains("(" + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                                double value = Convert.ToDouble(audioscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[3]), 2);
                                audioscrm[j] = final.ToString();
                                //added for more load modeling details
                                Main.sysscrmload[j].Add(Convert.ToInt32(wordSearched[3]));
                                //numaudiodsp = j;
                            }
                        }
                    }



                    // Video Load
                    if (syslogline.Contains("Video Load:") && !syslogline.Contains("HC"))
                    {

                        if (!syslogline.Contains("HD"))
                        {
                            string[] words = syslogline.Split('>');
                            for (int j = 1; j < 100; j++)
                            {
                                if (words[1].Contains("( " + j + ")"))
                                {

                                    char[] charSeparators = new char[] { ' ' };
                                    string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                    double value = Convert.ToDouble(videoscrm[j]);
                                    if (wordSearched[4].Contains("("))
                                    {
                                        string s = wordSearched[3].Replace("Load:", String.Empty);
                                        double final = value + Math.Pow(Convert.ToDouble(s.Replace(",", String.Empty).ToString()),2);
                                        videoscrm[j] = final.ToString();
                                        //added for more load modeling details
                                        int kl = Convert.ToInt32(s.Replace(",", String.Empty));
                                        Main.sysscrmload[j].Add(kl);
                                    }
                                    else
                                    {
                                        double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                        videoscrm[j] = final.ToString();
                                        //added for more load modeling details
                                        int kl = Convert.ToInt32(wordSearched[4]);
                                        Main.sysscrmload[j].Add(kl);
                                    }
                                    //numvideodsp = j;
                                }

                                else if (words[1].Contains("(" + j + ")"))
                                {
                                    char[] charSeparators = new char[] { ' ' };
                                    string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                    double value = Convert.ToDouble(videoscrm[j]);
                                    if (wordSearched[4].Contains("("))
                                    {
                                        string s = wordSearched[3].Replace("Load:", String.Empty);
                                        double final = value + Math.Pow(Convert.ToDouble(s.Replace(",", String.Empty).ToString()), 2);
                                        videoscrm[j] = final.ToString();
                                        //added for more load modeling details
                                        int kl = Convert.ToInt32(s.Replace(",", String.Empty));
                                        Main.sysscrmload[j].Add(kl);
                                    }
                                    else
                                    {
                                        double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                        videoscrm[j] = final.ToString();
                                        int kl = Convert.ToInt32(wordSearched[4]);
                                        Main.sysscrmload[j].Add(kl);
                                    }
                                    //numvideodsp = j;
                                }
                            }
                            for (int j = 100; j < 161; j++)
                            {
                                if (words[1].Contains("(" + j + ")"))
                                {

                                    char[] charSeparators = new char[] { ' ' };
                                    string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                    double value = Convert.ToDouble(videoscrm[j]);
                                    if (wordSearched[3].Contains("("))
                                    {
                                        string s = wordSearched[2].Replace("Load:", String.Empty);
                                        double final = value + Math.Pow(Convert.ToDouble(s.Replace(",", String.Empty).ToString()), 2);
                                        videoscrm[j] = final.ToString();
                                        //added for more load modeling details
                                        int kl = Convert.ToInt32(s.Replace(",", String.Empty));
                                        Main.sysscrmload[j].Add(kl);
                                    }
                                    else
                                    {
                                        double final = value + Math.Pow(Convert.ToDouble(wordSearched[3]), 2);
                                        videoscrm[j] = final.ToString();
                                        //added for more load modeling details
                                        int kl = Convert.ToInt32(wordSearched[3]);
                                        Main.sysscrmload[j].Add(kl);
                                    }
                                    //numvideodsp = j;
                                }
                            }
                        }

                    }
                    //Hc Load

                    if (syslogline.Contains("Video Load:") && syslogline.Contains("HC"))
                    {


                        string[] words = syslogline.Split('>');
                        for (int j = 1; j < 100; j++)
                        {
                            if (words[1].Contains("( " + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(hdscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[5]), 2);
                                hdscrm[j] = final.ToString();
                                //added for more load modeling details
                                int kl = Convert.ToInt32(wordSearched[5]);
                                Main.sysscrmload[j].Add(kl);

                            }

                            else if (words[1].Contains("(" + j + ")"))
                            {
                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(hdscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                hdscrm[j] = final.ToString();
                                //added for more load modeling details
                                int kl = Convert.ToInt32(wordSearched[4]);
                                Main.sysscrmload[j].Add(kl);

                            }
                        }
                        for (int j = 100; j < 161; j++)
                        {
                            if (words[1].Contains("(" + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(hdscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                hdscrm[j] = final.ToString();
                                //added for more load modeling details
                                int kl = Convert.ToInt32(wordSearched[4]);
                                Main.sysscrmload[j].Add(kl);

                            }
                        }

                    }
                    //HD

                    if (syslogline.Contains("Video Load:") && syslogline.Contains("HD"))
                    {


                        string[] words = syslogline.Split('>');
                        for (int j = 1; j < 100; j++)
                        {
                            if (words[1].Contains("( " + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(hdscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[5]), 2);
                                hdscrm[j] = final.ToString();
                                //added for more load modeling details
                                int kl = Convert.ToInt32(wordSearched[5]);
                                Main.sysscrmload[j].Add(kl);
                            }

                            else if (words[1].Contains("(" + j + ")"))
                            {
                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(hdscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                hdscrm[j] = final.ToString();
                                //added for more load modeling details
                                int kl = Convert.ToInt32(wordSearched[4]);
                                Main.sysscrmload[j].Add(kl);
                            }
                        }
                        for (int j = 100; j < 161; j++)
                        {
                            if (words[1].Contains("(" + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(hdscrm[j]);
                                double final = value + Math.Pow(Convert.ToDouble(wordSearched[4]), 2);
                                hdscrm[j] = final.ToString();
                                //added for more load modeling details
                                int kl = Convert.ToInt32(wordSearched[4]);
                                Main.sysscrmload[j].Add(kl);
                            }
                        }

                    }



                    // DSP Load
                    if (syslogline.Contains(") Load:"))
                    {
                        numofiteration++;
                        LoadModellingtext = "OK";
                        string[] words = syslogline.Split('>');
                        for (int j = 1; j < 100; j++)
                        {
                            if (words[1].Contains("( " + j + ")"))
                            {
                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(audiodsp[j]);
                                if (wordSearched[3].Contains("Max:"))
                                {
                                    string s = wordSearched[2].Replace("Load:", String.Empty);
                                    double final = value + Math.Pow(Convert.ToDouble(s.Replace(",", String.Empty).ToString()),2);
                                    audiodsp[j] = final.ToString();
                                    //added for more load modeling details
                                    int kl = Convert.ToInt32(s.Replace(",", String.Empty));
                                    Main.sysdspload[j].Add(kl);
                                }
                                else
                                {
                                    double final = value + Math.Pow(Convert.ToDouble(wordSearched[3].Replace(",", String.Empty).ToString()), 2);
                                    audiodsp[j] = final.ToString();
                                    //added for more load modeling details
                                    int kl = Convert.ToInt32(wordSearched[3].Replace(",", String.Empty).ToString());
                                    Main.sysdspload[j].Add(kl);
                                }

                            }

                            else if (words[1].Contains("(" + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(audiodsp[j]);
                                if (wordSearched[2].Contains("Max:"))
                                {
                                    string s = wordSearched[1].Replace("Load:", String.Empty);
                                    double final = value + Math.Pow(Convert.ToDouble(s.Replace(",", String.Empty).ToString()),2);
                                    audiodsp[j] = final.ToString();
                                    //added for more load modeling details
                                    int kl = Convert.ToInt32(s.Replace(",", String.Empty).ToString());
                                    Main.sysdspload[j].Add(kl);
                                }
                                else
                                {
                                    double final = value + Math.Pow(Convert.ToDouble(wordSearched[2].Replace(",", String.Empty).ToString()), 2);
                                    audiodsp[j] = final.ToString();
                                    //added for more load modeling details
                                    int kl = Convert.ToInt32(wordSearched[2].Replace(",", String.Empty).ToString());
                                    Main.sysdspload[j].Add(kl);
                                }



                            }
                        }
                        for (int j = 100; j < 161; j++)
                        {
                            if (words[1].Contains("(" + j + ")"))
                            {

                                char[] charSeparators = new char[] { ' ' };
                                string[] wordSearched = words[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                                double value = Convert.ToDouble(audiodsp[j]);
                                if (wordSearched[2].Contains("Max:"))
                                {
                                    string s = wordSearched[1].Replace("Load:", String.Empty);
                                    double final = value + Math.Pow(Convert.ToDouble(s.Replace(",", String.Empty).ToString()), 2);
                                    audiodsp[j] = final.ToString();
                                    //added for more load modeling details
                                    int kl = Convert.ToInt32(s.Replace(",", String.Empty).ToString());
                                    Main.sysdspload[j].Add(kl);
                                }
                                else
                                {
                                    double final = value + Math.Pow(Convert.ToDouble(wordSearched[2].Replace(",", String.Empty).ToString()), 2);
                                    audiodsp[j] = final.ToString();
                                    //added for more load modeling details
                                    int kl = Convert.ToInt32(wordSearched[2].Replace(",", String.Empty).ToString());
                                    Main.sysdspload[j].Add(kl);
                                }



                            }
                        }


                    }


                }
            }
            catch (SystemException se)
            {
                MessageBox.Show("Load Modeling Calculation exception for line  !!! " + syslogline + se.Message);
            }
            Console.WriteLine("Finished loadmodelingthread");

        }

        public void finalavg()
        {
            avgcaltxt = "OK";
            numaudiodsp = 0;
            numvideodsp = 0;
            numhddsp = 0;
            numtotaldsp = 0;
            try
            {

                //Calculation of total dsp
                //Audio DSP
                for (int k = 1; k < 161; k++)
                {
                    //Audio DSP
                    if (null != audioscrm[k])
                    {
                        numaudiodsp++;
                    }
                    //Video DSP
                    if (null != videoscrm[k])
                    {
                        numvideodsp++;
                    }
                    if (null != hdscrm[k])
                    {
                        numhddsp++;
                    }
                }
                if (numaudiodsp == 0 && numvideodsp == 0)
                {
                    //no action
                }
                else
                {
                    numtotaldsp = numvideodsp + numaudiodsp + numhddsp;
                    avginterval = numofiteration / numtotaldsp;
                    if (numhddsp > 0)
                    {
                        //numtotaldsp = numvideodsp + numaudiodsp + numhddsp ;
                        numhddsp = numhddsp * 3;
                        numtotaldsp = numvideodsp + numaudiodsp + numhddsp;
                    }

                    if (avginterval != 0)
                    {
                        //Audio DSP
                        for (int i = 1; i < numtotaldsp + 1; i++)
                        {
                            //Audio DSP
                            if (null != audioscrm[i])
                            {
                                int tmp1 = Convert.ToInt32(audioscrm[i]) / avginterval;
                                int tmp2 = Convert.ToInt32(audiodsp[i]) / avginterval;
                                double a = Math.Sqrt(tmp1);
                                double b = Math.Sqrt(tmp2);
                                audioscrm[i] = Math.Round(a, 0).ToString();
                                audiodsp[i] = Math.Round(b, 0).ToString();
                            }
                            //Video DSP
                            if (null != videoscrm[i])
                            {
                                int tmp1 = Convert.ToInt32(videoscrm[i]) / avginterval;
                                int tmp2 = Convert.ToInt32(audiodsp[i]) / avginterval;
                                double a = Math.Sqrt(tmp1);
                                double b = Math.Sqrt(tmp2);
                                videoscrm[i] = Math.Round(a, 0).ToString();
                                audiodsp[i] = Math.Round(b, 0).ToString();
                            }
                            if (null != hdscrm[i])
                            {
                                int tmp1 = Convert.ToInt32(hdscrm[i]) / avginterval;
                                int tmp2 = Convert.ToInt32(audiodsp[i]) / avginterval;
                                double a = Math.Sqrt(tmp1);
                                double b = Math.Sqrt(tmp2);
                                hdscrm[i] = Math.Round(a, 0).ToString();
                                audiodsp[i] = Math.Round(b, 0).ToString();
                            }
                        }
                    }

                }

            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception in function finalavg",se.Message);
            }
        }

        public void slip_super()
        {
            //Main.sysslip.Clear();
            //Main.syssuper.Clear();
            //Main.LostCmdsCPDSP.Clear();
            //Main.LostCmdsDSPCP.Clear();
            //Main.sysNonDecodedAudioFrames.Clear();
            //Main.sysJitterBufFramesDropped.Clear();
            //Main.sysburst.Clear();
            //Main.sysMaxVideoFrameDrop.Clear();
            //Main.sysBurstSlippedExceedLimit.Clear();
            //Main.sysNonDecodedVideoFrames.Clear();
            //Main.sysDroppedVideoPacket.Clear();
            //Main.sysAvgVideoFrameDrop.Clear();
            for (int i = 1; i <= 160; i++)
            {
                Main.sysslip[i] = new List<int>();
                Main.sysslip[i] = new List<int>();
                Main.syssuper[i] = new List<int>();
                Main.LostCmdsCPDSP[i] = new List<int>();
                Main.LostCmdsDSPCP[i] = new List<int>();
                Main.sysNonDecodedAudioFrames[i] = new List<int>();
                Main.sysJitterBufFramesDropped[i] = new List<int>();
                Main.sysburst[i] = new List<int>();
                Main.sysMaxVideoFrameDrop[i] = new List<int>();
                Main.sysBurstSlippedExceedLimit[i] = new List<int>();
                Main.sysNonDecodedVideoFrames[i] = new List<int>();
                Main.sysDroppedVideoPacket[i] = new List<int>();
                Main.sysAvgVideoFrameDrop[i] = new List<int>();
            }
            string[] logfile = Directory.GetFiles(Main.currentdir, "*.log");
            if (logfile.Length != 0)
            {


                int i = logfile.Length;
                IEnumerable<string> lines = File.ReadLines(logfile[i - 1]);
                foreach (string line in lines)
                {
                    string syslogline = line;
                    try
                    {



                        if (syslogline.Contains(", Slipped:"))
                        {
                            string[] words = syslogline.Split(',');
                            for (int j = 1; j < 100; j++)
                            {
                                if (words[0].Contains("( " + j + ")"))
                                {
                                    //Main.sysslip[j].Add(1);
                                    //Main.sysslip[j].Add(2);
                                    //Main.sysslip[j].Add(3);
                                    //Main.sysslip[j].Add(4);
                                    for (int k = 0; k < words.Length; k++)
                                    {
                                        if (words[k].Contains(" Slipped"))
                                        {
                                            Main.sysslip[j].Add(Convert.ToInt32(words[k].Replace(" Slipped:", String.Empty)));
                                        }
                                        if (words[k].Contains(" Super"))
                                        {
                                            Main.syssuper[j].Add(Convert.ToInt32(words[k].Replace(" Super:", String.Empty)));
                                        }
                                        if (words[k].Contains(" LostCmds(CP->DSP)"))
                                        {
                                            Main.LostCmdsCPDSP[j].Add(Convert.ToInt32(words[k].Replace(" LostCmds(CP->DSP):", String.Empty)));
                                        }
                                        if (words[k].Contains(" LostCmds(DSP->CP)"))
                                        {
                                            Main.LostCmdsDSPCP[j].Add(Convert.ToInt32(words[k].Replace(" LostCmds(DSP->CP):", String.Empty)));
                                        }
                                        if (words[k].Contains(" NonDecodedAudioFrames"))
                                        {
                                            Main.sysNonDecodedAudioFrames[j].Add(Convert.ToInt32(words[k].Replace(" NonDecodedAudioFrames:", String.Empty)));

                                        }
                                        if (words[k].Contains(" JitterBufFramesDropped"))
                                        {
                                            string temp = words[k].Replace(" JitterBufFramesDropped:", String.Empty);
                                            Main.sysJitterBufFramesDropped[j].Add(Convert.ToInt32(temp));
                                        }

                                    }


                                }
                                else if (words[0].Contains("(" + j + ")"))
                                {
                                    for (int k = 0; k < words.Length; k++)
                                    {
                                        if (words[k].Contains(" Slipped"))
                                        {
                                            Main.sysslip[j].Add(Convert.ToInt32(words[k].Replace(" Slipped:", String.Empty)));
                                        }
                                        if (words[k].Contains(" Super"))
                                        {
                                            Main.syssuper[j].Add(Convert.ToInt32(words[k].Replace(" Super:", String.Empty)));
                                        }
                                        if (words[k].Contains(" LostCmds(CP->DSP)"))
                                        {
                                            Main.LostCmdsCPDSP[j].Add(Convert.ToInt32(words[k].Replace(" LostCmds(CP->DSP):", String.Empty)));
                                        }
                                        if (words[k].Contains(" LostCmds(DSP->CP)"))
                                        {
                                            Main.LostCmdsDSPCP[j].Add(Convert.ToInt32(words[k].Replace(" LostCmds(DSP->CP):", String.Empty)));
                                        }
                                        if (words[k].Contains(" NonDecodedAudioFrames"))
                                        {
                                            Main.sysNonDecodedAudioFrames[j].Add(Convert.ToInt32(words[k].Replace(" NonDecodedAudioFrames:", String.Empty)));

                                        }
                                        if (words[k].Contains(" JitterBufFramesDropped"))
                                        {
                                            string temp = words[k].Replace(" JitterBufFramesDropped:", String.Empty);
                                            Main.sysJitterBufFramesDropped[j].Add(Convert.ToInt32(temp));
                                        }

                                    }

                                }
                            }
                            for (int j = 100; j < 161; j++)
                            {
                                if (words[0].Contains("(" + j + ")"))
                                {

                                    for (int k = 0; k < words.Length; k++)
                                    {
                                        if (words[k].Contains(" Slipped"))
                                        {
                                            Main.sysslip[j].Add(Convert.ToInt32(words[k].Replace(" Slipped:", String.Empty)));
                                        }
                                        if (words[k].Contains(" Super"))
                                        {
                                            Main.syssuper[j].Add(Convert.ToInt32(words[k].Replace(" Super:", String.Empty)));
                                        }
                                        if (words[k].Contains(" LostCmds(CP->DSP)"))
                                        {
                                            Main.LostCmdsCPDSP[j].Add(Convert.ToInt32(words[k].Replace(" LostCmds(CP->DSP):", String.Empty)));
                                        }
                                        if (words[k].Contains(" LostCmds(DSP->CP)"))
                                        {
                                            Main.LostCmdsDSPCP[j].Add(Convert.ToInt32(words[k].Replace(" LostCmds(DSP->CP):", String.Empty)));
                                        }
                                        if (words[k].Contains(" NonDecodedAudioFrames"))
                                        {
                                            Main.sysNonDecodedAudioFrames[j].Add(Convert.ToInt32(words[k].Replace(" NonDecodedAudioFrames:", String.Empty)));

                                        }
                                        if (words[k].Contains(" JitterBufFramesDropped"))
                                        {
                                            string temp = words[k].Replace(" JitterBufFramesDropped:", String.Empty);
                                            Main.sysJitterBufFramesDropped[j].Add(Convert.ToInt32(temp));
                                        }

                                    }
                                }
                            }
                        }

                        if (syslogline.Contains("MaxBurstSlipped:"))
                        {
                            string[] xBurst = syslogline.Split(',');
                            for (int j = 1; j < 100; j++)
                            {
                                if (xBurst[0].Contains("( " + j + ")"))
                                {
                                    for (int k = 0; k < xBurst.Length; k++)
                                    {
                                        if (xBurst[k].Contains(" MaxBurstSlipped:"))
                                        {
                                            Main.sysburst[j].Add(Convert.ToInt32(xBurst[k].Replace(" MaxBurstSlipped:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" MaxBurstSlipped:"))
                                        {
                                            string s = xBurst[k].Replace(" MaxBurstSlipped:", String.Empty);
                                            s = s.Replace("%", String.Empty);
                                            Main.sysMaxVideoFrameDrop[j].Add(Convert.ToInt32(s));
                                        }
                                        if (xBurst[k].Contains(" BurstSlippedExceedLimit:"))
                                         {

                                             Main.sysBurstSlippedExceedLimit[j].Add(Convert.ToInt32(xBurst[k].Replace(" BurstSlippedExceedLimit:", String.Empty)));
                                         }
                                        if (xBurst[k].Contains(" NonDecodedVideoFrames:"))
                                         {
                                            Main.sysNonDecodedVideoFrames[j].Add(Convert.ToInt32(xBurst[k].Replace(" NonDecodedVideoFrames:", String.Empty)));
                                         }
                                        if (xBurst[k].Contains(" DroppedVideoPacket:"))
                                         {
                                            Main.sysDroppedVideoPacket[j].Add(Convert.ToInt32(xBurst[k].Replace(" DroppedVideoPacket:", String.Empty)));
                                         }
                                        if (xBurst[k].Contains(" AvgVideoFrameDrop:"))
                                         {
                                             string w = xBurst[k].Replace(" AvgVideoFrameDrop:", String.Empty);
                                             w = w.Replace("%", String.Empty);
                                             Main.sysAvgVideoFrameDrop[j].Add(Convert.ToInt32(w));
                                         }
                                       
                                    }


                                }
                                else if (xBurst[0].Contains("(" + j + ")"))
                                {
                                    for (int k = 0; k < xBurst.Length; k++)
                                    {
                                        if (xBurst[k].Contains(" MaxBurstSlipped:"))
                                        {
                                            Main.sysburst[j].Add(Convert.ToInt32(xBurst[k].Replace(" MaxBurstSlipped:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" MaxBurstSlipped:"))
                                        {
                                            string s = xBurst[k].Replace(" MaxBurstSlipped:", String.Empty);
                                            s = s.Replace("%", String.Empty);
                                            Main.sysMaxVideoFrameDrop[j].Add(Convert.ToInt32(s));
                                        }
                                        if (xBurst[k].Contains(" BurstSlippedExceedLimit:"))
                                        {

                                            Main.sysBurstSlippedExceedLimit[j].Add(Convert.ToInt32(xBurst[k].Replace(" BurstSlippedExceedLimit:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" NonDecodedVideoFrames:"))
                                        {
                                            Main.sysNonDecodedVideoFrames[j].Add(Convert.ToInt32(xBurst[k].Replace(" NonDecodedVideoFrames:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" DroppedVideoPacket:"))
                                        {
                                            Main.sysDroppedVideoPacket[j].Add(Convert.ToInt32(xBurst[k].Replace(" DroppedVideoPacket:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" AvgVideoFrameDrop:"))
                                        {
                                            string w = xBurst[k].Replace(" AvgVideoFrameDrop:", String.Empty);
                                            w = w.Replace("%", String.Empty);
                                            Main.sysAvgVideoFrameDrop[j].Add(Convert.ToInt32(w));
                                        }

                                    }
                                }
                            }
                            for (int j = 100; j < 161; j++)
                            {
                                if (xBurst[0].Contains("( " + j + ")"))
                                {
                                    for (int k = 0; k < xBurst.Length; k++)
                                    {
                                        if (xBurst[k].Contains(" MaxBurstSlipped:"))
                                        {
                                            if (Convert.ToInt32(xBurst[k].Replace(" MaxBurstSlipped:", String.Empty)) != 0)
                                            {
                                                Main.sysburst[j].Add(Convert.ToInt32(xBurst[k].Replace(" MaxBurstSlipped:", String.Empty)));
                                            }
                                        }
                                        if (xBurst[k].Contains(" MaxBurstSlipped:"))
                                        {
                                            string s = xBurst[k].Replace(" MaxBurstSlipped:", String.Empty);
                                            s = s.Replace("%", String.Empty);
                                            Main.sysMaxVideoFrameDrop[j].Add(Convert.ToInt32(s));
                                        }
                                        if (xBurst[k].Contains(" BurstSlippedExceedLimit:"))
                                        {

                                            Main.sysBurstSlippedExceedLimit[j].Add(Convert.ToInt32(xBurst[k].Replace(" BurstSlippedExceedLimit:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" NonDecodedVideoFrames:"))
                                        {
                                            Main.sysNonDecodedVideoFrames[j].Add(Convert.ToInt32(xBurst[k].Replace(" NonDecodedVideoFrames:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" DroppedVideoPacket:"))
                                        {
                                            Main.sysDroppedVideoPacket[j].Add(Convert.ToInt32(xBurst[k].Replace(" DroppedVideoPacket:", String.Empty)));
                                        }
                                        if (xBurst[k].Contains(" AvgVideoFrameDrop:"))
                                        {
                                            string w = xBurst[k].Replace(" AvgVideoFrameDrop:", String.Empty);
                                            w = w.Replace("%", String.Empty);
                                            Main.sysAvgVideoFrameDrop[j].Add(Convert.ToInt32(w));
                                        }

                                    }
                                }
                            }



                        }
                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show("Exception in slipcycle function"+line, se.Message);
                        Console.WriteLine("Exception in slipcycle function"+line, se.Message);
                        break;
                    }
                }



            }
        }
        public void slip_super_burst_percore()
        {
            try
            {
                for (int i = 1; i < Main.numtotaldsp + 1; i++)
                {
                    int j=i;
                    if (null != Main.audioscrm[i])
                    {
                        //slip audio
                        if (sysslip[j].Sum() >= 90)
                        {
                            //15 min interval
                            for (int k = 15; k < sysslip[j].Count; k = k + 15)
                            {
                                
                                int sum = 0;
                                for (int m = k - 15; m <= k; m++ )
                                {
                                    sum += sysslip[j][m];
                                }
                                if (sum >= 90)
                                {
                                    slip[i] = "Slip cycle found at Audio DSP" + i.ToString();
                                    break;
                                }
                            }
                            if (slip[i] == null)
                            {
                                //slip[i] = "No slip cycle found.Total ΣSlip sum is " + sysslip[i].Sum().ToString() + " But slip cycle does not exceed more than 15 for any 15 min duration for Audio DSP " + i.ToString();
                            }
                        }
                        else
                        {
                           // slip[i] = "No slip cycle found.Total ΣSlip sum is " + sysslip[i].Sum().ToString() + "for Audio DSP" + i.ToString();
                        }

                        
                        //super audio
                        if (syssuper[j].Sum() >= 1)
                        {
                            //15 min interval
                            super[i] = "Total "+syssuper[j].Sum().ToString()+" Super Slip cycle found at Audio DSP" + i.ToString();
                             
                        }
                        else
                        {
                            //super[i] = "No Super Slip cycle found.Total ΣSlip sum is " + sysslip[i].Sum().ToString() + "for Audio DSP" + i.ToString();
                        }



                        if (sysburst[j].Sum() >= 1)
                        {
                            burst[i] = "Total " + sysburst[j].Sum().ToString() + " Burst Slip cycle found at Audio DSP" + i.ToString();
                          
                        }
                        else
                        {
                           // burst[i] = "No Burst slip cycle found.Total Σ Burst Slip sum is " + sysslip[i].Sum().ToString() + "for Audio DSP" + i.ToString();
                        }

                       

                    }
                    if (null != Main.videoscrm[i])
                    {
                        //super Video

                        if (syssuper[j].Sum() >= 90)
                        {
                            //15 min interval
                            for (int k = 15; k < syssuper[j].Count; k = k + 15)
                            {
                                int sum = 0;
                                for (int m = k - 15; m <= k; m++)
                                {
                                    sum += syssuper[j][m];
                                }
                                if (sum >= 90)
                                {
                                    super[i] = "Super Slip cycle found at Video DSP" + i.ToString();
                                    break;
                                }
                            }
                            if (super[i] == null)
                            {
                                //super[i] = "Super slip cycle found.Total ΣSuper Slip sum is " + sysslip[i].Sum().ToString() + " But Super slip cycle does not exceed more than 90 for any 15 min duration for Video DSP " + i.ToString();
                            }
                        }
                        else
                        {
                           // super[i] = "No Super Slip cycle found.Total ΣSuper Slip sum is " + sysslip[i].Sum().ToString() + "for Video DSP" + i.ToString();
                        }

                        if (sysburst[j].Sum() >= 1)
                        {
                            //15 min interval
                            burst[i] = "Total " + sysburst[j].Sum().ToString() + " Burst Slip cycle found at Video DSP" + i.ToString();
                          
                        }
                        else
                        {
                           // burst[i] = "No Burst slip cycle found.Total Σ Burst Slip sum is " + sysslip[i].Sum().ToString() + "for Video DSP" + i.ToString();
                        }
                        if (sysBurstSlippedExceedLimit[j].Sum() >= 1)
                        {
                            //15 min interval
                            burst[i] = "Total " + sysBurstSlippedExceedLimit[j].Sum().ToString() + " BurstSlippedExceedLimit found at Video DSP" + i.ToString();

                        }
                        else
                        {
                            // burst[i] = "No Burst slip cycle found.Total Σ Burst Slip sum is " + sysslip[i].Sum().ToString() + "for Video DSP" + i.ToString();
                        }

                        if (sysBurstSlippedExceedLimit[j].Sum() >= 1 && sysburst[j].Sum() >= 1)
                        {
                            //15 min interval
                            burst[i] = "BurstSlippedExceedLimit & Burst Slip cycle found at Video DSP" + i.ToString();

                        }
                        else
                        {
                            // burst[i] = "No Burst slip cycle found.Total Σ Burst Slip sum is " + sysslip[i].Sum().ToString() + "for Video DSP" + i.ToString();
                        }




                    }
                    if (null != Main.hdscrm[i])
                    {

                        if (sysBurstSlippedExceedLimit[j].Sum() >= 90)
                        {
                            //15 min interval
                            for (int k = 15; k < sysBurstSlippedExceedLimit[j].Count; k = k + 15)
                            {
                                int sum = 0;
                                for (int m = k - 15; m <= k; m++)
                                {
                                    sum += sysBurstSlippedExceedLimit[j][m];
                                }
                                if (sum >= 90)
                                {

                                    burst[i] = "BurstSlippedExceedLimit found at HC DSP" + i.ToString();
                                    break;
                                }
                            }
                            if (burst[i] == null)
                            {
                               // burst[i] = "Burst slip cycle found.Total ΣSlip sum is " + sysslip[i].Sum().ToString() + " But burst slip cycle does not exceed more than 90 for any 15 min duration for HC DSP " + i.ToString();
                            }

                            else
                            {
                              //  burst[i] = "No Burst slip cycle found.Total Σ Burst Slip sum is " + sysslip[i].Sum().ToString() + "for HC DSP" + i.ToString();
                            }

                        }
                    }
                }
               


                //for (int i = 1; i < 161; i++)
                //{
                //    for(int j=0;j<sysslip[i].Count;j++)
                //    {
                //        slip[i] = (Convert.ToInt32(slip[i]) + sysslip[i][j]).ToString();
                //        //if(sysslip[i][j]-sysslip[
                //    }
                //    for (int j = 0; j < syssuper[i].Count; j++)
                //    {
                //        super[i] = (Convert.ToInt32(super[i]) + syssuper[i][j]).ToString();
                //    }
                //    for (int j = 0; j < sysburst[i].Count; j++)
                //    {
                //        burst[i] = (Convert.ToInt32(burst[i]) + sysburst[i][j]).ToString();
                //    }
                //    
                //}
                //for (int i = 1; i < 161; i++)
                //{
                //    if (sysslip[i].Count > 1)
                //    {
                //        if (sysslip[i].Count / 15 != 0)
                //        {
                //            slip[i] = (Convert.ToInt32(slip[i]) / (sysslip[i].Count / 15)).ToString();
                //        }
                //    }
                //    if (syssuper[i].Count > 1)
                //    {
                //        if (syssuper[i].Count / 15 != 0)
                //        {
                //            super[i] = (Convert.ToInt32(slip[i]) / (syssuper[i].Count / 15)).ToString();
                //        }
                //
                //    }
                //    if (sysburst[i].Count > 1)
                //    {
                //        if (sysburst[i].Count / 15 != 0)
                //        {
                //            burst[i] = (Convert.ToInt32(slip[i]) / (sysburst[i].Count / 15)).ToString();
                //        }
                //    }
                //}



            }
            catch (SystemException se)
            {
                MessageBox.Show("Exception in slip_super_burst_percore function", se.Message);
                Console.WriteLine("Exception in slip_super_burst_percore function", se.Message);
            }

        }

        private void slipCycleAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModeling")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Load Modeling Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                LoadModelling obj = new LoadModelling();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.tabPage2.Select();
                obj.Show();
                toolStripStatusLabel1.Text = "Load Modeling Analysis";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void dSPUsageGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rTPPacketLostToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Statistics_Analysis")
                    {
                        Isopen = true;
                        toolStripStatusLabel1.Text = "Statistics Analysis";
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Statistics_Analysis obj = new Statistics_Analysis();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                toolStripStatusLabel1.Text = "Statistics Analysis";
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        public void statistic_analysis()
        {
            List<string> stats = new List<string>();
            string[] statfilename = Directory.GetFiles(currentdir, "statistics*");
            string[] logsfile = Directory.GetFiles(currentdir, "*.log");
            if (statfilename.Length != 0 && logsfile.Length != 0)
            {
                //logsfile[0].Replace(currentdir, String.Empty);
                int slot = 0;
                string mrftype = "";
                if (logsfile[0].Contains("mpc"))
                {
                    //MPX
                    for (int i = 1; i < 15; i++)
                    {
                        if (logsfile[0].Contains("mpc" + i.ToString() + "-"))
                        {
                            slot = i;
                            break;
                        }
                    }

                }
                else
                {
                    slot = 1;
                    mrftype = "SWMS";
                }
                if (slot != 14 && mrftype != "SWMS")
                {

                    IEnumerable<string> lines = File.ReadLines(statfilename[0]);
                    string sslot = "Slot: " + slot.ToString();
                    string sslotnext = "Slot: " + (slot + 1).ToString();
                    string sslotnext2 = "Slot: " + (slot + 2).ToString();

                    bool considerSlot = false;
                    foreach (string line in lines)
                    {
                        string statsline = line;

                        if (statsline.Contains(sslot))
                        {
                            considerSlot = true;

                        }
                        if (statsline.Contains(sslotnext))
                        {
                            considerSlot = false;
                            break;

                        }
                        //incase of slot 6
                        if (statsline.Contains(sslotnext2))
                        {
                            considerSlot = false;

                        }
                        if (considerSlot != false)
                        {

                            stats.Add(statsline);
                        }


                    }


                }
                else
                {
                    //MRF Slot 14 and SWMS
                    IEnumerable<string> lines = File.ReadLines(statfilename[0]);
                    string sslot = "Slot: " + slot.ToString();

                    bool considerSlot = false;
                    foreach (string line in lines)
                    {
                        string statsline = line;

                        if (statsline.Contains(sslot))
                        {
                            considerSlot = true;

                        }
                        if (considerSlot != false)
                        {
                            stats.Add(statsline);
                        }


                    }
                }

                //For Avg CPU Core Utilization 
                //For RTP Packets Anlaysis and slip cycle
                for (int i = 0; i < stats.Count; i++)
                {
                    //For Statistic Time stamp:, 
                    if (stats[i].Contains("Time stamp:"))
                    {
                        if (statisticsinterval == 0)
                        {
                            try
                            {
                                string[] words = stats[i].Split(',');
                                String t2 = words[2];
                                String t1 = words[1];
                                var splitt1 = Regex.Split(t1, @"([:::])");
                                var splitt2 = Regex.Split(t2, @"([:::])");
                                //MessageBox.Show(splitt1[2].ToString());
                                //MessageBox.Show(splitt2[2].ToString());
                                statisticsinterval = Convert.ToInt32(splitt1[2]) - Convert.ToInt32(splitt2[2]);
                                

                            }
                            catch (SystemException se)
                            {

                                MessageBox.Show("Exception in Statistic Time stamp", se.Message);
                            }
                        }

                    }


                    if (stats[i].Contains("RTP Packets Lost"))
                    {
                        try
                        {
                            string[] words = stats[i].Split(',');
                            int len = words.Length;
                            rtppacketlosttotal = words[len - 5];
                            //MessageBox.Show("OK");
                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at statistic_analysis for RTP Packets Lost", se.Message);
                        }

                    }

                    if (stats[i].Contains("RTP Packets TX"))
                    {
                        try
                        {
                            string[] words = stats[i].Split(',');
                            int len = words.Length;
                            rtppackettxttotal = words[len - 5];
                            // MessageBox.Show("OK");
                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at statistic_analysis for RTP Packets TX", se.Message);
                        }

                    }

                    if (stats[i].Contains("RTP Packets RX"))
                    {
                        try
                        {
                            string[] words = stats[i].Split(',');
                            int len = words.Length;
                            rtppacketrxtotal = words[len - 5];
                            //MessageBox.Show("OK");
                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at statistic_analysis for RTP Packets RX", se.Message);
                        }

                    }
                    if (stats[i].Contains("Slipped Cycle Count"))
                    {
                        try
                        {
                            string[] words = stats[i].Split(',');
                            int len = words.Length;
                            Slipcycle = words[len - 5];

                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at statistic_analysis for Slipped Cycle Count", se.Message);
                        }

                    }
                    if (stats[i].Contains("Avg DSP CPU Utilization"))
                    {
                        AvgDSPUtilization.Clear();
                        try
                        {
                            string[] words = stats[i].Split(',');
                            for (int p = 1; p < words.Length - 10; p++)
                            {
                                AvgDSPUtilization.Add(words[p]);

                            }


                        }
                        catch (SystemException se)
                        {
                            MessageBox.Show("Exception at statistic_analysis for Avg DSP CPU Utilization", se.Message);
                        }

                    }

                    //Super Slipped Cycle Count (Audio)
                    //Max Burst Slip Cycles (Audio)
                    //Super Slipped Cycle Count (Video)
                    //Max Burst Slip Cycles (Video)
                    //Max Burst Slip Cycles (HC Video)
                    //Burst Slip Cycles Exceed Limit (HC Video)
                    //Max Video Encoder Frame Rate Drop Rate
                    //For Frame Drap rate
                    try
                    {

                        if (stats[i].Contains("Super Slipped Cycle Count (Audio)"))
                        {
                            superslipaudio.Clear();
                            string junk = "";
                            try
                            {
                               string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        junk=words[p];
                                        superslipaudio.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                superslipaudio.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Super Slipped Cycle Count (Audio)" + junk, se.Message);
                               
                        
                            }
                        }
                        
                        if (stats[i].Contains("Max Burst Slip Cycles (Audio)"))
                        {
                            burstslipaudio.Clear();
                            try
                            {
                               string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        burstslipaudio.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                burstslipaudio.Sort();
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Max Burst Slip Cycle Count (Audio)", se.Message);
                        
                            }
                        }
                        
                        if (stats[i].Contains("Super Slipped Cycle Count (Video)"))
                        {
                            superslipvideo.Clear();
                            try
                            {
                               string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        superslipvideo.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                superslipvideo.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Super Slipped Cycle Count (Video)", se.Message);
                        
                            }
                        }
                        
                        if (stats[i].Contains("Max Burst Slip Cycles (Video)"))
                        {
                            burstslipvideo.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        burstslipvideo.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                burstslipvideo.Sort();
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Max Burst Slip Cycles (Video)", se.Message);
                        
                            }
                        }
                        
                        if (stats[i].Contains("Max Burst Slip Cycles (HC Video)"))
                        {
                            burstslipHC.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        burstslipHC.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                burstslipHC.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Max Burst Slip Cycles (HC Video)", se.Message);
                        
                            }
                        }
                        //NonDecodedVideoFrames
                        if (stats[i].Contains("Non Decoded Video Frames "))
                        {
                            NonDecodedVideoFrames.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        NonDecodedVideoFrames.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                NonDecodedVideoFrames.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Non Decoded Video Frames", se.Message);
                        
                            }
                        }
                        //Non Decoded Audio Frames
                        if (stats[i].Contains("Non Decoded Audio Frames"))
                        {
                            NonDecodedAudioFrames.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        NonDecodedAudioFrames.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                NonDecodedAudioFrames.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Non Decoded Audio Frames", se.Message);
                        
                            }
                        }
                        
                        //Jitter Buffer Frames Dropped
                        if (stats[i].Contains("Jitter Buffer Frames Dropped"))
                        {
                            JitterBufferFramesDropped.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        JitterBufferFramesDropped.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                JitterBufferFramesDropped.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Jitter Buffer Frames Dropped", se.Message);
                        
                            }
                        }
                        
                        //Dropped Video Packets
                        if (stats[i].Contains("Dropped Video Packets"))
                        {
                            DroppedVideoPackets.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        DroppedVideoPackets.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                DroppedVideoPackets.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Dropped Video Packets", se.Message);
                        
                            }
                        }
                        
                        //Max Frames Dropped (HC Video)
                        if (stats[i].Contains("Max Frames Dropped (HC Video)"))
                        {
                            MaxFramesDroppedHCVideo.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        MaxFramesDroppedHCVideo.Add(Convert.ToInt32(words[p]));

                                    }


                                }
                                MaxFramesDroppedHCVideo.Sort();
                        
                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Max Frames Dropped (HC Video)", se.Message);
                        
                            }
                        }
                        //Max Video Encoder Frame Rate Drop Rate 
                        if (stats[i].Contains("Max Video Encoder Frame Rate Drop Rate "))
                        {

                             MaxVideoEncoderFrameRateDropRate.Clear();
                            try
                            {
                                string[] words = stats[i].Split(',');
                                for (int p = 1; p < words.Length - 10; p++)
                                {

                                    if (words[p].Contains(" -"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        MaxVideoEncoderFrameRateDropRate.Add(Convert.ToInt32(words[p]));
                                       
                                    }


                                }
                                MaxVideoEncoderFrameRateDropRate.Sort();

                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Max Video Encoder Frame Rate Drop Rate ", se.Message);

                            }
                        }



                        //Adding for Hung Call status
                        if (stats[i].Contains("Current Ports Active"))
                        {
                            try
                            {
                                string[] words = stats[i].Split(',');
                                //int len = words.Length;
                                currentportactive = words[1];

                            }
                            catch (SystemException se)
                            {
                                MessageBox.Show("Exception at statistic_analysis for Current Ports Active", se.Message);

                            }
                        }

                    }
                    catch (SystemException se)
                    {
                        MessageBox.Show("Exception at statistic_analysis", se.Message);
                    }


                    //MessageBox.Show(stats[i]);
                    //if (stats[i].Contains("Max Video Encoder"))
                    //{
                    //    frameratedrop.Clear();
                    //    try
                    //    {
                    //        string[] words = stats[i].Split(',');
                    //        for (i = 1; i < words.Length - 10; i++)
                    //        {
                    //            frameratedrop.Add(words[i]);
                    //
                    //        }
                    //    }
                    //    catch (SystemException se)
                    //    {
                    //        MessageBox.Show(se.Message);
                    //    }
                    //
                    //}
                    //


                }
            }
            else
            {
                MessageBox.Show("statistics* is not available in current directory hence ignore statistics analysis ");
            }
        }
        public void dsputilizatio()
        {
            if (AvgDSPUtilization.Count != 0)
            {
                for (int i = 0; i < AvgDSPUtilization.Count - 1; i++)
                {

                    if (AvgDSPUtilization[i + 1].Contains(" -"))
                    {
                        AvgDSPUtilizationlen = i;
                        break;

                    }
                    else
                    {
                        if (AvgDSPUtilization[i].Contains(" 0") || AvgDSPUtilization[i + 1].Contains(" 0"))
                        {
                            dspstable = "Stable";
                        }
                        else
                        {
                            if ((Convert.ToInt32(AvgDSPUtilization[i]) - Convert.ToInt32(AvgDSPUtilization[i])) >= 15)
                            {
                                dspstable = "DSP Spike found!!!!";
                                break;
                            }
                            else
                            {
                                dspstable = "Stable";
                            }

                        }

                    }
                }

            }

        }

        private void dSPUtilizationStabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Statistics_Analysis")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Statistics_Analysis obj = new Statistics_Analysis();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "Help")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                Help obj = new Help();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(1);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp1")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp1";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(2);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp2")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp2";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(3);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp3")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp3";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(4);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp4")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp4";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(5);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp5")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp5";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(6);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp6")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp6";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(7);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp7")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp7";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(8);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp8")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp8";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(9);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp9")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp9";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(10);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp10")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp10";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(11);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp11")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp11";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mpcMp12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dspoverallmemoryleak(12);
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "DSP Chart for MpcMp12")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                DSPChart obj = new DSPChart();
                obj.MdiParent = this;
                obj.Text = "DSP Chart for MpcMp12";
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void loadModellingGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModellingindetails")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                LoadModellingindetails obj = new LoadModellingindetails();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        private void mM18ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                {
                    if (f.Text == "LoadModellingindetails")
                    {
                        Isopen = true;
                        f.Focus();
                        break;
                    }
                }

            }
            if (Isopen == false)
            {
                LoadModellingindetails obj = new LoadModellingindetails();
                obj.MdiParent = this;
                obj.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                obj.Show();
                obj.Width = this.Width;
                obj.Height = this.Height;
            }
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
        /// <summary>
        /// For Snmp related query
        /// </summary>
        public void snmpget()
        {
            string[] checksnmpsdll = Directory.GetFiles(currentdir, "SnmpSharpNet.dll");
            if (checksnmpsdll.Length != 0)
            {
                OctetString community = new OctetString("CONV");

                // Define agent parameters class
                AgentParameters param = new AgentParameters(community);
                // Set SNMP version to 1 (or 2)
                param.Version = SnmpVersion.Ver2;


                //string[] atfile = Directory.GetFiles(currentdir, "*.dat");
                string[] atfile = Directory.GetFiles(currentdir, "*at_output*");
                //IEnumerable<string> lines = File.ReadLines(atfile[0]);
                try
                {
                    IEnumerable<string> lines = File.ReadLines(atfile[0]);
                    if (atfile.Length != 0)
                    {
                        foreach (string line in lines)
                        {
                            string atline = line;
                            //getting the IP address
                            if (atline.Contains("SipMSIPAddressSCC=")&&atline.Contains("MSName :"))
                            {
                                try
                                {
                                    string[] words = atline.Split('=');
                                    SipMSIPAddressSCC = words[1];
                                    Console.WriteLine("SipMSIPAddressSCC={0}", SipMSIPAddressSCC);
                                }
                                catch (SystemException se)
                                {
                                    MessageBox.Show(se.Message);
                                }
                            }
                            //getting the slot
                            if (atline.Contains("_Mpc_Card_1_Slot="))
                            {
                                try
                                {
                                    string[] words = atline.Split('=');
                                    mpcslot = words[1];
                                    Console.WriteLine("mpcslot={0}", mpcslot);
                                }
                                catch (SystemException se)
                                {
                                    MessageBox.Show(se.Message);
                                }
                            }
                            //getting CardType
                            if (atline.Contains("_RetrievedFromMS_MPC1_CardType="))
                            {
                                try
                                {
                                    string[] words = atline.Split('=');
                                    mscardtype = words[1];
                                    Console.WriteLine("mscardtype={0}", mscardtype);
                                }
                                catch (SystemException se)
                                {
                                    MessageBox.Show(se.Message);
                                }
                            }
                        }

                        // Construct the agent address object
                        // IpAddress class is easy to use here because
                        //  it will try to resolve constructor parameter if it doesn't
                        //  parse to an IP address

                        IpAddress agent = new IpAddress(SipMSIPAddressSCC);

                        // Construct target
                        UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

                        // Pdu class used for all requests
                        Pdu pdu = new Pdu(PduType.Get);

                        pdu.VbList.Add("1.3.6.1.2.1.1.1.0"); //sysDescr
                        pdu.VbList.Add("1.3.6.1.2.1.1.2.0"); //sysObjectID
                        pdu.VbList.Add("1.3.6.1.2.1.1.3.0"); //sysUpTime
                        pdu.VbList.Add("1.3.6.1.2.1.1.4.0"); //sysContact
                        pdu.VbList.Add("1.3.6.1.2.1.1.5.0"); //sysName
                        if (mscardtype != "SWMS")
                        {
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.3." + mpcslot + ""); //catNumMpCores 
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.4." + mpcslot + ""); //catNumVideoCores 
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.5." + mpcslot + ""); //catNumHdEncCores 
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.6." + mpcslot + ""); //catNumVxmlCores  
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.7." + mpcslot + ""); //catMpSync  
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.8." + mpcslot + ""); //catNumMph6AudioCores   
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.95.1.9." + mpcslot + ""); //catNumMph6HdEncCores  
                        }

                        else
                        {
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.4.0"); //caNumMpCores 
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.7.0"); //caNumVideoCores 
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.8.0"); //caNumHdEncCores 
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.5.0"); //caNumVxmlCores  
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.6.0"); //caMpSync  
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.9.0"); //caNumMph6AudioCores   
                            pdu.VbList.Add("1.3.6.1.4.1.7569.1.2.1.79.10.0"); //caNumMph6HdEncCores  

                        }
                        // Make SNMP request
                        if (PingHost(SipMSIPAddressSCC))
                        {
                            try
                            {

                                // Make SNMP request
                                SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);
                                // If result is null then agent didn't reply or we couldn't parse the reply.
                                if (result != null)
                                {
                                    // ErrorStatus other then 0 is an error returned by 
                                    // the Agent - see SnmpConstants for error definitions
                                    if (result.Pdu.ErrorStatus != 0)
                                    {
                                        // agent reported an error with the request
                                        Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                                            result.Pdu.ErrorStatus,
                                            result.Pdu.ErrorIndex);
                                    }
                                    else
                                    {
                                        // Reply variables are returned in the same order as they were added
                                        //  to the VbList
                                        Console.WriteLine("sysDescr({0}) ({1}): {2}",
                                            result.Pdu.VbList[0].Oid.ToString(),
                                            SnmpConstants.GetTypeName(result.Pdu.VbList[0].Value.Type),
                                            result.Pdu.VbList[0].Value.ToString());
                                        Console.WriteLine("sysObjectID({0}) ({1}): {2}",
                                            result.Pdu.VbList[1].Oid.ToString(),
                                            SnmpConstants.GetTypeName(result.Pdu.VbList[1].Value.Type),
                                            result.Pdu.VbList[1].Value.ToString());
                                        Console.WriteLine("sysUpTime({0}) ({1}): {2}",
                                            result.Pdu.VbList[2].Oid.ToString(),
                                            SnmpConstants.GetTypeName(result.Pdu.VbList[2].Value.Type),
                                            result.Pdu.VbList[2].Value.ToString());
                                        Console.WriteLine("sysContact({0}) ({1}): {2}",
                                            result.Pdu.VbList[3].Oid.ToString(),
                                            SnmpConstants.GetTypeName(result.Pdu.VbList[3].Value.Type),
                                            result.Pdu.VbList[3].Value.ToString());
                                        Console.WriteLine("sysName({0}) ({1}): {2}",
                                            result.Pdu.VbList[4].Oid.ToString(),
                                            SnmpConstants.GetTypeName(result.Pdu.VbList[4].Value.Type),
                                            result.Pdu.VbList[4].Value.ToString());
                                        Console.WriteLine("catNumMpCores({0}) ({1}): {2}",
                                          result.Pdu.VbList[5].Oid.ToString(),
                                          SnmpConstants.GetTypeName(result.Pdu.VbList[5].Value.Type),
                                          result.Pdu.VbList[5].Value.ToString());
                                        Console.WriteLine("catNumVideoCores({0}) ({1}): {2}",
                                          result.Pdu.VbList[6].Oid.ToString(),
                                          SnmpConstants.GetTypeName(result.Pdu.VbList[6].Value.Type),
                                          result.Pdu.VbList[6].Value.ToString());
                                        Console.WriteLine("catNumHdEncCores({0}) ({1}): {2}",
                                          result.Pdu.VbList[7].Oid.ToString(),
                                          SnmpConstants.GetTypeName(result.Pdu.VbList[7].Value.Type),
                                          result.Pdu.VbList[7].Value.ToString());
                                        Console.WriteLine("catNumVxmlCores({0}) ({1}): {2}",
                                          result.Pdu.VbList[8].Oid.ToString(),
                                          SnmpConstants.GetTypeName(result.Pdu.VbList[8].Value.Type),
                                          result.Pdu.VbList[8].Value.ToString());
                                        Console.WriteLine("catNumMph6AudioCores({0}) ({1}): {2}",
                                          result.Pdu.VbList[10].Oid.ToString(),
                                          SnmpConstants.GetTypeName(result.Pdu.VbList[10].Value.Type),
                                          result.Pdu.VbList[10].Value.ToString());
                                        Console.WriteLine("catNumMph6HdEncCores({0}) ({1}): {2}",
                                          result.Pdu.VbList[11].Oid.ToString(),
                                          SnmpConstants.GetTypeName(result.Pdu.VbList[11].Value.Type),
                                          result.Pdu.VbList[11].Value.ToString());
                                        if (mscardtype == "MPC-VI")
                                        {
                                            numaudiodspsnmp = result.Pdu.VbList[10].Value.ToString();
                                            numhddspsnmp = result.Pdu.VbList[11].Value.ToString();
                                            numvideodspsnmp = Convert.ToString(160 - (Convert.ToInt16(numaudiodspsnmp) + Convert.ToInt16(numhddspsnmp)));
                                            numtotaldspsnmp = Convert.ToString(Convert.ToInt16(numvideodspsnmp) + Convert.ToInt16(numhddspsnmp) + Convert.ToInt16(numaudiodspsnmp));
                                        }
                                        else
                                        {
                                            numtotaldspsnmp = result.Pdu.VbList[5].Value.ToString();
                                            numhddspsnmp = result.Pdu.VbList[7].Value.ToString();
                                            numvideodspsnmp = result.Pdu.VbList[6].Value.ToString();
                                            numaudiodspsnmp = Convert.ToString(Convert.ToInt16(numtotaldspsnmp) - (Convert.ToInt16(numhddspsnmp) + Convert.ToInt16(numvideodspsnmp)));

                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No response received from SNMP agent.");
                                }


                            }
                            catch (SnmpNetworkException se)
                            {
                                MessageBox.Show("Exception in function snmpget() while Making SNMP request", se.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine(SipMSIPAddressSCC + " is not pingble");
                            //MessageBox.Show(SipMSIPAddressSCC + " is not pingble. Exception while retriving core allocation thru SNMP");
                        }

                        target.Close();
                    }
                }
                catch (SystemException Se)
                {
                    MessageBox.Show(Se.Message, "Exception while retriving core allocation thru SNMP");
                }
            }
        }


    }

}

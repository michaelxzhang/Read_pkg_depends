using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace readpkg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string resfile = @"c:\temp\pkglist.txt";
            string filename = @textBox1.Text + @"\dpkg_1.16.1.2volcano7.2.orig\debian\control";
            //string filename = @"c:\temp\pkgtest\dpkg_1.16.1.2volcano7.2.orig\debian\control";

            StreamWriter sw = new StreamWriter(resfile);

            string[] subdirectoryEntries = Directory.GetDirectories(@textBox1.Text);

            foreach (string subdirectory in subdirectoryEntries)
            {
                if(checkBox1.Checked)
                    filename = @subdirectory + @"\debian\control";
                else
                    filename = @subdirectory + @"\control";

                if (File.Exists(filename) == true)
                {
                    string[] filelines = System.IO.File.ReadAllLines(filename);

                    string valuestr = "";
                    int cont = 0;

                    for (int cnt = 0; cnt < filelines.Length; cnt++)
                    {
                        string tmpline = filelines[cnt].Trim();

                        if (tmpline.IndexOf("Source:") >= 0)
                        {
                            valuestr = tmpline;
                        }
                        else if (tmpline.IndexOf("Build-Depends:") >= 0)
                        {
                            if (tmpline[tmpline.Length - 1] == ',')
                            {
                                cont = 1;
                                valuestr = valuestr + "\t" + tmpline;
                            }
                            else
                            {
                                valuestr = valuestr + "\t" + tmpline;
                                sw.WriteLine(valuestr);
                            }
                        }
                        else if (cont == 1)
                        {
                            if (tmpline[tmpline.Length - 1] == ',')
                            {
                                cont = 1;
                                valuestr = valuestr + tmpline;
                            }
                            else
                            {
                                cont = 0;
                                valuestr = valuestr + tmpline;
                                sw.WriteLine(valuestr);
                            }
                        }
                    }
                }
            }
            sw.Close();
        }
    }
}

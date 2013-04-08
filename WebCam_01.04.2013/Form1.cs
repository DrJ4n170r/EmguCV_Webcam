using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Threading;
using System.Reflection;

namespace WebCam_01._04._2013
{
    public partial class Form1 : Form
    {
        #region Properties

        Dictionary<string, IImage> classDict;
        List<IImage> classActions;

        Emgu.CV.Capture capture;
        Emgu.CV.Image<Bgr, Byte> image;

        #endregion Properties

        #region MainStuff
        public Form1()
        {
            InitializeComponent();

            capture = new Emgu.CV.Capture();
            image = new Image<Bgr, byte>(capture.Width, capture.Height);

            classDict = new Dictionary<string, IImage>();
            classActions = new List<IImage>();

            classDict.Add("Mirror Vertically", new MirrorVertically());
            classDict.Add("Threshold", new Threshold());

            listBox1.Items.Add("Mirror Vertically");
            listBox1.Items.Add("Threshold");

            // Event stuff f. listbox1
            listBox1.AllowDrop = true;
            listBox1.DragDrop += listBox1_DragDrop;
            listBox1.MouseUp += listBox1_MouseUp;
            listBox1.MouseDown += listBox1_MouseDown;
            listBox1.DragOver += listBox1_DragOver;
            
            // Event stuff f. listbox2
            listBox2.AllowDrop = true;
            listBox2.DragDrop += listBox2_DragDrop;
            listBox2.MouseUp += listBox2_MouseUp;
            //listBox2.MouseDown += listBox2_MouseDown;
            listBox2.DragOver += listBox2_DragOver;
            
            Application.Idle += Application_Idle;

        }

        // Application Idle
        void Application_Idle(object sender, EventArgs e)
        {
            image = capture.QueryFrame();

            /*foreach (var v in actions)
            {
                v.DynamicInvoke();
            }*/
            foreach (IImage v in classActions)
            {
                v.DoImageStuff(ref image);
            }
            imageBox1.Image = image;
        }

        #endregion MainStuff

        #region EventHandlers

        void listBox1_DragOver(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.All;
        }

        void listBox2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.Items.Count == 0)
            { return; }
            int index = listBox1.IndexFromPoint(e.X, e.Y);
            if (index >= 0)
            {
                string s = listBox1.Items[index].ToString();
                DragDropEffects dde1 = DoDragDrop(s, DragDropEffects.Copy);
            }
        }

        void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Left)
            {
                if (listBox2.Items.Count == 0)
                { return; }
                int index = listBox2.IndexFromPoint(e.X, e.Y);
                if (index >= 0)
                {
                    //actions[index].
                }
            }*/
        }

        void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            /*int index = listBox1.IndexFromPoint(e.X, e.Y);
            if (index >= 0 && listBox1.Items.Contains(listBox1.Items[index]))
            {
                listBox1.Items.Add(sender.ToString());
            }*/
        }
        
        void listBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox2.IndexFromPoint(e.X, e.Y);
                if (index >= 0)
                {
                    //actions.RemoveAt(index);
                    classActions.RemoveAt(index);
                    listBox2.Items.RemoveAt(index);
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                int index = listBox2.IndexFromPoint(e.X, e.Y);
                if (index >= 0)
                {
                    if (classActions[index].hasDialog)
                    {
                        classActions[index].ShowDialog();
                    }
                }
            }
        }

        void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            // Not allowed...
            /*
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(DataFormats.StringFormat);
                listBox1.Items.Add(str);
            }
            */
        }

        void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            if(listBox1.SelectedItem == null || listBox1.SelectedItem.ToString() == "")
            {
                return;
            }


            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                int index = listBox2.IndexFromPoint(e.X, e.Y);
                string stuff = (string)e.Data.GetData(DataFormats.StringFormat);
                
                listBox2.Items.Add(stuff);
                //actions.Add(dict[stuff]);
                classActions.Add(classDict[stuff]);
            }
        }

        private void imageBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else
            {
                Form1_KeyDown(null, null);
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else
            {
                Form1_KeyDown(null, null);
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else
            {
                Form1_KeyDown(null, null);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Why didn't I get here before...?");
            IImage mV = new MirrorVertically();
            mV.ShowDialog();
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox2.IndexFromPoint(e.X, e.Y);
            int stuff = (sender as ListBox).IndexFromPoint(e.X, e.Y);
        }

        #endregion EventHandlers

    }
}

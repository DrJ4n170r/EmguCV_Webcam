using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCam_01._04._2013
{
    public class Threshold : IImage
    {
        public int R_low = 0, G_low = 0, B_low = 0;
        public int R_high = 255, G_high = 255, B_high = 255;

        private const bool _hasDialog = true;
        public bool hasDialog { get { return _hasDialog; } }
        private DialogThreshold dlgM;
        public Threshold()
        {
            dlgM = new DialogThreshold();
        }

        public void DoImageStuff(ref Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image)
        {
            image = image.Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
            image._EqualizeHist();
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>[] Bgr = image.Split();
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> R = Bgr[2];
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> G = Bgr[1];
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> B = Bgr[0];

            R = R.ThresholdToZero(new Gray((double)R_low));
            R = R.ThresholdToZeroInv(new Gray((double)R_high));
            G = G.ThresholdToZero(new Gray((double)G_low));
            G = G.ThresholdToZeroInv(new Gray((double)G_high));
            B = B.ThresholdToZero(new Gray((double)B_low));
            B = B.ThresholdToZeroInv(new Gray((double)B_high));

            Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> tmp = new Image<Emgu.CV.Structure.Bgr, byte>(new Image<Gray, byte>[] { B, G, R });
            image = tmp;

        }

        public void ShowDialog()
        {
            dlgM.ShowDialog();
            if (dlgM.response == DialogResult.OK)
            {
                R_low = dlgM.R_low;
                G_low = dlgM.G_low;
                B_low = dlgM.B_low;
                R_high = dlgM.R_high;
                G_high = dlgM.G_high;
                B_high = dlgM.B_high;
            }
            else if (dlgM.response == DialogResult.Cancel)
            {
                // Do nothing...
            }
        }
    }
}

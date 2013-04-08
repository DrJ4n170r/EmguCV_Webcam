using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCam_01._04._2013
{
    public class EqualizeHistogram : IImage
    {
        public const bool _hasDialog = false;
        bool IImage.hasDialog
        {
            get { return _hasDialog; }
        }

        void IImage.DoImageStuff(ref Image<Bgr, byte> image)
        {
            image._EqualizeHist();
        }

        void IImage.ShowDialog()
        {
            // Does nothing
        }
    }
}

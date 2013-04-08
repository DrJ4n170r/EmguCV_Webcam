using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCam_01._04._2013
{
    public interface IImage
    {
        bool hasDialog { get; }
        void DoImageStuff(ref Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image);
        void ShowDialog();
    }
}

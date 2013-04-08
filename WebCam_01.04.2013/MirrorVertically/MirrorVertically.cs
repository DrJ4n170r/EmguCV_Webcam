using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Forms;

namespace WebCam_01._04._2013
{
    public class MirrorVertically : IImage
    {
        private const bool _hasDialog = false;
        public bool hasDialog { get { return _hasDialog; } }

        public MirrorVertically()
        {
        }

        public void DoImageStuff(ref Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image)
        {
            image = image.Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
        }

        public void ShowDialog()
        {
            // Do nothing
        }
    }
}

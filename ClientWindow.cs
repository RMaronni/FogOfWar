using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FogOfWar
{
    public partial class ClientWindow : Form
    {
        private Image ImageSource = null;
        private Image ImageShadow = null;
        public ClientWindow()
        {
            InitializeComponent();
        }

        public void SetImages(Image imageSource)
        {
            ImageSource = imageSource;
            ImageShadow = new Bitmap(imageSource);
            ImageShadow = MapImage.ShadowImageBlack(ImageShadow);
            PictureBox.Image = new Bitmap(ImageShadow);
        }

        public void UpdateMap(Point startingPoint, Point endingPoint)
        {
            Image map = PictureBox.Image;
            if (map != null)
            {
                map = MapImage.ShowRectangle(map, ImageSource, startingPoint, endingPoint);
                PictureBox.Image = map;
                Console.WriteLine("Client");
            }
        }

        public void ResetMap()
        {
            PictureBox.Image = ImageShadow;
        }
    }
}

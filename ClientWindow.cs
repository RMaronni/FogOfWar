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
    /// <summary>
    /// Class <c>ClientWindow</c> is the window displayed to the players.
    /// </summary>
    public partial class ClientWindow : Form
    {
        private Image ImageSource = null;
        private Image ImageShadow = null;

        /// <summary>
        /// Constructor method.
        /// </summary>
        public ClientWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method <c>SetImages</c> sets shadow and source image properties.
        /// </summary>
        public void SetImages(Image imageSource)
        {
            ImageSource = imageSource;
            ImageShadow = new Bitmap(imageSource);
            ImageShadow = MapImage.ShadowImageBlack(ImageShadow);
            PictureBox.Image = new Bitmap(ImageShadow);
        }

        /// <summary>
        /// Method <c>UpdateMap</c> updates the map with the new revealed area.
        /// </summary>
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

        /// <summary>
        /// Method <c>ResetMap</c> resets the map to a total hidden map.
        /// </summary>
        public void ResetMap()
        {
            PictureBox.Image = ImageShadow;
        }
    }
}

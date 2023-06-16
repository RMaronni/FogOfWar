using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FogOfWar
{
    /// <summary>
    /// Class <c>MainWindow</c> is the main window where the Dungeon Master defines the map image and the areas to be displayed.
    /// </summary>
    public partial class MainWindow : Form
    {
        private bool IsDrawing = false;
        private Point StartingPoint = Point.Empty;
        private Image ImageSource = null;
        private Image ImageShadow = null;
        private ClientWindow Client = null;


        /// <summary>
        /// Constructor method.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Client = new ClientWindow();
            Client.Show();
        }


        /// <summary>
        /// Method <c>LoadMapMenuItem_Click</c> sets the Click event to LoadMapMenuItem control.
        /// </summary>
        private void LoadMapMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "jpg files (*.jpg)|*.jpg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SetImages(openFileDialog.FileName);
                    PictureBox.Image = new Bitmap(ImageShadow);
                }
            }
        }


        /// <summary>
        /// Method <c>ResetMapMenuItem_Click</c> sets the Click event to ResetMapMenuItem control.
        /// </summary>
        private void ResetMapMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox.Image = ImageShadow;
            Client.ResetMap();
        }


        /// <summary>
        /// Method <c>QuitMenuItem_Click</c> sets the Click event to QuitMenuItem control.
        /// </summary>
        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// Method <c>PictureBox_MouseDown</c> sets the MouseDown event to PictureBox control.
        /// </summary>
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrawing = true;
            StartingPoint = e.Location;
            Console.WriteLine(StartingPoint);
        }


        /// <summary>
        /// Method <c>PictureBox_MouseUp</c> sets the MouseUp event to PictureBox control.
        /// </summary>
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {    
            if(IsDrawing)
            {
                Image map = PictureBox.Image;
                if (map != null)
                {
                    Point startingPoint = GetScaledImageLocation(StartingPoint);
                    Point endingPoint = GetScaledImageLocation(e.Location);
                    map = MapImage.ShowRectangle(map, ImageSource, startingPoint, endingPoint);
                    PictureBox.Image = map;
                    Client.UpdateMap(startingPoint, endingPoint);
                }
                IsDrawing = false;
                Console.WriteLine(e.Location);
            }
        }


        /// <summary>
        /// Method <c>GetScaledImageLocation</c> gets the pixel on the image associated to a pixel on the control PictureBox.
        /// </summary>
        private Point GetScaledImageLocation(Point location)
        {
            double imgWidth = PictureBox.Image.Width;
            double imgHeight = PictureBox.Image.Height;
            double boxWidth = PictureBox.Size.Width;
            double boxHeight = PictureBox.Size.Height;

            double X = location.X;
            double Y = location.Y;
            double scale;

            if (imgWidth/imgHeight > boxWidth/boxHeight)
            {
                scale = boxWidth/imgWidth;
                double blankPart = (boxHeight - scale*imgHeight) / 2;
                Y -= blankPart;
            }
            else
            {
                scale = boxHeight/imgHeight;
                double blankPart = (boxWidth - scale*imgWidth) / 2;
                X -= blankPart;
            }

            X /= scale;
            Y /= scale;

            return new Point((int)Math.Round(X), (int)Math.Round(Y));
        }


        /// <summary>
        /// Method <c>SetImages</c> sets shadow and source image properties.
        /// </summary>
        private void SetImages(string filePath)
        {
            ImageSource = new Bitmap(filePath);
            ImageShadow = new Bitmap(filePath); 
            ImageShadow = MapImage.ShadowImageGray(ImageShadow);
            Client.SetImages(ImageSource);
        }
    }
}

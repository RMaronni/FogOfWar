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
    public partial class MainWindow : Form
    {
        private bool IsDrawing = false;
        private Point StartingPoint = Point.Empty;
        private Image ImageSource = null;
        private Image ImageShadow = null;
        private ClientWindow Client = null;
        
        

        public MainWindow()
        {
            InitializeComponent();
            Client = new ClientWindow();
            Client.Show();
        }


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

        private void ResetMapMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox.Image = ImageShadow;
            Client.ResetMap();
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrawing = true;
            StartingPoint = e.Location;
            Console.WriteLine(StartingPoint);
        }
            
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


        private void SetImages(string filePath)
        {
            ImageSource = new Bitmap(filePath);
            ImageShadow = new Bitmap(filePath); 
            ImageShadow = MapImage.ShadowImageGray(ImageShadow);
            Client.SetImages(ImageSource);
        }

        
    }
}

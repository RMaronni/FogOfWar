using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogOfWar
{
    /// <summary>
    /// Class <c>MapImage</c> defines methods to manipulate the map image.
    /// </summary>
    internal class MapImage
    {
        /// <summary>
        /// Method <c>ShadowImage</c> converts the map image to a shadowed map.
        /// </summary>
        private static Image ShadowImage(Image img, int alpha)
        {
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            using (Graphics graph = Graphics.FromImage(img))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
                {
                    graph.FillRectangle(brush, rect);
                }
            }

            return img;
        }

        /// <summary>
        /// Method <c>ShadowImageGray</c> converts the map image to a gray shadowed map.
        /// </summary>
        public static Image ShadowImageGray(Image img) => ShadowImage(img, 128);

        /// <summary>
        /// Method <c>ShadowImageBlack</c> converts the map image to a black shadowed map.
        /// </summary>
        public static Image ShadowImageBlack(Image img) => ShadowImage(img, 255);

        /// <summary>
        /// Method <c>ShowRectangle</c> sets a rectangle on the map to be displayed to the players.
        /// </summary>
        public static Image ShowRectangle(Image map, Image texture, Point startingPoint, Point endingPoint)
        {
            Size rectangleSize = new Size(endingPoint.X - startingPoint.X, endingPoint.Y - startingPoint.Y);
            Rectangle rectangle = new Rectangle(startingPoint, rectangleSize);

            using (Graphics graph = Graphics.FromImage(map))
            {
                using (TextureBrush brush = new TextureBrush(texture))
                {
                    graph.FillRectangle(brush, rectangle);
                }
            }
            return map;
        }
    }
}

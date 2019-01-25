using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    class Query
    {
        private Bitmap Image {
            get
            {
                if (Image == null) {
                    Image = new Bitmap(this.ImagePath);
                }
                return Image;
            }
            set { }
        }
        private string ImagePath;
        private Rectangle Area;
        private int Tolerance = 20;

        public Query(string imagePath, int[] area, int tolerance)
        {
            if (area.Length != 4)
            {
                throw new ArgumentException("Capture area requires exactly 4 values: StartX, StartY, EndX, EndY");
            }
            ImagePath = imagePath;
            Area = new Rectangle(area[0], area[1], area[2], area[3]);
            this.Tolerance = tolerance;
        }

        public Query(string imagePath, int[] area) : this(imagePath, area, 20)
        {            
        }
    }
}

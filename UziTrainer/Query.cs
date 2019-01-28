using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UziTrainer
{
    class Query
    {
        private Bitmap _Image;
        public  Bitmap Image {
            get
            {
                if (_Image == null) {
                    var root = Path.GetDirectoryName(Application.ExecutablePath);
                    _Image = new Bitmap(Path.Combine(root, ImagePath));
                }
                return _Image;
            }
            private set { }
        }
        public string ImagePath {
            get;
            private set;
        }
        public Rectangle Area {
            get;
            private set;
        }
        public int Tolerance = 20;

        public Query(string imagePath, int[] area, int tolerance)
        {
            if (area.Length != 4)
            {
                throw new ArgumentException("Capture area requires exactly 4 values: StartX, StartY, EndX, EndY");
            }
            ImagePath = imagePath;
            Area = new Rectangle(area[0], area[1], area[2] - area[0], area[3] - area[1]);
            Tolerance = tolerance;
        }

        public Query(string imagePath, int[] area) : this(imagePath, area, 20)
        {            
        }

        public Query(string imagePath) : this(imagePath, new[] { 0, 0, 1280, 720}, 20)
        {
        }
    }
}

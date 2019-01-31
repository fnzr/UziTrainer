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
    public class Query
    {
        private static readonly string[] extensions = new[] { ".png", ".jpg", ".jpeg" };
        private Bitmap _Image;
        public  Bitmap Image {
            get
            {
                if (_Image == null) {
                    var filePath = Path.Combine("../../assets", ImagePath);
                    string fullPath;
                    foreach (var ext in Query.extensions)
                    {
                        fullPath = filePath + ext;
                        if (File.Exists(fullPath)) {
                            _Image = new Bitmap(fullPath);
                            return _Image;
                        }
                    }
                    throw new ArgumentException("[{0}] not found", ImagePath);
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
        private String assetsRoot;

        public Query(string assetsRoot)
        {
            this.assetsRoot = assetsRoot;
        }

        private Query(string imagePath, int[] area, int tolerance)
        {
            if (area.Length != 4)
            {
                throw new ArgumentException("Capture area requires exactly 4 values: StartX, StartY, EndX, EndY");
            }
            ImagePath = imagePath;
            Area = new Rectangle(area[0], area[1], area[2] - area[0], area[3] - area[1]);
            Tolerance = tolerance;
        }

        public Query Create(string imagePath, int[] area, int tolerance)
        {
            return new Query(Path.Combine(assetsRoot, imagePath), area, tolerance);
        }

        public Query Create(string imagePath, int[] area)
        {
            return Create(imagePath, area, 20);
        }

        public Query Create(string imagePath)
        {
            return Create(imagePath, new[] { 0, 0, 1280, 720 }, 20);
        }
    }
}

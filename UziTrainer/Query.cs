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
        public static string assetsRoot = "../../assets";
        private Bitmap _Image;
        public  Bitmap Image {
            get
            {
                if (_Image == null) {                    
                    string fullPath;
                    foreach (var ext in Query.extensions)
                    {
                        fullPath = Path.Combine(assetsRoot, ImagePath + ext);
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
        private string subdir;

        public Query(string subdir)
        {
            this.subdir = subdir;
        }

        private Query(string imagePath, Rectangle area, int tolerance)
        {
            ImagePath = imagePath;
            Area = area;
            Tolerance = tolerance;
        }

        public Query Create(string imagePath, Rectangle area, int tolerance)
        {
            return new Query(Path.Combine(subdir, imagePath), area, tolerance);
        }

        public Query Create(string imagePath, Rectangle area)
        {
            return Create(imagePath, area, 20);
        }

        public Query Create(string imagePath)
        {
            return Create(imagePath, Window.FullArea, 20);
        }
    }
}

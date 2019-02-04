using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;

namespace UziTrainer
{
    public class Query
    {
        private static readonly string[] extensions = new[] { ".png", ".jpg", ".jpeg" };
        public static string assetsRoot = "./assets";
        private Image<Rgba, byte> _Image;
        public Image<Rgba, byte> Image {
            get
            {
                if (_Image == null) {                    
                    string fullPath;
                    foreach (var ext in Query.extensions)
                    {
                        fullPath = Path.Combine(assetsRoot, ImagePath + ext);
                        if (File.Exists(fullPath)) {
                            _Image = new Image<Rgba, byte>(fullPath);
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
        public float Tolerance;
        public bool Debug
        {
            get;
            private set;
        }

        public Query(string imagePath, Rectangle area, float tolerance, bool debug = false)
        {
            ImagePath = imagePath;
            Area = area;
            Tolerance = tolerance;
            Debug = debug;
        }

        public Query(string imagePath, Rectangle area, bool debug = false) : this(imagePath, area, .95f, debug)
        {
        }

        public Query(string imagePath, float tolerance, bool debug = false) : this(imagePath, Window.FullArea, tolerance, debug)
        {
        }

        public Query(string imagePath, bool debug = false) : this(imagePath, Window.FullArea, debug)
        {
        }
    }
}

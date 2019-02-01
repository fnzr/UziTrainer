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
        public static string assetsRoot = "../../assets";
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
        public float Tolerance = .9f;

        private Query(string imagePath, Rectangle area, float tolerance)
        {
            ImagePath = imagePath;
            Area = area;
            Tolerance = tolerance;
        }

        public static Query Create(string imagePath, Rectangle area, float tolerance)
        {
            return new Query(imagePath, area, tolerance);
        }

        public static Query Create(string imagePath, Rectangle area)
        {
            return Create(imagePath, area, .9f);
        }

        public static Query Create(string imagePath, float tolerance)
        {
            return Create(imagePath, Window.FullArea, tolerance);
        }

        public static Query Create(string imagePath)
        {
            return Create(imagePath, Window.FullArea);
        }
    }
}

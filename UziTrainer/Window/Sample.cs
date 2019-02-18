using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer.Window
{
    public class Sample
    {
        public readonly static Sample Negative = new Sample("", new Rectangle());
        string _Name;
        public string Name { get { return _Name; }
            set {
                _Name = value;
                if (_Image != null)
                {
                    _Image.Dispose();
                    _Image = null;
                }
            }}
        public Rectangle SearchArea { get; private set; }
        public float Threshold { get; private set; }
        public Sample Next { get; private set; }

        public Sample(string name, Rectangle searchArea, Sample next = null, float threshold = .95f)
        {
            Name = name;
            SearchArea = searchArea;
            Threshold = threshold;
            Next = next;
        }

        static readonly string[] extensions = new[] { ".png", ".jpg", ".jpeg" };
        const string assetsRoot = "./assets";
        Image<Rgba, byte> _Image;
        public Image<Rgba, byte> Image
        {
            get
            {
                if (_Image == null)
                {
                    string fullPath;
                    foreach (var ext in extensions)
                    {
                        fullPath = Path.Combine(assetsRoot, Name + ext);
                        if (File.Exists(fullPath))
                        {
                            _Image = new Image<Rgba, byte>(fullPath);
                            return _Image;
                        }
                    }
                    throw new ArgumentException($"[{Name}] File does not exists");
                }
                return _Image;
            }
            private set { }
        }

        public Point RelativeTo(Point point)
        {
            return new Point(SearchArea.X + point.X, SearchArea.Y + point.Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer.Window
{
    public class Button : Sample
    {
        Func<Point, Rectangle> FnClickArea;
        public Button(string name, Rectangle searchArea, Sample next, float threshold = .95f, Func<Point, Rectangle> fnClickArea = null) : base(name, searchArea, next, threshold)
        {
            FnClickArea = fnClickArea;
        }

        public Rectangle ClickArea(Point foundCoordinates)
        {
            return FnClickArea == null ? SearchArea : FnClickArea(foundCoordinates);
        }
    }
}

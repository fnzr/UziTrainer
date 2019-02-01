using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace UziTrainer
{
    public class ImageSearch
    {

        public static bool FindPoint(Query query, out Point coordinates)
        {                   
            var found = Search(query.Image, Window.CaptureBitmap(query.Area), query.Tolerance, out coordinates);
            if (found)
            {
                coordinates.X += query.Area.X;
                coordinates.Y += query.Area.Y;
            }            
            if (query.Debug)
            {
                Trace.WriteLine("Starting debug");
                var position = coordinates;
                var debugForm = new ImageSearchForm(query);
                var thread = new Thread(new ThreadStart(delegate(){
                    debugForm.Show();
                    debugForm.SetFoundAt(position);
                    debugForm.DebugArea(query.Area, Color.Fuchsia);
                    if (found)
                    {
                        debugForm.DebugArea(new Rectangle(position, query.Image.Size), Color.Green);
                    }
                    Application.Run(debugForm);
                }));
                thread.Start();
                Trace.WriteLine("Waiting continue message");
                debugForm.DebugThread.WaitOne();
                debugForm.Dispose();
            }
            return found;
        }

        private static bool Search(Image<Rgba, byte> needle, Image<Rgba, byte> haystack, float threshold, out Point coordinates)
        {
            using (Image<Gray, float> result = haystack.MatchTemplate(needle, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] maxValues;
                Point[] maxLocations;
                result.MinMax(out _, out maxValues, out _, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] >= threshold)
                {
                    coordinates = maxLocations[0];
                    return true;
                }
            }
            coordinates = Point.Empty;
            return false;
        }
    }
}

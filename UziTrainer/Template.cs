using Emgu.CV;
using Emgu.CV.Structure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    public class Template
    {
        static readonly string TemplateFile = Properties.Settings.Default.Template_File;
        static JObject TemplateData;        

        public readonly Samples Name;
        public readonly Rectangle SearchArea;
        public readonly Rectangle ClickArea;
        public readonly Image<Rgba, byte> Image;
        public readonly Samples? Next;        
        static Template()
        {            
            if (File.Exists(TemplateFile))
            {
                TemplateData = JObject.Parse(File.ReadAllText(TemplateFile));
            }
            else
            {
                TemplateData = new JObject();
            }
        }

        public Template(Samples name, Rectangle searchArea, Bitmap bitmap, Rectangle clickArea, Samples? next)
        {
            Name = name;
            SearchArea = searchArea;
            ClickArea = clickArea;
            Image = new Image<Rgba, byte>(bitmap);
            Next = next;
        }

        public static void Add(Samples sample, Samples? next, Bitmap image, Rectangle searchArea, Rectangle clickArea)
        {            
            var template = new Template(sample, searchArea, image, clickArea, next);
            TemplateData[sample.ToString()] = JToken.FromObject(new TemplateSerial(template));
            File.WriteAllText(TemplateFile, TemplateData.ToString());
        }

        public static Template Get(Samples sample)
        {
            var name = sample.ToString();
            if (!TemplateData.ContainsKey(name))
            {
                var form = new FormImageCreator(sample);
                form.ShowDialog();
            }
            var serial = TemplateData[sample.ToString()].ToObject<TemplateSerial>();
            return serial.ToTemplate(sample);
        }

        static void Save()
        {
            
        }
    }
    class TemplateSerial
    {
        public int[] click_area;
        public int[] search_area;        
        public string next;
        public string image;

        public TemplateSerial()
        {

        }

        public Template ToTemplate(Samples name)
        {
            var cArea = click_area;
            var clickArea = new Rectangle(cArea[0], cArea[1], cArea[2], cArea[3]);

            var sArea = search_area;
            var searchArea = new Rectangle(sArea[0], sArea[1], sArea[2], sArea[3]);

            var byteArray = Convert.FromBase64String(image);
            Bitmap bmp;
            using (var ms = new MemoryStream(byteArray))
            {
                bmp = new Bitmap(ms);
            }            
            Samples? nextSample;
            if (string.IsNullOrEmpty(next))
            {
                nextSample = null;
            }
            else
            {
                nextSample = (Samples)Enum.Parse(typeof(Samples), next);
            }
            return new Template(name, searchArea, bmp, clickArea, nextSample);
        }

        public TemplateSerial(Template template)
        {
            var cArea = template.ClickArea;
            click_area = new int[] { cArea.X, cArea.Y, cArea.Width, cArea.Height };

            var sArea = template.SearchArea;
            search_area = new int[] { sArea.X, sArea.Y, sArea.Width, sArea.Height };

            using (var ms = new MemoryStream())
            {
                template.Image.Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                image = Convert.ToBase64String(ms.GetBuffer());
            }

            next = template.Next.ToString();
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    class Template
    {
        public static readonly Dictionary<string, Template> Dict = new Dictionary<string, Template>();
        public Rectangle SearchArea { get; set; }
        public string Data { get; set; }

        public Template()
        {

        }

        public Template(Rectangle search, string data)
        {
            this.SearchArea = search;
            this.Data = data;
        }

        public static void LoadDict()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "UziTrainer.templates.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var loaded = JToken.ReadFrom(new JsonTextReader(reader)).ToObject<Dictionary<string, Template>>();
                Dict.Concat(loaded);
            }
        }

        public static Template Get(string name)
        {
            if (!Dict.ContainsKey(name))
            {
                var form = new FormImage(name, new System.Drawing.Bitmap(@"C:\temp\screen.png"));
                form.ShowDialog();
            }
            return Dict[name];
        }

        public static void Add(string name, Template template)
        {
            Dict.Add(name, template);
        }
    }
}

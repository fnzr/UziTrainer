
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace UziTrainer
{
    class Doll
    {
        public string Rarity;
        public string Name;
        public string Type;
        private static JObject _rss;
        private static JObject rss
        {
            get {
                if (_rss == null)
                {
                    using (StreamReader reader = File.OpenText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "dolls.json")))
                    {
                        _rss = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    }
                }
                return _rss;
            }
            set { }
        }

        private Doll() { }

        public Doll(string rarity, string name, string type)
        {
            Rarity = rarity;
            Name = name;
            Type = type;
        }

        public static Doll Get(string name)
        {
            //JObject rss = JObject.Parse(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "dolls.json"));
            return ((JObject)rss[name]).ToObject<Doll>();
        }
    }
}

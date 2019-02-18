using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace UziTrainer
{
    public class Doll
    {
        public string Rarity;
        public string Name;
        public string Type;
        private static JObject _rss;
        private static JObject rss
        {
            get
            {
                if (_rss == null)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = "UziTrainer.dolls.json";
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        _rss = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    }
                }
                return _rss;
            }
            set { }
        }

        private Doll() { }

        public static readonly Doll Empty = new Doll();

        public Doll(string rarity, string name, string type)
        {
            Rarity = rarity;
            Name = name;
            Type = type;
        }

        public static Doll Get(string name)
        {
            //JObject rss = JObject.Parse(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "dolls.json"));
            try
            {
                Doll doll = ((JObject)rss[name]).ToObject<Doll>();
                doll.Name = name;
                return doll;
            }
            catch (System.NullReferenceException)
            {
                return Doll.Empty;
            }
        }
    }
}

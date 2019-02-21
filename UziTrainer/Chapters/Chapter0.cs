using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UziTrainer.Scenes;
using UziTrainer.Window;

namespace UziTrainer.Chapters
{
    class Chapter0 : Chapter
    {
        public static readonly Button DeployEchelonButton = new Button("Combat/DeployOK", new Rectangle(1106, 653, 148, 53), Combat.SanityCheck);
        private readonly string root;
        public Chapter0(Screen screen, string mission) : base(screen)
        {
            root = $"Missions/{mission}/";
        }

        public void Map0_2()
        {
            Button CommandPost = new Button(root + "CommandPost", new Rectangle(589, 334, 732, 453), DeployEchelonButton);
        }
    }
}

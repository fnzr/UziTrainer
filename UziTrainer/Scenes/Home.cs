using System.Drawing;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Home
    {
        public static readonly Sample LvSample = new Sample("HomePage/LV", new Rectangle(33, 49, 24, 19));
        public static readonly Button FormationButton = new Button("HomePage/Formation", new Rectangle(1064, 446, 194, 77), Formation.FormationScene);
        public static readonly Button CombatButton = new Button("HomePage/Combat", new Rectangle(835, 460, 185, 76), Combat.CombatScene);
    }
}

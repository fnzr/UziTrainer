using System.Drawing;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Formation
    {
        public static readonly Rectangle ReturnToBase = new Rectangle(14, 16, 85, 55);
        public static readonly Sample FormationScene = new Sample("FormationPage/FormationPage", new Rectangle(154, 19, 36, 53));
        public static readonly Sample FilterActiveSample = new Sample("FormationPage/FilterActive", new Rectangle(939, 241, 110, 36));
        public static readonly Button FilterResetButton = new Button("FormationPage/Reset", new Rectangle(460, 562, 190, 31), Sample.Negative);
        public static readonly Button FilterDollButton = new Button("FormationPage/Filter", new Rectangle(937, 209, 131, 67), FilterResetButton);
        public static readonly Button DollFormationButton = new Button("", new Rectangle(124, 246, 765, 282), FilterDollButton, .85f, SlotArea);

        public static readonly Button FilterConfirmButton = new Button("FormationPage/FilterConfirm", new Rectangle(693, 558, 186, 35), Sample.Negative);
        public static readonly Button DollSelectButton = new Button("", new Rectangle(4, 89, 909, 719), FormationScene, .74f, SelectDollArea);
        public static readonly Button FilterOptionButton = new Button("", new Rectangle(450, 119, 456, 355),
            new Button("", new Rectangle(450, 119, 456, 355), null), .98f, FilterOptionArea);
        public static readonly Sample EchelonClickedScene = new Sample("", new Rectangle(2, 126, 110, 667));
        public static readonly Button EchelonButton = new Button("", new Rectangle(2, 126, 110, 667), EchelonClickedScene, .95f, EchelonClickArea);

        private static Rectangle EchelonClickArea(Point arg)
        {
            int echelon = (arg.Y - 140) / 80;
            int y = 120 + (80 * echelon);
            return new Rectangle(5, y, 80, 60);
        }

        private static Rectangle FilterOptionArea(Point arg)
        {
            var filterXSize = 150;
            var filterYSize = 60;
            int y;
            int line;
            if (arg.Y > 290) // Filter Type
            {
                line = (arg.Y - 290 - 5) / filterYSize;
                y = 330 + (filterYSize * line - 1);
            }
            else // Filter Rarity
            {
                line = (arg.Y - 125 - 5) / filterYSize;
                y = 125 + (filterYSize * line - 1);
            }
            var column = (arg.X - 450 - 10) / filterXSize;
            var x = 450 + (column * filterXSize) + ((column + 1) * 15);
            return new Rectangle(new Point(x, y), new Size(100, 35));
        }

        private static Rectangle SelectDollArea(Point arg)
        {
            var slotXSize = 140;
            int line = (arg.Y - 95) / 260;
            var y = 100 + (line * 260);
            int column = (arg.X - 10) / slotXSize;

            var x = 15 + (column * slotXSize) + (column * 15);
            return new Rectangle(new Point(x, y), new Size(100, 150));
        }

        private static Rectangle SlotArea(Point foundAt)
        {
            var slotXSize = 150;
            int slot = foundAt.X / slotXSize;
            var x = 135 * slot + (20 * (slot - 1));
            return new Rectangle(x, 250, 130, 280);
        }

        readonly Screen screen;

        public Formation(Screen screen)
        {
            this.screen = screen;
        }

        public void SelectDoll(Doll doll)
        {
            DollSelectButton.Name = "Dolls/" + doll.Name;
            if (screen.Exists(DollSelectButton, 1000))
            {
                screen.Click(DollSelectButton);
                return;
            }
            var filterType = new Button("FormationPage/Filter" + doll.Type, new Rectangle(453, 305, 451, 147),
            new Button($"FormationPage/Filter{doll.Type}Clicked", new Rectangle(453, 305, 451, 147), null), .95f, FilterOptionArea);
            var filterRarity = new Button("FormationPage/Filter" + doll.Rarity, new Rectangle(453, 124, 456, 151),
            new Button($"FormationPage/Filter{doll.Rarity}Clicked", new Rectangle(453, 124, 456, 151), null), .95f, FilterOptionArea);
            screen.Click(FilterDollButton);
            if (screen.Exists(FilterActiveSample, 1000))
            {
                if (!(screen.Exists(filterType.Next, 0) && screen.Exists(filterRarity.Next)))
                {
                    screen.Click(FilterResetButton);
                    screen.Click(FilterDollButton);
                    screen.Click(filterRarity);
                    screen.Click(filterType);
                }
            }
            else
            {
                screen.Click(filterRarity);
                screen.Click(filterType);
            }
            screen.Click(FilterConfirmButton);
            screen.Click(DollSelectButton);
        }

        public void AddDollToEchelon(Doll doll, int echelon, int slot)
        {
            if (echelon > 9)
            {
                //TODO drag
            }
            EchelonButton.Name = $"FormationPage/Echelon{echelon.ToString()}";
            EchelonClickedScene.Name = $"FormationPage/Echelon{echelon.ToString()}Clicked";
            if (!screen.Exists(EchelonClickedScene, 1000))
            {
                screen.Click(EchelonButton);
            }
            var area = SlotArea(new Point(220 + (160 * (slot - 1)), 290));
            screen.Click(area, FilterDollButton);
            SelectDoll(doll);
        }

        public void ReplaceDoll(Doll dollOut, Doll dollIn)
        {
            screen.Wait(FormationScene);
            if (dollOut.Name.StartsWith("Zas"))
            {
                DollFormationButton.Name = "Dolls/Zas";
            }
            else
            {
                DollFormationButton.Name = "Dolls/" + dollOut.Name;
            }
            screen.Click(DollFormationButton);
            SelectDoll(dollIn);
        }

        public void ReplaceCorpseDragger()
        {
            var dollOut = Doll.Get(Properties.Settings.Default.DollExhausted);
            var dollIn = Doll.Get(Properties.Settings.Default.DollLoaded);
            ReplaceDoll(dollOut, dollIn);
            //AddDollToEchelon(dollOut, 2, 1);
            Properties.Settings.Default.DollExhausted = dollIn.Name;
            Properties.Settings.Default.DollLoaded = dollOut.Name;
            Properties.Settings.Default.Save();
        }

        public void ReplaceZas()
        {
            var dollOut = Doll.Get("Zas1");
            var dollIn = Doll.Get("Zas2Leader");
            ReplaceDoll(dollOut, dollIn);
            AddDollToEchelon(dollOut, 2, 1);
        }
    }
}

using System.Drawing;
using System.Threading;

namespace UziTrainer.Scenes
{
    class Formation
    {
        private static readonly int EchelonSlotX = 160;
        private static readonly int EchelonSlotXSize = 185;

        public Formation()
        {
        }


        private void SelectDoll(Doll doll)
        {
            Screen.Click(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            if (Screen.Exists(new Query("FormationPage/FilterActive", new Rectangle(1164, 329, 35, 31)))) {
                Screen.Click(new Query("FormationPage/Reset", new Rectangle(596, 692, 30, 30)), 10, 5);
                Thread.Sleep(500);
                Screen.Click(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            }
            Screen.Click(new Query("FormationPage/Filter" + doll.Rarity, new Rectangle(527, 168, 550, 170)), 10, 3);
            Screen.Click(new Query("FormationPage/Filter" + doll.Type, new Rectangle(527, 384, 550, 170)));
            Screen.Click(new Query("FormationPage/FilterConfirm", new Rectangle(876, 706, 30, 30)));
            Screen.Transition(new Query("Dolls/" + doll.Name, .85f), Screen.FormationQuery);
        }

        public void AddDollToEchelon(Doll doll, int echelon, int slot)
        {
            if (echelon > 7)
            {
                //TODO drag
            }
            if (!Screen.Exists(new Query("FormationPage/Echelon" + echelon.ToString() + "Clicked", new Rectangle(2,125,140,625)))) {
                Screen.Click(new Query("FormationPage/Echelon" + echelon.ToString(), new Rectangle(2, 125, 140, 625)));
                Screen.Wait(new Query("FormationPage/Echelon" + echelon.ToString() + "Clicked", new Rectangle(2, 125, 140, 625)));
            }
            Screen.Wait(new Query("FormationPage/WaitForFormation", new Rectangle(1198, 149, 25, 25)));
            var x = EchelonSlotX + (slot - 1) * EchelonSlotXSize + 80;
            Mouse.Click(x, 240);
            SelectDoll(doll);
        }

        public void ReplaceDoll(Doll DollOut, Doll DollIn)
        {
            Screen.Wait(new Query("FormationPage/WaitForFormation", new Rectangle(1198,149,25,25)));
            Screen.Click(new Query("Dolls/" + DollOut.Name, new Rectangle(136, 137, 934, 378), .85f));
            SelectDoll(DollIn);
        }

        public void SetDragFormation()
        {
            var doll1 = Doll.Get(SwapDoll.Default.ExhaustedDoll);
            var doll2 = Doll.Get(SwapDoll.Default.LoadedDoll);
            ReplaceDoll(doll1, doll2);
            AddDollToEchelon(doll1, 2, 1);
            Screen.Transition(Screen.FormationQuery, Screen.HomeQuery);
            SwapDoll.Default.ExhaustedDoll = doll2.Name;
            SwapDoll.Default.LoadedDoll = doll1.Name;
            SwapDoll.Default.Save();
            Program.UpdateDollText();
        }
    }
}

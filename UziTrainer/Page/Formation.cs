using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer.Scenes
{
    class Formation
    {
        private readonly int EchelonSlotX = 160;
        private readonly int EchelonSlotXSize = 185;

        private void SelectDoll(Doll doll)
        {
            Scene.Click(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            if (Scene.Exists(new Query("FormationPage/FilterActive", new Rectangle(1164, 329, 35, 31)))) {
                Scene.Click(new Query("FormationPage/Reset"));
                Thread.Sleep(500);
                Scene.Click(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            }
            Scene.Click(new Query("FormationPage/Filter" + doll.Rarity, new Rectangle(527, 168, 550, 170)));
            Scene.Click(new Query("FormationPage/Filter" + doll.Type, new Rectangle(527, 384, 550, 170)));
            Mouse.Click(928, 714, 5);
            Scene.Click(new Query("Dolls/" + doll.Name));
        }

        public void AddDollToEchelon(Doll doll, int echelon, int slot)
        {
            if (echelon > 7)
            {
                //TODO drag
            }
            if (!Scene.Exists(new Query("FormationPage/Echelon" + echelon.ToString() + "Clicked", new Rectangle(2,125,140,625)))) {
                Scene.Click(new Query("FormationPage/Echelon" + echelon.ToString(), new Rectangle(2, 125, 140, 625)));
                Scene.Wait(new Query("FormationPage/Echelon" + echelon.ToString() + "Clicked", new Rectangle(2, 125, 140, 625)));
            }
            Scene.Wait(new Query("FormationPage/WaitForFormation", new Rectangle(1198, 149, 25, 25)));
            var x = EchelonSlotX + (slot - 1) * EchelonSlotXSize + 80;
            Mouse.Click(x, 240);
            SelectDoll(doll);
        }

        public void ReplaceDoll(Doll DollOut, Doll DollIn)
        {
            Scene.Wait(new Query("FormationPage/WaitForFormation", new Rectangle(1198,149,25,25)));
            Scene.Click(new Query("Dolls/" + DollOut.Name, new Rectangle(136, 137, 934, 378)));
            SelectDoll(DollIn);
        }

        public void SetDragFormation(string dollOut, string dollIn)
        {
            var doll1 = Doll.Get(dollOut);
            var doll2 = Doll.Get(dollIn);
            ReplaceDoll(doll1, doll2);
            AddDollToEchelon(doll1, 2, 1);
        }
    }
}

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

        public void SelectDoll(Doll doll)
        {
            Scene.Click(Query.Create("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            if (Scene.Exists(Query.Create("FormationPage/FilterActive", new Rectangle(1164, 329, 35, 31)))) {
                Scene.Click(Query.Create("FormationPage/Reset"));
                Thread.Sleep(500);
                Scene.Click(Query.Create("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            }
            Scene.Click(Query.Create("FormationPage/Filter" + doll.Rarity, new Rectangle(527, 168, 550, 170)));
            Scene.Click(Query.Create("FormationPage/Filter" + doll.Type, new Rectangle(527, 384, 550, 170)));
            Mouse.Click(928, 714, 5);
            Scene.Click(Query.Create("Dolls/" + doll.Name));
        }

        public void AddDollToEchelon(Doll doll, int echelon, int slot)
        {
            if (echelon > 7)
            {
                //TODO drag
            }
            if (!Scene.Exists(Query.Create("FormationPage/Echelon" + echelon.ToString() + "Clicked", new Rectangle(2,125,140,625)))) {
                Scene.Click(Query.Create("FormationPage/Echelon" + echelon.ToString(), new Rectangle(2, 125, 140, 625)));
                Scene.Wait(Query.Create("FormationPage/Echelon" + echelon.ToString() + "Clicked", new Rectangle(2, 125, 140, 625)));
            }
            Scene.Wait(Query.Create("FormationPage/WaitForFormation", new Rectangle(1198, 149, 25, 25)));
            var x = EchelonSlotX + (slot - 1) * EchelonSlotXSize + 80;
            Mouse.Click(x, 240);
            SelectDoll(doll);
        }

        public void ReplaceDoll(Doll ExhaustedDoll, Doll LoadedDoll)
        {
            Scene.Wait(Query.Create("FormationPage/WaitForFormation", new Rectangle(1198,149,25,25)));
            Scene.Click(Query.Create("Dolls/" + ExhaustedDoll.Name, new Rectangle(136, 137, 934, 378)));
            SelectDoll(LoadedDoll);
        }

        public void SetDragFormation(Doll doll1, Doll doll2)
        {
            ReplaceDoll(doll1, doll2);
            AddDollToEchelon(doll1, 2, 1);
        }
    }
}

using System.Drawing;
using System.Threading;

namespace UziTrainer.Scenes
{
    public class Formation
    {
        private static readonly int EchelonSlotX = 160;
        private static readonly int EchelonSlotXSize = 185;


        private static void SelectDoll(Doll doll)
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
            Scene.Click(new Query("Dolls/" + doll.Name, true));
        }

        public static void AddDollToEchelon(Doll doll, int echelon, int slot)
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

        public static void ReplaceDoll(Doll DollOut, Doll DollIn)
        {
            Scene.Wait(new Query("FormationPage/WaitForFormation", new Rectangle(1198,149,25,25)));
            Scene.Click(new Query("Dolls/" + DollOut.Name, new Rectangle(136, 137, 934, 378)));
            SelectDoll(DollIn);
        }

        public static void SetDragFormation()
        {
            var doll1 = Doll.Get(SwapDoll.Default.ExhaustedDoll);
            var doll2 = Doll.Get(SwapDoll.Default.LoadedDoll);
            ReplaceDoll(doll1, doll2);
            AddDollToEchelon(doll1, 2, 1);

            SwapDoll.Default.ExhaustedDoll = doll2.Name;
            SwapDoll.Default.LoadedDoll = doll1.Name;
            SwapDoll.Default.Save();
        }
    }
}

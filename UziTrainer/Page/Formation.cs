using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer.Scenes
{
    static class Formation
    {
        public static void SelectDoll(int Doll)
        {
            Scene.Click(new Query("Formation/Filter", new int[] { 1106, 269, 1214, 311 }));
            /*
            FindAndClick("FormationPage/Filter a1106,269,100,150")
            if ImageExists("FormationPage/FilterActive a1164,329,30,30") {
                FindAndClick("FormationPage/Reset")
            FindAndClick("FormationPage/Filter a1106,269,100,150")
            }
            GunType:= % Doll %[1]
            Rarity:= % Doll %[2]
            FindAndClick("FormationPage/Filter" Rarity " a527,168,550,170")
            FindAndClick("FormationPage/Filter" GunType " a527,384,550,170")
            Click([928, 714], 5)
            FindAndClick("Dolls/" Doll "Profile", 0, 140)
          */
        }
    }
}

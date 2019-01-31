using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer.Scenes
{
    class Formation
    {
        readonly Query Query = new Query("FormationPage");

        
        public void SelectDoll(Doll doll)
        {
            Scene.Click(Query.Create("Filter", new[] { 1106, 269, 1214, 311 }));
            if (Scene.Exists(Query.Create("FilterActive", new[] { 1164, 329, 1200, 360 }))) {
                Scene.Click(Query.Create("Reset"));
            }
            //Scene.Click(Query.Create("Filter", new[] { 1106, 269, 1214, 311 }));
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

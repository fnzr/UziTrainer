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
        private readonly Query query = new Query("FormationPage");

        public void SelectDoll(Doll doll)
        {
            Scene.Click(query.Create("Filter", new Rectangle(1106, 269, 110, 45)));
            if (Scene.Exists(query.Create("FilterActive", new Rectangle(1164, 329, 35, 31)))) {
                Scene.Click(query.Create("Reset"));
                Thread.Sleep(500);
                Scene.Click(query.Create("Filter", new Rectangle(1106, 269, 110, 45)));
            }
            Scene.Click(query.Create("Filter" + doll.Rarity, new Rectangle(527, 168, 550, 170)));
            Scene.Click(query.Create("Filter" + doll.Type, new Rectangle(527, 384, 550, 170)));
            Mouse.Click(928, 714, 5);
            Scene.Click(query.Create("Filter" + doll.Name));
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

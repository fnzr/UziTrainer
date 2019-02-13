using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer.Page
{
    class Factory
    {
        const int DollSlotX = 90;
        const int DollSlotY = 260;
        const int DollSlotXSize = 165;
        const int DollSlotYSize = 290;
        Screen scene;

        public Factory(Screen scene)
        {
            this.scene = scene;
        }

        private void Retire2Stars()
        {
            scene.Click(new Query("FactoryPage/SelectRetirement"));
            Point coords;
            if (scene.Exists(new Query("FactoryPage/SmartSelect", new Rectangle(1168,662,25,25)), 2000, out coords)) {
                Mouse.Click(coords);
                if (scene.Exists(new Query("FactoryPage/OKRetire", new Rectangle(1144,655,25,25)), 2000, out coords)) {
                    Mouse.Click(coords);
                    scene.Wait(new Query("FactoryPage/FactoryPage", new Rectangle(56, 649, 40, 40)));
                    Mouse.Click(1182, 652);
                    Thread.Sleep(500);
                    Mouse.Click(744, 527);
                    Thread.Sleep(3000);
                }
                else
                {
                    Mouse.Click(71, 75);
                    scene.Wait(Screen.FactoryQuery);
                }
            }
            else
            {
                Mouse.Click(632, 550);
            }
        }

        private void Retire3Stars()
        {
            scene.Click(new Query("FactoryPage/SelectRetirement"));
            if (scene.Exists(new Query("FactoryPage/SmartSelect", new Rectangle(1168,662,25,25)), 2000)) {
                scene.Click(new Query("FactoryPage/Filter", new Rectangle(1099, 266, 172, 104)));
                scene.Click(new Query("FactoryPage/3", new Rectangle(527, 168, 550, 170)));
                Mouse.Click(933, 709); //Confirm
                Thread.Sleep(500);        
                for(var j=0; j<2; j++) {
                    var y = 260 + (j * DollSlotYSize);
                    for(var i=0;i<6;i++)
                    {
                        var x = 90 + (i * DollSlotXSize);
                        Mouse.Click(x, y);
                    }
                }
                if (scene.Exists(new Query("FactoryPage/OKRetire", new Rectangle(1144,655,25,25)), 2000, out Point coords)) {
                    Mouse.Click(coords);
                    scene.Wait(new Query("FactoryPage/FactoryPage", new Rectangle(56, 649, 40, 40)));
                    Mouse.Click(1182, 652);
                    Thread.Sleep(500);
                    Mouse.Click(744, 527);
                    Thread.Sleep(3000);
                }
                else
                {
                    Mouse.Click(71, 75);
                    scene.Wait(Screen.FactoryQuery);
                }
            }
            else
            {
                Mouse.Click(632, 550);
            }
        }

        public void DollRetirement()
        {
            /*
            IniRead, DoRetire, % A_LineFile %\..\..\config.ini, Options, Retirement
            if !DoRetire {
                PerformTransition(FactoryPage, BaseBackground, [68, 78])
                return
            }
            */
            scene.Wait(Screen.FactoryQuery);
            Mouse.Click(98, 470);
            Retire2Stars();
            Retire3Stars();
            scene.Transition(Screen.FactoryQuery, Screen.HomeQuery, new Point(68, 78));
        }

        public bool ClickEnhanceableDoll()
        {
            for(var j=0; j<2; j++)
            {
                var y = 265 + (j * DollSlotYSize) + (20 * j);
                for(var i=0; i<6; i++) {
                    int x = (14 * (i + 1)) + (i * DollSlotXSize);
                    var area = new Rectangle(x, y, DollSlotXSize, 90);
                    if (!scene.Exists(new Query("FactoryPage/InLogistics", area))) {
                        if (true)
                        //if (!scene.Exists(new Query("FactoryPage/InTraining", area)))
                        {
                            scene.Transition(new Query("FactoryPage/Filter", new Rectangle(1175, 280, 25, 25)),
                                new Query("FactoryPage/SelectFodder"), new Point(x + 80, y));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool SmartSelectEnhanceFodder()
        {
            Point coords;
            scene.Click(new Query("FactoryPage/SmartSelect", new Rectangle(1115, 621, 150, 80)));
            if (scene.Exists(new Query("FactoryPage/OKRetire", new Rectangle(1131,629,50,50)), 2000, out coords)) {
                Mouse.Click(coords);
                Thread.Sleep(500);
                Mouse.Click(1182, 643);
                Thread.Sleep(3500);
                var success = scene.Exists(new Query("FactoryPage/PowerUpSuccess", new Rectangle(579, 535, 30, 30)), 4000, out coords);
                if (success) {
                    Mouse.Click(coords);
                }
                else
                {
                    scene.Exists(new Query("FactoryPage/NotEnoughDolls", new Rectangle(468, 510, 30, 30)), 1000, out coords);
                    Mouse.Click(coords);
                    return false;
                }
                return true;
            }
            else
            {
                Mouse.Click(64, 77);
                return false;
            }
        }

        public void DollEnhancement()
        {
            /*
            IniRead, DoEnhance, % A_LineFile %\..\..\config.ini, Options, Enhancement
            if !DoEnhance {
                DollRetirement()
                return
            }
            */
            scene.Wait(Screen.FactoryQuery);
            do
            {
                Mouse.Click(98, 377);
                scene.Transition(new Query("FactoryPage/SelectCharacter", new Rectangle(231, 161, 170, 375)),
                    new Query("FactoryPage/Filter", new Rectangle(1175, 280, 25, 25)));
                if (ClickEnhanceableDoll())
                {
                    scene.Transition(Screen.FactoryQuery, new Query("FactoryPage/SmartSelect", new Rectangle(1115, 621, 150, 80)), new Point(536, 247));
                }
            } while (SmartSelectEnhanceFodder());
            DollRetirement();
        }
    }
}

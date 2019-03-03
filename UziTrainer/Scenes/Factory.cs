using System.Drawing;
using System.Threading;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Factory
    {
        public static readonly Sample FactoryScene = new Sample("FactoryPage/FactoryPage", new Rectangle(56, 649, 40, 40));
        public static readonly Rectangle ReturnButton = new Rectangle(19, 58, 100, 46);

        public static readonly Sample DollEnhancementClicked = new Sample("FactoryPage/DollEnhancementClicked", new Rectangle(9, 349, 152, 55));
        public static readonly Button DollEnhancementButton = new Button("FactoryPage/DollEnhancement", new Rectangle(9, 349, 152, 55), DollEnhancementClicked);

        public static readonly Button FilterConfirmButton = new Button("FormationPage/FilterConfirm", new Rectangle(815, 693, 233, 36), Sample.Negative);
        public static readonly Button FilterResetButton = new Button("FormationPage/Reset", new Rectangle(540, 694, 233, 36), Sample.Negative);
        public static readonly Button FilterButton = new Button("FormationPage/Filter", new Rectangle(1105, 280, 105, 65), FilterResetButton);

        public static readonly Button SmartSelectOK = new Button("FactoryPage/OKRetire", new Rectangle(1108, 624, 151, 67), null);
        public static readonly Button SmartSelect = new Button("FactoryPage/SmartSelect", new Rectangle(1115, 621, 150, 80), null);

        public static readonly Button PowerUpSuccess = new Button("FactoryPage/PowerUpSuccess", new Rectangle(579, 535, 30, 30), Sample.Negative);
        public static readonly Button NotEnoughDolls = new Button("FactoryPage/NotEnoughDolls", new Rectangle(468, 510, 30, 30), Sample.Negative);



        Screen screen;
        const int DollSlotX = 90;
        const int DollSlotY = 260;
        const int DollSlotXSize = 165;
        const int DollSlotYSize = 290;

        public Factory(Screen screen)
        {
            this.screen = screen;
        }

        public bool SelectEnhaceable()
        {
            Sample InTraining = new Sample("FactoryPage/InTraining", Rectangle.Empty);
            Sample InLogistics = new Sample("FactoryPage/InLogistics", Rectangle.Empty);
            for (var j = 0; j < 2; j++)
            {
                var y = 265 + (j * DollSlotYSize) + (20 * j);
                for (var i = 0; i < 6; i++)
                {
                    int x = (14 * (i + 1)) + (i * DollSlotXSize);
                    InLogistics.SearchArea = new Rectangle(x, y, DollSlotXSize, 90);
                    if (!screen.Exists(InLogistics, 0))
                    {
                        if (true)
                        //if (!Screen.Exists(new Query(, area)))
                        {
                            screen.Click(new Rectangle(x + 80, y, 10, 10), DollEnhancementClicked);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool SmartSelectFodder()
        {
            screen.Click(SmartSelect);            
            if (screen.Exists(SmartSelectOK))
            {                
                screen.Click(SmartSelectOK);
                Thread.Sleep(500);
                screen.Click(new Rectangle(1106, 622, 130, 44));
                Thread.Sleep(3500);
                if (screen.Exists(PowerUpSuccess))
                {
                    screen.Click(PowerUpSuccess);
                }
                else
                {
                    if (screen.Exists(NotEnoughDolls))
                    {
                        screen.Click(NotEnoughDolls);
                    }
                    return false;
                }
                return true;
            }
            else
            {
                screen.Click(new Rectangle(14, 59, 96, 49)); //Cancel select fodder
                return false;
            }
        }

        public void DollEnhancement()
        {
            screen.Wait(FactoryScene);
            if (!screen.Exists(DollEnhancementClicked))
            {
                screen.Click(DollEnhancementButton);
                Thread.Sleep(500);
            }
            SmartSelectOK.Next = DollEnhancementClicked;
            do
            {
                screen.Click(new Rectangle(242, 184, 141, 273), FilterButton);
                if (!SelectEnhaceable())
                {
                    break;
                }
                screen.Click(new Rectangle(465, 208, 136, 70), FilterButton);
            } while (SmartSelectFodder());
        }
    }
}

using System.Drawing;
using System.Threading;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Factory
    {
        public static readonly Sample FactoryScene = new Sample("FactoryPage/FactoryPage", new Rectangle(161, 15, 51, 54));
        public static readonly Rectangle ReturnButton = new Rectangle(11, 14, 91, 51);
        public static readonly Rectangle CancelSelectFodder = new Rectangle(9, 13, 95, 50);

        public static readonly Sample DollEnhancementClicked = new Sample("FactoryPage/DollEnhancementClicked", new Rectangle(6, 264, 135, 55));
        public static readonly Button DollEnhancementButton = new Button("FactoryPage/DollEnhancement", new Rectangle(6, 264, 135, 55), DollEnhancementClicked);

        public static readonly Sample DollRetirementClicked = new Sample("FactoryPage/DollRetirementClicked", new Rectangle(9, 342, 128, 55));
        public static readonly Button DollRetirementButton = new Button("FactoryPage/DollRetirement", new Rectangle(9, 342, 128, 55), DollRetirementClicked);

        public static readonly Button FilterConfirmButton = new Button("FormationPage/FilterConfirm", new Rectangle(686, 561, 192, 32), Sample.Negative);
        public static readonly Button FilterResetButton = new Button("FormationPage/Reset", new Rectangle(459, 560, 177, 36), Sample.Negative);
        public static readonly Button FilterButton = new Button("FormationPage/Filter", new Rectangle(941, 205, 126, 74), FilterResetButton);

        public static readonly Button SmartSelectOK = new Button("FactoryPage/OKRetire", new Rectangle(938, 491, 127, 74), null);
        public static readonly Button SmartSelect = new Button("FactoryPage/SmartSelect", new Rectangle(938, 491, 127, 74), null);

        public static readonly Button PowerUpSuccess = new Button("FactoryPage/PowerUpSuccess", new Rectangle(475, 520, 123, 43), Sample.Negative);
        public static readonly Button NotEnoughDolls = new Button("FactoryPage/NotEnoughDolls", new Rectangle(385, 500, 119, 44), Sample.Negative);



        Screen screen;
        const int DollSlotX = 90;
        const int DollSlotY = 260;
        const int DollSlotXSize = 140;
        const int DollSlotYSize = 240;

        public Factory(Screen screen)
        {
            this.screen = screen;
        }

        public bool SelectEnhaceable()
        {
            Sample InTraining = new Sample("FactoryPage/InTraining", Rectangle.Empty);
            Sample InLogistics = new Sample("FactoryPage/InLogistics", Rectangle.Empty);
            Sample Zas = new Sample("Dolls/Zas", Rectangle.Empty, null, .8f);
            var samples = new Sample[] { /*InTraining,*/ InLogistics, Zas };
            for (var j = 0; j < 2; j++)
            {
                var y = 130 + (j * DollSlotYSize) + (15 * j);
                for (var i = 0; i < 6; i++)
                {
                    int x = 10 + (12 * i) + (i * DollSlotXSize);
                    InLogistics.SearchArea = new Rectangle(x, y, DollSlotXSize, 190);
                    InTraining.SearchArea = InLogistics.SearchArea;
                    Zas.SearchArea = InLogistics.SearchArea;
                    //!screen.Exists(InTraining, 0)                    
                    if (screen.ExistsAny(samples) == -1)
                    {
                        screen.Click(new Rectangle(x + 10, y, 20, 10));
                        return screen.Exists(DollEnhancementClicked, 2000);
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
                return true;
            }
            else
            {
                screen.Click(CancelSelectFodder); //Cancel select fodder
                return false;
            }
        }

        public void DollEnhancement()
        {
            screen.Wait(FactoryScene);
            if (!screen.Exists(DollEnhancementClicked, 0))
            {
                screen.Click(DollEnhancementButton);
                Thread.Sleep(500);
            }
            SmartSelectOK.Next = DollEnhancementClicked;
            do
            {
                screen.Click(new Rectangle(200, 243, 136, 214), FilterButton);
                if (!SelectEnhaceable())
                {
                    screen.Click(CancelSelectFodder, DollEnhancementClicked);
                    break;
                }
                screen.Click(new Rectangle(389, 148, 125, 59), FilterButton);

                if (!SmartSelectFodder())
                {
                    break;
                };
                Thread.Sleep(500);
                screen.Click(new Rectangle(939, 706, 111, 37));
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
                    break;
                }
            } while (true);
            DollRetirement();
        }

        public void Retire3Stars()
        {
            screen.Click(new Rectangle(236, 127, 119, 73));
            Thread.Sleep(2000);
            if (!screen.Exists(FilterButton))
            {
                return;
            }            
            screen.Click(FilterButton);

            Formation.FilterOptionButton.Name = "FormationPage/Filter3";
            Formation.FilterOptionButton.Next.Name = "FormationPage/Filter3Clicked";
            screen.Click(Formation.FilterOptionButton);
            screen.Click(Formation.FilterConfirmButton);

            for (var y = 0; y < 2; y++)
            {
                for (var x = 0; x < 6; x++)
                {
                    var posX = 16 + x * 154;
                    var posY = 130 + y * 229;
                    screen.Click(new Rectangle(posX, posY, 125, 173));
                }
            }
            screen.Click(new Rectangle(940, 513, 125, 68));
            screen.Click(new Rectangle(931, 712, 109, 37));
            Thread.Sleep(300);
            screen.Click(new Rectangle(574, 506, 113, 38));
        }

        public void DollRetirement()
        {
            screen.Wait(FactoryScene);
            if (!screen.Exists(DollRetirementClicked))
            {
                screen.Click(DollRetirementButton);
                Thread.Sleep(500);
            }
            SmartSelectOK.Next = DollRetirementClicked;

            screen.Click(new Rectangle(236, 127, 119, 73), FilterButton);
            SmartSelectFodder();

            screen.Click(new Rectangle(931, 712, 109, 37));
            Thread.Sleep(3000);
            Retire3Stars();
            
        }
    }
}

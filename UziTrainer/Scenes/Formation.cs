﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Formation
    {
        public static readonly Sample FormationScene = new Sample("FormationPage/FormationPage", new Rectangle(1188, 145, 40, 35));
        public static readonly Sample FilterActiveScene = new Sample("FormationPage/FilterActive", new Rectangle(1105, 280, 105, 80));
        public static readonly Button FilterResetButton = new Button("FormationPage/Reset", new Rectangle(540, 694, 233, 36), Sample.Negative);
        public static readonly Button FilterDollButton = new Button("FormationPage/Filter", new Rectangle(1105, 280, 105, 65), FilterResetButton);
        public static readonly Button DollFormationButton = new Button("", new Rectangle(150, 186, 902, 339), FilterDollButton, .9f, SlotArea);        
        
        public static readonly Button FilterConfirmButton = new Button("FormationPage/FilterConfirm", new Rectangle(815, 693, 233, 36), Sample.Negative);
        public static readonly Button DollSelectButton = new Button("", new Rectangle(5, 136, 1075, 606), FormationScene, .85f, SelectDollArea);
        public static readonly Button FilterOptionButton = new Button("", new Rectangle(528, 169, 546, 422), 
            new Button("", new Rectangle(528, 169, 546, 412), null), .95f, FilterOptionArea);
        public static readonly Sample EchelonClickedScene = new Sample("", new Rectangle(7, 145, 130, 596));
        public static readonly Button EchelonButton = new Button("", new Rectangle(7, 145, 130, 596), EchelonClickedScene);

        private static Rectangle FilterOptionArea(Point arg)
        {
            var filterXSize = 157;
            var filterYSize = 67;
            int y;
            int line;
            if (arg.Y > 365) // Filter Type
            {
                line = (arg.Y - 395 - 5) / filterYSize;
                y = 400 + (filterYSize * line);
            }
            else // Filter Rarity
            {
                line = (arg.Y - 180 - 5) / filterYSize;
                y = 185 + (filterYSize * line - 1);
            }            
            var column = (arg.X - 543 - 10) / filterXSize;
            var x = 537 + (column * filterXSize) + ((column + 1) * 15);
            return new Rectangle(new Point(x, y), new Size(147, 45));
        }

        private static Rectangle SelectDollArea(Point arg)
        {
            var slotXSize = 160;
            int line = arg.Y / 440;
            var y = 170 + (line  * 320);
            int column = (arg.X - 20) / slotXSize;

            var x = 20 + (column  * slotXSize) + (column * 20);
            return new Rectangle(new Point(x, y), new Size(140, 190));
        }

        private static Rectangle SlotArea(Point foundAt)
        {
            var slotXSize = 175;
            int slot = foundAt.X / slotXSize;
            var x = 165 * slot + (15 * (slot - 1));
            return new Rectangle(x, 190, 151, 320);
        }

        readonly Screen screen;

        public Formation(Screen screen)
        {
            this.screen = screen;
        }

        public void SelectDoll(Doll doll)
        {
            screen.Click(FilterDollButton);
            if (screen.Exists(FilterActiveScene))
            {
                screen.Click(FilterResetButton);
                screen.Click(FilterDollButton);
            }
            FilterOptionButton.Name = "FormationPage/Filter" + doll.Rarity;
            FilterOptionButton.Next.Name = FilterOptionButton.Name + "Clicked";
            screen.Click(FilterOptionButton);
            FilterOptionButton.Name = "FormationPage/Filter" + doll.Type;
            FilterOptionButton.Next.Name = FilterOptionButton.Name + "Clicked";
            screen.Click(FilterOptionButton);
            screen.Click(FilterConfirmButton);
            DollSelectButton.Name = "Dolls/" + doll.Name;
            screen.Click(DollSelectButton);
        }

        public void AddDollToEchelon(Doll doll, int echelon, int slot)
        {
            if (echelon > 7)
            {
                //TODO drag
            }
            EchelonButton.Name = $"FormationPage/Echelon{echelon.ToString()}";
            EchelonClickedScene.Name = $"FormationPage/Echelon{echelon.ToString()}Clicked";
            if (!screen.Exists(EchelonClickedScene))
            {
                screen.Click(EchelonButton);
            }
            var area = SlotArea(new Point(220 + (160 * slot), 290));
            var button = new Button("", area, FilterDollButton);
            screen.Click(button);
            SelectDoll(doll);
        }

        public void ReplaceDoll(Doll dollOut, Doll dollIn)
        {
            screen.Wait(FormationScene);
            DollFormationButton.Name = "Dolls/" + dollOut.Name;
            screen.Click(DollFormationButton);
            SelectDoll(dollIn);
        }

    }
}
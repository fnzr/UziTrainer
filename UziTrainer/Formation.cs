using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static UziTrainer.Screen;

namespace UziTrainer
{
    class DollData
    {
        public Samples Class;
        public Samples Rarity;

        public DollData(Samples @class, Samples rarity)
        {
            Class = @class;
            Rarity = rarity;
        }
    }
    
    static class Formation
    {
        static readonly Dictionary<Samples, DollData> Dolls;
        static Formation()
        {
            Dolls = new Dictionary<Samples, DollData>()
            {
                {Samples.SOPMOD, new DollData(Samples.FilterAR, Samples.Filter5Star) },
                {Samples.HK416, new DollData(Samples.FilterAR, Samples.Filter5Star) },
            };
        }

        public static void ReplaceDragger()
        {            
            var okOut = Enum.TryParse<Samples>(Properties.Settings.Default.DollOut, out var dollOut);
            var okIn = Enum.TryParse<Samples>(Properties.Settings.Default.DollIn, out var dollIn);
            if (!okOut || !okIn)
            {
                MessageBox.Show("Invalid draggers dolls!");
                return;
            }            
            ReplaceDolls(dollOut, dollIn);

            Properties.Settings.Default.DollIn = dollOut.ToString();
            Properties.Settings.Default.DollOut = dollIn.ToString();
            Properties.Settings.Default.Save();

            Tap(Samples.FormationExit);
        }
        public static void ReplaceDolls(Samples dollOut, Samples dollIn)
        {
            Wait(Samples.FormationScreen);
            Tap(dollOut);
            if (!Exists(dollIn, 2000))
            {                
                Tap(Samples.FormationFilter);
                if (Exists(Samples.FormationFilterActive))
                {
                    Tap(Samples.FilterReset);
                    Tap(Samples.FormationFilter);
                }
                var data = Dolls[dollIn];
                Tap(data.Rarity);
                Tap(data.Class);
                Tap(Samples.FilterConfirm);
            }
            Tap(dollIn);
        }
    }
}

using System.Threading;
using static UziTrainer.Screen;

namespace UziTrainer
{
    static class Maps
    {
        static void ExecutePlan()
        {
            Tap(Samples.ExecutePlan);

            while (true)
            {
                /* Maybe executed all planning */
                if (Exists(Samples.PlanningOff))
                {
                    Thread.Sleep(5000);
                    /* If PlanningOff dissapared, we're not done yet */
                    if (Exists(Samples.PlanningOff))
                    {
                        break;
                    }
                }
                Thread.Sleep(5000);
            }
        }

        static void ReturnToBase(Samples @base)
        {
            Wait(Samples.ReturnToBase);
            do
            {
                Android.Tap(535, 50, 50, 20);
                Thread.Sleep(500);
            } while (!Exists(@base));
        }

        static bool StorageLimitReached()
        {            
            if (Exists(Samples.DollCapacity))
            {
                Tap(Samples.DollCapacity);
                //Perform enhancement
                return true;
            }
            //TODO
            else if(false && Exists(Samples.EquipCapacity))
            {
                Tap(Samples.EquipCapacity);
                //Stop
                return true;
            }
            return false;
        }

        public static void PerformRepairs()
        {
            while (Exists(Samples.LowHP))
            {
                Tap(Samples.LowHP);
                Tap(Samples.EmergencyRepair);
            }            
        }
        public static void N0_2()
        {
            Wait(Samples.CombatScreen);
            if (!Exists(Samples.CombatMissionClicked))
            {
                Tap(Samples.CombatMission);
            }
            if (!Exists(Samples.Chapter0Clicked))
            {
                Tap(Samples.Chapter0);
            }
            Tap(Samples.M0_2);
            Tap(Samples.NormalBattle);

            if (StorageLimitReached())
            {
                //Pause
            }
            Wait(Samples.Turn0);


            //Check low hp maybe
            Tap(550, 406, Samples.DeployOK);

            PerformRepairs();

            Tap(Samples.EchelonFormation);
            Formation.ReplaceDragger();
            Wait(Samples.Turn0);

            Tap(550, 406, Samples.DeployOK);
            Tap(Samples.DeployOK);

            Tap(385, 403, Samples.DeployOK);
            Tap(Samples.DeployOK);

            Tap(Samples.StartOperation);
            Thread.Sleep(5000);

            Android.Tap(385, 400);
            Tap(385, 400, Samples.Resupply);
            Tap(Samples.Resupply);

            Android.Tap(550, 405);
            Tap(Samples.PlanningOff);

            Android.Tap(490, 150);
            Android.Tap(690, 165);

            ExecutePlan();

            Tap(Samples.EndRound);

            ReturnToBase(Samples.CombatScreen);
        }
    }
}

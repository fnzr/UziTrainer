using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer.Machine
{
    partial class UziMachine
    {
        TriggerWithParameters<Doll> SelectDollTrigger = new TriggerWithParameters<Doll>(Trigger.SelectDoll);
        TriggerWithParameters<Doll> FilterDollTrigger = new TriggerWithParameters<Doll>(Trigger.FilterDoll);

        void ConfigureFormationMachine()
        {
            Configure(State.Formation)
                .OnEntry(() => Screen.Wait(Screen.FormationQuery))        
                .OnExit(() => Screen.ClickUntilGone(Screen.FormationQuery, new RPoint(67, 80, 20)))
                .Permit(Trigger.FormationLeave, State.Home)
                .PermitIf(SelectDollTrigger, State.DollSelectPage, SelectDoll);

            Configure(State.DollSelectPage)
                .OnEntry(() => Screen.Wait(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45))))
                .Permit(Trigger.FilterDoll, State.FilterOpen);

            Configure(State.FilterOpen)
                .OnEntry(OpenFilter)
                .Permit(Trigger.FilterActive, State.Filtered)
                .Permit(Trigger.ConfirmFilter, State.DollSelectPage);

            Configure(State.Filtered)
                .Permit(Trigger.ResetFilter, State.DollSelectPage)
                .Permit(Trigger.ConfirmFilter, State.DollSelectPage);
        }

        void OpenFilter()
        {
            Screen.Wait(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)), out Point p);
            Screen.ClickUntilFound(new Query("FormationPage/Reset", new Rectangle(596, 692, 30, 30)), new RPoint(p, 10, 30));
            if (Screen.Exists(new Query("FormationPage/FilterActive", new Rectangle(1164, 329, 35, 31))))
            {
                Fire(Trigger.FilterActive);
            }
        }

        bool IsFiltered()
        {
            if (Screen.Exists(new Query("FormationPage/FilterActive", new Rectangle(1164, 329, 35, 31))))
            {
                Screen.ClickUntilGone(new Query("FormationPage/Reset", new Rectangle(596, 692, 30, 30)));
                Thread.Sleep(500);
                Screen.Click(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)));
            }
        }

        bool SelectDoll(Doll doll)
        {
            if(Screen.Exists(new Query($"Doll/{doll.Name}"), 3000, out Point coords)){
                Screen.Click(coords, 30);
                return true;
            }
            return false;
        }

        public void SelectFilters(Doll doll)
        {
            Screen.Click(new Query("FormationPage/Filter" + doll.Rarity, new Rectangle(527, 168, 550, 170)), 10, 3);
            Screen.Click(new Query("FormationPage/Filter" + doll.Type, new Rectangle(527, 384, 550, 170)));
        }
    }
}

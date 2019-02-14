using Stateless;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer.Machine
{
    enum FormationState
    {
        Initial,
        Exited,
        DollSelection,
        FilterOpen,
        Filtered
    }

    enum FormationTrigger
    {
        FormationLeave,
        FilterDoll,
        ExitDollSelectPage,
        FilterActive,
        ConfirmFilter,
        ResetFilter,
        SelectDoll
    }

    class FormationMachine : StateMachine<FormationState, FormationTrigger>
    {

        public TriggerWithParameters<Doll> SelectDollTrigger = new TriggerWithParameters<Doll>(FormationTrigger.SelectDoll);
        public TriggerWithParameters<Doll> FilterDollTrigger = new TriggerWithParameters<Doll>(FormationTrigger.FilterDoll);

        public FormationMachine(FormationState initialState) : base(initialState)
        {
            Configure(FormationState.Initial)
                .OnEntry(() => Screen.Wait(Screen.FormationQuery))
                .OnExit(() => Screen.ClickUntilGone(Screen.FormationQuery, new RPoint(67, 80, 20)))
                .Permit(FormationTrigger.FormationLeave, FormationState.Exited)
                .PermitIf(SelectDollTrigger, FormationState.DollSelection, SelectDoll);

            Configure(FormationState.DollSelection)
                .OnEntry(() => Screen.Wait(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45))))
                .Permit(FormationTrigger.FilterDoll, destinationState: FormationState.FilterOpen)
                .PermitIf(SelectDollTrigger, FormationState.Initial, SelectDoll)
                .PermitIf(FormationTrigger.ExitDollSelectPage, FormationState.Initial, ExitSelectPage);

            Configure(FormationState.FilterOpen)
                .OnEntry(OpenFilter)
                .Permit(FormationTrigger.FilterActive, FormationState.Filtered)
                .Permit(FormationTrigger.ConfirmFilter, FormationState.DollSelection);

            Configure(FormationState.Filtered)
                .Permit(FormationTrigger.ResetFilter, FormationState.DollSelection)
                .Permit(FormationTrigger.ConfirmFilter, FormationState.DollSelection);
        }

        void OpenFilter()
        {
            Screen.Wait(new Query("FormationPage/Filter", new Rectangle(1106, 269, 110, 45)), out Point p);
            Screen.ClickUntilFound(new Query("FormationPage/Reset", new Rectangle(596, 692, 30, 30)), new RPoint(p, 10, 30));
            if (Screen.Exists(new Query("FormationPage/FilterActive", new Rectangle(1164, 329, 35, 31))))
            {
                Fire(FormationTrigger.FilterActive);
            }
        }

        bool ExitSelectPage()
        {
            Screen.Click(new RPoint(69, 79, 40, 20));
            return true;
        }

        bool SelectDoll(Doll doll)
        {
            if(Screen.Exists(new Query($"Dolls/{doll.Name}", .9f), 3000, out Point coords)){
                Screen.Click(coords, 30);
                return true;
            }
            return false;
        }

        public void ApplyFilters(Doll doll)
        {
            Screen.Click(new Query("FormationPage/Filter" + doll.Rarity, new Rectangle(527, 168, 550, 170)), 10, 3);
            Screen.Click(new Query("FormationPage/Filter" + doll.Type, new Rectangle(527, 384, 550, 170)));
        }
    }
}

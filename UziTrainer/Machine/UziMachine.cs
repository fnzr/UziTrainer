using Stateless;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer.Machine
{
    public enum State
    {
        Home,
        
        /* Formation States */
        Formation,
        DollSelectPage,
        FilterOpen,
        Filtered,
        ResettingFilter
    }

    public enum Trigger
    {
        ExitHome,
        ExitHomeDone,
        ToFormation,
        SwapDolls,

        /* Formation Triggers */
        SelectDoll,
        DollSelectPage,
        FormationLeave,
        FormationClickFilter,
        FilterDoll,
        ConfirmFilter,
        ResetTrigger,
        FilterActive,
        ResetFilter,
        ExitDollSelectPage
    }

    partial class UziMachine
    {
        public FormationMachine Formation;
        public UziMachine()
        {
            Formation = new FormationMachine(FormationState.Initial);
        }

        private void SelectDoll(Doll doll)
        {
            Formation.Fire(FormationTrigger.FilterDoll);
            if (Formation.IsInState(FormationState.Filtered))
            {
                Formation.Fire(FormationTrigger.ResetFilter);
                Formation.Fire(FormationTrigger.FilterDoll);
            }
            Formation.ApplyFilters(doll);
            Formation.Fire(FormationTrigger.ConfirmFilter);
            Formation.Fire(Formation.SelectDollTrigger, doll);
        }

        public void ReplaceDoll(Doll DollOut, Doll DollIn)
        {
            Formation.Fire(Formation.SelectDollTrigger, DollOut);
            SelectDoll(DollIn);
        }

        public void SwapDolls()
        {
            var doll1 = Doll.Get(SwapDoll.Default.ExhaustedDoll);
            var doll2 = Doll.Get(SwapDoll.Default.LoadedDoll);
            Formation.Fire(Formation.SelectDollTrigger, doll1);
            ReplaceDoll(doll1, doll2);
            Screen.Transition(Screen.FormationQuery, Screen.HomeQuery);
            SwapDoll.Default.ExhaustedDoll = doll2.Name;
            SwapDoll.Default.LoadedDoll = doll1.Name;
            SwapDoll.Default.Save();
            Program.UpdateDollText();
        }
    }
}

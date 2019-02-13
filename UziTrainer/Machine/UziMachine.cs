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
        ResetFilter
    }

    partial class UziMachine : StateMachine<State, Trigger>
    {
        
        Screen screen = new Screen();
        public Dictionary<State, Point> ExitHomeCoordinates = new Dictionary<State, Point>()
        {
            {State.Formation, new Point(1161, 476)}
        };
        TriggerWithParameters<State> ExitHomeTrigger = new TriggerWithParameters<State>(Trigger.ExitHome);
        public UziMachine(State initialState) : base(initialState)
        {
            Configure(State.Home)
                .PermitDynamic(ExitHomeTrigger, _LeaveHome);
            ConfigureFormationMachine();
        }

        State _LeaveHome(State destination)
        {
            var coords = ExitHomeCoordinates[destination];
            do
            {
                screen.Click(coords);
            } while (screen.Exists(Screen.HomeQuery));
            return destination;
        }

        public void LeaveHome(State destination)
        {
            Fire(ExitHomeTrigger, destination);
        }
    }
}

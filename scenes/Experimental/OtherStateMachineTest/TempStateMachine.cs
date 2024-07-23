

using Godot;

namespace Experimental.StateMachineTest
{

    // https://www.youtube.com/watch?v=BNU8xNRk_oU&ab_channel=GameEndeavor
    public partial class TempStateMachine : Node
    {
        public object State { get; set; } = null;
        public object PreviousState { get; set; } = null;
        private Node _Parent { get; set; }

        public override void _Ready()
        {
            _Parent = GetParent();
        }

        public virtual void StateLogic(double delta)
        {
        }

        public virtual object GetTransition(double delta)
        {
            return null;
        }

        public virtual void EnterState(object newState, object oldState)
        {
            // set animation for the state
            // start tweens, timers
        }

        public virtual void ExitState(object oldState, object newState)
        {

        }

        public virtual void SetState(object newState)
        {
            PreviousState = State;
            State = newState;

            if (PreviousState != null)
            {
                ExitState(PreviousState, newState);
            }

            if (newState != null)
            {
                EnterState(newState, PreviousState);
            }

        }


        public override void _PhysicsProcess(double delta)
        {
            if (State != null)
            {
                StateLogic(delta);
                var transition = GetTransition(delta);
                if (transition != null)
                {
                    SetState(transition);
                }
            }
        }

    }
}
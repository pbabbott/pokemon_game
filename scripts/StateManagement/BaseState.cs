using System;
using System.Collections.Generic;
using Godot;
using Pokemon.UserInput;

namespace Pokemon.StateManagement
{

    public abstract partial class BaseState<T> : Node, IState
        where T : Node
    {
        // Reference to the state machine, to call its TransitionTo() method directly.
        // That's one unorthodox detail of our state implementation, as it adds a dependency betweeen the
        // state and the state machine objects, but we found it to be most effecient for our needs
        // The statemachine node will set it
        public StateMachine StateMachine { get; set; }

        protected T Target { get; private set; }

        string IState.Name => Name;

        // Virtual function.  Recieves events from the UnHandledInput() callback
        public virtual void HandleInput(InputEvent e)
        {
            return;
        }

        // Virtual function. Corresponds to the process() callback
        public virtual void Update(float delta)
        {
            return;
        }

        /// <summary>
        /// Virtual function. Corresponds to the _PhysicsProcess() callback
        /// </summary>
        public virtual void PhysicsUpdate(float delta)
        {
            return;
        }
        
        /// <summary>
        /// Virtual function. Called by the state machine upon changing the active state.  The 'message' 
        /// parameter is a dictionary with arbitrary data the state can use to initialize itself
        /// </summary>
        /// <param name="message"></param>
        public virtual void EnterState(Dictionary<string, object> message = null)
        {
            return;
        }

        
        /// <summary>
        /// Virtual function. Called by the state machine before changing the active state.
        /// use this function to cleanup the state.
        /// </summary>
        public virtual void ExitState()
        {
            return;
        }


        public override void _Ready()
        {
            // TODO: fix this to just get owner once this is all in a scene
            
            // Assume parent is StateMachine
            var parent = GetParent<Node>();

            // Assume grandparent is target
            var grandparent = parent.GetParent<T>();
            
            grandparent.Ready += OnGrandParentReady;
        }

        private void OnGrandParentReady()
        {
            // Assume parent is StateMachine
            var parent = GetParent<Node>();

            // Assume grandparent is target
            var grandparent = parent.GetParent<T>();
            
            Target = grandparent;

            if (Target == null){
                throw new System.Exception("Could not find owner for state");
            }

            _StateReady();
        }

        /// <summary>
        /// Called once parent and grand parent are ready.
        /// </summary>
        protected abstract void _StateReady();

    }

}
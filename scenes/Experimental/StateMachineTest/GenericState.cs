using Godot;

namespace Experimental.StateMachineTest
{
    
    public partial class GenericState<T> : State 
        where T : Node
    {
        protected T Character { get; private set; }

        public override void _Ready()
        {
            // TODO: fix this once this is all in a scene
            var parent = GetParent<Node>();
            var gpa = parent.GetParent<T>();
            
            
            gpa.Ready += OnOwnerReady;
        }

        private void OnOwnerReady()
        {
            
            var parent = GetParent<Node>();
            var gpa = parent.GetParent<T>();
            
            Character = gpa;

            if (Character == null){
                throw new System.Exception("Could not find owner for state");
            }
        }
    }
}
using Godot;

namespace Experimental.StateMachineTest
{
    // TODO: consider generic implementation?
    public partial class CharacterState : State
    {
        protected CharacterBody2D Character { get; private set; }

        public override void _Ready()
        {
            // TODO: fix this once this is all in a scene
            var parent = GetParent<Node>();
            var gpa = parent.GetParent<CharacterBody2D>();
            gpa.Ready += OnOwnerReady;
        }

        private void OnOwnerReady()
        {
            
            var parent = GetParent<Node>();
            var gpa = parent.GetParent<CharacterBody2D>();
            
            Character = gpa;

            if (Character == null){
                throw new System.Exception("Could not find owner for state");
            }
        }
    }
}
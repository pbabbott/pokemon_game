using System.Collections.Generic;
using Godot;
using Pokemon.UserInput;

namespace Experimental.StateMachineTest
{
    public partial class Idle : CharacterState
    {

        private UserInputReader userInputReader = new UserInputReader();

        /// <summary>
        /// Upon entering the state, we set the velocity to zero
        /// </summary>
        public override void EnterState(Dictionary<string, object> message = null)
        {
            // We must declare all the properties we access through the 'owner' 
            Character.Velocity = Vector2.Zero;
        }

        public override void Update(float delta)
        {
            userInputReader.DetectInput();
            if (userInputReader.IsKeyDown)
            {
                StateMachine.TransitionTo("Walk");
            }
        }
    }
}
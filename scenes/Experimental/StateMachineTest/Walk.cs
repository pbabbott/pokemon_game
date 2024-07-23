using System.Collections.Generic;
using Godot;
using Pokemon.Movement;
using Pokemon.UserInput;

namespace Experimental.StateMachineTest
{
    public partial class Walk : CharacterState
    {
        private UserInputReader userInputReader = new UserInputReader();

        public override void PhysicsUpdate(float delta)
        {
            userInputReader.DetectInput();
            var normalizedInputVector = userInputReader.NormalizedInputVector;
            var isKeyDown = userInputReader.IsKeyDown;

            var movementController = new CharacterMovementController(Character);
            movementController.ApplyUserInput(normalizedInputVector, isKeyDown, delta);
            movementController.ApplyFriction(delta);
            Character.MoveAndSlide();

            // set player velocity

            if (Character.Velocity.IsEqualApprox(Vector2.Zero)) {
                StateMachine.TransitionTo("Idle");
            }
            
            if (Input.IsActionJustPressed("ui_accept")) {
                StateMachine.TransitionTo("Attack");
            }

            // if: just pressed attack button
            // then: transition to attack state

            // if is_equal_approx (velocity, 0)
            // then: transition to idle state
        }
    }
}
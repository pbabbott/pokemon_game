

using System.Collections.Generic;
using Godot;

namespace Pokemon.StateManagement
{
    public interface IState
    {
        void EnterState(Dictionary<string, object> message = null);
        void ExitState();
        void HandleInput(InputEvent @event);
        void Update(float delta);
        void PhysicsUpdate(float delta);
        string Name { get; }
        StateMachine StateMachine { get;set;}  
    }
}
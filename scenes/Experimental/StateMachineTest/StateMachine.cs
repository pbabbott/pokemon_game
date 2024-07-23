

using System.Collections.Generic;
using Godot;

namespace Experimental.StateMachineTest
{

	// Generic state machine. Initializes states and delegates engine callbacks
	// (_PhysicsProcess, _UnhandledInput) to the active state.
	public partial class StateMachine : Node
	{
		/// <summary>
		/// Emitted when transitioning to a new state
		/// </summary>
		[Signal]
		public delegate void TransitionedEventHandler(string stateName);

		/// <summary>
		///  Path to the initial active state.  We export it to be able to pick the initial state in the inspector.
		/// </summary>
		[Export]
		public NodePath InitialState;


		/// <summary>
		/// The Current Active state.  At the start of the game, we get the InitialState
		/// </summary>
		public State State { get; private set; }

	
		public override void _Ready()
		{
			var owner = GetParent<Node>();
			owner.Ready += OnOwnerReady;
		}

		private void OnOwnerReady()
		{
			State = GetNode<State>(InitialState);

			foreach (var child in GetChildren()){
				var state = child as State;
				state.StateMachine = this;
			}

			State.EnterState();
		}

		// The state machine subscribes to node callbacks and delegates them to the state objects.
		public override void _UnhandledInput(InputEvent @event)
		{
			State.HandleInput(@event);
		}

		public override void _Process(double delta)
		{
			State.Update((float)delta);
		}

		public override void _PhysicsProcess(double delta)
		{
			State.PhysicsUpdate((float)delta);
		}

		/// <summary>
		/// This function calls the current state's Exit() function, then changes the active state,
		/// and calls its Enter function. 
		/// It optionally takes a `message` dictionary to pass to the next state's Enter() function.
		/// </summary>
		/// <param name="targetStateName"></param>
		/// <param name="message"></param>
		public void TransitionTo(string targetStateName, Dictionary<string, object> message = null)
		{
			// Safety check, you could use Debug.Assert() here to report an error if the state name is incorrect.
			// We don't use an assert here to help with code reuse. If you reuse a state in different state machines
			// but you don't want them all, they won't be able to transition to states that aren't in the scene tree.
			if (!HasNode(targetStateName))
			{
				return;
			}

			State.ExitState();
			State = GetNode<State>(targetStateName);
			State.EnterState(message);
			EmitSignal("transitioned", State.Name);
		}
	}
}

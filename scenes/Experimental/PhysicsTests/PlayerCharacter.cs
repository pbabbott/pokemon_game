

using System;
using Godot;

namespace Experimental.PhysicsTests
{

	public partial class PlayerCharacter : CharacterBody2D
	{
		[Export(PropertyHint.Range, "0,5000,100")]
		public float PushForce = 100f;

		[Export(PropertyHint.Range, "0,100,5")]
		public float CharacterPushForce = 50f;

		public float Speed = 200.0f;
		public float Acceleration = 2000f;
		public float Friction { get; set; }

		public override void _PhysicsProcess(double delta)
		{
			this.Friction = this.Acceleration / this.Speed;
			PlayerMovement((float)delta);
			CheckCollisions((float)delta);
		}

		private void CheckCollisions(float delta)
		{
			var count = GetSlideCollisionCount();

			for (int i = 0; i < count; i++)
			{

				var collision = GetSlideCollision(i);
				var collider = collision.GetCollider();

				var rigidBody = collider as RigidBody2D;

				if (rigidBody != null)
				{
					rigidBody.ApplyCentralImpulse(-collision.GetNormal() * PushForce);
				}

				var characterBody = collider as CharacterBody2D;
				if (characterBody != null)
				{
					characterBody.Velocity += -collision.GetNormal() * CharacterPushForce;
				}
			}

		}

		public void PlayerMovement(float delta)
		{
			// move to first thing (input controller)
			var downKey = Input.IsActionPressed("ui_down");
			var rightKey = Input.IsActionPressed("ui_right");
			var leftKey = Input.IsActionPressed("ui_left");
			var upKey = Input.IsActionPressed("ui_up");

			var isKeyDown = downKey || rightKey || leftKey || upKey;
			ApplyUserInput(isKeyDown, delta);
			ApplyFriction(delta);
			MoveAndSlide();
		}

		public void ApplyUserInput(bool isKeyDown, float delta)
		{
			var facing = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

			if (isKeyDown)
			{
				var accelerationVector = facing * Acceleration;
				Velocity += accelerationVector * delta;
			}
		}

		public void ApplyFriction(float delta)
		{
			Velocity -= Velocity * Friction * delta;
		}

	}

}
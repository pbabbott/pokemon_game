
using System;
using Godot;

public class CharacterMovementController
{
	private CharacterBody2D _Character { get; set; }
	public Vector2 Velocity
	{
		get => _Character.Velocity;
		set => _Character.Velocity = value;
	}
	public CharacterMovementController(CharacterBody2D character)
	{
		_Character = character;
	}

	public CharacterMovementConfig MovementConfig { get; set; } = new CharacterMovementConfig();

	private float Friction => MovementConfig.Friction;
	private float Acceleration => MovementConfig.Acceleration;

	public void ApplyUserInput(Vector2 normalizedInputVector, bool isKeyDown, float delta)
	{
		// No need to apply speed check, as friction will do it.

		if (isKeyDown)
		{
			Velocity += normalizedInputVector * Acceleration * delta;
		}
	}
	
	public void ApplyFriction(float delta)
	{
		Velocity -= Velocity * Friction * delta;
	}

	public void ApplyCleanStop()
	{
		if (Velocity.Length() < .1f)
		{
			Velocity = Vector2.Zero;
		}
	}
	// public void ApplyFriction()
	// {
	// 	if (Velocity != Vector2.Zero)
	// 	{
	// 		var normalizedVelocity = Velocity.Normalized();
	// 		var xFriction = Math.Min(Friction, Math.Abs(Velocity.X)) * normalizedVelocity.X;
	// 		var yFriction = Math.Min(Friction, Math.Abs(Velocity.Y)) * normalizedVelocity.Y;
	// 		var frictionVector = new Vector2(xFriction, yFriction);
	// 		Velocity -= frictionVector;
	// 	}
	// }

	// private void ApplyPush(CharacterBody2D pusher, CharacterBody2D pushee)
	// {
	// 	// Calculate push direction
	// 	Vector2 pushDirection = (pushee.GlobalPosition - pusher.GlobalPosition).Normalized();

	// 	var maxPushSpeed = Speed * PushScale;

	// 	pusher.Velocity = pusher.Velocity.Normalized() * maxPushSpeed;
	// 	var pushVelocity = (pushDirection + pusher.Velocity).Normalized() * maxPushSpeed;
	// 	// var pushVelocity = pushDirection * maxPushSpeed;
	// 	pushee.Velocity = pushVelocity;
	// }

	// public void ApplyPush(CharacterBody2D pusher)
	// {
	// 	ApplyPush(pusher, _Character);
	// 	// ApplyPush(_Character, pusher, OpposingScale);
	// }

	

}
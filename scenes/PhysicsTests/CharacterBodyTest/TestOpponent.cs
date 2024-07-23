using Godot;
using System;

public partial class TestOpponent : CharacterBody2D
{
	public float Speed = 200.0f;
	public float Acceleration = 2000f;
	public float Friction { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		this.Friction = this.Acceleration / this.Speed;
		this.ApplyFriction((float)delta);
		MoveAndSlide();
	}

	public void ApplyFriction(float delta)
	{
		Velocity -= Velocity * Friction * delta;
	}
}

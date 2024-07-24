using Godot;
using Godot.NativeInterop;
using System;
using Pokemon.Movement;
using Pokemon.SpriteDirection;
using Pokemon.UserInput;

public partial class Player : CharacterBody2D
{
	[Export]
	public bool DebugVelocity { get; set; }

	

	public CollisionShape2D CollisionShape { get; private set;}
	public PokemonAnimatedSprite2D AnimatedSprite { get; private set; }
	

	public Player()
	{
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CollisionShape = GetNode<CollisionShape2D>("CharacterCollider");

		AnimatedSprite = GetNode<PokemonAnimatedSprite2D>("./PlayerAnimation");
		
		AnimatedSprite.InitializeSprite("0007-Squirtle", AnimationType.Walk);
		AnimatedSprite.InitializeSprite("0007-Squirtle", AnimationType.Attack);

		AnimatedSprite.Play("Walk_Down");
		AnimatedSprite.SetFrameAndProgress(0, 0);
		AnimatedSprite.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (DebugVelocity)
		{
			QueueRedraw();
		}
	}

	public override void _Draw()
	{
		if (DebugVelocity)
		{
			DrawString(ThemeDB.FallbackFont, Vector2.Zero, $"Velocity: {Velocity.Length()}");
		}
	}



}

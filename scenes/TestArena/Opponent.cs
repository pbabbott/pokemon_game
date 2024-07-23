using Godot;
using System;
using Pokemon.Movement;

public partial class Opponent : CharacterBody2D
{

	// private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
	private PokemonAnimatedSprite2D _animatedSprite;
	private readonly CharacterMovementController _movementController;

	public Opponent()
	{
		_movementController = new CharacterMovementController(this);
	}

	public override void _Ready()
	{
		_animatedSprite = GetNode<PokemonAnimatedSprite2D>("./OpponentAnimation");
		_animatedSprite.InitializeSprite("0004-Charmander", AnimationType.Walk);
		_animatedSprite.Play("Walk_Down");
		_animatedSprite.SetFrameAndProgress(0, 0);
		_animatedSprite.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{
		var d = (float)delta;

		_movementController.ApplyFriction(d);
		_movementController.ApplyCleanStop();

		MoveAndSlide();
	}



	// [Export]
	// public bool DebugVelocity { get; set; }

	
	// private readonly CharacterCollisionController _collsionController;
	// private readonly CharacterMovementController _movementController;


	// [Export(PropertyHint.Range, "0,1,.1")]
	// public float PushScale = .2f;

	// [Export(PropertyHint.Range, "0,50,2")]
	// public float PostPushImpulse = 50f;

	// public Opponent()
	// {
	// 	_movementController = new CharacterMovementController(this);
	// 	_collsionController = new CharacterCollisionController(this);
	// }

	// public override void _Draw()
	// {
	// 	if (DebugVelocity)
	// 	{
	// 		DrawString(ThemeDB.FallbackFont, Vector2.Zero, $"Velocity: {Velocity.Length()}");
	// 	}
	// }
	
	// public void ApplyPush(CharacterBody2D pusher)
	// {
	// 	_movementController.ApplyPush(pusher);
	// 	this.MoveAndSlide();
	// }


	// // TODO: Convert to interface
	// public void ApplyImpulse(CharacterBody2D pusher)
	// {
	// 	_movementController.ApplyImpulse(pusher);
	// 	MoveAndSlide();
	// }
}

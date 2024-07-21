using Godot;
using System;

public partial class Opponent : CharacterBody2D, IPushable
{
	private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
	private PokemonAnimatedSprite2D _animatedSprite;
	private readonly CharacterCollisionController _collsionController;
	private readonly CharacterMovementController _movementController;


	[Export(PropertyHint.Range, "0,200,5")] 
	public float PushForce = 80f;

	public Opponent()
	{
		_movementController = new CharacterMovementController(this);
		_collsionController = new CharacterCollisionController(this);
	}

	public override void _Draw()
	{
		DrawString(ThemeDB.FallbackFont, Vector2.Zero, $"Velocity: {Velocity}");
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
		_movementController.PushForce = PushForce;

		QueueRedraw();
		_movementController.ApplyFriction();
		_movementController.ApplyCleanStop();
		_collsionController.HandleCollisions();

		MoveAndSlide();
	}

	public void ApplyPush(CharacterBody2D pusher)
	{
		_movementController.ApplyPush(pusher);
		this.MoveAndSlide();
	}

}

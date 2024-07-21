using Godot;
using Godot.NativeInterop;
using System;

public partial class Player : CharacterBody2D
{
	private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
	private readonly SpriteDirectionController _spriteDirectionController = new SpriteDirectionController();
	private readonly CharacterMovementController _movementController;
    private readonly CharacterCollisionController _collsionController;
    private PokemonAnimatedSprite2D _animatedSprite;
	private SpriteDirection Direction => _spriteDirectionController.Direction;

	
	public Player()
	{
		_movementController = new CharacterMovementController(this);
		_collsionController = new CharacterCollisionController(this);
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<PokemonAnimatedSprite2D>("./PlayerAnimation");
		_animatedSprite.InitializeSprite("0007-Squirtle", AnimationType.Walk);
		_animatedSprite.Play("Walk_Down");
		_animatedSprite.SetFrameAndProgress(0, 0);
		_animatedSprite.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{

		PlayerMovement(delta);
		QueueRedraw();
		_collsionController.HandleCollisions();
		MoveAndSlide();
	}

	public override void _Draw()
	{
		DrawString(ThemeDB.FallbackFont, Vector2.Zero, $"Velocity: {Velocity}");
	}

	

	public void PlayerMovement(double delta)
	{
		var downKey = Input.IsActionPressed("ui_down");
		var rightKey = Input.IsActionPressed("ui_right");
		var leftKey = Input.IsActionPressed("ui_left");
		var upKey = Input.IsActionPressed("ui_up");

		var isKeyDown = downKey || rightKey || leftKey || upKey;
		_spriteDirectionController.SetSpriteDirection(downKey, rightKey, leftKey, upKey);

		var isMoving = Velocity != Vector2.Zero;
		MovementAnimation(isMoving);

		_movementController.ApplyUserInput(isKeyDown);
		_movementController.ApplyFriction();
		_movementController.ApplyCleanStop();
		
	}

	private void MovementAnimation(bool isMoving)
	{
		var animationName = _animationNameFactory.GetAnimationName(AnimationType.Walk, Direction);

		var activeAnimation = _animatedSprite.Animation.ToString();
		var isPlaying = _animatedSprite.IsPlaying();

		if (activeAnimation != animationName || !isPlaying) {
			_animatedSprite.Play(animationName);	
		}

		if(!isMoving) {
			_animatedSprite.Stop();
			_animatedSprite.SetFrameAndProgress(0, 0);
		}
	}

    // public void ApplyPush(CharacterBody2D pusher)
	// {

	// 	_movementController.ApplyPush(pusher);
	// 	this.MoveAndSlide();
	// }

}

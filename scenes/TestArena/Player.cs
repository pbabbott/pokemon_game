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

	private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
	private readonly SpriteDirectionController _spriteDirectionController = new SpriteDirectionController();
	private readonly UserInputReader _userInputReader = new UserInputReader();
	
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
		_animatedSprite.InitializeSprite("0007-Squirtle", AnimationType.Attack);

		_animatedSprite.Play("Walk_Down");
		_animatedSprite.SetFrameAndProgress(0, 0);
		_animatedSprite.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{
		var d = (float)delta;

		_userInputReader.DetectInput();
		PlayerMovement(d);
		CheckCollisions(d);

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

	private void CheckCollisions(float delta) 
	{
		_collsionController.HandleCollisions();
	}

	private void PlayerMovement(float delta)
	{
		_spriteDirectionController.SetSpriteDirection(_userInputReader);

		var isMoving = Velocity != Vector2.Zero;
		var isKeyDown = _userInputReader.IsKeyDown;
		MovementAnimation(isMoving || isKeyDown);

		var normalizedInputVector = _userInputReader.NormalizedInputVector;
		_movementController.ApplyUserInput(normalizedInputVector, isKeyDown, delta);
		_movementController.ApplyFriction(delta);
		_movementController.ApplyCleanStop();
		MoveAndSlide();
		

	}

	private void MovementAnimation(bool isMoving)
	{
		var animationName = _animationNameFactory.GetAnimationName(AnimationType.Walk, Direction);

		var activeAnimation = _animatedSprite.Animation.ToString();
		var isPlaying = _animatedSprite.IsPlaying();

		if (activeAnimation != animationName || !isPlaying)
		{
			_animatedSprite.Play(animationName);
		}

		if (!isMoving)
		{
			_animatedSprite.Stop();
			_animatedSprite.SetFrameAndProgress(0, 0);
		}
	}

}

using Godot;
using System;

public partial class Bulbasaur : CharacterBody2D
{
	private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
	private PokemonAnimatedSprite2D _animatedSprite;
	private SpriteDirection Direction = SpriteDirection.DOWN;
	[Export(PropertyHint.Range, "0,200,10")] 
	public float Speed = 100.0f;
	[Export(PropertyHint.Range, "0,50,5")] 
	public float Acceleration = 30f;
	[Export(PropertyHint.Range, "0,50,5")] 
	public float Friction = 15f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<PokemonAnimatedSprite2D>("./BulbasaurAnimation");
    	_animatedSprite.InitializeSprite("0001-Bulbasaur", AnimationType.Walk);
		_animatedSprite.Play("Walk_Down");
		_animatedSprite.SetFrameAndProgress(0, 0);
		_animatedSprite.Stop();
	}

	// public override void _Draw()
	// {
	// 	DrawString(ThemeDB.FallbackFont, Vector2.Zero, $"Velocity: {Velocity}");
	// }

	private void SetSpriteDirection(bool downKey, bool rightKey, bool leftKey, bool upKey)
	{
		if (downKey)
		{
			if (rightKey)
			{
				Direction = SpriteDirection.DOWN_RIGHT;
			}
			else if (leftKey)
			{
				Direction = SpriteDirection.DOWN_LEFT;
			}
			else
			{
				Direction = SpriteDirection.DOWN;
			}
		}
		else if (upKey)
		{
			if (rightKey)
			{
				Direction = SpriteDirection.UP_RIGHT;
			}
			else if (leftKey)
			{
				Direction = SpriteDirection.UP_LEFT;
			}
			else
			{
				Direction = SpriteDirection.UP;
			}
		}
		else if (rightKey)
		{
			Direction = SpriteDirection.RIGHT;
		}
		else if (leftKey)
		{
			Direction = SpriteDirection.LEFT;
		}
	}

	public void PlayerMovement(double delta)
	{
		var downKey = Input.IsActionPressed("ui_down");
		var rightKey = Input.IsActionPressed("ui_right");
		var leftKey = Input.IsActionPressed("ui_left");
		var upKey = Input.IsActionPressed("ui_up");

		var isKeyDown = downKey || rightKey || leftKey || upKey;

		var facing = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

		this.SetSpriteDirection(downKey, rightKey, leftKey, upKey);

		MovementAnimation(isMoving: isKeyDown);
		
		if (isKeyDown) {
			
			var accelerationVector = facing * Acceleration;
			 Velocity += accelerationVector;

			if (Velocity.Length() > Speed) {
				Velocity = Velocity.Normalized() * Speed;
			}

		} else {
			if (Velocity.Length() < 1f) {
				Velocity = Vector2.Zero;
			}
		}

		if (Velocity != Vector2.Zero) {

			var xFriction = Math.Min(Friction, Math.Abs(Velocity.X));
			var yFriction = Math.Min(Friction, Math.Abs(Velocity.Y));

			var normalizedVelocity = Velocity.Normalized();
			var frictionVector = new Vector2(-xFriction*normalizedVelocity.X, -yFriction*normalizedVelocity.Y);
			Velocity += frictionVector;
		}
	}

	private void MovementAnimation(bool isMoving)
	{
		var animationName = _animationNameFactory.GetAnimationName(AnimationType.Walk, Direction);

		var activeAnimation = _animatedSprite.Animation.ToString();
		var isPlaying = _animatedSprite.IsPlaying();

		if (activeAnimation != animationName || isMoving) {
			_animatedSprite.Play(animationName);	
		}

		if(!isMoving) {
			_animatedSprite.Stop();
			_animatedSprite.SetFrameAndProgress(0, 0);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		PlayerMovement(delta);
		// QueueRedraw();
		MoveAndSlide();
	}
}

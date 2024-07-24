using System.Collections.Generic;
using Godot;
using Pokemon.Movement;
using Pokemon.SpriteDirection;
using Pokemon.StateManagement;
using Pokemon.UserInput;


public partial class Walk : BaseState<Player>
{
    private readonly UserInputReader _userInputReader = new UserInputReader();
    private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
    private readonly SpriteDirectionController _spriteDirectionController = new SpriteDirectionController();

    private CharacterCollisionController _collsionController;
    private CharacterMovementController _movementController;

    private SpriteDirection _spriteDirection => _spriteDirectionController.Direction;


    protected override void _StateReady()
    {
        _collsionController = new CharacterCollisionController(Target);
        _movementController = new CharacterMovementController(Target);
    }

    [Export]
    public float IdleThreshold { get; set; } = 1f;


    public override void EnterState(Dictionary<string, object> message = null)
    {
        Target.CollisionShape.DebugColor = new Color("d345b76b"); // Pink
    }

    public override void PhysicsUpdate(float delta)
    {
        // Get user input
        _userInputReader.DetectInput();
        var normalizedInputVector = _userInputReader.NormalizedInputVector;
        var isKeyDown = _userInputReader.IsMovementKeyDown;

        // Move the character
        _movementController.ApplyUserInput(normalizedInputVector, isKeyDown, delta);
        _movementController.ApplyFriction(delta);
        Target.MoveAndSlide();

        // Check collisions
        CheckCollisions(delta);

        var eventData = new Dictionary<string, object>()
        {
            { "sprite_direction", _spriteDirection}
        };


        // Transition state
        if (Target.Velocity.Length() < IdleThreshold)
        {
            StateMachine.TransitionTo("Idle", eventData);
        }
        if (Input.IsActionJustPressed("ui_accept"))
        {
            StateMachine.TransitionTo("Attack", eventData);
        }
    }

    public override void Update(float delta)
    {
        _spriteDirectionController.SetSpriteDirection(_userInputReader);

        var isMoving = Target.Velocity != Vector2.Zero;
        var isKeyDown = _userInputReader.IsMovementKeyDown;
        MovementAnimation(isMoving || isKeyDown);
    }


    private void MovementAnimation(bool isMoving)
    {
        var animationName = _animationNameFactory.GetAnimationName(AnimationType.Walk, _spriteDirection);

        var animatedSprite = Target.AnimatedSprite;
        var activeAnimation = animatedSprite.Animation.ToString();
        var isPlaying = animatedSprite.IsPlaying();

        if (activeAnimation != animationName || !isPlaying)
        {
            animatedSprite.Play(animationName);
        }


    }

    private void CheckCollisions(float delta)
    {
        _collsionController.HandleCollisions();
    }



}
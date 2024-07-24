using System.Collections.Generic;
using Godot;
using Pokemon.SpriteDirection;
using Pokemon.StateManagement;
using Pokemon.UserInput;


public partial class Idle : BaseState<Player>
{
    private UserInputReader userInputReader = new UserInputReader();
    private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();
    private SpriteDirection _spriteDirection;


    public override void EnterState(Dictionary<string, object> message = null)
    {
        Target.Velocity = Vector2.Zero;
        Target.CollisionShape.DebugColor = new Color("0099b36b"); // default blue

        // Get direction
        if (message != null)
        {
            var spriteDirection = message["sprite_direction"];
            _spriteDirection = spriteDirection == null ? SpriteDirection.DOWN : (SpriteDirection)spriteDirection;
        }

        // Set animation to first frame of walk
        var animationName = _animationNameFactory.GetAnimationName(AnimationType.Walk, _spriteDirection);
        var animatedSprite = Target.AnimatedSprite;
        animatedSprite.Play(animationName);
        animatedSprite.Stop();
        animatedSprite.SetFrameAndProgress(0, 0);
    }

    public override void Update(float delta)
    {
        userInputReader.DetectInput();
        if (userInputReader.IsMovementKeyDown)
        {
            StateMachine.TransitionTo("Walk");
        }

        if (userInputReader.PrimaryAttackKey)
        {
            var eventData = new Dictionary<string, object>()
            {
                { "sprite_direction", _spriteDirection}
            };

            StateMachine.TransitionTo("Attack", eventData);
        }
    }

    protected override void _StateReady()
    {
    }
}
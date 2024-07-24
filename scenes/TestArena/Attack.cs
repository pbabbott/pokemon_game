


using System.Collections.Generic;
using Experimental.StateMachineTest;
using Godot;
using Pokemon.SpriteDirection;
using Pokemon.StateManagement;

public partial class Attack : BaseState<Player>
{
	private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();

    private SpriteDirection _spriteDirection;


    protected override void _StateReady()
    {
    }

    public override void EnterState(Dictionary<string, object> message = null)
    {
        Target.CollisionShape.DebugColor = new Color("d761016b"); // orange

        if (message != null)
        {
            _spriteDirection = (SpriteDirection)message["sprite_direction"];
        }

            
        var animatedSprite = Target.AnimatedSprite;
        animatedSprite.AnimationLooped += AnimationCompleted;
    }

    public override void ExitState()
    {
        var animatedSprite = Target.AnimatedSprite;
        animatedSprite.AnimationLooped -= AnimationCompleted;
    }

    private void AnimationCompleted() 
    {
        var eventData = new Dictionary<string, object>()
        {
            { "sprite_direction", _spriteDirection}
        };

        if (Target.Velocity != Vector2.Zero)
            StateMachine.TransitionTo("Walk");
        else 
            StateMachine.TransitionTo("Idle", eventData);
    }

    public override void PhysicsUpdate(float delta)
    {
    }

    public override void Update(float delta)
    {
		var animationName = _animationNameFactory.GetAnimationName(AnimationType.Attack, _spriteDirection);
        
        var animatedSprite = Target.AnimatedSprite;
		var activeAnimation = animatedSprite.Animation.ToString();

		if (activeAnimation != animationName)
		{
			animatedSprite.Play(animationName);
		}

 
    }
}
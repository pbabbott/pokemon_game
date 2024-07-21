
using System;
using Godot;

public class CharacterMovementController
{
    private CharacterBody2D _Character { get; set; }
    public Vector2 Velocity
    {
        get => _Character.Velocity;
        set => _Character.Velocity = value;
    }
    public CharacterMovementController(CharacterBody2D character)
    {
        _Character = character;
    }

	public float PushForce {get;set;}

    [Export(PropertyHint.Range, "0,100,10")] 
	public float Speed = 100.0f;

	[Export(PropertyHint.Range, "0,20,5")] 
	public float Acceleration = 10f;

	[Export(PropertyHint.Range, "0,20,5")] 
	public float Friction = 7f;

	public void ApplyCleanStop() {
		if (Velocity.Length() < .01f) {
			Velocity = Vector2.Zero;
		}
	}
	
    public void ApplyFriction(){
        if (Velocity != Vector2.Zero) {
			var normalizedVelocity = Velocity.Normalized();
            var xFriction = Math.Min(Friction, Math.Abs(Velocity.X)) * normalizedVelocity.X;
			var yFriction = Math.Min(Friction, Math.Abs(Velocity.Y)) * normalizedVelocity.Y;
			var frictionVector = new Vector2(xFriction, yFriction);
			Velocity -= frictionVector;
		}
    }

	private void ApplyPush(CharacterBody2D pusher, CharacterBody2D pushee, float scale){
		// Calculate push direction
		Vector2 pushDirection = (pushee.GlobalPosition - pusher.GlobalPosition).Normalized();

		// Apply push force
		Vector2 pushVelocity = pushDirection * PushForce * scale;
		
		// Option 2: Add to current velocity
		pushee.Velocity += pushVelocity;

		if (pushee.Velocity.Length() >= Speed) {
			pushee.Velocity = pushee.Velocity.Normalized() * Speed;
		}
	}

	public void ApplyPush(CharacterBody2D pusher) {
		
		ApplyPush(_Character, pusher, .25f);
		ApplyPush(pusher, _Character, 1f);

	}
    public void ApplyUserInput(bool isKeyDown) 
    {
		var facing = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

        if (isKeyDown) {
			
			var accelerationVector = facing * Acceleration;
			Velocity += accelerationVector;

			if (Velocity.Length() >= Speed) {
				Velocity = Velocity.Normalized() * Speed;
			}

		} else {
			// apply clean stop?	
		}
    }

}
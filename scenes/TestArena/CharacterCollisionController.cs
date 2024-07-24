using Godot;

public class CharacterCollisionController 
{

	public float CharacterPushForce = 50f;

    private CharacterBody2D _Character { get; set; }

    public CharacterCollisionController(CharacterBody2D character)
    {
        _Character = character;
    }


    public void HandleCollisions() {

		var collisionCount = _Character.GetSlideCollisionCount();

		for (int i = 0; i < collisionCount; i++) {
			var collision = _Character.GetSlideCollision(i);
			var collider = collision.GetCollider();

			var characterBody = collider as CharacterBody2D;
			if (characterBody != null){
				characterBody.Velocity += -collision.GetNormal()*CharacterPushForce;
			}
		}
	}
}
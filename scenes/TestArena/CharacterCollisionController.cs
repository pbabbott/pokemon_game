using Godot;

public class CharacterCollisionController 
{
    private CharacterBody2D _Character { get; set; }
    public CharacterCollisionController(CharacterBody2D character)
    {
        _Character = character;
    }


    public void HandleCollisions() {
		var count = _Character.GetSlideCollisionCount();

		for (int i = 0; i < count; i++) {
			var collision = _Character.GetSlideCollision(i);
			var collider = collision.GetCollider();
			var otherCharacter = collider as IPushable;
			if (otherCharacter != null) {
                otherCharacter.ApplyPush(_Character);
			}
		}
	}
}
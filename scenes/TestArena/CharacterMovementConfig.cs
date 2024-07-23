public class CharacterMovementConfig 
{
	public float Speed = 200.0f;
	public float Acceleration = 2000f;
	public float Friction 
    {
        get {
            return Acceleration / Speed;
        }
    }
}
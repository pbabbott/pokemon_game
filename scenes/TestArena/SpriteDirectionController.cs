public class SpriteDirectionController 
{
    public SpriteDirection Direction { get; private set;}
    public void SetSpriteDirection(bool downKey, bool rightKey, bool leftKey, bool upKey)
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using Pokemon.SpriteDirection;

public class AnimationNameFactory
{
    private string GetDirectionAsString(SpriteDirection direction) {
        switch(direction) {
            case SpriteDirection.DOWN:
                return "Down";
            case SpriteDirection.DOWN_RIGHT:
                return "Down_Right";
            case SpriteDirection.RIGHT:
                return "Right";
            case SpriteDirection.UP_RIGHT:
                return "Up_Right";
            case SpriteDirection.UP:
                return "Up";
            case SpriteDirection.UP_LEFT:
                return "Up_Left";
            case SpriteDirection.LEFT:
                return "Left";
            case SpriteDirection.DOWN_LEFT:
                return "Down_Left";
        }

        throw new NotImplementedException("Missing sprite direction naming convention");
    }

    public string GetAnimationName(AnimationType animationType, SpriteDirection direction)
    {
        var directionalAnimationNames = new List<AnimationType>(){
            AnimationType.Walk,
            AnimationType.Attack
        };

        if (directionalAnimationNames.Any(x => x.Equals(animationType))) {
            var dirAsString = GetDirectionAsString(direction);
            return $"{animationType}_{dirAsString}";
        }

        throw new NotImplementedException($"Need to implement naming convention for {animationType} and direction: {direction}");
    }
}
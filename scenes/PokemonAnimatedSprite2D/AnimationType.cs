using System;

public class AnimationType : IEquatable<AnimationType>
{
    private AnimationType(string value) { Value = value; }

    public string Value { get; private set; }

    public static AnimationType Walk { get { return new AnimationType("Walk"); } }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        AnimationType other = (AnimationType)obj;
        return Value == other.Value;
    }

    public bool Equals(AnimationType other)
    {
        return other != null && Value == other.Value;
    }
    public override string ToString()
    {
        return Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

}
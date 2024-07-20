using Godot;
using System;

public partial class PokemonAnimatedSprite2D : AnimatedSprite2D
{
	private readonly SpriteInitializer _spriteInitalizer = new SpriteInitializer();

	public void InitializeSprite(string spriteDirectory, AnimationType animationType) 
	{
		_spriteInitalizer.Initialize(SpriteFrames, spriteDirectory, animationType);
	}

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
	}
}

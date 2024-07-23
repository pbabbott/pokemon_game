using Godot;
using System;

public partial class Pikachu2 : Node2D
{

	private AnimatedSprite2D _sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite =  GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var spriteFrames = _sprite.SpriteFrames;
		var texture = spriteFrames.GetFrameTexture("walk_down", 1);
		
		_sprite.Play("walk_left");
	}
}

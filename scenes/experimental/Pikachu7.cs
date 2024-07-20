using Godot;
using System;
using System.Linq;

public partial class Pikachu7 : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var texture = GD.Load<Texture2D>("res://pokemon_data/0025-Pikachu/Walk-Anim.png");
		
		var atlasTexture = new AtlasTexture()
		{
			Atlas = texture,
			Region = new Rect2(0, 0, 32, 40)
		};

		this.SpriteFrames.AddAnimation("walk_down");
		this.SpriteFrames.AddFrame("walk_down", atlasTexture);

		this.Play("walk_down");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

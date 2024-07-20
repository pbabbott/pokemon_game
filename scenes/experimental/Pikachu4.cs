using Godot;
using System;
using System.Linq;

public partial class Pikachu4 : Node2D
{

    private Sprite2D _sprite;

	public override void _Ready()
	{
        _sprite = GetNode<Sprite2D>("Sprite2D");

        var texture = GD.Load<Texture2D>("res://pokemon_data/0025-Pikachu/Walk-Anim.png");
        
        var atlasTexture = new AtlasTexture()
        {
            Atlas = texture,
            Region = new Rect2(0, 0, 128, 40)
        };

        _sprite.Texture = atlasTexture; 
        _sprite.Hframes = 4;
        _sprite.Vframes = 1;
        _sprite.Frame = 0;
	}

	public override void _Process(double delta)
	{
	}
}

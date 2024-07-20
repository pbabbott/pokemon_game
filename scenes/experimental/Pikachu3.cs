using Godot;
using System;
using System.Linq;

public partial class Pikachu3 : Node2D
{

	private AnimationPlayer _player;
	private Sprite2D _sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player =  GetNode<AnimationPlayer>("AnimationPlayer");
		_sprite = GetNode<Sprite2D>("Sprite2D");

		
		// var spriteFrames = _sprite.SpriteFrames;
		
		// spriteFrames.AddAnimation("walk_down");
		// var animation = spriteFrames.Animations.ToList().First();
		
		// this.Position = new Vector2(100, 100);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// _sprite =  GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		// var spriteFrames = _sprite.SpriteFrames;
		// var animation = spriteFrames.Animations.ToList().First();
		if (!_player.IsPlaying())
		{
			_player.Play("walking/walk_down");
		}
		var animation = _player.GetAnimation("walking/walk_down");
		var animations = _player.GetAnimationList().ToArray();
		// GD.Print($"Pk3: Current frame: {_sprite.Frame}, Animation playing: {_player.IsPlaying()}, Current animation: {_player.CurrentAnimation}");

	}
}

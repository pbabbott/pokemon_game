using Godot;
using System;
using System.Linq;

public partial class Pikachu5 : Node2D
{

	private AnimationPlayer _player;
	private Sprite2D _sprite;

	public override void _Ready()
	{
		// https://github.com/TheDoorworlds/AutoAnimationCreatorDEMO/blob/master/Autoloads/AnimationLibrary.gd
		_sprite =  GetNode<Sprite2D>("Sprite2D");
		_player =  GetNode<AnimationPlayer>("AnimationPlayer");

		var animation = new Animation();
		var trackId = animation.AddTrack(Animation.TrackType.Value);

		animation.Step = 0.1f;
		animation.LoopMode = Animation.LoopModeEnum.Linear;
		animation.Length = .5f;
		
		animation.ValueTrackSetUpdateMode(trackId, Animation.UpdateMode.Discrete);
		
		var path = _sprite.GetPath()+":frame";
		animation.TrackSetPath(trackId, path);

		var frameCount = 4;
		
		var time = 0f;
		for (int i = 0; i < frameCount; i++) {
			GD.Print($"frameTrackid: ${trackId} time: ${time} i: ${i}");
			animation.TrackInsertKey(trackId, time, i);
			time += .1f;
		}

		
		var animationLib = new AnimationLibrary();
		_player.AddAnimationLibrary("walking", animationLib);
		animationLib.AddAnimation("walk_down", animation);

		_player.Play("walking/walk_down");
	}

	public override void _Process(double delta)
	{
		if (!_player.IsPlaying())
		{
			_player.Play("walking/walk_down");
		}
		var animation = _player.GetAnimation("walking/walk_down");
		var animations = _player.GetAnimationList().ToArray();
		// GD.Print($"Pk5: Current frame: {_sprite.Frame}, Animation playing: {_player.IsPlaying()}, Current animation: {_player.CurrentAnimation}");
		
	}
}

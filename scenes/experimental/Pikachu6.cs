using Godot;
using System;

public partial class Pikachu6 : Node2D
{

	private AnimationPlayer _player;
	private Sprite2D _sprite;

	private const string TexturePath = "res://pokemon_data/0025-Pikachu/Walk-Anim.png";

	public override void _Ready()
	{
        _sprite = GetNode<Sprite2D>("Sprite2D");
		_player =  GetNode<AnimationPlayer>("AnimationPlayer");

		var vFrames = 4;
		InitSpriteTexture(vFrames, 8, 32, 40);
		InitAnimation(vFrames);
	}

	private void InitSpriteTexture(int Hframes, int Vframes, int frameWidth, int frameHeight)
	{
        var texture = GD.Load<Texture2D>(TexturePath);

		var width = (float)(Hframes * frameWidth);
		var height = (float)(Vframes * frameHeight);
		//GD.Print($"width: ${width} height: ${height}");
		var atlasTexture = new AtlasTexture()
		{
			Atlas = texture,
			Region = new Rect2(0, 0, width, height)
		};

        _sprite.Texture = atlasTexture; 
        _sprite.Hframes = Hframes;
        _sprite.Vframes = Vframes;
        _sprite.Frame = 0;
	}

	private void InitAnimation(int vFrames) 
	{
		var animationLib = new AnimationLibrary();
		_player.AddAnimationLibrary("walking", animationLib);

		var animation = new Animation()
		{
			Step = 0.1f,
			LoopMode = Animation.LoopModeEnum.Linear,
			Length = 0.5f
		};

		InitAnimationTrack(animation, vFrames);	
		
		animationLib.AddAnimation("walk_down", animation);

		_player.Play("walking/walk_down");
	}

	private void InitAnimationTrack(Animation animation, int vFrames) 
	{
		var trackId = animation.AddTrack(Animation.TrackType.Value);
		animation.ValueTrackSetUpdateMode(trackId, Animation.UpdateMode.Discrete);
		
		var path = _sprite.GetPath()+":frame";
		animation.TrackSetPath(trackId, path);
		
		var time = 0f;
		for (int i = 0; i < vFrames; i++) {
			//GD.Print($"frameTrackid: ${trackId} time: ${time} i: ${i}");
			animation.TrackInsertKey(trackId, time, i);
			time += .1f;
		}

	}

	public override void _Process(double delta)
	{
	}
}

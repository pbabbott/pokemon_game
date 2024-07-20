using Godot;
using System;

public partial class Rattata : Node2D
{
	private PokemonAnimatedSprite2D _animatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<PokemonAnimatedSprite2D>("RattataAnimation");
		_animatedSprite.InitializeSprite("0019-Rattata", AnimationType.Walk);
		_animatedSprite.Play("Walk_Down");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

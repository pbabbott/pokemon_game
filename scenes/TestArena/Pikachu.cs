using Godot;
using System;

public partial class Pikachu : Node
{
	private PokemonAnimatedSprite2D _animatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<PokemonAnimatedSprite2D>("./PikachuAnimation");
		_animatedSprite.InitializeSprite("0025-Pikachu", AnimationType.Walk);
		_animatedSprite.Play("Walk_Down");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

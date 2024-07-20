

using System;
using System.Linq;
using Godot;


public class SpriteInitializer
{
	private const float AnimationDurationScale = 0.1f;
	private readonly SpriteMetadataLoader _metadataLoader = new SpriteMetadataLoader();
	private readonly AnimationNameFactory _animationNameFactory = new AnimationNameFactory();

	private const string SpriteResourceDirectory = "res://pokemon_data/";
	private string ComputeTexturePath(string directory, AnimationType animationType)
	{
		return $"{SpriteResourceDirectory}/{directory}/{animationType}-Anim.png";
	}
	private Texture2D GetImageTexture(string spriteDirectory, AnimationType animationType)
	{
		var texturePath = ComputeTexturePath(spriteDirectory, animationType);
		return GD.Load<Texture2D>(texturePath);
	}

	private AtlasTexture GetAtlas(SpriteCoordinate coordinate, Anim metadata)
	{
		var (row, column) = coordinate;
		var width = metadata.FrameWidth;
		var height = metadata.FrameHeight;

		return new AtlasTexture()
		{
			Region = new Rect2(
				x: column * width,
				y: row * height,
				width,
				height)
		};
	}

	internal void Initialize(SpriteFrames spriteFrames, string spriteDirectory, AnimationType animationType)
	{
		GD.Print($"Initializing sprite: {spriteDirectory} {animationType}");

		var texture = GetImageTexture(spriteDirectory, animationType);
		var metadata = _metadataLoader.GetAnimData(spriteDirectory);

		var metadataForType = metadata.Anims.Single(x => x.Name == animationType.ToString());

		// Determine frame height and width
		var frameHeight = metadataForType.FrameHeight;
		var frameWidth = metadataForType.FrameWidth;

		// Compute VFrames and HFrames
		var Vframes = metadataForType.Durations.Count();
		var Hframes = texture.GetHeight() / frameHeight;

		// Iterate through the rows
		for (int i = 0; i < Hframes; i++)
		{
			// Create one animation per row
			// For now, assume that the row is the sprite direction
			// this will need to be changed for animations that don't conform to the cardinal directions
			var animationName = _animationNameFactory.GetAnimationName(animationType, (SpriteDirection)i);
			spriteFrames.AddAnimation(animationName);

			// Iterate by column
			for (int j = 0; j < Vframes; j++)
			{
				var coordinate = new SpriteCoordinate(i, j);
				var atlasTexture = GetAtlas(coordinate, metadataForType);
				atlasTexture.Atlas = texture;
				var duration = metadataForType.Durations[j] * AnimationDurationScale;
				spriteFrames.AddFrame(animationName, atlasTexture, duration, j);
			}
		}
	}
}

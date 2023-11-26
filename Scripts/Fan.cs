using Godot;

public partial class Fan : AnimatedSprite2D
{
	public override void _Ready()
	{
		SpriteFrames.SetAnimationLoop("on", true);
		Play("on");
	}

	public override void _Process(double delta)
	{
		// TODO check if the game state is in poweroutage and stop fan
	}
}

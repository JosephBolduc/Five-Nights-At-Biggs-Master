using Godot;
using System;

public partial class Background : Node2D
{
	private AnimatedSprite2D mainBackground;
	private door_and_buttons doorController;
	private ArmedController armedController;

	public override void _Ready()
	{
		mainBackground = GetNode<AnimatedSprite2D>("Foreground");
		armedController = GetNode<ArmedController>("ArmedController");
		doorController = GetNode<door_and_buttons>("DoorAndButtons");
	}

	public override void _Process(double delta)
	{
		if(doorController.leftLightOn) mainBackground.Play("left");
		else if(doorController.rightLightOn) mainBackground.Play("right");
		else mainBackground.Play("default");
		
	}
}

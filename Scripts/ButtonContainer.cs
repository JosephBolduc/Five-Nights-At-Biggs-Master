using System.Collections.Generic;
using FiveNightsAtPoobs.Scripts;
using Godot;
using Godot.Collections;

public partial class ButtonContainer : Node2D
{
	public List<cameraButton> buttonList;

	public override void _Ready()
	{
		buttonList = new List<cameraButton>();
		Array<Node> children = GetChildren();
		foreach (Node child in children) buttonList.Add(child as cameraButton);
	}

	public void UpdateMovement(BotMovementController.Robot movedBot)
	{

	}
}

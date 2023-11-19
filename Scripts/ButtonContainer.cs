using Godot;
using System;
using System.Collections.Generic;

public partial class ButtonContainer : Node2D
{
	public List<cameraButton> buttonList;
	public override void _Ready()
	{
		buttonList = new();
		var children = GetChildren();
		foreach (Node child in children)
		{
			buttonList.Add(child as cameraButton);
		}
	}
}

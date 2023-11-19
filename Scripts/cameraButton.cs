using Godot;
using System;
using System.Collections.Generic;

public partial class cameraButton : Node2D
{
	private static List<cameraButton> buddies;
	[Export] private string nodeName;
	public AnimatedSprite2D outline;
	private Label label;
	public bool Active { set; get; }

	public override void _Ready()
	{
		if (buddies == null) buddies = new();
		buddies.Add(this);
		outline = GetNode<AnimatedSprite2D>("Outline");
		label = GetNode<Label>("Label");
		label.Text = $"CAM\n{nodeName}";
	}
	
	
	private void InputHandler(Node viewport, InputEvent @event, long shape_idx)
	{
		// Skips processing if already pressed
		if (Active) return;
		
		// Makes sure it was a left click
		if (!(@event is InputEventMouseButton mbEvent && mbEvent.Pressed && mbEvent.ButtonIndex == MouseButton.Left)) return;
		
		// Makes sure this is the only active one
		buddies.ForEach(button =>
		{
			if (!button.Active) return;
			button.Active = false;
			button.outline.Play("unselected");
		});

		Active = true;
		outline.Play("selected");
	}

}



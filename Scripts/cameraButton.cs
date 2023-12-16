using System.Collections.Generic;
using FiveNightsAtPoobs.Scripts;
using FiveNightsAtPoobs.Scripts.tablet_cameras;
using Godot;

public partial class cameraButton : Node2D
{
	private static List<cameraButton> buddies;
	private Label label;
	
	[Export] private string nodeName;
	
	public AnimatedSprite2D outline;
	public bool Active { set; get; }
	public ITabletViewable TabletView;
	
	public override void _Ready()
	{
		if (buddies == null) buddies = new List<cameraButton>();
		buddies.Add(this);
		outline = GetNode<AnimatedSprite2D>("Outline");
		label = GetNode<Label>("Label");
		label.Text = $"CAM\n{nodeName}";
		TabletView = GetNode<Node2D>("ViewManager") as ITabletViewable;
	}
	

	private void InputHandler(Node viewport, InputEvent @event, long shape_idx)
	{
		// Skips processing if already pressed
		if (Active) return;

		// Makes sure it was a left click
		if (!(@event is InputEventMouseButton mbEvent && mbEvent.Pressed &&
			  mbEvent.ButtonIndex == MouseButton.Left)) return;

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

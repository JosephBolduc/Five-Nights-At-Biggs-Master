using Godot;

public partial class Tablet_Toggle : Node2D
{
	private Tablet tablet;
	private double timeSince;


	public override void _Ready()
	{
		tablet = GetNode<Tablet>("%Tablet");
	}


	private void _on_button_area_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if (@event is InputEventMouseButton mouseButtonEvent)
			if (mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left)
				tablet.ToggleMonitor();
	}
}

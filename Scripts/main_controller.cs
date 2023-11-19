using System.Linq;
using Godot;

public partial class main_controller : Node2D
{
	// Public APIs
	public enum GameState
	{
		Office,
		Cameras,
		Blackout,
		Scare,
	}

	// Camera control things
	[Export] private ButtonContainer camButtons;

	// Door control things
	[Export] private door_and_buttons doorControls;
	private RichTextLabel monitorStateLabel;
	private RichTextLabel panLabel;


	// Screen pan stuff
	private panning_zones panner;

	// Power indicator things
	[Export] private PowerIndicators powerIndicators;
	private int powerLeft = 100;

	private byte powerUsage = 1;

	// The room itself
	private Node2D room;
	public GameState State = GameState.Cameras;

	// Tablet stuff
	public Tablet tablet;

	private Node2D tabletContents;
	public Area2D tabletToggleButton;

	// Debug stuff
	private double timeSince;


	public override void _Ready()
	{
		room = GetNode<Node2D>("Room/Background");

		tablet = GetNode<Tablet>("Room/Tablet");
		tabletToggleButton = GetNode<Area2D>("UI/Tablet_Toggle/ButtonArea");

		tabletContents = GetNode<Node2D>("UI/Tablet_Content");

		panner = GetNode<panning_zones>("UI/PanningZones");

		monitorStateLabel = GetNode<RichTextLabel>("UI/Debug Info/MonitorState");
		panLabel = GetNode<RichTextLabel>("UI/Debug Info/PanningState");

		cameraButton stageCam = camButtons.buttonList.First(button => button.Name.Equals("1A"));
		stageCam.outline.Play("selected");
		stageCam.Active = true;
	}


	public override void _Process(double delta)
	{
		// Time increment for misc things
		timeSince += delta;
		if (timeSince > .75) timeSince = 0;

		// Toggles the monitor if space is pressed
		// The other toggle is elsewhere in a display of bad practice
		if (Input.IsActionJustPressed("toggle_monitor")) tablet.ToggleMonitor();

		// Moves the room around
		if (tablet.status == Tablet.MonitorStatus.Down && room.GlobalPosition.X is <= 160 and >= -160)
			room.GlobalTranslate(new Vector2((float)(panner.GetPanning() * delta * -100), 0));

		// Clamps the room transform if needed
		if (room.GlobalPosition.X <= -160) room.GlobalPosition = new Vector2(-159.9f, 0);
		if (room.GlobalPosition.X >= 160) room.GlobalPosition = new Vector2(159.9f, 0);

		// Hides/shows the static and other UI elements 
		if (tablet.status == Tablet.MonitorStatus.Up)
		{
			tabletContents.Show();
			State = GameState.Cameras;
		}
		else
		{
			tabletContents.Hide();
			State = GameState.Office;
		}

		// Hides the button while monitor is moving
		if (tablet.status is Tablet.MonitorStatus.Lowering or Tablet.MonitorStatus.Raising)
			tabletToggleButton.GetParent<Node2D>().Hide();
		else tabletToggleButton.GetParent<Node2D>().Show();

		// Update power usage and stuff
		CalculateUsage();
		powerIndicators.UpdatePower(powerLeft);
		powerIndicators.UpdateUsage(powerUsage);
		//TODO decrease remaining power as a function of usage
		
		// Updated debug info on screen
		monitorStateLabel.Text = $"Monitor State: {tablet.status}";
		panLabel.Text = $"Pan: {panner.GetPanning()} \t BG Position: {room.GlobalPosition.X}";
	}

	private void CalculateUsage()
	{
		powerUsage = 0;
		if (tablet.status == Tablet.MonitorStatus.Up) powerUsage++;
		if (doorControls.leftDoorDown) powerUsage++;
		if (doorControls.rightDoorDown) powerUsage++;
		if (doorControls.leftLightOn) powerUsage++;
		if (doorControls.rightLightOn) powerUsage++;
	}
}

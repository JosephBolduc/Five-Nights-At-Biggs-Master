using Godot;

public partial class door_and_buttons : Node
{
	private AudioStreamPlayer doorNoise;

	private AnimatedSprite2D leftButton;
	private AnimatedSprite2D leftDoor;
	private Area2D leftDoorButton;

	public bool leftDoorDown;
	private Area2D leftLightButton;
	public bool leftLightOn;

	private AudioStreamPlayer lightNoise;
	private AnimatedSprite2D rightButton;
	private AnimatedSprite2D rightDoor;
	private Area2D rightDoorButton;
	public bool rightDoorDown;
	private Area2D rightLightButton;
	public bool rightLightOn;

	public override void _Ready()
	{
		leftDoorButton = GetNode<Area2D>("Left_Buttons/door_button");
		leftLightButton = GetNode<Area2D>("Left_Buttons/light_button");
		rightDoorButton = GetNode<Area2D>("Right_Buttons/door_button");
		leftDoorButton = GetNode<Area2D>("Right_Buttons/light_button");

		leftButton = GetNode<AnimatedSprite2D>("Left_Buttons/buttons");
		leftDoor = GetNode<AnimatedSprite2D>("Left_Door");
		rightButton = GetNode<AnimatedSprite2D>("Right_Buttons/buttons");
		rightDoor = GetNode<AnimatedSprite2D>("Right_Door");

		lightNoise = GetNode<AudioStreamPlayer>("LightPlayer");
		lightNoise.Finished += () =>
		{
			leftLightOn = false;
			rightLightOn = false;
		};
		doorNoise = GetNode<AudioStreamPlayer>("DoorPlayer");
	}

	public override void _Process(double delta)
	{
		if (!leftLightOn && !rightLightOn) lightNoise.StreamPaused = true;
		ProcessSprites();
	}

	private void ButtonInputEvent(Node viewport, InputEvent @event, long shape_idx, long buttonId)
	{
		if (!(@event is InputEventMouseButton mbEvent && mbEvent.Pressed &&
			  mbEvent.ButtonIndex == MouseButton.Left)) return;
		switch (buttonId)
		{
			case 1:
				leftDoorDown = !leftDoorDown;
				if (leftDoorDown) leftDoor.Play();
				else leftDoor.PlayBackwards();
				doorNoise.Play();
				break;
			case 2:
				leftLightOn = !leftLightOn;
				lightNoise.Play();
				break;
			case 3:
				rightDoorDown = !rightDoorDown;
				if (rightDoorDown) rightDoor.Play();
				else rightDoor.PlayBackwards();
				doorNoise.Play();
				break;
			case 4:
				rightLightOn = !rightLightOn;
				lightNoise.Play();
				break;
		}
	}

	private void ProcessSprites()
	{
		switch (leftDoorDown, leftLightOn)
		{
			case (false, false):
				leftButton.Play("none");
				break;
			case (true, false):
				leftButton.Play("top");
				break;
			case (false, true):
				leftButton.Play("bottom");
				break;
			case (true, true):
				leftButton.Play("both");
				break;
		}

		switch (rightDoorDown, rightLightOn)
		{
			case (false, false):
				rightButton.Play("none");
				break;
			case (true, false):
				rightButton.Play("top");
				break;
			case (false, true):
				rightButton.Play("bottom");
				break;
			case (true, true):
				rightButton.Play("both");
				break;
		}
	}
}

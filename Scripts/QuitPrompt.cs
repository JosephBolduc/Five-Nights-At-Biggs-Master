using Godot;
using System;

public partial class QuitPrompt : Node2D
{
	[Export]
	private RichTextLabel quitPrompt;
	// How much time the prompt will take to quit
	private const double quitTime = 3;
	// The current time left to hold esc, when 0, quit
	private double currentTime = quitTime;

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("quit"))
		{
			Show();
			currentTime -= delta;
		}
		else
		{
			Hide();
			currentTime = quitTime;
		}

		if (currentTime <= 0) GetTree().Quit();
		//byte transparency = (byte)(255 * (quitTime - currentTime) / quitTime);
		//quitPrompt.Theme.SetColor("Theme Overrides", "Colors", Color.Color8(0xd2, 0xe0, 0x00, transparency));
		//quitPrompt.PushColor(Color.Color8(0xd2, 0xe0, 0x00, transparency));
		quitPrompt.Text = $"Quitting in {Math.Ceiling(currentTime)} second{(Math.Ceiling(currentTime) != 1 ? "s" : "")}!";
	}
}

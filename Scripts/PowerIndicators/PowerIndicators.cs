using Godot;
using System;

public partial class PowerIndicators : Node2D
{
	[Export] private AnimatedSprite2D usageGraph;
	[Export] private Label powerLeftText;
	[Export] private Tablet tablet;

	private int powerLeft = 100;
	private byte usage = 1;

	// Update the text to the new value
	public void UpdatePower(int newPower)
	{
		powerLeft = newPower;
		powerLeftText.Text = $"{powerLeft}%";
	}
	
	// Update usage graph to new value, must be 1-5
	public void UpdateUsage(byte newUsage)
	{
		usage = newUsage;
		usageGraph.Frame = usage;
	}

	public override void _Process(double delta)
	{
		if (tablet.status is Tablet.MonitorStatus.Lowering or Tablet.MonitorStatus.Raising)
		{
			Hide();
		}
		else Show();
	}
}

using Godot;

public partial class PowerIndicators : Node2D
{
	private int powerLeft = 100;
	[Export] private Label powerLeftText;
	[Export] private Tablet tablet;
	private byte usage = 1;
	[Export] private AnimatedSprite2D usageGraph;

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
			Hide();
		else Show();
	}
}

using System;
using Godot;

public partial class Tablet : AnimatedSprite2D
{
	public enum MonitorStatus
	{
		Down,
		Raising,
		Lowering,
		Up
	}

	private AudioStreamPlayer noise;

	public MonitorStatus status = MonitorStatus.Down;

	public override void _Ready()
	{
		noise = GetNode<AudioStreamPlayer>("%TabletNoise");
	}

	public override void _Process(double delta)
	{
		switch (status)
		{
			case MonitorStatus.Raising:
				if (Frame < SpriteFrames.GetFrameCount("flip")) Play("flip");
				if (Frame + 1 == SpriteFrames.GetFrameCount("flip")) status = MonitorStatus.Up;
				break;
			case MonitorStatus.Lowering:
				if (Frame > 0) PlayBackwards("flip");
				if (Frame == 0) status = MonitorStatus.Down;
				break;
			case MonitorStatus.Up:
			case MonitorStatus.Down:
				break;
		}
	}


	public void RaiseMonitor()
	{
		switch (status)
		{
			case MonitorStatus.Down:
			case MonitorStatus.Lowering:
				status = MonitorStatus.Raising;
				break;
			case MonitorStatus.Raising:
			case MonitorStatus.Up:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public void LowerMonitor()
	{
		switch (status)
		{
			case MonitorStatus.Down:
			case MonitorStatus.Lowering:
				break;
			case MonitorStatus.Raising:
			case MonitorStatus.Up:
				status = MonitorStatus.Lowering;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public void ToggleMonitor()
	{
		noise.Play();
		switch (status)
		{
			case MonitorStatus.Down:
			case MonitorStatus.Lowering:
				status = MonitorStatus.Raising;
				break;
			case MonitorStatus.Raising:
			case MonitorStatus.Up:
				status = MonitorStatus.Lowering;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}

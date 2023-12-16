using Godot;

namespace FiveNightsAtPoobs.Scripts.tablet_cameras;

public partial class GenericViewManager : Node2D, ITabletViewable
{
	public GenericViewManager(bool snas, bool yasu, bool honk, bool amm)
	{
		supportsSnas = snas;
		supportsYasu = yasu;
		supportsHonk = honk;
		supportsAmm = amm;
	}

	[Export] public bool supportsSnas { get; set; }
	[Export] public bool supportsYasu { get;set;  }
	[Export] public bool supportsHonk { get;set;  }
	[Export] public bool supportsAmm { get; set; }

	[Export] public Sprite2D snas;
	[Export] public Sprite2D yasu;
	[Export] public Sprite2D honk;
	[Export] public Sprite2D amm;
	[Export] public Sprite2D empty;
	
	public bool hasSnas { get; set; }
	public bool hasYasu { get; set; }
	public bool hasHonk { get; set; }
	public bool hasAmm { get; set; }
}

using Godot;

namespace FiveNightsAtPoobs.Scripts.tablet_cameras;

public interface ITabletViewable
{
	public bool supportsSnas { get; set;  }
	public bool supportsYasu { get; set;  }
	public bool supportsHonk { get; set;  }
	public bool supportsAmm { get;set;  }
	
	public bool hasSnas { get; set; }
	public bool hasYasu { get; set; }
	public bool hasHonk { get; set; }
	public bool hasAmm { get; set; }

	public void UpdateView(bool snas, bool yasu, bool honk, bool amm)
	{
		hasSnas = snas;
		hasYasu = yasu;
		hasHonk = honk;
		hasAmm = amm;
	}
}
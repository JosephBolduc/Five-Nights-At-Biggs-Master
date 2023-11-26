using Godot;

public partial class panning_zones : Node
{
	private bool farLeftActive;
	private bool farRightActive;
	private bool leftActive;
	private bool rightActive;

	// Returns -2 to 2 depending on if far left to far right zones are active
	public int GetPanning()
	{
		var value = 0;
		if (farLeftActive) value -= 4;
		if (leftActive) value -= 1;
		if (rightActive) value += 1;
		if (farRightActive) value += 4;
		return value;
	}

	// Far left to right ids as -2 to 2
	private void MouseEntered(long id)
	{
		switch (id)
		{
			case -2:
				farLeftActive = true;
				break;
			case -1:
				leftActive = true;
				break;
			case 1:
				rightActive = true;
				break;
			case 2:
				farRightActive = true;
				break;
		}
	}

	private void MouseLeft(long id)
	{
		switch (id)
		{
			case -2:
				farLeftActive = false;
				break;
			case -1:
				leftActive = false;
				break;
			case 1:
				rightActive = false;
				break;
			case 2:
				farRightActive = false;
				break;
		}
	}
}

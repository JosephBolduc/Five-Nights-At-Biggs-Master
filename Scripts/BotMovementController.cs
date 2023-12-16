using System.Collections.Generic;
using System.Linq;
using Godot;

namespace FiveNightsAtPoobs.Scripts;

public class BotMovementController
{


	// Defining where each bot can be, poorly
	private readonly Location oneA = new("1A");
	private readonly Location oneB = new("1B");
	private readonly Location oneC = new("1C");
	private readonly Location twoA = new("2A");
	private readonly Location twoB = new("2B");
	private readonly Location leftDoor = new("Left Door");
	private readonly Location three = new("3");
	private readonly Location fourA = new("4A");
	private readonly Location fourB = new("4B");
	private readonly Location rightDoor = new("Right Door");
	private readonly Location five = new("5");
	private readonly Location six = new("6");
	private readonly Location seven = new("7");

	// The robots
	private readonly Robot fredbear;
	private readonly Robot bonnie;
	private readonly Robot chika;
	private readonly Robot foxy;

	// Text indicators for where the bots are
	private readonly RichTextLabel freddyLocation;
	private readonly RichTextLabel bonnieLocation;
	private readonly RichTextLabel chickenLocation;
	private readonly RichTextLabel foxLocation;


	public BotMovementController(RichTextLabel fLabel, RichTextLabel bLabel, RichTextLabel cLabel, RichTextLabel foxLabel)
	{
		// Initializing the text label fields
		freddyLocation = fLabel;
		bonnieLocation = bLabel;
		chickenLocation = cLabel;
		foxLocation = foxLabel;

		// Initializing location links
		oneA.AddAdjacentcy(new []{oneB});
		oneB.AddAdjacentcy(new []{oneA, five, twoA, fourA, seven, six, three});
		oneC.AddAdjacentcy(new []{twoA});
		// Bots are locked into moving between hall and pocket or attacking, no symmetric link
		twoA.AddAdjacentcy(new []{twoB, leftDoor});
		twoB.AddAdjacentcy(new []{twoA, leftDoor});
		leftDoor.AddAdjacentcy(new []{oneA, oneB, oneC});
		three.AddAdjacentcy(new []{oneB, twoA});
		fourA.AddAdjacentcy(new []{fourA, rightDoor});
		fourB.AddAdjacentcy(new []{fourB, rightDoor});
		rightDoor.AddAdjacentcy(new []{oneA, oneB, oneC});
		five.AddAdjacentcy(new []{oneB});
		six.AddAdjacentcy(new []{oneB, fourA});
		seven.AddAdjacentcy(new []{oneB, fourA});
		
		// Initializing the bots and their starting and allowed locations
		fredbear = new Robot(oneA, new[] { oneB, seven, fourA, fourB, rightDoor }, Robot.BotID.Honk);
		bonnie = new Robot(oneA, new[] { oneB, three, twoA, twoB, leftDoor }, Robot.BotID.Snas);
		chika = new Robot(oneA, new[] { oneB, six, fourA, fourB, rightDoor }, Robot.BotID.Yasu);
		foxy = new Robot(oneC, new[] { twoA, twoB, leftDoor }, Robot.BotID.Amm);
	}

	public void ProecssMovement()
	{
		fredbear.Move();
		freddyLocation.Text = $"Freddy: {fredbear.currentLocation.Name}";
		bonnie.Move();
		bonnieLocation.Text = $"Bonnie: {bonnie.currentLocation.Name}";
		chika.Move();
		chickenLocation.Text = $"Chika: {chika.currentLocation.Name}";
		foxy.Move();
		foxLocation.Text = $"Foxy: {foxy.currentLocation.Name}";
	}

	/// <summary>
	///     A representation of the animatronic robots, excluding foxy.
	///     Maintains location by reference to the Location graph
	/// </summary>
	public class Robot
	{
		// The collection of all the instantiated locations
		public static readonly List<Robot> Robots = new();
		public List<Location> allowedLocations = new(); // Where the bot can roam to
		public Location currentLocation; // Current location, initialized onstage A1
		public BotID BotId;

		public enum BotID
		{
			Snas,
			Yasu,
			Honk,
			Amm
		}

		public Robot(Location startingSpot, IEnumerable<Location> possibleLocations, BotID id)
		{
			Robots.Add(this);
			currentLocation = startingSpot;
			allowedLocations.Add(startingSpot);
			foreach (Location spot in possibleLocations) allowedLocations.Add(spot);
		}

		/// <summary>
		///     Moves to bot to an available location, if available, otherwise nothing.
		///     Deal with randomness and movement rates before calling
		/// </summary>
		public void Move()
		{
			List<Location> possibleLocations = allowedLocations.Where(spot =>
				{
					if (!currentLocation.Adjacencies.Contains(spot)) return false;
					foreach (Robot robot in Robots)
						if (robot.currentLocation == spot)
							return false;
					return true;
				}
			).ToList();
			if (possibleLocations.Count == 0) return;
			Location target = possibleLocations[(int)(GD.Randi() % possibleLocations.Count)];
			currentLocation = target;
		}
	}


	public class Location
	{
		// The collection of all the instantiated locations
		public static readonly List<Location> Locations = new();

		// Where the room connects to
		public readonly List<Location> Adjacencies = new();

		// The room's name (MEGA FAILURE DEBUG USE ONLY)
		public readonly string Name;

		public Location(string name)
		{
			Locations.Add(this);
			Name = name;
		}

		// Adds the adjacency with it's symmetric link
		public void AddAdjacentcy(IEnumerable<Location> itemList)
		{
			foreach (Location loc in itemList)
			{
				Adjacencies.Add(loc);
				// Not making the links symetric
				// loc.Adjacencies.Add(this);
			}
		}
	}
}
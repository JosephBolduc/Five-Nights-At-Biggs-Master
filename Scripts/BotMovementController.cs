using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Godot;

namespace FiveNightsAtPoobs.Scripts;

public class BotMovementController
{
	// Defining where each bot can be, poorly
	private readonly Location OneA = new("1A");
	private readonly Location OneB = new("1B");
	private readonly Location OneC = new("1C");
	private readonly Location RightDoor = new("Right Door");
	private readonly Location Seven = new("7");
	private readonly Location Six = new("6");
	private readonly Location Three = new("3");
	private readonly Location TwoA = new("2A");
	private readonly Location TwoB = new("2B");
	private readonly Location Five = new("5");
	private readonly Location FourA = new("4A");
	private readonly Location FourB = new("4B");
	private readonly Location LeftDoor = new("Left Door");

	// The robots
	Robot fredbear;
	Robot bonnie;
	Robot chika;

	// Have foxy do stuff down here idk
	// TODO: foxy

	// Text indicators for where the bots are
	RichTextLabel freddyLocation;
	RichTextLabel bonnieLocation;
	RichTextLabel chickenLocation;

	public BotMovementController(RichTextLabel fLabel, RichTextLabel bLabel, RichTextLabel cLabel)
	{
		// Initializing the text label fields
		freddyLocation = fLabel;
		bonnieLocation = bLabel;
		chickenLocation = cLabel;

		OneA.AddAdjacentcy(new[] { OneB });
		OneB.AddAdjacentcy(new[] {Five, Seven, Six, Three, TwoA, FourA});
		TwoA.AddAdjacentcy(new[] {Three, LeftDoor, TwoB});
		TwoB.AddAdjacentcy(new []{LeftDoor});
		FourA.AddAdjacentcy(new []{FourB, RightDoor});
		FourB.AddAdjacentcy(new []{RightDoor});


		fredbear = new(OneA, new[] { OneB, OneC, Seven, FourA, FourB, RightDoor });
		bonnie = new(OneA, new[] { OneB, OneC, Three, TwoA, TwoB, LeftDoor });
		chika = new(OneA, new[] { OneB, OneC, Six, FourA, FourB, RightDoor });
	}

	public void ProecssMovement()
	{
		fredbear.Move();
		freddyLocation.Text = $"Freddy: {fredbear.currentLocation.Name}";
		bonnie.Move();
		bonnieLocation.Text = $"Bonnie: {bonnie.currentLocation.Name}";
		chika.Move();
		chickenLocation.Text = $"Chika: {chika.currentLocation.Name}";
	}

	/// <summary>
	/// A representation of the animatronic robots, excluding foxy.
	/// Maintains location by reference to the Location graph
	/// </summary>
	public class Robot
	{
		// The collection of all the instantiated locations
		public static readonly List<Robot> Robots = new();
		public List<Location> allowedLocations = new();     // Where the bot can roam to
		public Location currentLocation;                    // Current location, initialized onstage A1
		public Robot(Location startingSpot, IEnumerable<Location> possibleLocations)
		{
			Robots.Add(this);
			currentLocation = startingSpot;
			allowedLocations.Add(startingSpot);
			foreach (Location spot in possibleLocations) allowedLocations.Add(spot);
		}

		/// <summary>
		/// Moves to bot to an available location, if available, otherwise nothing.
		/// Deal with randomness and movement rates before calling
		/// </summary>
		public void Move()
		{
			List<Location> possibleLocations = allowedLocations.Where((spot) =>
			{
				if (!currentLocation.Adjacencies.Contains(spot)) return false;
				foreach (var robot in Robots)
				{
					if (robot.currentLocation == spot) return false;
				}
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
				loc.Adjacencies.Add(this);
			}
		}
	}
}
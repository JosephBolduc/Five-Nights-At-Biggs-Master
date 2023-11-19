using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace FiveNightsAtPoobs.Scripts;

public class BotMovementController
{
	// Defining where each bot can be, poorly
	private readonly Location OneA = new();
	private readonly Location OneB = new();
	private readonly Location OneC = new();
	private readonly Location RightDoor = new();
	private readonly Location Seven = new();
	private readonly Location Six = new();
	private readonly Location Three = new();
	private readonly Location TwoA = new();
	private readonly Location TwoB = new();
	private Location Five = new();
	private readonly Location FourA = new();
	private readonly Location FourB = new();
	private readonly Location LeftDoor = new();

	public BotMovementController()
	{
		OneA.storeMulti = true;
		
		OneA.AddAdjacentcy(new[] { OneB });
		OneB.AddAdjacentcy(new[] { OneC, Seven });
		OneC.AddAdjacentcy(new[] { TwoA, FourA, Six, Seven });
		TwoA.AddAdjacentcy(new[] { OneC, TwoB, Three, LeftDoor });
		TwoB.AddAdjacentcy(new[] { LeftDoor });
		FourA.AddAdjacentcy(new[] { FourB, RightDoor });
		FourB.AddAdjacentcy(new[] { RightDoor });
	}


	private class Bot
	{			
		RandomNumberGenerator rng = new();
		public enum BotType
		{
			Freddy,
			Chika,
			Foxy,
			Bonny,
			Empty
		}

		public BotType botType;
		private BotMovementController parent;

		public Bot(BotType type, BotMovementController parentT)
		{
			botType = type;
			parent = parentT;
		}

		public void Move()
		{
			Location currentSpot = Location.GetLocation(this);
			var targetLocations = currentSpot.Adjacencies.Where(location =>
				{
					if (location.storeMulti) return true;
					return location.Occupant.FirstOrDefault() == null;
				}
			);
			
			int newPick = rng.RandiRange(0, targetLocations.Count() - 1);
			currentSpot.RemoveBot(this);
			Location newSpot = Location.Locations[newPick];
			newSpot.AddBot(this);

		}
	}

	private class Location
	{
		public static readonly List<Location> Locations = new();
		public readonly List<Location> Adjacencies = new();
		public List<Bot> Occupant;
		public bool storeMulti = false;
		


		// Adds the adjacency with it's symmetric link
		public void AddAdjacentcy(IEnumerable<Location> itemList)
		{
			foreach (Location loc in itemList)
			{
				Locations.Add(loc);
				loc.Adjacencies.Add(loc);
			}
		}

		// Returns where a bot is
		public static Location GetLocation(Bot bot)
		{
			return Locations.FirstOrDefault(location => location.Occupant.Contains(bot));
		}
		
		// Adds and removes bots
		public void AddBot(Bot addition)
		{
			throw new NotImplementedException();
		}
		
		public void RemoveBot(Bot removalTarget)
		{
			throw new NotImplementedException();
		}
	}
}
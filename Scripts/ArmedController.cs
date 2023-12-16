using Godot;
using System;

public partial class ArmedController : Node2D
{
	public Sprite2D amm { get; private set; }
	public Sprite2D yasu{ get; private set; }
	public Sprite2D honk{ get; private set; }
	public Sprite2D snas{ get; private set; }

	public override void _Ready()
	{
		amm = GetNode<Sprite2D>("amm");
		yasu = GetNode<Sprite2D>("yasu");
		honk = GetNode<Sprite2D>("honk");
		snas = GetNode<Sprite2D>("snas");
	}
}

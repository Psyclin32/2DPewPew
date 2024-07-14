using Godot;
using System;
using System.Diagnostics;

public partial class Ptest : Node2D
{

	[Export] public UnitData Stats;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
			GD.Print(Stats.Health);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

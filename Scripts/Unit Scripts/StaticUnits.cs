using Godot;
using System;

public partial class StaticUnits : StaticBody2D
{
	[Export] public UnitStatsResource StatsUnitStats;
	[Export] public WeaponHardPoint fixedTurret;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		fixedTurret.RotatetoTarget(GetGlobalMousePosition());
		fixedTurret.FireTurret(CollisionLayer);
	}
}

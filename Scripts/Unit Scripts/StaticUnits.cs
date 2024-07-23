using Godot;
using System;

public partial class StaticUnits : StaticBody2D
{
	[Export] public UnitStatsResource StatsUnitStats;
	[Export] public WeaponHardPoint fixedTurret;

	public GlobalVars globalVars {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		globalVars = GetNodeOrNull("/root/Globals") as GlobalVars;
		//GD.Print(globalVars.Player);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(globalVars.Player is null) return;

		

		//fixedTurret.RotatetoTarget(GetGlobalMousePosition());
		//fixedTurret.FireTurret(CollisionLayer);
	}

    public override void _PhysicsProcess(double delta)
    {
		fixedTurret.RotatetoTarget(globalVars.Player.GlobalPosition);
		fixedTurret.FireTurret(CollisionLayer);

        base._PhysicsProcess(delta);
    }

	public void TakeDamage(int damage)  //damage stored as possitives
    {
        if (StatsUnitStats.TotalArmor > damage)
        {
            return;
        }
        else
        {
            StatsUnitStats.ChangeHealth(-(damage-StatsUnitStats.TotalArmor)); //passing damage as negative value
        }
        GD.Print(StatsUnitStats.Health);
    }

}

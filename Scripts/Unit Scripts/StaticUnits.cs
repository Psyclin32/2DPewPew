using Godot;
using System;

public partial class StaticUnits : StaticBody2D
{
	[Export] public UnitStatsResource unitStats;
	[Export] public WeaponHardPoint fixedTurret;

	[Export] public AnimatedSprite2D unitSprite;

	[Export] public bool isDead= false;

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
		DeathCheck();
		base._Process(delta);
	}

    public override void _PhysicsProcess(double delta)
    {	
		if(globalVars.Player is not null)
		{
		fixedTurret.RotatetoTarget(globalVars.Player.GlobalPosition);
		fixedTurret.FireTurret(CollisionLayer);
		}

        base._PhysicsProcess(delta);
    }

	public void TakeDamage(int damage)  //damage stored as possitives
    {
        if (unitStats.TotalArmor > damage)
        {
            return;
        }
        else
        {
            unitStats.ChangeHealth(-(damage-unitStats.TotalArmor)); //passing damage as negative value
        }
        GD.Print(unitStats.Health);
    }

	public void DeathCheck()
    {
        if(unitStats.Health <=0  & !isDead)  //Play Death animation when dead
        {
            //DeathAnimation();
			isDead = true;
            GD.Print("Play Dead Anim");
            GD.Print(isDead);
        }
        else if(!unitSprite.IsPlaying() & isDead)	
        {
            GD.Print("I should Die now");
            QueueFree();
        }   //When done playing, kill unit.
    }
}

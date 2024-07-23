using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
public partial class GeneralUnit : RigidBody2D
{

[ExportGroup("General Nodes")]
[Export] public WeaponHardPoint     turret;
[Export] public EnginesAssets       engines;
[Export] public AnimatedSprite2D    unitSprite;

protected bool isDead = false;
protected bool ON = true;
protected bool OFF = false;


[ExportGroup("Units Stats")]
[Export] public UnitStatsResource unitStats;
[ExportGroup("Vehicle Stats")]
[Export] public VehicleStats vehicleStats;
//create an instances of the UnitStatsResource
// Is edited in the inspector for now where its default values can be configured. 

    public override void _Ready()
    {   
        //unitStats.ResourceLocalToScene = true; -> should be already set on the resources. 
        unitSprite.Animation = "DamageStates";
        //GD.Print(engines.ForwardEngine.GetInstanceId());
        base._Ready();
    }

    public override void _Process(double delta)
    {
     
        if(!isDead) DeathCheck();   //only check while alive. 
        
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
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
            DeathAnimation();
            isDead = true;
            //GD.Print("Play Dead Anim");
            //GD.Print(isDead);
        }
    }

    public void DeathAnimation()
    {   
        unitSprite.Play("Death");
        GetChild<CollisionShape2D>(0).SetDeferred(CollisionShape2D.PropertyName.Disabled, true); 
        
        //If error thrown, check that the scene tree has the collider as the fist child for the time being.  
        //Above is a bit cumbersome. Likely want to automate this by using Animation players instead so more complicated sequences can play out with a single call..
        // instead of needing all those nodes and properties loaded up all the time. 
    }

}

using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

public partial class GeneralUnit : RigidBody2D
{

[ExportGroup("General Nodes")]
[Export] public WeaponHardPoint     turret;
[Export] public Node2D              engines;

[Export] public AnimatedSprite2D    unitSprite;

private bool isDead = false;

[ExportGroup("General Stats")]
[Export]
public UnitStatsResource unitStats; //create an instances of the UnitStatsResource
// Is edited in the inspector for now where its default values can be configured. 

    public override void _Ready()
    {   
        unitStats.ResourceLocalToScene = true;
        unitSprite.Animation = "DamageStates";
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if(unitStats.Health <=0  & !isDead)  //Play Death animation when dead
        {
            DeathAnimation();
        }
        
        else if(!unitSprite.IsPlaying() & isDead)
        {
            GD.Print("I should Die now");
            QueueFree();
        }             //When done playing, kill unit. 
        base._Process(delta);
    }

    public void TakeDamage(int damage)  //damage stored as possitives
    {
        if (unitStats.Armor > damage)
        {
            return;
        }
        else
        {
            unitStats.ChangeHealth(-(damage-unitStats.Armor)); //passing damage as negative value
        }
        GD.Print(unitStats.Health);
    }

    public void DeathAnimation()
    {   
        unitSprite.Play("Death");
        GetChild<CollisionShape2D>(0).SetDeferred(CollisionShape2D.PropertyName.Disabled, true); 
        //If error thrown, check that the scene tree has the collider as the fist child for the time being.  
        //Above is a bit cumbersome. Likely want to automate this by using Animation players instead so more complicated sequences can play out with a single call..
        // instead of needing all those nodes and properties loaded up all the time. 
        isDead = true;
    }

}

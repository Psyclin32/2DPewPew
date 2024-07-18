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

private bool isDead = false;
private bool ON = true;
private bool OFF = false;


[ExportGroup("General Stats")]
[Export]
public UnitStatsResource unitStats; //create an instances of the UnitStatsResource
// Is edited in the inspector for now where its default values can be configured. 

    public override void _Ready()
    {   


        unitStats.ResourceLocalToScene = true;
        unitSprite.Animation = "DamageStates";
        //GD.Print(engines.ForwardEngine.GetInstanceId());
        base._Ready();
    }

    public override void _Process(double delta)
    {
        //Engine Animations
        EngineAnimations();

        //GD.Print(engines.ForwardEngine.GetInstanceId());
        
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
    public void EngineAnimations()
    {
        //Forward Movement 
        if (Input.IsActionPressed("Forward"))
        {
            engines.Forward(ON);
        }
        else
        { 
            engines.Forward(OFF);
        }
        //GD.Print(Input.IsActionPressed("Forward"));
        

        //Rotation Movement 
        float axis  = Input.GetAxis("Rotate Left", "Rotate Right");
        if (axis > 0)
        {
            engines.RightYaw(ON);
            engines.LeftYaw(OFF);
        } 
        else if (axis < 0)
        {
            engines.RightYaw(OFF);
            engines.LeftYaw(ON);
        }
        else
        {
            engines.RightYaw(OFF);
            engines.LeftYaw(OFF);
        }
        //GD.Print(axis);
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

    public void DeathCheck()
    {
        if(unitStats.Health <=0  & !isDead)  //Play Death animation when dead
        {
            DeathAnimation();
        }
        else if(!unitSprite.IsPlaying() & isDead)
        {
            GD.Print("I should Die now");
            QueueFree();
        }   //When done playing, kill unit.
    }

    public void DeathAnimation()
    {   
        unitSprite.Play("Death");
        GetChild<CollisionShape2D>(0).SetDeferred(CollisionShape2D.PropertyName.Disabled, true); 
        isDead = true;
        //If error thrown, check that the scene tree has the collider as the fist child for the time being.  
        //Above is a bit cumbersome. Likely want to automate this by using Animation players instead so more complicated sequences can play out with a single call..
        // instead of needing all those nodes and properties loaded up all the time. 
        
    }

}

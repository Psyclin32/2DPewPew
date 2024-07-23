using Godot;
using System;

public partial class ObjectUnits : RigidBody2D
{
    [Export] public UnitStatsResource objectStats; 
    [Export] public AnimatedSprite2D    unitSprite;

    private bool isDead = false;

    public override void _Process(double delta)
    {
        DeathCheck();
        
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
    public void TakeDamage(int damage)  //damage stored as possitives
    {
        if (objectStats.TotalArmor > damage)
        {
            return;
        }
        else
        {
            objectStats.ChangeHealth(-(damage-objectStats.TotalArmor)); //passing damage as negative value
        }
        //GD.Print(objectStats.Health);
    }
    public void DeathCheck()
    {
        if(objectStats.Health <=0  & !isDead)  //Play Death animation when dead
        {
            DeathAnimation();
            GD.Print("Play Dead Anim");
            GD.Print(isDead);
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

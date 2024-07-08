using Godot;
using System;
using System.Diagnostics;

public partial class Asteroid : RigidBody2D
{   
    private StatsClass _objectStats;

    private AnimatedSprite2D rock; 
    private AnimationPlayer rockAnim;

    private StatStruct.Stats MyStats;
    private bool isDead = false;


    public override void _Ready()
    {   
        MyStats = new StatStruct.Stats(10,0);

        rock = GetNode<AnimatedSprite2D>("Asteroid");
        rock.Frame = 0;
        rockAnim = GetNode<AnimationPlayer>("AsteroidAnim");
        base._Ready();
    }

    public override void _Process(double delta)
    {   
        
        

        base._Process(delta);
    }


    private void OnBodyEntered(Bullet body)
    {
        if (Bullet.CanDamage)
        {
            MyStats.Health  -= body.GetDamage();
        }

        Debug.Print("You hit: " + Name);
        Debug.Print(" HP of target hit: " + MyStats.Health);

        //Debug.Print("Damage Expected: " + -body.GetDamage())
        //Debug.Print("New HP: " + _objectStats.GetStatValues(StatsClass._statNames.Health))
    }
    // private void OnBodyEntered(Node2D body)
    // {
    //    Debug.Print("Signal over load check");
    // }

}   


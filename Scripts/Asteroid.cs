using Godot;
using System;
using System.Diagnostics;

public partial class Asteroid : RigidBody2D
{   
    private StatsClass _objectStats;
    
    [Export] private static int[] _asteroidStats = {10, 0, 2};

    private AnimatedSprite2D rock; 
    private AnimationPlayer rockAnim;

    private bool isDead = false;


    public override void _Ready()
    {   
        _objectStats = new StatsClass();
        _objectStats.SetStatValues(_asteroidStats);
        // Debug.Print("HP = " + _objectStats.GetStatValues(StatsClass._statNames.Health));
        // Debug.Print("Shields = " + _objectStats.GetStatValues(StatsClass._statNames.Shields));  
        // Debug.Print("Armor = " + _objectStats.GetStatValues(StatsClass._statNames.Armor));
        //BodyEntered += OnBodyEntered();

        rock = GetNode<AnimatedSprite2D>("Asteroid");
        rock.Frame = 0;

        rockAnim = GetNode<AnimationPlayer>("AsteroidAnim");
        base._Ready();
    }

    public override void _Process(double delta)
    {   
        

        // if(!isDead)
        // {    
        //     int hp = _objectStats.GetStatValues(StatsClass._statNames.Health);
        //     if  (hp > 7 ) 
        //     {
        //         this.rock.Frame = 0;
        //     }
        //     else if (3 <= hp & hp <= 7)
        //     {
        //         rock.Frame = 1;
        //     }
        //     else if (0 < hp)
        //     {
        //         rock.Frame = 2;
        //     }
        //     else if (hp <= 0) 
        //     {
        //         rockAnim.Play("Explosion");
        //         isDead = true;
                
        //     }
        // }
        

        base._Process(delta);
    }


    private void OnBodyEntered(Bullet body)
    {
        if (Bullet.CanDamage)
        {
            _objectStats.SetStatValues(StatsClass._statNames.Health, -body.GetDamage()); 
        }

        //Debug.Print("You hit: " + Name)
        //Debug.Print(" HP of target hit: " + _objectStats.GetStatValues(StatsClass._statNames.Health));

        //Debug.Print("Damage Expected: " + -body.GetDamage());

        //Debug.Print("New HP: " + _objectStats.GetStatValues(StatsClass._statNames.Health))
    }
    // private void OnBodyEntered(Node2D body)
    // {
    //    Debug.Print("Signal over load check");
    // }

}   


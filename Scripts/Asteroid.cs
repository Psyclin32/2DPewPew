using Godot;
using System;
using System.Diagnostics;

public partial class Asteroid : RigidBody2D
{   
    //private int Health = 10; 
    private AnimatedSprite2D rock; 
    private AnimationPlayer rockAnim;

    public StatStruct.Stats MyStats;
    private bool isDead = false;


    public override void _Ready()
    {   
        MyStats = new StatStruct.Stats(10,0,0);

        rock = GetNode<AnimatedSprite2D>("Asteroid");
        rock.Animation = "DamageStates";
        rock.Frame = 0;

        //rockAnim = GetNode<AnimationPlayer>("AsteroidAnim");
        base._Ready();
    }

    public override void _Process(double delta)
    {   
        if(!isDead)
        {        
            if  (MyStats.Health > 7 ) 
            {
                this.rock.Frame = 0;
            }
            else if (3 <= MyStats.Health & MyStats.Health <= 7)
            {
                rock.Frame = 1;
            }
            else if (0 < MyStats.Health)
            {
                rock.Frame = 2;
            }
            else if (MyStats.Health <= 0) 
            {
                // CollisionShape2D collider = GetNode<CollisionShape2D>("Collider");
                // collider.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
                
                GetChild(1).SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

                rock.Play("Explosion");
                isDead = true;
            }
        }
        else
        {   
            //Disable physics collider while animation is finishing
            //Use SetDeferred() due to being tied into the physics process. 
            if (!rock.IsPlaying())
            {
                QueueFree(); //When animation finishes, destyroy the instance scene.
            }
        }
        base._Process(delta);
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Bullet)
        {
            Bullet bullet = (Bullet) body;
            MyStats.Health -= bullet.GetDamage(); 
        }

        // Debug.Print("You hit: " + Name);
        // Debug.Print("Health of target hit: " + MyStats.Health);

        //Debug.Print("Damage Expected: " + -body.GetDamage());

        //Debug.Print("Damage Expected: " + -body.GetDamage())
        //Debug.Print("New HP: " + _objectStats.GetStatValues(StatsClass._statNames.Health))
    }
}   


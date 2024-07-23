using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Numerics;

public partial class WeaponHardPoint : Node2D
{
        /*expected Node structure
        Parent  - Sprite with loaded weapon texture
                - Marker2D with proectile spawn point. 
                - AnimatedSprite2D with FX
                - Animation Player with fire routine 
                - Timer with expected ROF in seconds / shot. 
          */   
    [ExportGroup("Stats")]
    //[Export] public DamageStatsResource  damageStats; //defaults can be configured in inspector. 

    [Export] public float turningSpeed = 45; //in degrees per second? (easier for intuitive changes, need conversion to radians in methods) 
    
    [ExportGroup("Attached Nodes")]
    [Export] public PackedScene             projectile;
    [Export] public Sprite2D                turretSprite;
    [Export] public AnimatedSprite2D        shootingVFX;
    [Export] public AnimationPlayer         animator;
    [Export] public Marker2D                muzzel;
    [Export] public Timer                   timerROF;

    private float rotationSpeed;
    public bool ReadyFire = true;
    public override void _Ready()
    {
        //Rotation Speed converted from Degrees to radians / second
        rotationSpeed = Mathf.DegToRad(turningSpeed);

        //Animator Setup
        animator.AnimationFinished += OnAnimatorFinished; //connects Animation Player
        //Animator.GetAnimation("name")

        //Timer Setup
		timerROF.Timeout += OnTimerROFTimeout; //connects to the Timers Timeout signal
    }

    public override void _Process(double delta)
    {
        //RotatetoTarget(); -> called from controlling owner
        base._Process(delta);
    }
    // General Methods
    public void FireTurret(uint collisionLayer)
    {  
        if(ReadyFire)
        {
            //Timer control for ROF
            ReadyFire = false; 
            //control firing animation
            if(!animator.IsPlaying())
                {
                    animator.Play("Fire");
                }
            //Create Projectile
            ProjectileAsset newProjectile = projectile.Instantiate<ProjectileAsset>();
            newProjectile.ExternalData(collisionLayer, GlobalRotation, muzzel.GlobalPosition);
            AddChild(newProjectile);
            //Debug.Print("Bullet Added");
            timerROF.Start();

        }


    }

    public void RotatetoTarget(Godot.Vector2 target) //expecting global position. 
    {   
        //Weapon turret looking at assigend target
		LookAt(target); //Lookat requires Global Coords of mouse pointers  
        //can be called in _Process as turret is not a physics body
        
        //----  
        //Not currently working, might need some added effort to deep dive it. 
        //float tweenTime = MathF.Abs(GetAngleTo(target)) * rotationSpeed;
        //Tween tween = GetTree().CreateTween();
        //tween.TweenProperty( this, "rotation", GetAngleTo(target), tweenTime);

    } 

public void FireFixedRotationed(uint collisionLayer, float shipRotation)
    {  
        if(ReadyFire)
        {
            //Timer control for ROF
            ReadyFire = false; 
            //control firing animation
            if(!animator.IsPlaying())
                {
                    animator.Play("Fire");
                }
            //Create Projectile
            ProjectileAsset newProjectile = projectile.Instantiate<ProjectileAsset>();
            newProjectile.ExternalData(collisionLayer, shipRotation, muzzel.GlobalPosition);
            AddChild(newProjectile);
            //GD.Print("Bullet Added");
            timerROF.Start();

        }


    }



    //Signal Methods
    private void OnTimerROFTimeout()
    {
        ReadyFire = true;
    }
    private void OnAnimatorFinished(StringName animName)
    {
        //unsure if going to be used. Seems potentially useful? 
    }





}

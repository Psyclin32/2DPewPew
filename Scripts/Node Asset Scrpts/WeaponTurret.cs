using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Numerics;

public partial class WeaponTurret : Node2D
{
        /*expected Node structure
        Parent  - Sprite with loaded weapon texture
                - Marker2D with proectile spawn point. 
                - AnimatedSprite2D with FX
                - Animation Player with fire routine 
                - Timer with expected ROF in seconds / shot. 
          */   
    [ExportGroup("Stats")]
    [Export] public DamageStatsResource  weaponStats; //defaults can be configured in inspector. 
    [Export] public float turningSpeed = 45; //in degrees per second? (easier for intuitive changes, need conversion to radians in methods) 
    
    [ExportGroup("Attached Nodes")]
    [Export] public Sprite2D                turretSprite;
    [Export] public AnimatedSprite2D        shootingVFX;
    [Export] public AnimationPlayer         animator;
    [Export] public Marker2D                muzzel;
    [Export] public Timer                   timerROF;

    private float rotationSpeed;
    private bool ReadyFire = true;
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
    public void FireWeapon(Godot.Vector2 targetPos)
    {   
        //Timer control for ROF
        ReadyFire = false;
        timerROF.Start();

        //control firing animation
        if(!animator.IsPlaying())
			{
				animator.Play("Fire");
			}
        
        GD.Print("Bang!");
        //Spawn the projectile on Marker2D pointed at the target.
    }

    public void RotatetoTarget(Godot.Vector2 target) //expecting global position. 
    {   
        float tweenTime = MathF.Abs(GetAngleTo(target)) * rotationSpeed;
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty( this, "rotation", GetAngleTo(target), tweenTime);

        Debug.Print("target angle: " + GetAngleTo(target).ToString());
        Debug.Print("current rotation: " + Rotation.ToString());

        //Weapon turret looking at cursor
		//LookAt(target); //Lookat requires Global Coords of mouse pointers  
        //can be called in _Process as turret is not a physics body
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

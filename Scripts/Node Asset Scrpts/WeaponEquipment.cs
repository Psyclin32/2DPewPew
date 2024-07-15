using Godot;
using System;

public partial class WeaponEquipment : Sprite2D
{
        /*expected Node structure
        Parent  - Sprite with loaded weapon texture
                - Marker2D with proectile spawn point. 
                - AnimatedSprite2D with FX
                - Animation Player with fire routine 
                - Timer with expected ROF in seconds / shot. 
          */   
    [Export] public EquipmentStatsResource  weaponStats; //defaults can be configured in inspector. 
    [Export] public Sprite2D                WeaponTurret;
    [Export] public AnimatedSprite2D        shootingVFX;
    [Export] public AnimationPlayer         Animator;
    [Export] public Marker2D                muzzel;
    [Export] public Timer                   TimerROF;

    private bool ReadyFire = true;
    public override void _Ready()
    {
        //Animator Setup
        Animator.AnimationFinished += OnAnimatorFinished; //connects Animation Player
        //Animator.GetAnimation("name")

        //Timer Setup
		TimerROF.Timeout += OnTimerROFTimeout; //connects to the Timers Timeout signal
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
    // General Methods
    public void FireWeapon()
    {
        ReadyFire = false;
    }

    public void RotatetoTarget()
    {
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

using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public partial class Enemy : RigidBody2D
{
private Player target; 

[Export]
private PackedScene _bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

[Export]
public Timer _weaponTimer;

[Export]
public int BulletSpeed = 250; 

[Export] Timer RotationTimer;

private StatStruct.Stats MyStats = new StatStruct.Stats(10,0,0);
private StatStruct.ObjectFlags EnemyFlags = new StatStruct.ObjectFlags(false, StatStruct.ObjectFlags.IFF.Enemy);

[Export]
private Node2D SpawnContainer;

[Export] Sprite2D Weapon;

[Export] Marker2D gun_Barrel;

public bool Reload = false;

    public override void _Ready()
    {
        SpawnContainer = GetNode<Node2D>("ChildSpawns");

        target = GetNode<Player>("/root/Main/Player");

        _weaponTimer = GetNode<Timer>("WeaponTimer");
		_weaponTimer.Timeout += OnWeaponTimerTimeout;    

        gun_Barrel = Weapon.GetNode<Marker2D>("GunBarrelPos");

        // RotationTimer = GetNode<Timer>("RotationTimer");
		// RotationTimer.Timeout += OnRotationTimerTimeout;  
    }

    public override void _Process(double delta)
    {
       
       
        AquireTarget();
        if(Reload) { EnemyFire(gun_Barrel.GlobalPosition); }
        base._Process(delta);
    }

    public void AquireTarget()
    {
        LookAt(target.GlobalPosition);
        Rotate (MathF.PI/2);
    }
    public void OnRotationTimerTimeout()
    {
        LookAt(target.GlobalPosition);
        Rotate (MathF.PI/2);
    }

    public void OnWeaponTimerTimeout()
    {
        Reload = true;
    }

    public void EnemyFire(Vector2 pos)
    {
        // Debug.Print("Enemy FIRE!");
        Reload = false;
        _weaponTimer.Start();
        RigidBody2D projectile = _bulletScene.Instantiate<RigidBody2D>();
        projectile.CollisionLayer = 16;
        projectile.CollisionMask = 14;
        projectile.Position = pos; //Muzzel position
        projectile.LookAt(target.GlobalPosition);
        //projectile.Rotate();
        projectile.LinearVelocity =  -Transform.Y * BulletSpeed;
        projectile.TopLevel = true;
        SpawnContainer.AddChild(projectile);
    }




}

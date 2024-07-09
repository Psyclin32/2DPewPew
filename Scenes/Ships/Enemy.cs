using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class Enemy : RigidBody2D
{
[Export]
private PackedScene _bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

[Export]
public Timer _weaponTimer;

[Export]
public int BulletSpeed = 250; 

private StatStruct.Stats MyStats = new StatStruct.Stats(10,0,0);
private StatStruct.ObjectFlags EnemyFlags = new StatStruct.ObjectFlags(false, StatStruct.ObjectFlags.IFF.Enemy);

public bool Reload = false;

    public override void _Ready()
    {
        _weaponTimer = GetNode<Timer>("WeaponTimer");
		_weaponTimer.Timeout += OnWeaponTimerTimeout;    
    }

    public override void _Process(double delta)
    {
        if(Reload) { EnemyFire(); }
        base._Process(delta);
    }

    public void OnWeaponTimerTimeout()
    {
        Reload = true;
    }

    public void EnemyFire()
    {
        Reload = false;
        Debug.Print("Enemy FIRE!");
        _weaponTimer.Start();
        Marker2D spawn_pos = (Marker2D) FindChild("GunBarrelPos");
        RigidBody2D projectile = _bulletScene.Instantiate<RigidBody2D>();
        projectile.CollisionLayer = 16;
        projectile.CollisionMask = 15;
        projectile.Position = spawn_pos.Position;
        projectile.LookAt(-Position);
        projectile.LinearVelocity = projectile.Position.DirectionTo(-Position).Normalized() * BulletSpeed;
        AddChild(projectile);

    }




}

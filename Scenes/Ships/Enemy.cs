using Godot;
using System;

public partial class Enemy : RigidBody2D
{
[Export]
private PackedScene _bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
private StatStruct.Stats MyStats = new StatStruct.Stats(10,0,0);
private StatStruct.ObjectFlags EnemyFlags = new StatStruct.ObjectFlags(false, StatStruct.ObjectFlags.IFF.Enemy);
    public override void _Ready()
    {
        
    }





}

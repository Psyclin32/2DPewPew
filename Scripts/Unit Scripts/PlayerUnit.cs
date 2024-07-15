using Godot;
using System;

public partial class PlayerUnit : GeneralUnit
{
    //[Export] public UnitStats PlayerStats; -> comes from general units class

    [ExportGroup("Attached Nodes")]
    //[Export] RigidBody2D    Rigidbody;
    [Export] WeaponTurret   turret;
    [Export] Node2D         engines;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        //continue to look at the mouse
       AquireTarget(GetGlobalMousePosition());
        
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public void AquireTarget(Vector2 target)
    {   //should be passed global mopuse POS for player turret Operations
        turret.RotatetoTarget(target);

    }



}

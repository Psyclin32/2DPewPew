using Godot;
using System;
using System.Diagnostics;

public partial class EnemyUnit : GeneralUnit
{
    private bool Reloaded = false;
    public override void _Ready()
    {   
        turret.timerROF.Timeout += OnReload;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if(Reloaded) 
        {
            turret.FireWeapon(CollisionLayer);
            Debug.Print("Fire!");
        }
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    { 
        base._PhysicsProcess(delta);
    }

    public void OnReload()
    {
        Reloaded = true;
        Debug.Print("Reload!");
    }




}

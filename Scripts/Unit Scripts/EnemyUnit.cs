using Godot;
using System;
using System.Diagnostics;

public partial class EnemyUnit : GeneralUnit
{
    private bool Reloaded = false;
    
    [Export] public Timer debugTimer;
    
    public override void _Ready()
    {   
        //debugTimer.Timeout += OnReload;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if(turret.ReadyFire) 
        {
            turret.FireFixedRotationed(CollisionLayer, Rotation); //IMPORTANT: FIXED HARDPOINTS NEED SHIP ORIENTATION ALIGNED IN SAME DIRECTION AS MUZZEL. 
            //Debug.Print("Fire!");
            //Reloaded = false;
            //debugTimer.Start();
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

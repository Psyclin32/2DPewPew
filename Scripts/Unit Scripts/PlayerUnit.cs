using Godot;
using System;

public partial class PlayerUnit : GeneralUnit
{
    //[Export] public UnitStats PlayerStats; -> comes from general units class

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {  
       
        //continue to look at the mouse, asses mouse position before firing for best accuracy.
       AquireTarget(GetGlobalMousePosition());
       //Fire on mouse click
       if(Input.IsActionPressed("Fire"))  turret.FireTurret(CollisionLayer);

        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        //PlayerMovement Physics
        Rotate(Mathf.DegToRad(Input.GetAxis("Rotate Left", "Rotate Right") * 100 * (float)delta));

        EngineAnimations();

        base._PhysicsProcess(delta);
    }

    public void AquireTarget(Vector2 target)
    {   //should be passed global mopuse POS for player turret Operations
        turret.RotatetoTarget(target);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        Vector2 vel_dir = new Vector2(0f, Input.GetAxis("Forward", "Reverse"));
		vel_dir = vel_dir*vehicleStats.ForwardThrust;
		ApplyCentralForce(vel_dir.Rotated(Rotation));


    }
    public void EngineAnimations()
    {
        //Forward Movement 
        if (Input.IsActionPressed("Forward"))
        {
            engines.Forward(ON);
        }
        else
        { 
            engines.Forward(OFF);
        }
        //GD.Print(Input.IsActionPressed("Forward"));
        

        //Rotation Movement 
        float axis  = Input.GetAxis("Rotate Left", "Rotate Right");
        if (axis > 0)
        {
            engines.RightYaw(ON);
            engines.LeftYaw(OFF);
        } 
        else if (axis < 0)
        {
            engines.RightYaw(OFF);
            engines.LeftYaw(ON);
        }
        else
        {
            engines.RightYaw(OFF);
            engines.LeftYaw(OFF);
        }
    }
}

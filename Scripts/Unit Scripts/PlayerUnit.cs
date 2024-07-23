using Godot;
using System;
using System.Collections;

public partial class PlayerUnit : GeneralUnit
{
    //[Export] public UnitStats PlayerStats; -> comes from general units class
    
    private Timer Death;

    public GlobalVars globalVars {get; set;}//

    public override void _Ready()
    {   
        Death = GetNode<Timer>("Death"); 
        Death.Timeout += OnDeathTimeout;

        globalVars = GetNodeOrNull("/root/Globals") as GlobalVars;
        globalVars.Player = this;
        base._Ready();
    }

    public override void _Process(double delta)
    {  
       
        if(isDead)  Death.Start();

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

        if(!isDead) EngineAnimations();

        base._PhysicsProcess(delta);
    }

    public void AquireTarget(Vector2 target)
    {   //should be passed global mopuse POS for player turret Operations
        turret.RotatetoTarget(target);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        if(!isDead)
        {
        Vector2 vel_dir = new Vector2(0f, Input.GetAxis("Forward", "Reverse"));
		vel_dir = vel_dir*vehicleStats.ForwardThrust;
		ApplyCentralForce(vel_dir.Rotated(Rotation));
        }

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

    public void OnDeathTimeout()
    {
        //Play death animations here
        //Likely done in sync with the animation, a Method would call when DeathAnim finished to bring up Game Over Screen
        
        //Steps:  
        //1) prepare camera in scene tree, moving off player ship. 
        //2) move control to camera's position, add as child. 


        OnGameOver(); //Player GameOver screen when animations have completed. 
        //GD.Print(GetParent().GetTreeStringPretty());
        QueueFree();
        
    }

    public void OnGameOver()
    {
        //GetChild<CollisionShape2D>(0).SetDeferred(CollisionShape2D.PropertyName.Disabled, true); 
        // ^Above is duplicate and technically handled in GeneralUnits 


        CanvasLayer canvasLayer = GetNode<CanvasLayer>("../CanvasLayer");
        PackedScene Scene  = GetNode<SceneLoader>("/root/SceneLoader").gameOver;
        Control gameOver = Scene.Instantiate<Control>();
        gameOver.Position = Position;
        canvasLayer.AddChild(gameOver);
        
        
        


    }


}

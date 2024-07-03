using Godot;
using System;
using System.Diagnostics;

public partial class player : CharacterBody2D
{
	
	[Export] public float acceleration = 10;
	//public const float acceleration  = 10.0f;
	[Export] public int rotation_speed = 100;
	[Export] public float dampening = 2;
	//public const float dampening = 1.0f;	
	[Export] public float max_speed = 300;

	[Export] public Node2D turret;

	
	// public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Process(double delta)
    {	

		turret.LookAt(GetGlobalMousePosition()); //Lookat requires Global Coords of mouse pointers
		turret.Rotate(MathF.PI/2); //current asset is -90deg off, needs dynamic rotation to keep accurate. Rotation in code is in Radians thus PI/2.  

        base._Process(delta);
    }



    public override void _PhysicsProcess(double delta)
	{	
		
		float time =  (float)delta;
		
		Vector2 inputs = new Vector2(0f, Input.GetAxis("Forward", "Reverse"));
		Velocity += inputs.Rotated(Rotation) * acceleration; 
		Velocity = Velocity.LimitLength(max_speed); //limtis magnitude to input variable. 

		// apparently with the engine, I don't need Velocity to always be multiplied by delta time? This breaks when I add it manually. 
		//C# refresher - capitol letters are indicating class objects that are inherited. In this case Velocity  & Rotation. 
		// inputs vector is put on Y axis as that is how this specific sprite is oriented. Change "forward" axis acording to sprite layout.
		//note also for the inputs that "forward" is considered negative as Godot defaults to -y as "up". 
		// NOT the same for the x-axis. +x is to the right like a platformer.  
		
		//side thoughts: note that with the global Vars I don't need to make so many locals and can directly manipulate as needed. 

		//Debug.Print("Speed:   "+ Velocity.Length());


		Rotate(Mathf.DegToRad(Input.GetAxis( "Rotate Left","Rotate Right")*rotation_speed *time));
		// above is very dense but here it is. 
		// GetAxis returns [-1 0 1] based on input. In this case, left is -1 rotation direction and right is +1 direction. 
		// Returns 0 if nothing pressed

		//MATH: Need Mathf library for DegtoRad method as the function Rotate from Node2D expects angles in RADS. 
		// so put simply Rotate(rotation direction * speed * Delta Time)
		//I guess this one does needed delta time for better smoothing. 
		
		if (inputs.Y == 0){
			Velocity = Velocity.MoveToward(Vector2.Zero,dampening);
		}
		//a very quick and dirty "gravity" but works regardless of current velocity direction. 
		// change dampening factor for different effects. 

	
		MoveAndSlide();
		//mandatory call for physics movement. Call after math calcs for that. 
	}
}
//CODE GRAVEYARD 
//rot_dir += dir; 
		
		//Debug.Print("Rotation reading as: " + rot_dir);
		
		/*
		if (Input.IsActionPressed("Rotate Right")){
		rot_dir += 1;
		}

		if (Input.IsActionPressed("Rotate Right")){
		rot_dir -= 1;
		}*/


		/*
		if (Input.IsActionPressed("Forward")){
			//Debug.Print("Forward Pressed!");
			velocity.X += acceleration eleration *time; 
		}
		else{
			velocity.X -= dampen*time;
			if(velocity.X <= (float)(0.0)){
				velocity.X = 0;	
			}
		}
		*/
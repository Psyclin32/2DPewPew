using Godot;
using System;
using System.Diagnostics;

public partial class player : CharacterBody2D
{
	
	[Export] public float acceleration ;
	//public const float acceleration  = 10.0f;
	[Export] public int rotation_speed ;
	[Export] public float dampening;
	//public const float dampening = 1.0f;	
	[Export] public float max_speed;

	
	// public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();



	public override void _PhysicsProcess(double delta)
	{	
		
		float time =  (float)delta;
		
		Vector2 inputs = new Vector2(0f, Input.GetAxis("Forward", "Reverse"));
		Velocity += inputs.Rotated(Rotation) * acceleration ;
		//Velocity = Velocity.LimitLength(max_speed);
		Rotate(Mathf.DegToRad(Input.GetAxis( "Rotate Left","Rotate Right")*rotation_speed *time));
		
		if (inputs.Y == 0){
			Velocity = Velocity.MoveToward(Vector2.Zero,dampening);
		}
	
		MoveAndSlide();
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
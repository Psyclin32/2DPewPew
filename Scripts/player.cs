using Godot;
using System;
using System.Diagnostics;

public partial class player : CharacterBody2D
{
	
	[Export] public float acc;
	//public const float Acc = 10.0f;
	[Export] public int rot_speed;


	[Export] public float dampen;
	//public const float dampening = 1.0f;	
	
	// public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	float rotation= 0;
	float rot_dir = 0;
	public override void _PhysicsProcess(double delta)
	{	
		
		Vector2 velocity = Velocity;
		float time =  (float)delta;
		//float rot_dir = 0f;
		float dir = Input.GetAxis( "Rotate Right","Rotate Left");
		
		rot_dir += dir; 
		
		//Debug.Print("Rotation reading as: " + rot_dir);
		
		/*
		if (Input.IsActionPressed("Rotate Right")){
		rot_dir += 1;
		}

		if (Input.IsActionPressed("Rotate Right")){
		rot_dir -= 1;
		}*/


		if (Input.IsActionPressed("Forward")){
			//Debug.Print("Forward Pressed!");
			velocity.X += acc*time; 
		}
		else{
			velocity.X -= dampen*time;
			if(velocity.X <= (float)(0.0)){
				velocity.X = 0;	
			}
		}
		
		Debug.Print("Velocity before rotation:" + velocity);

		rotation += rot_speed * rot_dir * time;
		Velocity = velocity.Rotated(rotation);
		Debug.Print("Velocity post rotation:" + Velocity);
		MoveAndSlide();
	}
}

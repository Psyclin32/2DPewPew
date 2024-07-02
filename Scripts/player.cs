using Godot;
using System;
using System.Diagnostics;

public partial class player : CharacterBody2D
{
	public const float Acc = 10.0f;
	// public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// if (direction != Vector2.Zero)
		// {
		// 	velocity.X = direction.X * Acc;
		// }
		// else
		// {
		// 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Acc);
		// }
		if (Input.IsActionPressed("Forward")){
			Debug.Print("Forward Pressed!");
		}





		Velocity = velocity;
		MoveAndSlide();
	}
}

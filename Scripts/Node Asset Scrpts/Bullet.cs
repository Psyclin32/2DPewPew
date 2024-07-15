using Godot;
using System;
using System.Collections;
using System.Diagnostics;

public partial class Bullet : RigidBody2D  //Needs to extend to get physics methods when object called. 
{	
	
	[Export] private AnimatedSprite2D animation; 
	[Export] private Timer timer; 
	[Export] private int damage = 5;
	private bool hasDeltDamage = false;

	public override void _Ready()
	{	
		//Start Animation on spawn
		animation.Play(); //begin the animation

		//Timer Configs
		timer.Timeout += OnBulletExpireTimeout; //connects Timeout signal to the method shown on the right side.  
		timer.Start();//Note that this method of grabbing time needs autoStart disabbled.	
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{			

	}
    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(LinearVelocity * (float)delta);  //gathers the identified collision object
		if(collision != null & !hasDeltDamage)  //null check, else it would fail. Checks if damage has already been triggered, prevents any double tap issues if QueueFree() isnt fast enough.  
		{
			hasDeltDamage = true;
			if(collision.GetCollider() is GeneralUnit unitInstance)  //Checks type of entity hit. 
			{
				unitInstance.TakeDamage(damage);
				QueueFree();
			}
			else if (collision.GetCollider() is PlayerUnit playerInstance)
			{
				playerInstance.TakeDamage(damage);
				QueueFree();
			}
		}
        base._PhysicsProcess(delta);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
		//Debug.Print("Bullet Velocity: " + LinearVelocity.ToString());
		base._IntegrateForces(state);
    }

	//Signaling call backs
	private void OnBulletExpireTimeout()
	{
		QueueFree();  //Destroys instances, freeing memory. 
	}
	private void OnBodyEntered()
	{
		//might not be needed with collision implementation show above.  
	}
}

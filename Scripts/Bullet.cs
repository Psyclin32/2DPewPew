using Godot;
using System;
using System.Collections;

public partial class Bullet : RigidBody2D  //Needs to extend to get physics methods when object called. 
{
	 private AnimatedSprite2D anims = null; 
	 //Bullet.GetNode<AnimatedSprite2D>("BulletSprites");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		anims = GetNode<AnimatedSprite2D>("BulletSprites");
		anims.Play();

		var timer = GetNode<Timer>("BulletExpire");
		timer.Timeout += OnBulletExpireTimeout;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{			

	}
	private void OnBulletExpireTimeout()
	{
		QueueFree();
	}
}

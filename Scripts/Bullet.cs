using Godot;
using System;
using System.Collections;

public partial class Bullet : RigidBody2D  //Needs to extend to get physics methods when object called. 
{	
	private AnimatedSprite2D anims = null; 
	private StatStruct.DamageObjects damageObjects;
	//public static bool CanDamage = true;	
	//private int damage = 1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		damageObjects = new StatStruct.DamageObjects(StatStruct.DamageObjects.WeaponType.Ballistic, true, 1);

		anims = GetNode<AnimatedSprite2D>("BulletSprites");
		anims.Play();

		var timer = GetNode<Timer>("BulletExpire");
		//timer.WaitTime = LifeTIme; 
		timer.Timeout += OnBulletExpireTimeout;
		timer.Start();//Note that this method of grabbing time needs autoStart disabbled.

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{			

	}

	//Class Methods
	public int GetDamage()
	{
		return damageObjects.damageValue;
	}

	//Signaling call backs
	private void OnBulletExpireTimeout()
	{
		QueueFree();  //Destroys instances, freeing memory. 
	}
	private void OnBodyEntered(Node2D target)
	{	
		QueueFree();
	}
}

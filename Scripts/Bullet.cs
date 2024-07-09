using Godot;
using System;
using System.Collections;
using System.Diagnostics;

public partial class Bullet : RigidBody2D  //Needs to extend to get physics methods when object called. 
{	
	private AnimatedSprite2D anims; 
	[Export] private static int damage = 1;
	private StatStruct.DamageObjects damageObjects = new StatStruct.DamageObjects(StatStruct.DamageObjects.WeaponType.Ballistic, true, damage);
	
	public override void _Ready()
	{	
		//damageObjects 

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
		// if (target.Owner == Owner)
		// {
		// 	Debug.Print(target.Owner.ToString());
		// 	Debug.Print(Owner.ToString());
			
		// 	return;
		// }
		if (target is RigidBody2D)
		{	
		QueueFree();
		}
		else if (target is player)
		{
		Debug.Print("Player Self Hit!");
		EmitSignal(player.SignalName.PlayerTakesDammage, damageObjects.damageValue);
		}
		else if (target is Enemy)
		{
		
		}
	}
}

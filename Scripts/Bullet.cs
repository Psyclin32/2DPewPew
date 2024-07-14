using Godot;
using System;
using System.Collections;
using System.Diagnostics;

public partial class Bullet : RigidBody2D  //Needs to extend to get physics methods when object called. 
{	
	private AnimatedSprite2D anims; 
	[Export] private static int damage = 1;

	[Export] public bool DamageDelt = false;
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

		// var parent = FindParent("Player");
		// if (parent == null)
		// {
		// 	CollisionLayer = 16;
		// 	CollisionMask = 
		// } 
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{			

	}
    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(LinearVelocity * (float)delta);
		if(collision != null & !DamageDelt)
		{
			if(collision.GetCollider() is Player playerInstance)
			{
				//Debug.Print("Cast!");
				playerInstance.TakeDamage(GetDamage());
			}

			if(collision.GetCollider() is Enemy enemyInstance)
			{
				//Debug.Print("Cast!");
				enemyInstance.TakeDamage(GetDamage());
			}
			if(collision.GetCollider() is Asteroid rockInstance)
			{
				//Debug.Print("Cast!");
				rockInstance.TakeDamage(GetDamage());
			}
			// if(collision.GetCollider().HasMethod("TakeDamage"))
			// {
			// 	collision.GetCollider().CallDeferred("TakeDamage", GetDamage());
			// }
			DamageDelt = true;
		}
        base._PhysicsProcess(delta);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        
		//Debug.Print("Bullet Velocity: " + LinearVelocity.ToString());
		base._IntegrateForces(state);
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
		if (target is Asteroid)
		{
			QueueFree();
		} 

		if (target is Player)
		{	
			QueueFree();
		//Debug.Print("Player Self Hit!");
		//EmitSignal(player.SignalName.PlayerTakesDammage, damageObjects.damageValue);
		}
		else if (target is Enemy)
		{
			
			QueueFree();
		}
	}
}

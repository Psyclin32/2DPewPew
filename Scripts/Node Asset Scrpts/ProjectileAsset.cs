using Godot;
using System;
using System.Diagnostics;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class ProjectileAsset : RigidBody2D
{
	[Export] public DamageStatsResource  damageStats;
	
	[ExportGroup("Attached Nodes")]
	//[Export] public RigidBody2D 		rigidBody;
	[Export] public AnimatedSprite2D 	animatedSprite;
	[Export] public Timer 				lifeTimer;

	private bool hasDeltDamage = false;

	//private float 						newRotation;
	// Called when the node enters the scene tree for the first time
	
	
	public void ExternalData(uint OwnerColliderLayer, float turretRotation, Vector2 muzzel)
	{
		SetMask(OwnerColliderLayer);	//See method for details.
		Rotation = turretRotation;      			//rotation, needs global Coords. 
		Position = muzzel;				//Position of muzzel. Should be global position because bullet is sent to top level. 
		SetPhysics(turretRotation);
	}
	
	
	public override void _Ready()
	{	
		lifeTimer.Timeout += OnLifeTimerTimeout; //connects to the Timers Timeout signal, Timer should be set to auto start on Instance spawn. 
		//SetPhysics();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		CheckCollisions((float) delta);
	
	}

	public void CheckCollisions(float delta)
	{	KinematicCollision2D collision = MoveAndCollide(LinearVelocity * (float)delta);  //gathers the identified collision object
		if(collision != null & !hasDeltDamage)  //null check, else it would fail. Checks if damage has already been triggered, prevents any double tap issues if QueueFree() isnt fast enough.  
		{
			hasDeltDamage = true;
			if(collision.GetCollider() is GeneralUnit unitInstance)  //Checks type of entity hit. 
			{
				unitInstance.TakeDamage(damageStats.Damage);
				QueueFree();
			}
			else if (collision.GetCollider() is PlayerUnit playerInstance)
			{
				playerInstance.TakeDamage(damageStats.Damage);
				QueueFree();
			}
			else if (collision.GetCollider() is ObjectUnits objectInstance)
			{
				objectInstance.TakeDamage(damageStats.Damage);
				QueueFree();
			}
			else if (collision.GetCollider() is StaticUnits staticInstance)
			{
				staticInstance.TakeDamage(damageStats.Damage);
				QueueFree();
			}
		}
        base._PhysicsProcess(delta);
	}

	private void SetMask(uint bitMask)
	{
		CollisionMask =  CollisionMask ^ bitMask; 
		//example idea
		// Player is on layer 			0b00001
		// Bullet default mask is on 	0b10111
		// Resulting Mask if Player spawns Bullet = 0b1011, preventing selv collisions. 
	}
private void SetPhysics(float turretRotation)
	{
		ApplyCentralImpulse(Vector2.Right.Rotated(turretRotation) * damageStats.Speed);
		//ApplyCentralImpulse(Position.DirectionTo(target) * damageStats.Speed);       //the parent is rotatated. Forward is in +x direction of the asset. 
		//GD.Print("Angle from BuletPOS: " + Position.DirectionTo(target));
		//rigidBody.ApplyCentralImpulse(damageStats.DamageStats.["projectileSpeed"]);
	}
	public void OnLifeTimerTimeout()
	{
		QueueFree();
	}

	


}

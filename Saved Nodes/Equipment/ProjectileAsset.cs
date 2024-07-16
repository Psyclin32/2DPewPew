using Godot;
using System;

public partial class ProjectileAsset : Node2D
{
	[Export] public DamageStatsResource  damageStats;
	
	[ExportGroup("Attached Nodes")]
	[Export] public RigidBody2D 		rigidBody;
	[Export] public AnimatedSprite2D 	animatedSprite;
	[Export] public Timer 				lifeTimer;

	private float 						newRotation;
	// Called when the node enters the scene tree for the first time
	
	public ProjectileAsset(uint OwnerColliderLayer, float rotation)
	{
		SetMask(OwnerColliderLayer);	
		newRotation = rotation;      //rotation

	}
	
	
	
	public override void _Ready()
	{	
		

		lifeTimer.Timeout += OnLifeTimerTimeout; //connects to the Timers Timeout signal
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
	}

	public void SetMask(uint bitMask)
	{
		rigidBody.CollisionMask =  rigidBody.CollisionMask ^ bitMask; 
		//example idea
		// Player is on layer 			0b00001
		// Bullet default mask is on 	0b10111
		// Resulting Mask if Player spawns Bullet = 0b1011, preventing selv collisions. 
	}

	public void OnLifeTimerTimeout()
	{
		QueueFree();
	}


}

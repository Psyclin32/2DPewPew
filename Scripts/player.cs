using Godot;
using System;
using System.Diagnostics;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Security.Cryptography.X509Certificates;

public partial class Player : RigidBody2D
{

	[Export] public float acceleration = 10;
	//public const float acceleration  = 10.0f;
	[Export] public int rotation_speed = 100;
	[Export] public float dampening = 2;
	//public const float dampening = 1.0f;	
	[Export] public float max_speed = 300;

	[Export] public int Bullet_Speed = 350;

	//[Signal]  public delegate void PlayerTakesDammageEventHandler();

	//[Export] public StatsClass PCStats;

	//[Export] public Node2D Weapon;

	// public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	
	private Timer _weaponTimer;
	private AnimationPlayer _weaponAnims;
	private bool ready_fire = false;
	private PackedScene _bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

	private StatStruct.Stats PCStats = new StatStruct.Stats(10,0,0);
	private StatStruct.ObjectFlags PCFlags = new StatStruct.ObjectFlags(true, StatStruct.ObjectFlags.IFF.Player);
	
	[Export]
	private Sprite2D Weapon; 

	[Export]
	private Node2D SpawnContainer; 

	

	public override void _Ready()
	{	
		//Debug.Print(GetPath());
		//MaxSlides = 1;

		SpawnContainer = GetNode<Node2D>("ChildSpawns");
		Weapon = GetNode<Sprite2D>("Weapon");
		//load and configure animations for player object
		_weaponAnims = GetNode<AnimationPlayer>("Weapon/FireAnims");
		//load up the timer object and configure the signal 
		_weaponTimer = GetNode<Timer>("WeaponTimer");
		_weaponTimer.Timeout += OnWeaponTimerTimeout;
		base._Ready();
	}

	public override void _Process(double delta)
	{
		//Declare local nodes
		//var Weapon = GetNode<Sprite2D>("Weapon");
		var gun_Barrel = Weapon.GetNode<Marker2D>("GunBarrelPos");
		
		//Weapon turret looking at cursor
		Weapon.LookAt(GetGlobalMousePosition()); //Lookat requires Global Coords of mouse pointers
		Weapon.Rotate(MathF.PI / 2); //current asset is -90deg off, needs dynamic rotation to keep accurate. Rotation in code is in Radians thus PI/2.  

		

		//Firing mechanism for gun
		if (Input.IsActionPressed("Fire Weapon") & ready_fire)
		{
			//Spawn in bullet instance then disable based on ROF with timer. 
			fire_Weapon(gun_Barrel.GlobalPosition); //Marker needs to be child of weapon to properly track rotation
			ready_fire = false;
			_weaponTimer.Start();

			//Trigger the animations
			if(!_weaponAnims.IsPlaying())
			{
				_weaponAnims.Play("Fire");
			}
		}
		base._Process(delta);
	}

	private void fire_Weapon(Vector2 pos)
	{
		RigidBody2D projectile = _bulletScene.Instantiate<RigidBody2D>(); //<> notation converts type of resource as RigidBody2D. Associated Nodes script must extend the Node version we want to upack at its top level. 
		projectile.Position = pos;  // gun_barrel POS
		projectile.LookAt(GetGlobalMousePosition()); //rotate the sprite to point at mouse
		projectile.LinearVelocity = LinearVelocity + pos.DirectionTo(GetGlobalMousePosition()) * Bullet_Speed;  // sets initial velocity. Currently accounts for ship velocity. Realistic but maybe not good for UX.																									
		projectile.TopLevel = true;  //prevents the bullets being tied to the Player nodes's transform changes
		projectile.CollisionMask = 38; //bit mask for bits 2 + 3 + 6;
		SpawnContainer.AddChild(projectile); 
		
		//GetNode("Level Environment").AddChild(projectile);
		//Debug.Print(GetTreeStringPretty());
	}

	public void TakeDamage(int damage)
	{
		PCStats.Health -= damage;
		//Debug.Print("HP of ship:" + PCStats.Health);
	}

	private void OnWeaponTimerTimeout()
	{
		ready_fire = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		float time = (float)delta;
		Vector2 inputs = new Vector2(0f, Input.GetAxis("Forward", "Reverse"));
		LinearVelocity += inputs.Rotated(Rotation) * acceleration;
		LinearVelocity = LinearVelocity.LimitLength(max_speed); //limtis magnitude to input variable. 

		// apparently with the engine, I don't need Velocity to always be multiplied by delta time? This breaks when I add it manually. 
		//C# refresher - capitol letters are indicating class objects that are inherited. In this case Velocity  & Rotation. 
		// inputs vector is put on Y axis as that is how this specific sprite is oriented. Change "forward" axis acording to sprite layout.
		//note also for the inputs that "forward" is considered negative as Godot defaults to -y as "up". 
		// NOT the same for the x-axis. +x is to the right like a platformer.  
		//side thoughts: note that with the global Vars I don't need to make so many locals and can directly manipulate as needed. 
		//Debug.Print("Speed:   "+ Velocity.Length());

		Rotate(Mathf.DegToRad(Input.GetAxis("Rotate Left", "Rotate Right") * rotation_speed * time));
		// above is very dense but here it is. 
		// GetAxis returns [-1 0 1] based on input. In this case, left is -1 rotation direction and right is +1 direction. 
		// Returns 0 if nothing pressed
		//MATH: Need Mathf library for DegtoRad method as the function Rotate from Node2D expects angles in RADS. 
		// so put simply Rotate(rotation direction * speed * Delta Time)
		//I guess this one does needed delta time for better smoothing. 
		// if (inputs.Y == 0)
		// {
		// 	LinVelocity = Velocity.MoveToward(Vector2.Zero, dampening);
		// }
		//a very quick and dirty "gravity" but works regardless of current velocity direction. 
		// change dampening factor for different effects. 
		//MoveAndSlide();
		//mandatory call for physics movement. Call after math calcs for that. 
	}
	private void OnPlayerTakesDamage(int x)
	{
		PCStats.Health -= x;
	}
}

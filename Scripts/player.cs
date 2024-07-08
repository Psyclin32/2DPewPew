using Godot;
using System;
using System.Diagnostics;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Security.Cryptography.X509Certificates;

public partial class player : CharacterBody2D
{

	[Export] public float acceleration = 10;
	//public const float acceleration  = 10.0f;
	[Export] public int rotation_speed = 100;
	[Export] public float dampening = 2;
	//public const float dampening = 1.0f;	
	[Export] public float max_speed = 300;

	[Export] public int Bullet_Speed = 350;

	//[Export] public StatsClass PCStats;

	//[Export] public Node2D Weapon;

	// public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	
	private Timer _weaponTimer;
	private AnimationPlayer _weaponAnims;
	private bool ready_fire = false;
	private PackedScene _bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

	public override void _Ready()
	{	
		//Establishing Default Character stats
		var PCStats = new StatsClass();
		PCStats.SetStatValues();

		//Debug.Print("Health = " + PCStats.GetStatValues(StatsClass._statNames.Health));
		//Debug.Print("Shields = " + PCStats.GetStatValues(StatsClass._statNames.Shields));
		//Debug.Print("Armor = " + PCStats.GetStatValues(StatsClass._statNames.Armor));

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

		var Weapon = GetNode<Sprite2D>("Weapon");
		var gun_Barrel = Weapon.GetNode<Marker2D>("GunBarrelPos");
		
		//var recoil_anim = Weapon.GetNode<AnimationPlayer>("Recoil");
		//gun_Barrel = GetNode<Marker2D>("GunBarrelPos");

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

			//Debug.Print("Weapon Fired!");
		}

		//Thruster Code?


		base._Process(delta);
	}

	private void fire_Weapon(Vector2 pos)
	{

		RigidBody2D projectile = (RigidBody2D)_bulletScene.Instantiate(); //cast resource as Rigid Body. Associated Nodes script must extend the Node version we want to upack at its top level. 
		projectile.Position = pos;  // gun_barrel POS
		projectile.LookAt(GetGlobalMousePosition()); //rotate the sprite to point at mouse
		projectile.LinearVelocity = Velocity + pos.DirectionTo(GetGlobalMousePosition()) * Bullet_Speed;  // sets initial velocity. Currently accounts for ship velocity. Realistic but maybe not good for UX.																									
		AddSibling(projectile); //DO NOT WANT AS CHILD TO PLAYER. BINDS ROTATION TO PLAYER CONTROLS. 
		//GetNode("Level Environment").AddChild(projectile);
		//Debug.Print(GetTreeStringPretty());

	}

	private void OnWeaponTimerTimeout()
	{
		ready_fire = true;
		/* if for debug only
		if(!ready_fire)
		{
			ready_fire = true;
			Debug.Print("Reloaded and Ready!");
		}*/
	}

	public override void _PhysicsProcess(double delta)
	{

		float time = (float)delta;

		

		Vector2 inputs = new Vector2(0f, Input.GetAxis("Forward", "Reverse"));
		Velocity += inputs.Rotated(Rotation) * acceleration;
		Velocity = Velocity.LimitLength(max_speed); //limtis magnitude to input variable. 

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

		if (inputs.Y == 0)
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, dampening);
		}
		//a very quick and dirty "gravity" but works regardless of current velocity direction. 
		// change dampening factor for different effects. 


		MoveAndSlide();
		//mandatory call for physics movement. Call after math calcs for that. 
	}
}

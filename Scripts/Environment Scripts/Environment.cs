using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class Environment : Node2D
{
	[ExportGroup("Environment Packed Scenes")]
	[Export]
	public PackedScene[]  aseteroids;
	//public Godot.Collections.Dictionary<PackedScene, int> Scenes { get; set; }
		//seems to fail an not be able to save scene paths

	[Export]
	public int numberEnemy = 1;
	[Export(PropertyHint.Enum,"Minimum quantity, Max quantity")]
	public Godot.Collections.Array<int> range;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//AsteroidSpawner(range[0], range[1]);
		//EnemySpawner(numberEnemy);
	}

	private void AsteroidSpawner(int low, int high)
	{
		int n = GD.RandRange(low, high);
		for (int i = 0 ;  i < n ; i++)
		{
		GeneralUnit new_roid =  aseteroids[0].Instantiate<GeneralUnit>();
		new_roid.Position = new Vector2(GD.RandRange(-200, 200), GD.RandRange(-200, 200));
		new_roid.Name = "Asteroid"+ i;
		AddChild(new_roid);
		}
	} 
	// private void EnemySpawner(int n)
	// {
	// 	for (int i =1 ;  i <= n ; i++)
	// 	{
	// 	RigidBody2D newEnemy = Enemy.Instantiate<RigidBody2D>();
	// 	newEnemy.Position = new Vector2(150,150);
	// 	AddChild(newEnemy);
	// 	}

	// }
// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

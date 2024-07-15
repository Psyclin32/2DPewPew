using Godot;
using System;
using System.Diagnostics;

public partial class Environment : Node2D
{
	[ExportGroup("Environment Packed Scenes")]
	[Export]
	public Godot.Collections.Array<PackedScene> Scenes { get; set; }
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

	// private void AsteroidSpawner(int low, int high)
	// {
	// 	int n = GD.RandRange(low, high);
	// 	for (int i = 0 ;  i < n ; i++)
	// 	{
	// 	RigidBody2D new_roid = Asteroid.Instantiate<RigidBody2D>();
	// 	new_roid.Position = new Vector2(GD.RandRange(-500, 500), GD.RandRange(-500, 500));
	// 	new_roid.Name = "Asteroid"+ i;
	// 	AddChild(new_roid);
	// 	}
	// } 
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

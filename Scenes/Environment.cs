using Godot;
using System;
using System.Diagnostics;

public partial class Environment : Node2D
{
	public PackedScene Asteroid = GD.Load<PackedScene>("res://Scenes/asteroid.tscn");
	public PackedScene Enemy = GD.Load<PackedScene>("res://Scenes/Enemy02.tscn");

	[Export]
	public int numberEnemy = 1;
	[Export]
	public int[] range = new int[] {5, 15};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AsteroidSpawner(range[0], range[1]);
		EnemySpawner(numberEnemy);
	}
	private void AsteroidSpawner(int low, int high)
	{
		int n = GD.RandRange(low, high);
		for (int i = 0 ;  i < n ; i++)
		{
		RigidBody2D new_roid = Asteroid.Instantiate<RigidBody2D>();
		new_roid.Position = new Vector2(GD.RandRange(-500, 500), GD.RandRange(-500, 500));
		new_roid.Name = "Asteroid"+ i;
		AddChild(new_roid);
		}
	} 
	private void EnemySpawner(int n)
	{
		for (int i =0 ;  i <= n ; i++)
		{
		CharacterBody2D newEnemy = Enemy.Instantiate<CharacterBody2D>();
		newEnemy.Position = new Vector2(150,150);
		AddChild(newEnemy);
		}

	}
// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

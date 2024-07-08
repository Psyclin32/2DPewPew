using Godot;
using System;
using System.Diagnostics;

public partial class Environment : Node2D
{
	[Export] public PackedScene Asteroid = GD.Load<PackedScene>("res://Scenes/asteroid.tscn");

	public int[] range = new int[] {5, 15};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AsteroidSpawner(range[0], range[1]);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void AsteroidSpawner(int low, int high)
	{
		int number_spawns = GD.RandRange(low, high);
		for (int i =1 ;  i <= number_spawns ; i++)
		{
		RigidBody2D new_roid = Asteroid.Instantiate<RigidBody2D>();
		new_roid.Position = new Vector2(GD.RandRange(-500, 500), GD.RandRange(-500, 500));
		new_roid.Name = "Asteroid"+ i;
		AddChild(new_roid);
		}
	} 


}

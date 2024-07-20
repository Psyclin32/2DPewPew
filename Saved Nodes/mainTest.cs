using Godot;
using System;

public partial class mainTest : Node2D
{
	public GlobalVars Globals;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Globals = GetNode("/root/Globals") as GlobalVars;
		Globals.Player = GD.Load<PackedScene>(Globals.PlayerScenePath).Instantiate<PlayerUnit>();
		Globals.Player.GlobalPosition = Vector2.Zero;
		AddChild(Globals.Player);		
		
		GD.Print(GetTreeStringPretty());
		//GetNode("/root/Main");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

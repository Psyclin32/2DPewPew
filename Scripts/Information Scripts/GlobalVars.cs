using Godot;
using System;

public partial class GlobalVars : Node
{

//public Player 
public PlayerUnit Player;

public string PlayerScenePath = "res://Saved Nodes/Units/CharacterTempalte.tscn";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// PlayerUnit Player = GD.Load<PackedScene>(PlayerScenePath).Instantiate<PlayerUnit>();
		// Player.GlobalPosition = Vector2.Zero;
		// GD.Print(GetTreeStringPretty());
		//AddChild(Player);
		//GD.Print(Player.GlobalPosition);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}

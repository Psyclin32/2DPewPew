using Godot;
using System;
using System.Linq;

public partial class EnginesAssets : Node2D
{
	//Simple interface that turns on the animations for each thruster when given the command externally. 
	//Is there where the physcis should occur? Likely not, just animation control as the parent RigidBodies need to manager the unit specific physics movement.

	private enum Engine_Type
	{
		Forward,
		RightYaw,
		LeftYaw,
		RightStrafe,
		LeftStrafe,
	}


	[Export] public AnimatedSprite2D[] Engines;


	// [Export] public AnimatedSprite2D ForwardEngine;
	// [Export] public AnimatedSprite2D RightYawEngine;
	// [Export] public AnimatedSprite2D LeftYawEngine;
	// [Export] public AnimatedSprite2D RightStrafeEngine;
	// [Export] public AnimatedSprite2D LeftStrafeEngine;
	



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		int length = GetChildCount();
		for (int i = 0; i < length; i++)
		{
			Engines.Append<AnimatedSprite2D>(GetChild<AnimatedSprite2D>(i));
			Engines[i].Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		GD.Print(Engines[(int) Engine_Type.Forward].Name);

	}


	public void Forward()
	{
		if(Engines[(int) Engine_Type.Forward] == null) return;

		Engines[(int) Engine_Type.Forward].Play();
		Engines[(int) Engine_Type.Forward].Visible = true;


	}

	public void RightYaw()
	{
		if(Engines[(int) Engine_Type.RightYaw] == null) return;
	}

	public void LeftYaw()
	{
		if(Engines[(int) Engine_Type.LeftYaw] == null) return;
	}

	public void RightStrafe()
	{
		if(Engines[(int) Engine_Type.RightStrafe] == null) return;
		
	}
	public void LeftStrafe()
	{
		if(Engines[(int) Engine_Type.LeftStrafe] == null) return;
		
	}

}

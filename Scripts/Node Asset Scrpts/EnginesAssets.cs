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
	[ExportGroup("Engine Nodes")]
	[Export] public AnimatedSprite2D ForwardEngine;
	[Export] public AnimatedSprite2D RightYawEngine;
	[Export] public AnimatedSprite2D LeftYawEngine;
	[Export] public AnimatedSprite2D RightStrafeEngine;
	[Export] public AnimatedSprite2D LeftStrafeEngine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	private void EnableEngine(AnimatedSprite2D engine)
	{
		engine.Visible = true;
		engine.Play();
		//engine.Frame = 0;
	}
	private void DisableEngine(AnimatedSprite2D engine)
	{
		engine.Visible = false;
		engine.Stop();
		engine.Frame = 0;
	}
	public void Forward(bool on)
	{
		if (!on) 
		{
			DisableEngine(ForwardEngine);
			return;
		}
		EnableEngine(ForwardEngine);
	}
	public void RightYaw(bool on)
	{	
		if (!on) 
		{
			DisableEngine(RightYawEngine);
			return;
		}
		EnableEngine(RightYawEngine);
	}
	public void LeftYaw(bool on)
	{
		if (!on) 
		{
			DisableEngine(LeftYawEngine);
			return;
		}
		EnableEngine(LeftYawEngine);
	}
	public void RightStrafe(bool on)
	{
		if(RightStrafeEngine is null) return;
		if (!on) 
		{
			DisableEngine(RightStrafeEngine);
			return;
		}
		EnableEngine(RightStrafeEngine);
	}
	public void LeftStrafe(bool on)
	{	
		if(LeftStrafeEngine is null) return;
		if (!on) 
		{
			DisableEngine(LeftStrafeEngine);
			return;
		}
		EnableEngine(LeftStrafeEngine);
	}
}

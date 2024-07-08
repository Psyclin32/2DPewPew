using Godot;
using System;

public partial class StatStruct : Node
{

	public struct Stats
	{
		public Stats(int x, int y)
		{
		Health = x;	
		
		Shields = y;
		}
		
		public int Health;
		public int Shields;
	}

}

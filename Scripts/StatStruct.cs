using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class StatStruct : Node
{

	//Use for tracking both PC and NPC stats (My create separately if needed, unlikely in shrot term)
	//Tracking vitals like HP + shields and other "Live" data that is used at gameplay layer. 
	public struct Stats
	{	

		public int Health;
		public int Shields;

		public int Armor;

		public Stats(int x, int y, int z)
		{
		Health = x;	
		
		Shields = y;
		
		Armor = z;
		}
		
	}

}

using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class StatStruct : Node
{
	//Use for creating global flag and data objects (My create separately if needed, unlikely in short term)
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

	public struct ObjectFlags
	{// Used for higher node scense
		public enum IFF
		{
			Player,
			Neutral,
			Ally,
			Enemy,
		}
		public bool isPlayer;
		public IFF unitIFF;
		public ObjectFlags(bool isPlayer, IFF unitIFF)
		{
			this.isPlayer = isPlayer;
			this.unitIFF = unitIFF;
		}
	}

	public struct DamageObjects
	{ //Used for weapon templates Likely might become an extended class as being here is quite high up in the data heirarchy
		public enum WeaponType
		{
			Ballistic,
			Missile,
			Beam,
		}
		public WeaponType weaponType;
		public int damageValue;
		public bool hasPhysics;
		
		public DamageObjects(WeaponType type, bool physics, int damage)
		{
			weaponType = type;
			hasPhysics = physics;
			damageValue = damage;
			
		}
	}


}

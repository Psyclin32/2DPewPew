using Godot;
using System;

[GlobalClass]
public partial class EquipmentStatsResource : Resource
{
    //[Export] private AnimatedSprite2D weaponSprite; NOT RECCOMENDED!! Error says resources do not inherit from NODE base class. Likely cuases issues or just wont work. 

    [ExportGroup("Equipment Stats")]
    [Export]
    public Godot.Collections.Dictionary<string, int> DamageStats { get; set; } = new Godot.Collections.Dictionary<string, int>
    {
        // ["projectileDamage"] = 1,
        // //["turretRotationSpeed"] = 300,  //In degrees per second because turrets are not Rigidbodies. Toque not needed - Likely needs tuning as a default. 
        // // Removing as thinking of divorcing projectile stats from the turret stats. Keeping this object strictlty for the damage projectiles
        // ["projectileHealth"] = 1,       //only matters if said object is put on the proper collision mask.     
        // ["projectileSpeed"] = 500,      //speed is going to be in the form of impulse force applied
        // ["projectileMass"] = 5,         //Kilograms
        // ["projectileBloom"] = 2,        //Weapon shooting spread,  In degrees for easy tuning per object. Likley need to convert to radians on the interaction layer. 
    };
     public EquipmentStatsResource() {}

     //public EquipmentStatsResource() {}







}

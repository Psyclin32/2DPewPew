using Godot;
using System;

[GlobalClass]
public partial class VehicleStats : Resource
{
//All of these assumed RigidBody Motion and will be used in the "IntegrateForces()" method for that specific unit control script.   
//Thrust needs to be fairly large (>1000) once the objects get over 100 KG to have anything remotely arcadey in feel and responsiveness.
[Export] public float ForwardThrust;  
[Export] public float RotationThrust;
[Export] public float StrafeThrust;
[Export] public float ReverseRatio;
[Export] public float Mass;        //in KG
[Export] public float Dampening; //should be <1 unless I really want this thing to stop on a dime. 



}

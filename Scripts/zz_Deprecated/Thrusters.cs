using Godot;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.WebSockets;

public partial class Thrusters : AnimatedSprite2D
{   
    //enum for types. 
    public enum Engine_Type
    {
        LEFT,
        RIGHT,
        FORWARD,
        OTHER = -1,
    }
    [Export] 
    public Engine_Type This_Engine_Type;  //Make Local enum and export for options setting in editor. 

    public Node Parent;
    public override void _Ready()
    {   
        Parent = FindParent("Player"); 
        Visible = false;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        PlayerThrusters();
        base._Process(delta);
    }

    public void PlayerThrusters()
    {
        //LEFT ENGINE
        if(Input.IsActionPressed("Rotate Left") & This_Engine_Type == Engine_Type.LEFT){
            //Debug.Print("ROTATE LEFT THRUSTER");
            Visible = true;
            Play("Exhaust",1); //node is animatedsprite2D, no need to make separate object call. Cna direct call Method. 

        }

        //Right ENGINE
        else if(Input.IsActionPressed("Rotate Right") & This_Engine_Type == Engine_Type.RIGHT)
        {   
            //Debug.Print("ROTATE LEFT THRUSTER");
            Visible = true;
            Play();
        }

        //FORWARD ENGINE
        else if(Input.IsActionPressed("Forward") & This_Engine_Type == Engine_Type.FORWARD){
            //Debug.Print("ROTATE LEFT THRUSTER");
            Visible = true;
            Play();
        }
         else
         {
            Visible = false;
            Frame = 0;
         }
    }
    
}

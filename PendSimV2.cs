//============================================================================
// PendSimV2.cs: Class for simulating pendulum, with inheritance. 
//   (Child)     This file is specific to the PENDULUM  
//============================================================================

using System;

namespace Sim
{

    public class PendSimV2 : Simulator  //PendSimV2 inherits Sumulator
    {
        private double L; //Pendulum length

        //--------------------------------------------------------------------
        // Constructor : Initializes the conditions necessary
        //--------------------------------------------------------------------
        public PendSimV2() : base(2) //Sends the base class 2
        {
            Console.WriteLine("PendSimV2 constructor");
            L = 1.0;    // Set default value for length 

            // Setting Initial Conditions
            x[0] = 1.0; // Pendulum angle
            x[1] = 0.0; // Rotation rate

            SetRHSFunc(RhsFunc);
        }
        
        
        //-------------------------------------------------------------------------
        // RHSFunc: Calculates the right hand side of the differential equation
        //          for the pendulum
        //-------------------------------------------------------------------------
        private void RhsFunc(double[] st, double t, double[] ff)
        {
            ff[0] = st[1];
            ff[1] = -g*Math.Sin(st[0])/L;
        }   

        //--------------------------------------------------------------------  
        // Defining getters and setters
        //--------------------------------------------------------------------
        public double Length
        {
            get {return L;}
            set {if(value>0){
                 L = value; }
                }    
        }
        public double Angle
        {
            get {return x[0];}
            set {x[0] = value;}  
        }
        public double Angledot
        {
            get {return x[1];}
            set {x[1] = value;}
        }

    }

}
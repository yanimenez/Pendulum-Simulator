//============================================================================
//           PendSim.cs : Class to implement a pendulum simulation
//============================================================================
using System;

namespace Sim
{
    public class PendSim //Used to make objects
    {
        private double L;   // Pendulum length
        private double g;   // Gravitational field strength

        private double[] x; // Array of states
        private double[] xi;  //Array of intermediate state (xA)
        private double[][] f; // Array holding values of rhs of odes  
        private int n;      // Defines number of first order odes used
        //--------------------------------------------------------------------
        // Constructor : Initializes the conditions necessary
        //--------------------------------------------------------------------
        public PendSim() //Constructor
        {
            L =1.0; g = 9.81;     // Default values for variables
            n=2;
            x = new double[n];    // x1 = theta, x2 = omega
            xi = new double[n];
            f = new double[2][];
            f[0] = new double[n]; // f0
            f[1] = new double[n]; // fA 

            // Set initial conditions for pendulum
            x[0] = 1.0;     // Pendulum angle
            x[1] = 0.0;     // Rotation rate
        }
        //--------------------------------------------------------------------  
        // StepEuler : Executes one numerical integration step using 
        //             Eulers method. Public so user can access the method
        //--------------------------------------------------------------------  
        public void StepEuler(double time, double dTime)
        {
            int i;
            RhsFunc(x,time,f[0]); //Giving RHS state, time and f
            for(i=0;i<n;++i)
            {
                x[i] += f[0][i] *dTime;
            }
        } 

        //--------------------------------------------------------------------  
        // StepRK2 : Executes one numerical integration step using the RK2
        //           method. Public so user can access the method.
        //--------------------------------------------------------------------  
        public void StepRK2(double time, double dTime)
        {
            int i;
            RhsFunc(x,time,f[0]); //Giving RHS state, time and f
            for(i=0;i<n;++i)
            {
                xi[i] = x[i] + f[0][i]*dTime;
            }
            RhsFunc(xi,time+dTime, f[1]);
            for(i=0;i<n;++i)
            {
                x[i] += 0.5*(f[0][i] + f[1][i])*dTime;
            }
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
        // StateString: Constructs a string in csv format that contains 
        //              the state. Public for user access
        //--------------------------------------------------------------------  
        public string StateString(double time)
        {
            string s = time.ToString();
            for(int i =0;i<n;++i)
            {
                s+= ',' + x[i].ToString();
            }
            return s;
        }

        public void TestFunc()
        {
            Console.WriteLine("TestFunc Called");
            Console.WriteLine("L = " + L.ToString() + ", g = " +g.ToString());
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





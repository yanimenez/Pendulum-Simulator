//============================================================================
//      Simulator.cs: Defines the base class for creating simulations.    
//        (PARENT)    Base calss provides the common pieces for others sims 
//============================================================================ 
using System;

namespace Sim
{
    public class Simulator
    {
        protected double g;     //Gravitational field strength. 
                                //Available to inheriting file, nothing else

        protected int n;     //Number of first order ODEs
        protected double[] x;   //Array of states
        protected double[] xi;   //Array of intermediate states
        protected double[][] f;  //Array of values on the right hand side
        private Action<double[], double, double[]> rhsFunc;


        //--------------------------------------------------------------------
        //Constructor:Initializes the conditions necessary. Same name as class
        //--------------------------------------------------------------------
        public Simulator(int nn)
        {
            Console.WriteLine("simulator constructor");
            n = nn;
            x = new double[n];    // x1 = theta, x2 = omega
            xi = new double[n];
            f = new double[2][];
            f[0] = new double[n]; // f0
            f[1] = new double[n]; // fA 

            g = 9.81;
            rhsFunc = nothing;
        }

        //--------------------------------------------------------------------  
        // StepEuler : Executes one numerical integration step using 
        //             Eulers method. Public so user can access the method
        //--------------------------------------------------------------------  
        public void StepEuler(double time, double dTime)
        {
            int i;
            
            rhsFunc(x,time,f[0]); //Giving RHS state, time and f
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
            rhsFunc(x,time,f[0]); //Giving RHS state, time and f
            for(i=0;i<n;++i)
            {
                xi[i] = x[i] + f[0][i]*dTime;
            }
            rhsFunc(xi,time+dTime, f[1]);
            for(i=0;i<n;++i)
            {
                x[i] += 0.5*(f[0][i] + f[1][i])*dTime;
            }
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


        //--------------------------------------------------------------------  
        // SetRHSFunc: Recieves function from child to calculate RHS of ODE
        //-------------------------------------------------------------------- 

        protected void SetRHSFunc(Action<double[], double, double[]> rhs)
        {
            rhsFunc = rhs;

        }
        private void nothing(double[] st, double t, double[] ff)
        {
            
        }  
    }
}

//============================================================================
//               Program to integrate differential equations 
//============================================================================
using System;
using Sim;

class Program
{
    static void Main()
    {
        PendSimV2 pend = new PendSimV2(); //Object for PendSimV2

        //PendSim pend = new PendSim(); //Creates PendSim obj & puts into pend 
        pend.Length = 1.4;
        pend.Angle = 0.5;
        pend.Angledot = 1;
        
        double t = 0.0;     // Start time 
        double dt = 0.02;   // Time step
        double tEnd = 50.0;  // Ending time

        // Simulation loop
        Console.WriteLine(pend.StateString(t));
        while(t<tEnd - dt*0.5)
        {
            pend.StepRK2(t,dt); //Sends over current time and time step 
            t += dt;
            Console.WriteLine(pend.StateString(t));
            
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CancelacionHilos
{
    public class Ejemplo
    {
        public static void Main()
        {
            // Create the token source.
            CancellationTokenSource cts = new CancellationTokenSource();
            // Pass the token to the cancelable operation.
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoSomeWork), cts.Token);
            Thread.Sleep(2500);
            // Request cancellation.
            cts.Cancel();
            Console.WriteLine("Cancelación establecida en la fuente del token...");
            Thread.Sleep(2500);
            // Cancellation should have happened, so call Dispose.
            cts.Dispose();
        }
        // Thread 2: The listener
        static void DoSomeWork(object? obj)
        {
            if (obj is null)
                return;
            CancellationToken token = (CancellationToken)obj;
            for (int i = 0; i < 100000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("En la iteracion {0}, la cancelacion a sido solicitada...",
                    i + 1);
                    // Perform cleanup if necessary.
                    //...
                    // Terminate the operation.
                    break;
                }

                // Simulate some work.
                Thread.SpinWait(500000);
            }
        }
    }
  

}

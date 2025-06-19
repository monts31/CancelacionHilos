using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancelacionHilos
{
    public class Ejemplo1
    {
        class CancelableObject
        {
            public string id;
            public CancelableObject(string id)
            {
                this.id = id;
            }
            public void Cancel()
            {
                Console.WriteLine($"Objeto {id} Cancelar devolución de llamada");
                // Perform object cancellation here.
            }
        }
        
            public static void Main()
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                // User defined Class with its own method for cancellation
                var obj1 = new CancelableObject("1");
                var obj2 = new CancelableObject("2");
                var obj3 = new CancelableObject("3");
                // Register the object's cancel method with the token's
                // cancellation request.
                token.Register(() => obj1.Cancel());
                token.Register(() => obj2.Cancel());
                token.Register(() => obj3.Cancel());
                // Request cancellation on the token.
                cts.Cancel();
                cts.Dispose();
            }
        
    }
}

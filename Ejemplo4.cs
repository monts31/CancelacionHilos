using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancelacionHilos
{
    public class Ejemplo4
    {
        public static void Main()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            StartWebRequest(cts.Token);
            // Cancellation will cause the web
            // request to be cancelled.
            cts.Cancel();
        }
        static void StartWebRequest(CancellationToken token)
        {
            var client = new HttpClient();
            token.Register(() =>
            {
                client.CancelPendingRequests();
                Console.WriteLine("Solicitud cancelada!");
            });
            Console.WriteLine("Comenzando solicitud.");
            client.GetStringAsync(new Uri("http://www.contoso.com"));
        }
    }
}

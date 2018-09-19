using A1.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

           
            Console.Write("enter number of actors");
            int numberOfActors = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= numberOfActors; i++)
            {
                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(i.ToString()));
                Guid guid = new Guid(hash);

                IA1 actor = ActorProxy.Create<IA1>(new ActorId(guid), new Uri("fabric:/Rem1/A1ActorService"));
                Task<string> retval = actor.GetHelloWorldAsync(guid, i);
            }

            Console.Write("finally");
            Console.ReadLine();
        }
    }
}

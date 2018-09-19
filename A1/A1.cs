using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using A1.Interfaces;
using A2.Interfaces;

namespace A1
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class A1 : Actor, IA1
    {
        /// <summary>
        /// Initializes a new instance of A1
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public A1(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        public Task<string> GetHelloWorldAsync(Guid guid, int clientId)
        {
            IA2 actor = ActorProxy.Create<IA2>(new ActorId(guid), new Uri("fabric:/Rem1/A2ActorService"));
            Task retval = actor.GetSomethingAsync(clientId);

            return Task.FromResult("Hello from my A1 actor!");
        }
    }
}

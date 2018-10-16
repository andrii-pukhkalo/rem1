using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using A2.Interfaces;

namespace A2
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
    internal class A2 : Actor, IA2, IRemindable
    {
        /// <summary>
        /// Initializes a new instance of A2
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public A2(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {

        }

        public Task GetSomethingAsync(int clientId)
        {
            
            TimeSpan period = TimeSpan.FromMilliseconds(-1);
          
            for (int gameId = 1; gameId <= 14; gameId++)
            {
                TimeSpan dueTime = TimeSpan.FromSeconds(gameId * 10);
               
                string remainderName = "client"+Id.ToString()+"game"+gameId.ToString();

                RegisterReminderAsync(remainderName, BitConverter.GetBytes(gameId), dueTime, period);
                ActorEventSource.Current.ActorMessage(this, remainderName + " registered");
            }

            return Task.FromResult(true);
        }

        public Task ReceiveReminderAsync(string reminderName, byte[] context, TimeSpan dueTime, TimeSpan period)
        {
            string clientId = Id.ToString();
            int gameId = BitConverter.ToInt32(context, 0);
            
            if (reminderName.Equals("client"+Id.ToString()+"game"+gameId.ToString()))
            {
                //ActorEventSource.Current.ActorMessage(this, reminderName + " is started");
                //emulateGame();
                //ActorEventSource.Current.ActorMessage(this, reminderName + " is finished");

                // some changes on master need to be in development

                // merge via git cli

                IActorReminder reminder = GetReminder(reminderName);
                if (reminder != null)
                {
                    UnregisterReminderAsync(reminder);
                    ActorEventSource.Current.ActorMessage(this, reminderName + " unregistered");
                }
            }

            return Task.FromResult(true);
        }

        private void emulateGame()
        {
            for (int i = 0; i < 800000000; i++) { }
        }
    }
}

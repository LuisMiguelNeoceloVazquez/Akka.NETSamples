﻿using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImmutabilitySample
{
    #region Messages

    class Greeting
    {
        public string Name { get; set; }
    }

    class GreetingImmutable
    {
        public GreetingImmutable(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }
    }

    #endregion

    #region Actors

    class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<GreetingImmutable>(m =>
            {
                Console.WriteLine("Hello, {0}!", m.Name);
            });
            Receive<Greeting>(m =>
            {
                m.Name += "<Testing out immutability>";
                Console.WriteLine("Hello, {0}!", m.Name);
            });
        }
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("greetingSystem");
            var greetingActor = system.ActorOf<GreetingActor>("greetingActor");

            var message = new Greeting
            {
                Name = "Akka.NET"
            };

            var immutalbleMessage = new GreetingImmutable("Akka.NET");

            greetingActor.Tell(message);
            greetingActor.Tell(message);
            greetingActor.Tell(message);

            greetingActor.Tell(immutalbleMessage);

            Console.ReadLine();
        }
    }
}

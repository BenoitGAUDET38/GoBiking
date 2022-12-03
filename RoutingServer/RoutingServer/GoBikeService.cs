using RoutingServer.Tools;
using RoutingServer.Tools.JCDecaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
	public class GoBikeService : IGoBikeService
	{
		/**
		 * Generate the initary instructions and sent them into a queue
		 * Return the queue name.
		 */
		public string GetItinary(string originAdress, string destinationAdress)
		{
			// Get itinary instructions
			Itinary itinary = new Itinary();
			string itinaryInstructions = itinary.GetItinaryAsync(originAdress, destinationAdress).Result;
			//return itinaryInstructions;

			// Generate a random queue name
			string queueName = ActiveMqHelper.GenerateRandomQueueName();
			Console.WriteLine("Queue name : " + queueName);

			// Create the producer and sent the instructions
			ActiveMqHelper activeMqHelper = new ActiveMqHelper();
			activeMqHelper.CreateProducer(queueName);
			activeMqHelper.SendMessagesOnNewLine(itinaryInstructions);
			activeMqHelper.CloseSession();

			// Return the queue name
			Console.WriteLine("New instructions sent to queue : " + queueName);
			return queueName;
			try
			{

			}
			catch (Exception)
			{
				return "Des problèmes ont été rencontré par le serveur...";
			}
		}
	}
}

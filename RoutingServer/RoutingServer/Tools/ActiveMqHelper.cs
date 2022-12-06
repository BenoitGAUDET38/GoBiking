using Apache.NMS.ActiveMQ;
using Apache.NMS;
using System;
using System.Linq;
using Apache.NMS.ActiveMQ.Commands;

namespace RoutingServer.Tools
{
	internal class ActiveMqHelper
	{
		private static Random RANDOM = new Random();

		private IConnection _connection;
		private ISession _session;
		private IMessageProducer _producer;

		/**
		 * Create the queue for the producer
		 */
		public void CreateProducer(string queueName)
		{
			// Create a Connection Factory.
			Uri connecturi = new Uri("activemq:tcp://localhost:61616");
			ConnectionFactory connectionFactory = new ConnectionFactory(connecturi);

			// Create a single Connection from the Connection Factory.
			_connection = connectionFactory.CreateConnection();
			_connection.Start();

			// Create a session from the Connection.
			_session = _connection.CreateSession();

			// Use the session to target a queue.
			IDestination destination = _session.GetQueue(queueName);

			// Create a Producer targetting the selected queue.
			_producer = _session.CreateProducer(destination);

			// You may configure everything to your needs, for instance:
			_producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
		}

		/**
		 * Must call create create producer before this methode
		 */
		public void SendMessage(string messageText)
		{
			// Finally, to send messages:
			ITextMessage message = _session.CreateTextMessage(messageText);
			_producer.Send(message);
		}

		/**
		 * Call this methode after finishing the queue
		 */
		public void CloseSession()
		{
			_session.Close();
			_connection.Close();
		}

		/**
		 * Must call create create producer before this methode
		 */
		public void SendMessagesOnNewLine(string longText)
		{
			// split the text using NewLine as separator
			string[] lines = longText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			// send a message for each line
			foreach (string line in lines)
			{
				SendMessage(line);
			}
		}

		/**
		 * Generate a rand Queue name
		 */
		public static string GenerateRandomQueueName()
		{
			return "GoBike_Queue_" + RandomString(10);
		}

		/**
		 * Create a random numeric string with the given length
		 */
		public static string RandomString(int length)
		{
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[RANDOM.Next(s.Length)]).ToArray());
		}
	}
}

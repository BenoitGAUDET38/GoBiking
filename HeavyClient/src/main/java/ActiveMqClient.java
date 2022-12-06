import javax.jms.*;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import java.util.Hashtable;

public class ActiveMqClient implements javax.jms.MessageListener {
    private javax.jms.Connection connect = null;
    private javax.jms.Session sendSession = null;
    private javax.jms.Session receiveSession = null;
    private javax.jms.MessageProducer sender = null;
    private javax.jms.Queue queue = null;
    InitialContext context = null;

    public void configurer(String queueName) {
        try
        {	// Create a connection
            Hashtable properties = new Hashtable();
            properties.put(Context.INITIAL_CONTEXT_FACTORY,
                    "org.apache.activemq.jndi.ActiveMQInitialContextFactory");
            properties.put(Context.PROVIDER_URL, "tcp://localhost:61616");

            context = new InitialContext(properties);

            javax.jms.ConnectionFactory factory = (ConnectionFactory) context.lookup("ConnectionFactory");
            connect = factory.createConnection();

            this.configurerConsommateur(queueName);
            connect.start(); // on peut activer la connection.
//            System.out.println("CONNECTION STARTED");

        } catch (javax.jms.JMSException jmse){
            jmse.printStackTrace();
        } catch (NamingException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    private void configurerConsommateur(String queueName) throws JMSException, NamingException{
        // Open session
        receiveSession = connect.createSession(false,javax.jms.Session.AUTO_ACKNOWLEDGE);
        Queue queue = (Queue) context.lookup("dynamicQueues/" + queueName);
        javax.jms.MessageConsumer qReceiver = receiveSession.createConsumer(queue);
        // Set a message listener to call onMessage methode when a message is in the queue
        qReceiver.setMessageListener(this);
    }

    // Print message body
    @Override
    public void onMessage(Message message) {
        // Methode permettant au consommateur de consommer effectivement chaque msg recu
        // via la queue
        TextMessage m = (TextMessage) message;
        try {
            System.out.println(m.getText());
        } catch (JMSException e) {
            e.printStackTrace();
        }
    }
}

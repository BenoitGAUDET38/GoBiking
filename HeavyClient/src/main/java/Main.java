import com.soap.ws.client.generated.GoBikeService;
import com.soap.ws.client.generated.IGoBikeService;

import javax.jms.JMSException;

public class Main {
    public static void main(String[] args) throws JMSException {
        GoBikeService goBikeService = new GoBikeService();
        IGoBikeService iGoBikeService = goBikeService.getBasicHttpBindingIGoBikeService();

        String queueName = iGoBikeService.getItinary("3 Place de la République, Mulhouse", "BOULEVARD CHARLES STOESSEL, Mulhouse");
        System.out.println("Read instructions on queue '" + queueName + "' :");

        ActiveMqClient a = new ActiveMqClient();
        a.configurer(queueName);
    }
}

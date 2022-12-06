import com.soap.ws.client.generated.IGoBikeService;

import java.util.Scanner;

public class ClientInterface {

    /**
     * Print the itinerary after asking for origin and destination address
     * @param iGoBikeService
     */
    static void execute(IGoBikeService iGoBikeService) {
        Scanner scanner = new Scanner(System.in);
        String startAddress, destinationAddress;
        boolean firstTurn = true;
        do {
            if (!firstTurn) {
                System.out.println("Error in the addresses, please write them correctly.");
            }

            System.out.print("Enter the start address : ");
            startAddress = scanner.nextLine();
            System.out.print("Enter the destination address : ");
            destinationAddress = scanner.nextLine();
        } while (startAddress.isEmpty() || destinationAddress.isEmpty());

        // Ask for the itinerary
        String queueName = iGoBikeService.getItinary(startAddress, destinationAddress);
        System.out.println("Read instructions on queue '" + queueName + "' :");

        ActiveMqClient a = new ActiveMqClient();
        a.configurer(queueName);
    }
}

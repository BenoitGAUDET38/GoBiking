import com.soap.ws.client.generated.GoBikeService;
import com.soap.ws.client.generated.IGoBikeService;

public class Main {
    public static void main(String[] args) {
        GoBikeService goBikeService = new GoBikeService();
        IGoBikeService iGoBikeService = goBikeService.getBasicHttpBindingIGoBikeService();

        String res = iGoBikeService.getItinary("3 Place de la République, Mulhouse", "BOULEVARD CHARLES STOESSEL, Mulhouse");
        System.out.println(res);
    }
}

public class PlayerUtils {

    public static Player getOpponent (Player player) {
        return player == Player.WHITE ? Player.BLACK : Player.WHITE;
    }
}

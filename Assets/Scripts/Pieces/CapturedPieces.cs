using UnityEngine;

public class CapturedPieces : MonoBehaviour {

    [SerializeField]
    private GameObject[] capturedWhitePiecePositions;
    [SerializeField]
    private GameObject[] capturedBlackPiecePositions;

    public void playerCapturesPiece (Player capturer, PieceController piece) {
        piece.captured = true;
        piece.currentPosition = null;

        if (capturer == Player.WHITE) {
            playerCapturesPiece (capturer, piece, capturedBlackPiecePositions);
        } else {
            playerCapturesPiece (capturer, piece, capturedWhitePiecePositions);
        }
    }

    private void playerCapturesPiece (Player capturer, PieceController piece, GameObject[] capturePiecePositions) {
        foreach (GameObject pos in capturePiecePositions) {
            if (pos.transform.childCount == 0) {
                piece.transform.SetParent (pos.transform);
                piece.transform.position = pos.transform.position;
                break;
            }
        }
    }
}

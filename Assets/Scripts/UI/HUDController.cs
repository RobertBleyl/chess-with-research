using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    [SerializeField]
    private GameObject promoteWhitePiecePanel;
    [SerializeField]
    private GameObject promoteBlackPiecePanel;

    [SerializeField]
    private GameObject whiteQueenPrefab;
    [SerializeField]
    private GameObject whiteKnightPrefab;
    [SerializeField]
    private GameObject whiteBishopPrefab;
    [SerializeField]
    private GameObject whiteRookPrefab;

    [SerializeField]
    private GameObject blackQueenPrefab;
    [SerializeField]
    private GameObject blackKnightPrefab;
    [SerializeField]
    private GameObject blackBishopPrefab;
    [SerializeField]
    private GameObject blackRookPrefab;

    [SerializeField]
    private GameObject endResultTextPanel;
    [SerializeField]
    private Text endResultText;
    [SerializeField]
    private GameObject restartButton;

    private PieceController pieceToBePromoted;

    private void Start () {
        Events.instance.onPromotionInitiated += onPromotionInitiated;
        Events.instance.onCheckMate += onCheckMate;
        Events.instance.onDraw += onDraw;

        promoteWhitePiecePanel.SetActive (false);
        promoteBlackPiecePanel.SetActive (false);

        endResultTextPanel.SetActive (false);
        restartButton.SetActive (false);
    }

    private void onPromotionInitiated (PieceController piece) {
        pieceToBePromoted = piece;

        if (piece.getPlayer () == Player.WHITE) {
            promoteWhitePiecePanel.SetActive (true);
        } else {
            promoteBlackPiecePanel.SetActive (true);
        }
    }

    private void onCheckMate (Player winner) {
        showEndResult (winner + " has won!", winner == Player.WHITE ? Color.white : Color.black);
    }

    private void onDraw () {
        showEndResult ("Draw!", Color.black);
    }

    private void showEndResult (string text, Color color) {
        endResultText.text = text;
        endResultText.color = color;

        endResultTextPanel.SetActive (true);
        restartButton.SetActive (true);
    }

    public void onClickRestart () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    private void spawnPiece (GameObject prefab) {
        GameObject newPiece = Instantiate (prefab);
        newPiece.transform.SetParent (pieceToBePromoted.transform.parent);
        newPiece.transform.position = pieceToBePromoted.transform.position;

        PieceController pieceController = newPiece.GetComponent<PieceController> ();
        pieceController.currentPosition = pieceToBePromoted.currentPosition;
        pieceToBePromoted.currentPosition.currentPiece = pieceController;

        Destroy (pieceToBePromoted.gameObject);

        Events.instance.turnDone (pieceController.getPlayer ());

        promoteWhitePiecePanel.SetActive (false);
        promoteBlackPiecePanel.SetActive (false);

        Events.instance.promotionFinished ();
    }

    public void spawnWhiteQueen () {
        spawnPiece (whiteQueenPrefab);
    }

    public void spawnWhiteKnight () {
        spawnPiece (whiteKnightPrefab);
    }

    public void spawnWhiteBishop () {
        spawnPiece (whiteBishopPrefab);
    }

    public void spawnWhiteRook () {
        spawnPiece (whiteRookPrefab);
    }

    public void spawnBlackQueen () {
        spawnPiece (blackQueenPrefab);
    }

    public void spawnBlackKnight () {
        spawnPiece (blackKnightPrefab);
    }

    public void spawnBlackBishop () {
        spawnPiece (blackBishopPrefab);
    }

    public void spawnBlackRook () {
        spawnPiece (blackRookPrefab);
    }
}

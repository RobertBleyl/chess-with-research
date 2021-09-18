using UnityEngine;

public class PositionController : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer hoverHighlight;
    [SerializeField]
    private SpriteRenderer possibleMovementHighlight;

    public PieceController currentPiece;

    public void showHoverHighlight (bool show) {
        hoverHighlight.gameObject.SetActive (show);
    }

    public void showPossibleMovementHighlight (bool show) {
        possibleMovementHighlight.gameObject.SetActive (show);
    }
}

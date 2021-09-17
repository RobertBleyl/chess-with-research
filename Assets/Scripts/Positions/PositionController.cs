using UnityEngine;

public class PositionController : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer hoverHighlight;
    [SerializeField]
    private SpriteRenderer possibleMovementHighlight;

    public PieceController currentPiece;

    public void showHoverHighlight () {
        hoverHighlight.gameObject.SetActive (true);
    }

    public void hideHoverHighlight () {
        hoverHighlight.gameObject.SetActive (false);
    }

    public void showPossibleMovementHighlight () {
        possibleMovementHighlight.gameObject.SetActive (true);
    }

    public void hidePossibleMovementHighlight () {
        possibleMovementHighlight.gameObject.SetActive (false);
    }
}

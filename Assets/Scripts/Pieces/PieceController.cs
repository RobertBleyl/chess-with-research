using UnityEngine;

public class PieceController : MonoBehaviour {

    [SerializeField]
    private PieceMoveSet moveSet;

    private PickingPieces pickingPieces;
    public bool isPicked;
    private PositionController lastHightlightedPosition;

    private void Start () {
        pickingPieces = GameObject.FindGameObjectWithTag (Tags.Player).GetComponent<PickingPieces> ();
    }

    private void Update () {
        if (isPicked) {
            transform.position = pickingPieces.getMousePos ();
        } else {
            pickingPieces.checkPiece (this);

            if (lastHightlightedPosition != null) {
                lastHightlightedPosition.hideHighlight ();
                lastHightlightedPosition = null;
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (isPicked) {
            PositionController positionController = collider.GetComponent<PositionController> ();

            if (positionController != null) {
                if (lastHightlightedPosition != null) {
                    lastHightlightedPosition.hideHighlight ();
                }

                lastHightlightedPosition = positionController;
                lastHightlightedPosition.showHighlight ();
            }
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (isPicked) {
            PositionController positionController = collider.GetComponent<PositionController> ();

            if (positionController != null) {
                positionController.hideHighlight ();
            }
        }
    }
}

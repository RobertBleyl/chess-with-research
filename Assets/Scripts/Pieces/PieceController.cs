using UnityEngine;
using System.Collections.Generic;

public class PieceController : MonoBehaviour {

    [SerializeField]
    public PieceMoveSet moveSet;

    private PickingPieces pickingPieces;
    private bool isPickedUp;
    private PositionController lastHightlightedHoverPosition;

    public PositionController currentPosition;
    public List<PositionController> possibleMovementPositions = new List<PositionController> ();

    private void Start () {
        GameObject player = GameObject.FindGameObjectWithTag (Tags.Player);
        pickingPieces = player.GetComponent<PickingPieces> ();
    }

    private void Update () {
        if (isPickedUp) {
            transform.position = pickingPieces.getMousePos ();
        } else {
            pickingPieces.checkPiece (this);
        }
    }

    private void removeHoverHighlightFromLastPosition () {
        if (lastHightlightedHoverPosition != null) {
            lastHightlightedHoverPosition.hideHoverHighlight ();
            lastHightlightedHoverPosition = null;
        }
    }

    public void pickUp () {
        isPickedUp = true;

        possibleMovementPositions.ForEach (p => p.showPossibleMovementHighlight ());
    }

    public void release () {
        isPickedUp = false;

        if (lastHightlightedHoverPosition != null && possibleMovementPositions.Contains (lastHightlightedHoverPosition)) {
            currentPosition = lastHightlightedHoverPosition;
        }

        transform.position = currentPosition.transform.position;

        removeHoverHighlightFromLastPosition ();
        possibleMovementPositions.ForEach (p => p.hidePossibleMovementHighlight ());
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        PositionController positionController = collider.GetComponent<PositionController> ();

        if (isPickedUp) {

            if (positionController != null) {
                removeHoverHighlightFromLastPosition ();

                lastHightlightedHoverPosition = positionController;
                lastHightlightedHoverPosition.showHoverHighlight ();
            }
        } else if (currentPosition == null) {
            currentPosition = positionController;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (isPickedUp) {
            PositionController positionController = collider.GetComponent<PositionController> ();

            if (positionController != null) {
                positionController.hideHoverHighlight ();
            }
        }
    }
}

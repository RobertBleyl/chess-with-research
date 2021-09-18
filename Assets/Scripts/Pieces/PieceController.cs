using UnityEngine;
using System.Collections.Generic;
using System;

public class PieceController : MonoBehaviour {

    [SerializeField]
    public PieceMoveSet moveSet;
    [SerializeField]
    private Player player;

    private PickingPieces pickingPieces;
    private CapturedPieces capturedPieces;
    private bool isPickedUp;
    private PositionController lastHightlightedHoverPosition;

    public bool captured;
    public PositionController currentPosition;
    public HashSet<PositionController> possibleMovementPositions = new HashSet<PositionController> ();

    private void Start () {
        GameObject player = GameObject.FindGameObjectWithTag (Tags.Player);
        pickingPieces = player.GetComponent<PickingPieces> ();
        capturedPieces = player.GetComponent<CapturedPieces> ();
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
            lastHightlightedHoverPosition.showHoverHighlight (false);
            lastHightlightedHoverPosition = null;
        }
    }

    public void pickUp () {
        isPickedUp = true;

        showPossibleMovementHighlights (true);
    }

    public void release () {
        isPickedUp = false;
        bool turnDone = false;

        if (lastHightlightedHoverPosition != null && possibleMovementPositions.Contains (lastHightlightedHoverPosition)) {
            currentPosition.currentPiece = null;

            if (lastHightlightedHoverPosition.currentPiece != null) {
                capturedPieces.playerCapturesPiece (player, lastHightlightedHoverPosition.currentPiece);
                lastHightlightedHoverPosition.currentPiece = null;
            }

            currentPosition = lastHightlightedHoverPosition;
            turnDone = true;
        }

        transform.position = currentPosition.transform.position;

        removeHoverHighlightFromLastPosition ();
        showPossibleMovementHighlights (false);

        if (turnDone) {
            Events.instance.turnDone (player);
        }
    }

    private void showPossibleMovementHighlights (bool show) {
        foreach (PositionController positionController in possibleMovementPositions) {
            positionController.showPossibleMovementHighlight (show);
        }
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        PositionController positionController = collider.GetComponent<PositionController> ();

        if (isPickedUp) {
            if (positionController != null && possibleMovementPositions.Contains (positionController)) {
                removeHoverHighlightFromLastPosition ();

                lastHightlightedHoverPosition = positionController;
                lastHightlightedHoverPosition.showHoverHighlight (true);
            }
        } else if (currentPosition == null) {
            currentPosition = positionController;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (isPickedUp) {
            PositionController positionController = collider.GetComponent<PositionController> ();

            if (positionController == lastHightlightedHoverPosition) {
                removeHoverHighlightFromLastPosition ();
            }
        }
    }

    public Player getPlayer () {
        return player;
    }
}

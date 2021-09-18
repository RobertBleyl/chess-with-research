using UnityEngine;
using System.Collections.Generic;

public class PieceController : MonoBehaviour {

    [SerializeField]
    public PieceMoveSet moveSet;
    [SerializeField]
    private Player player;

    private PickingPieces pickingPieces;
    private CapturedPieces capturedPieces;
    private bool isPickedUp;
    private PositionController lastHightlightedHoverPosition;

    public bool quickStartPossible;
    public bool castlePossible;
    public bool captured;
    public PositionController currentPosition;
    public HashSet<PositionController> possibleMovementPositions = new HashSet<PositionController> ();

    private void Awake () {
        quickStartPossible = moveSet.quickStartRank > 0 && moveSet.quickStartRank == transform.position.y;
        castlePossible = moveSet.canInitCastle || moveSet.canBeCastledWith;
    }

    private void Start () {
        GameObject main = GameObject.FindGameObjectWithTag (Tags.Main);
        pickingPieces = main.GetComponent<PickingPieces> ();
        capturedPieces = main.GetComponent<CapturedPieces> ();
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
        bool castledSuccessful = false;

        if (lastHightlightedHoverPosition != null && possibleMovementPositions.Contains (lastHightlightedHoverPosition)) {
            currentPosition.currentPiece = null;

            if (lastHightlightedHoverPosition.currentPiece != null) {
                if (moveSet.canInitCastle && castlePossible && lastHightlightedHoverPosition.currentPiece.castlePossible) {
                    int rookCastlePos = (int)lastHightlightedHoverPosition.currentPiece.transform.position.x == 1 ? 4 : 6;
                    lastHightlightedHoverPosition.currentPiece.transform.position = new Vector3 (rookCastlePos, lastHightlightedHoverPosition.currentPiece.transform.position.y, 0f);
                    lastHightlightedHoverPosition.currentPiece.castlePossible = false;

                    int kingCastlePos = rookCastlePos == 4 ? 3 : 7;
                    transform.position = new Vector3 (kingCastlePos, lastHightlightedHoverPosition.currentPiece.transform.position.y, 0f);

                    castledSuccessful = true;
                } else {
                    capturedPieces.playerCapturesPiece (player, lastHightlightedHoverPosition.currentPiece);

                    GameObject capturedPieceObject = lastHightlightedHoverPosition.currentPiece.gameObject;
                    lastHightlightedHoverPosition.currentPiece = null;

                    Destroy (capturedPieceObject);
                }
            }

            if (!castledSuccessful) {
                currentPosition = lastHightlightedHoverPosition;
            }

            quickStartPossible = false;
            turnDone = true;

            if (currentPosition.transform.position.y == moveSet.promotationRank) {
                Events.instance.promotionInitiated (this);
                turnDone = false;
            }

            castlePossible = false;
        }

        if (!castledSuccessful) {
            transform.position = currentPosition.transform.position;
        }

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

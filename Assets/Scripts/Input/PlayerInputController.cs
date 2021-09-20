using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    [SerializeField]
    private PickingPieces pickingPieces;
    [SerializeField]
    private PossibleMovementCalculator possibleMovementCalculator;

    private InputActions controls;
    private PieceController pickedPiece;

    private bool promotionInProgress;

    private void Awake () {
        controls = new InputActions ();
        controls.Player.PlacePieceOnBoard.performed += ctx => pickPiece ();
        controls.Player.PlacePieceOnBoard.canceled += ctx => releasePiece ();
    }

    private void Start () {
        Events.instance.onPromotionInitiated += onPromotionInitiated;
        Events.instance.onPromotionFinished += onPromotionFinished;
    }

    private void onPromotionInitiated (PieceController piece) {
        promotionInProgress = true;
    }

    private void onPromotionFinished () {
        promotionInProgress = false;
    }

    private void pickPiece () {
        if (promotionInProgress) {
            return;
        }

        if (pickedPiece == null) {
            pickedPiece = pickingPieces.currentNearestPiece;

            if (pickedPiece != null) {
                pickedPiece.pickUp ();
            }
        }
    }

    private void releasePiece () {
        if (promotionInProgress) {
            return;
        }

        if (pickedPiece != null) {
            pickedPiece.release ();
            pickedPiece = null;
        }
    }

    private void OnEnable () {
        controls.Enable ();
    }

    private void OnDisable () {
        controls.Disable ();
    }
}

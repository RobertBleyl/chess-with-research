using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    [SerializeField]
    private PickingPieces pickingPieces;

    private InputActions controls;
    private PieceController pickedPiece;

    private void Awake () {
        controls = new InputActions ();
        controls.Player.PlacePieceOnBoard.performed += ctx => pickPiece ();
        controls.Player.PlacePieceOnBoard.canceled += ctx => releasePiece ();
    }

    private void Start () {

    }

    private void pickPiece () {
        if (pickedPiece == null) {
            pickedPiece = pickingPieces.currentNearestPiece;

            if (pickedPiece != null) {
                pickedPiece.isPicked = true;
            }
        }
    }

    private void releasePiece () {
        if (pickedPiece != null) {
            pickedPiece.isPicked = false;
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

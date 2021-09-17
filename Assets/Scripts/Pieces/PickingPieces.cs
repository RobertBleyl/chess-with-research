using UnityEngine;
using UnityEngine.InputSystem;

public class PickingPieces : MonoBehaviour {

    private Vector3 currentMousePos;

    private float currentNearestDist;
    public PieceController currentNearestPiece;

    private bool wasReset;

    public void checkPiece (PieceController pieceController) {
        if (!wasReset) {
            currentNearestPiece = null;
            wasReset = true;
        }

        float dist = Vector3.Distance (currentMousePos, pieceController.transform.position);

        if (dist <= 0.45f && dist < currentNearestDist) {
            dist = currentNearestDist;
            currentNearestPiece = pieceController;
        }
    }

    public Vector3 getMousePos () {
        Vector2 mousePos2D = Mouse.current.position.ReadValue ();
        Vector3 mousePos = new Vector3 (mousePos2D.x, mousePos2D.y);
        Vector3 pos = Camera.main.ScreenToWorldPoint (mousePos);
        pos.z = 0f;
        return pos;
    }

    private void LateUpdate () {
        currentNearestDist = float.MaxValue;
        currentMousePos = getMousePos ();
        wasReset = false;
    }
}

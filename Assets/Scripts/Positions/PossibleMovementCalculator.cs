using System.Collections.Generic;
using UnityEngine;

public class PossibleMovementCalculator : MonoBehaviour {

    private Dictionary<Vector2, PositionController> positions = new Dictionary<Vector2, PositionController> ();

    private void Start () {
        GameObject[] positionObjects = GameObject.FindGameObjectsWithTag (Tags.Position);

        foreach (GameObject position in positionObjects) {
            Vector2 pos = new Vector2 (position.transform.position.x, position.transform.position.y);
            positions[pos] = position.GetComponent<PositionController> ();
        }

        calculatePossibleMovements ();

        Events.instance.onTurnDone += onTurnDone;
    }

    private void onTurnDone (Player player) {
        calculatePossibleMovements ();
    }

    public void calculatePossibleMovements () {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag (Tags.Piece);

        foreach (GameObject piece in pieces) {
            PieceController pieceController = piece.GetComponent<PieceController> ();
            pieceController.possibleMovementPositions.Clear ();

            if (pieceController.captured) {
                continue;
            }

            Vector2 piecePos = new Vector2 (piece.transform.position.x, piece.transform.position.y);
            positions[piecePos].currentPiece = pieceController;

            foreach (PieceMoveSetItem item in pieceController.moveSet.items) {
                if (item.move.Length == 2) {
                    // Knight movement
                    Vector2 endLocation = piecePos;

                    foreach (Vector2 direction in item.move) {
                        endLocation += direction;
                    }

                    addPossiblePosition (pieceController, endLocation);
                } else {
                    // Everything else's movement
                    Vector2 direction = item.move[0];
                    float max = Mathf.Max (direction.x, direction.y);
                    Vector2 normDir = direction.normalized;

                    Vector2 endLocation = piecePos;

                    for (int i = 1; i <= direction.magnitude; i++) {
                        endLocation += new Vector2 (normDir.x, normDir.y);
                        endLocation.x = Mathf.Round (endLocation.x);
                        endLocation.y = Mathf.Round (endLocation.y);

                        if (endLocation.x < 1 || endLocation.x > 8 || endLocation.y < 1 || endLocation.y > 8 || !addPossiblePosition (pieceController, endLocation)) {
                            break;
                        }
                    }
                }
            }
        }
    }

    private bool addPossiblePosition (PieceController pieceController, Vector2 endLocation) {
        PositionController endPosition;
        positions.TryGetValue (endLocation, out endPosition);

        if (endPosition != null && endPosition.currentPiece?.getPlayer () != pieceController.getPlayer ()) {
            pieceController.possibleMovementPositions.Add (endPosition);
            return endPosition.currentPiece == null;
        }

        return false;
    }
}

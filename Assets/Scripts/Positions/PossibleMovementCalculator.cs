using System;
using System.Collections.Generic;
using UnityEngine;

public class PossibleMovementCalculator : MonoBehaviour {

    private Dictionary<string, PositionController> positions = new Dictionary<string, PositionController> ();

    private void Start () {
        GameObject[] positionObjects = GameObject.FindGameObjectsWithTag (Tags.Position);

        foreach (GameObject position in positionObjects) {
            positions[toKey (position.transform.position)] = position.GetComponent<PositionController> ();
        }

        calculatePossibleMovements ();

        Events.instance.onTurnDone += onTurnDone;
    }

    private string toKey (Vector2 value) {
        int x = (int)value.x;
        int y = (int)value.y;
        return x + "_" + y;
    }

    private PositionController getPos (Vector2 location) {
        PositionController position;
        positions.TryGetValue (toKey (location), out position);
        return position;
    }

    private void onTurnDone (Player player) {
        calculatePossibleMovements ();
    }

    public void calculatePossibleMovements () {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag (Tags.Piece);

        List<PieceController> movablePieces = new List<PieceController> ();

        foreach (GameObject pieceObject in pieces) {
            PieceController piece = pieceObject.GetComponent<PieceController> ();

            if (piece.captured) {
                continue;
            }

            PositionController pos = getPos (piece.transform.position);
            pos.currentPiece = piece;
            piece.currentPosition = pos;

            movablePieces.Add (piece);
        }

        foreach (PieceController piece in movablePieces) {
            piece.possibleMovementPositions.Clear ();

            if (piece.captured) {
                continue;
            }

            PieceMoveSet moveSet = piece.moveSet;

            foreach (PieceMoveSetItem item in moveSet.items) {
                if (item.move.Length == 2) {
                    // Knight movement
                    Vector2 endLocation = piece.transform.position;

                    foreach (Vector2 direction in item.move) {
                        endLocation += direction;
                    }

                    addPossiblePosition (piece, endLocation, false);
                } else {
                    // Everything else's movement
                    Vector2 direction = item.move[0];
                    Vector2 normDir = direction.normalized;
                    normDir.x = Mathf.Round (normDir.x);
                    normDir.y = Mathf.Round (normDir.y);

                    Vector2 endLocation = piece.transform.position;

                    for (int i = 1; i <= direction.magnitude; i++) {
                        endLocation += new Vector2 (normDir.x, normDir.y);

                        if (!addPossiblePosition (piece, endLocation, moveSet.hasDiagonalCapture)) {
                            break;
                        }
                    }

                    if (piece.quickStartPossible) {
                        addPossiblePosition (piece, endLocation + direction, moveSet.hasDiagonalCapture);
                    }

                    if (moveSet.hasDiagonalCapture) {
                        checkDiagonalCaptureLocation (piece, endLocation + Vector2.left);
                        checkDiagonalCaptureLocation (piece, endLocation + Vector2.right);
                    }

                    if (moveSet.canInitCastle && piece.castlePossible) {
                        checkCastling (piece, Vector2.left);
                        checkCastling (piece, Vector2.right);
                    }
                }
            }
        }
    }

    private void checkCastling (PieceController piece, Vector2 direction) {
        Vector2 currentLocation = piece.transform.position;
        currentLocation += direction;

        while (currentLocation.x >= 1 && currentLocation.x <= 8) {
            PositionController pos = getPos (currentLocation);
            PieceController currentPiece = pos.currentPiece;

            if (currentPiece != null) {
                if (currentPiece.getPlayer () == piece.getPlayer () && currentPiece.castlePossible) {
                    piece.possibleMovementPositions.Add (pos);
                }

                break;
            }

            currentLocation += direction;
        }
    }

    private void checkDiagonalCaptureLocation (PieceController piece, Vector2 endLocation) {
        PositionController endPosition = getPos (endLocation);

        if (endPosition != null && endPosition.currentPiece != null && endPosition.currentPiece.getPlayer () != piece.getPlayer ()) {
            piece.possibleMovementPositions.Add (endPosition);
        }
    }

    private bool addPossiblePosition (PieceController piece, Vector2 endLocation, bool hasDiagonalCapture) {
        if (endLocation.x < 1 || endLocation.x > 8 || endLocation.y < 1 || endLocation.y > 8) {
            return false;
        }

        PositionController endPosition = getPos (endLocation);

        if (endPosition != null && (endPosition.currentPiece == null || (!hasDiagonalCapture && endPosition.currentPiece.getPlayer () != piece.getPlayer ()))) {
            piece.possibleMovementPositions.Add (endPosition);
            return endPosition.currentPiece == null;
        }

        return false;
    }
}

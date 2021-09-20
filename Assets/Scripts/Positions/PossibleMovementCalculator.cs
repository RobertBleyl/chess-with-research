using System.Collections.Generic;
using UnityEngine;

public class PossibleMovementCalculator {

    private readonly PositionsContainer positions;
    private readonly CheckCalculator checkCalculator;
    private readonly PieceController piece;

    private readonly HashSet<PositionController> possiblePositions = new HashSet<PositionController> ();

    public PossibleMovementCalculator (PositionsContainer positions, CheckCalculator checkCalculator, PieceController piece) {
        this.positions = positions;
        this.checkCalculator = checkCalculator;
        this.piece = piece;
    }

    public PositionCalculationResult calculatePossibleMovements () {
        PieceMoveSet moveSet = piece.moveSet;

        foreach (PieceMoveSetItem item in moveSet.items) {
            if (item.move.Length == 2) {
                checkKnightMovements (item);
            } else {
                Vector2 direction = item.move[0];
                Vector2 normDir = direction.normalized;
                normDir.x = Mathf.Round (normDir.x);
                normDir.y = Mathf.Round (normDir.y);

                Vector2 endLocation = piece.transform.position;

                for (int i = 1; i <= direction.magnitude; i++) {
                    endLocation += new Vector2 (normDir.x, normDir.y);

                    if (!addPossiblePosition (endLocation, moveSet.hasDiagonalCapture)) {
                        break;
                    }
                }

                if (piece.quickStartPossible) {
                    addPossiblePosition (endLocation + direction, moveSet.hasDiagonalCapture);
                }

                if (moveSet.hasDiagonalCapture) {
                    checkDiagonalCapture (endLocation + Vector2.left);
                    checkDiagonalCapture (endLocation + Vector2.right);
                }
            }
        }

        checkCastling ();

        return new PositionCalculationResult (possiblePositions);
    }

    private void checkKnightMovements (PieceMoveSetItem item) {
        Vector2 endLocation = piece.transform.position;

        foreach (Vector2 direction in item.move) {
            endLocation += direction;
        }

        addPossiblePosition (endLocation, false);
    }

    private void checkDiagonalCapture (Vector2 endLocation) {
        PositionController endPosition = positions.getPosition (endLocation);

        if (endPosition != null && endPosition.currentPiece != null && endPosition.currentPiece.getPlayer () != piece.getPlayer ()) {
            if (checkCalculator != null && checkCalculator.moveWouldResultInOwnCheck (piece, endPosition)) {
                return;
            }

            possiblePositions.Add (endPosition);
        }
    }

    private void checkCastling () {
        PieceMoveSet moveSet = piece.moveSet;

        if (moveSet.canInitCastle && piece.castlePossible) {
            if (checkCalculator != null && checkCalculator.positionIsInCheck (piece.currentPosition)) {
                return;
            }

            checkCastling (piece, Vector2.left);
            checkCastling (piece, Vector2.right);
        }
    }

    private void checkCastling (PieceController piece, Vector2 direction) {
        Vector2 currentLocation = piece.transform.position;
        currentLocation += direction;

        while (currentLocation.x >= 1 && currentLocation.x <= 8) {
            PositionController pos = positions.getPosition (currentLocation);
            PieceController currentPiece = pos.currentPiece;

            if (checkCalculator != null && checkCalculator.positionIsInCheck (pos)) {
                break;
            }

            if (currentPiece != null) {
                if (currentPiece.getPlayer () == piece.getPlayer () && currentPiece.castlePossible) {
                    possiblePositions.Add (pos);
                }

                break;
            }

            currentLocation += direction;
        }
    }

    private bool addPossiblePosition (Vector2 endLocation, bool hasDiagonalCapture) {
        if (endLocation.x < 1 || endLocation.x > 8 || endLocation.y < 1 || endLocation.y > 8) {
            return false;
        }

        PositionController endPosition = positions.getPosition (endLocation);

        if (endPosition != null && (endPosition.currentPiece == null || (!hasDiagonalCapture && endPosition.currentPiece.getPlayer () != piece.getPlayer ()))) {
            if (checkCalculator != null && checkCalculator.moveWouldResultInOwnCheck (piece, endPosition)) {
                return false;
            }

            possiblePositions.Add (endPosition);
            return endPosition.currentPiece == null;
        }

        return false;
    }
}

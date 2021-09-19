using System.Collections.Generic;
using UnityEngine;

public class CheckCalculator {

    private readonly PositionsContainer positions;
    private readonly List<PieceController> pieces;
    private readonly Player opponent;

    private readonly PieceController king;

    public CheckCalculator (PositionsContainer positions, List<PieceController> pieces, Player opponent) {
        this.positions = positions;
        this.pieces = pieces;
        this.opponent = opponent;

        foreach (PieceController piece in pieces) {
            if (piece.getPlayer () != opponent && piece.moveSet.canInitCastle) {
                king = piece;
                break;
            }
        }
    }

    public bool positionIsInCheck (PositionController pos) {
        foreach (PieceController piece in pieces) {
            if (piece.getPlayer () == opponent && piece.possibleMovementPositions.Contains (pos)) {
                return true;
            }
        }

        return false;
    }

    public bool moveWouldResultInOwnCheck (PieceController pieceToMove, PositionController posToMoveTo) {
        Vector3 originalPiecePos = pieceToMove.transform.position;
        pieceToMove.transform.position = posToMoveTo.transform.position;

        PositionController originalPos = pieceToMove.currentPosition;
        pieceToMove.currentPosition = posToMoveTo;

        bool checkFound = false;

        foreach (PieceController piece in pieces) {
            if (piece.getPlayer () != opponent) {
                continue;
            }

            PossibleMovementCalculator calculator = new PossibleMovementCalculator (positions, null, piece);
            PositionCalculationResult result = calculator.calculatePossibleMovements ();

            if (result.getPossibleMovementPositions ().Contains (king.currentPosition)) {
                checkFound = true;
                break;
            }
        }

        pieceToMove.transform.position = originalPiecePos;
        pieceToMove.currentPosition = originalPos;

        return checkFound;
    }
}

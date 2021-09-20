using System.Collections.Generic;
using UnityEngine;

public class PossibleMovements : MonoBehaviour {

    private PositionsContainer positions;

    private void Start () {
        positions = new PositionsContainer ();

        Events.instance.onTurnDone += onTurnDone;

        onTurnDone (Player.BLACK);
    }

    private void onTurnDone (Player playerWhoMadeTurn) {
        Dictionary<Player, List<PieceController>> playerToPiecesMap = initPieces ();

        calculatePossibleMovements (playerToPiecesMap[playerWhoMadeTurn], null);

        Player nextPlayer = PlayerUtils.getOpponent (playerWhoMadeTurn);

        List<PieceController> allPieces = new List<PieceController> (playerToPiecesMap[playerWhoMadeTurn]);
        allPieces.AddRange (playerToPiecesMap[nextPlayer]);

        calculatePossibleMovements (playerToPiecesMap[nextPlayer], new CheckCalculator (positions, allPieces, playerWhoMadeTurn));
    }

    private Dictionary<Player, List<PieceController>> initPieces () {
        GameObject[] pieceObjects = GameObject.FindGameObjectsWithTag (Tags.Piece);
        Dictionary<Player, List<PieceController>> pieces = new Dictionary<Player, List<PieceController>> ();

        foreach (GameObject pieceObject in pieceObjects) {
            PieceController piece = pieceObject.GetComponent<PieceController> ();

            PositionController pos = positions.getPosition (piece);
            pos.currentPiece = piece;
            piece.currentPosition = pos;

            List<PieceController> pieceList;
            pieces.TryGetValue (piece.getPlayer (), out pieceList);

            if (pieceList == null) {
                pieceList = new List<PieceController> ();
                pieces[piece.getPlayer ()] = pieceList;
            }

            pieceList.Add (piece);
        }

        return pieces;
    }

    private void calculatePossibleMovements (List<PieceController> pieces, CheckCalculator checkCalculator) {
        foreach (PieceController piece in pieces) {
            PossibleMovementCalculator possibleMovementCalculator = new PossibleMovementCalculator (positions, checkCalculator, piece);

            PositionCalculationResult result = possibleMovementCalculator.calculatePossibleMovements ();

            piece.possibleMovementPositions = result.getPossibleMovementPositions ();
        }
    }
}

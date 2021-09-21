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
        Player nextPlayer = PlayerUtils.getOpponent (playerWhoMadeTurn);
        Dictionary<Player, List<PieceController>> playerToPiecesMap = initPieces ();

        List<PieceController> piecesOfPlayerWhoMadeTurn = playerToPiecesMap[playerWhoMadeTurn];
        List<PieceController> piecesOfNextPlayer = playerToPiecesMap[nextPlayer];

        calculatePossibleMovements (piecesOfPlayerWhoMadeTurn, null);


        List<PieceController> allPieces = new List<PieceController> (piecesOfPlayerWhoMadeTurn);
        allPieces.AddRange (piecesOfNextPlayer);

        calculatePossibleMovements (piecesOfNextPlayer, new CheckCalculator (positions, allPieces, playerWhoMadeTurn));

        detectCheckMate (piecesOfNextPlayer, piecesOfPlayerWhoMadeTurn, playerWhoMadeTurn);
    }

    private Dictionary<Player, List<PieceController>> initPieces () {
        GameObject[] pieceObjects = GameObject.FindGameObjectsWithTag (Tags.Piece);
        Dictionary<Player, List<PieceController>> pieces = new Dictionary<Player, List<PieceController>> ();

        foreach (GameObject pieceObject in pieceObjects) {
            PieceController piece = pieceObject.GetComponent<PieceController> ();
            int x = (int)piece.transform.position.x;
            int y = (int)piece.transform.position.y;
            piece.transform.position = new Vector3 (x, y, 0f);

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

    private void detectCheckMate (List<PieceController> ownPieces, List<PieceController> opponentPieces, Player playerWhoMadeTurn) {
        PieceController pieceWithMoves = ownPieces.Find (p => p.possibleMovementPositions.Count > 0);

        if (pieceWithMoves == null) {
            PieceController king = ownPieces.Find (p => p.moveSet.checkMateTarget);

            if (opponentPieces.Find (p => p.possibleMovementPositions.Contains (king.currentPosition)) != null) {
                Events.instance.checkMate (playerWhoMadeTurn);
            } else {
                Events.instance.draw ();
            }
        }
    }

    public PositionController getPosition (Vector2 location) {
        return positions.getPosition (location);
    }
}

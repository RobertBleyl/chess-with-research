using System.Collections.Generic;
using UnityEngine;

public class PossibleMovements : MonoBehaviour {

    private PositionsContainer positions;

    private void Start () {
        positions = new PositionsContainer ();

        Events.instance.onTurnDone += onTurnDone;
    }

    private void onTurnDone (Player player) {

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
}

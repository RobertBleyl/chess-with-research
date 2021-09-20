using UnityEngine;

[CreateAssetMenu (fileName = "PieceMoveSet", menuName = "PieceMoveSet")]
public class PieceMoveSet : ScriptableObject {

    [SerializeField]
    public PieceMoveSetItem[] items;
    [SerializeField]
    public int quickStartRank;
    [SerializeField]
    public bool hasDiagonalCapture;
    [SerializeField]
    public int promotationRank;
    [SerializeField]
    public bool canInitCastle;
    [SerializeField]
    public bool canBeCastledWith;
    [SerializeField]
    public bool checkMateTarget;
}

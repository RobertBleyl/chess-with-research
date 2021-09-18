using UnityEngine;

[CreateAssetMenu (fileName = "PieceMoveSet", menuName = "PieceMoveSet")]
public class PieceMoveSet : ScriptableObject {

    [SerializeField]
    public PieceMoveSetItem[] items;
    [SerializeField]
    public bool hasQuickStart;
    [SerializeField]
    public bool hasDiagonalCapture;
}

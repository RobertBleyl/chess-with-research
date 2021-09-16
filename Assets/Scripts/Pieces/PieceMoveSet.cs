using UnityEngine;

[CreateAssetMenu (fileName = "PieceMoveSet", menuName = "PieceMoveSet")]
public class PieceMoveSet : ScriptableObject {

    [SerializeField]
    public PieceMoveSetItem[] items;
}

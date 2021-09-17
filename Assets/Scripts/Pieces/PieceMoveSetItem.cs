using UnityEngine;

[CreateAssetMenu (fileName = "PieceMoveSetItem", menuName = "PieceMoveSetItem")]
public class PieceMoveSetItem : ScriptableObject {

    [SerializeField]
    public Vector2[] move;
}

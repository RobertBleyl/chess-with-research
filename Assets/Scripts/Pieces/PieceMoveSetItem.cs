using UnityEngine;

[CreateAssetMenu (fileName = "PieceMoveSetItem", menuName = "PieceMoveSetItem")]
public class PieceMoveSetItem : ScriptableObject {

    [SerializeField]
    public Vector3[] move;
}

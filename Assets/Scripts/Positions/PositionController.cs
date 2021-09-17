using UnityEngine;

public class PositionController : MonoBehaviour {

    [SerializeField]
    private string positionName;
    [SerializeField]
    private SpriteRenderer highlight;

    public void showHighlight () {
        highlight.gameObject.SetActive (true);
    }

    public void hideHighlight () {
        highlight.gameObject.SetActive (false);
    }
}

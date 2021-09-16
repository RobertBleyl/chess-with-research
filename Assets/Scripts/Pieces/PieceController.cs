using UnityEngine;
using UnityEngine.InputSystem;

public class PieceController : MonoBehaviour {

    [SerializeField]
    private PieceMoveSet moveSet;

    private bool isPickedUp;

    private InputActions inputActions;

    private void Start () {
        inputActions = new InputActions ();
        inputActions.StandardInput.PlacePieceOnBoard.performed += ctx => placePieceOnBoard ();
        Debug.Log ("start");
    }

    private void placePieceOnBoard () {
        Debug.Log ("clicked");
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        Debug.Log (collider);
    }

    private void Update () {
        if (isPickedUp) {
            transform.position = getMousePos ();
        }
    }

    private Vector3 getMousePos () {
        Vector2 mousePos2D = Mouse.current.position.ReadValue ();
        Vector3 mousePos = new Vector3 (mousePos2D.x, mousePos2D.y);
        return Camera.main.ScreenToWorldPoint (mousePos);
    }
}

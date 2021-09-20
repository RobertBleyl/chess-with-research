using UnityEngine;
using System;

public class Events : MonoBehaviour {

    public static Events instance;

    private void Awake () {
        instance = this;
    }

    public event Action<Player> onTurnDone;

    public event Action<PieceController> onPromotionInitiated;

    public event Action onPromotionFinished;

    public void turnDone (Player playerWhoMadeTurn) {
        if (onTurnDone != null) {
            onTurnDone (playerWhoMadeTurn);
        }
    }

    public void promotionInitiated (PieceController piece) {
        if (onPromotionInitiated != null) {
            onPromotionInitiated (piece);
        }
    }

    public void promotionFinished () {
        if (onPromotionFinished != null) {
            onPromotionFinished ();
        }
    }
}

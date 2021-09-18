using UnityEngine;
using System;

public class Events : MonoBehaviour {

    public static Events instance;

    private void Awake () {
        instance = this;
    }

    public event Action<Player> onTurnDone;

    public void turnDone (Player player) {
        if (onTurnDone != null) {
            onTurnDone (player);
        }
    }
}

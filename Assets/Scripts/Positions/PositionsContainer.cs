using System.Collections.Generic;
using UnityEngine;

public class PositionsContainer {

    private readonly Dictionary<string, PositionController> positions = new Dictionary<string, PositionController> ();

    public PositionsContainer () {
        GameObject[] positionObjects = GameObject.FindGameObjectsWithTag (Tags.Position);

        foreach (GameObject position in positionObjects) {
            PositionController positionController = position.GetComponent<PositionController> ();
            positions[getPosKey (positionController.transform.position)] = positionController;
        }
    }

    private string getPosKey (Vector2 location) {
        int x = (int)location.x;
        int y = (int)location.y;
        return x + "_" + y;
    }

    public PositionController getPosition (MonoBehaviour monoBehaviour) {
        return getPosition (monoBehaviour.transform.position);
    }

    public PositionController getPosition (GameObject gameObject) {
        return getPosition (gameObject.transform.position);
    }

    public PositionController getPosition (Vector2 location) {
        PositionController result;
        positions.TryGetValue (getPosKey (location), out result);
        return result;
    }
}

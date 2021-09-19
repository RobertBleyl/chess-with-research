using System.Collections.Generic;

public class PositionCalculationResult {

    private readonly HashSet<PositionController> possibleMovementPositions;

    public PositionCalculationResult (HashSet<PositionController> possibleMovementPositions) {
        this.possibleMovementPositions = possibleMovementPositions;
    }

    public HashSet<PositionController> getPossibleMovementPositions () {
        return possibleMovementPositions;
    }
}

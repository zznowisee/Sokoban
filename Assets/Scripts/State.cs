using UnityEngine;

public enum EditorState
{
    EDITING = 0,
    RUNNING,
    REPLAYING
}

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum EntityMovable
{
    MOVABLE,
    UNMOVABLE
}

public enum TeleportPointState
{
    AVAILABLE,
    UNAVAILABLE,
    NULL
}

public enum EntityType
{
    WALL,
    PLAYER,
    BOX,
    CHECKPOINT
}

public static class DirectionExtension
{
    public static Vector2Int GetValue(this Direction direction)
    {
        switch (direction)
        {
            default:
            case Direction.DOWN: return Vector2Int.down;
            case Direction.UP: return Vector2Int.up;
            case Direction.RIGHT: return Vector2Int.right;
            case Direction.LEFT: return Vector2Int.left;
        }
    }

    public static Direction Opposite(this Direction direction)
    {
        switch (direction)
        {
            default:
            case Direction.DOWN: return Direction.UP;
            case Direction.UP: return Direction.DOWN;
            case Direction.RIGHT: return Direction.LEFT;
            case Direction.LEFT: return Direction.RIGHT;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public bool Active
    {
        get { return state != TeleportPointState.NULL; }
    }

    private MapUnit currentUnit;
    private TeleportPointState state;

    public void Setup()
    {
        state = TeleportPointState.NULL;
    }

    public void SetUnit(MapUnit newUnit)
    {
        currentUnit = newUnit;
    }
}

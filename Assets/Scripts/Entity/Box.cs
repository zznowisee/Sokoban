using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MovableEntity
{

    public override void Setup(MapUnit initUnit)
    {
        base.Setup(initUnit);
        entityType = EntityType.BOX;
    }

    public override bool CanMove(Direction direction)
    {
        MapUnit targetUnit = MapManager.Instance[direction.GetValue() + Index];
        if(targetUnit != null)
        {
            if (targetUnit.IsEmpty)
            {
                return true;
            }
        }
        return false;
    }

}

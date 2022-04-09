using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmovableEntity : Entity
{
    public override void Setup(MapUnit initUnit)
    {
        base.Setup(initUnit);
        entityMovable = EntityMovable.UNMOVABLE;
    }
}

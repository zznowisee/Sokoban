using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovableEntity : Entity
{
    protected bool isMoving = false;
    [SerializeField] protected float moveTime = .2f;
    public virtual event Action OnMoveEnd;
    public override void Setup(MapUnit initUnit)
    {
        base.Setup(initUnit);
        entityMovable = EntityMovable.MOVABLE;
    }

    protected IEnumerator SmoothMove(MapUnit targetUnit)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = targetUnit.transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
        OnMoveEnd?.Invoke();
        Teleport(targetUnit);
    }

    public virtual void MoveTo(Direction direction)
    {
        if (CanMove(direction))
        {
            MapUnit unit = MapManager.Instance[direction.GetValue() + Index];
            StartCoroutine(SmoothMove(unit));
        }
    }
    public virtual bool CanMove(Direction direction) { return true; }
}

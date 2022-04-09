using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableEntity
{
    [SerializeField] private TeleportPoint pfTeleportPoint;
    private int recordMaxNum = 4;
    private int currentRecordNum = 0;
    private List<TeleportPoint> teleportPoints;
    private void Awake()
    {
        teleportPoints = new List<TeleportPoint>();
        OnMoveEnd += TeleportPointRecord;
    }

    private void Update()
    {
        if (!EditorManager.TestingLevel || isMoving)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) MoveTo(Direction.UP);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) MoveTo(Direction.DOWN);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) MoveTo(Direction.LEFT);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) MoveTo(Direction.RIGHT);
    }

    public override void Setup(MapUnit initUnit)
    {
        base.Setup(initUnit);
        entityType = EntityType.PLAYER;
    }

    public override bool CanMove(Direction direction)
    {
        MapUnit targetUnit = MapManager.Instance[direction.GetValue() + Index];
        if (targetUnit != null)
        {
            if (targetUnit.IsEmpty)
            {
                return true;
            }
            else
            {
                if (targetUnit.entity.Movable)
                {
                    MovableEntity movableEntity = targetUnit.entity as MovableEntity;
                    if (movableEntity.CanMove(direction))
                    {
                        movableEntity.MoveTo(direction);
                        return true;
                    }
                }
                return false;
            }
        }

        return false;
    }

    private void TeleportPointRecord()
    {
        if(currentRecordNum < recordMaxNum)
        {
            TeleportPoint tp = Instantiate(pfTeleportPoint, currentUnit.transform.position, Quaternion.identity);
            teleportPoints.Add(tp);
            currentRecordNum++;
        }
        else
        {
            teleportPoints[teleportPoints.Count - 1].transform.position = currentUnit.transform.position;
            teleportPoints.Insert(0, teleportPoints[teleportPoints.Count - 1]);
            teleportPoints.RemoveAt(teleportPoints.Count - 1);
        }
    }
}

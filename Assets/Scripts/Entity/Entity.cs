using UnityEngine;

public class Entity : MonoBehaviour, ISelectable
{
    public Vector2Int Index
    {
        get { return currentUnit.Index; }
    }

    public bool Movable
    {
        get { return entityMovable == EntityMovable.MOVABLE; }
    }

    protected MapUnit currentUnit;
    protected EntityType entityType;
    protected EntityMovable entityMovable;
    public virtual void Setup(MapUnit initUnit)
    {
        currentUnit = initUnit;
        currentUnit.entity = this;
        transform.position = initUnit.transform.position;
    }

    protected void OnEnable() => EditorManager.Instance.buildEntitiesList.Add(this);
    protected void OnDisable() => EditorManager.Instance.buildEntitiesList.Remove(this);

    protected void Teleport(MapUnit mapUnit)
    {
        if (currentUnit.entity == this)
        {
            currentUnit.entity = null;
        }
        currentUnit = mapUnit;
        currentUnit.entity = this;
        transform.position = currentUnit.transform.position;
    }

    public void Editor_CancelBuild()
    {
        if(currentUnit.entity == this)
            currentUnit.entity = null;
        Destroy(gameObject);
    }

    public void LeftClick()
    {

    }

    public void LeftRelease()
    {

    }

    public void RightClick()
    {
        Editor_CancelBuild();
    }

    public void Dragging()
    {
        MapUnit mapUnit = InputHelper.GetUnitUnderMousePosition();
        if (mapUnit == null)
            return;
        if (!mapUnit.IsEmpty || mapUnit == currentUnit)
            return;

        Teleport(mapUnit);
    }
}
    
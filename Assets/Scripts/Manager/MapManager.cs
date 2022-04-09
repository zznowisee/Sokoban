using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager Instance { get; private set; }

    [SerializeField] private Color unitDefaultCol;
    [SerializeField] private Color unitHighlightCol;
    [SerializeField] private float slotOutlinePercent = 0.05f;
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private float cellSize;
    [SerializeField] private MapUnit pfMapUnit;

    private Grid<GridSlot> grid;
    private MapUnit[,] mapUnits;
    public MapUnit this[Vector2Int index] => GetUnit(index.x, index.y);
    private void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {
        string holderName = "SlotHolder";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        Transform holder = new GameObject(holderName).transform;
        holder.transform.parent = transform;

        Vector2 origin = new Vector2((mapSize.x - 1) * cellSize, (mapSize.y - 1) * cellSize) / -2f;
        grid = new Grid<GridSlot>(mapSize.x, mapSize.y, cellSize, origin, (int x, int y) => new GridSlot(x, y));
        mapUnits = new MapUnit[mapSize.x, mapSize.y];

        for (int y = 0; y < grid.GetHeight(); y++)
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                MapUnit unit = Instantiate(pfMapUnit, grid.GetCenterPosition(x, y), Quaternion.identity, holder);
                unit.transform.localScale = Vector3.one * (1 - slotOutlinePercent) * cellSize;
                unit.Setup(new Vector2Int(x, y), unitDefaultCol, unitHighlightCol);
                mapUnits[x, y] = unit;
            }
        }
    }

    private MapUnit GetUnit(int x, int y)
    {
        if (x >= 0 && x < mapUnits.GetLength(0) && y >= 0 && y < mapUnits.GetLength(1))
        {
            return mapUnits[x, y];
        }
        return null;
    }

    public class GridSlot
    {
        public Vector2Int index;
        public GridSlot(int x, int y)
        {
            index = new Vector2Int(x, y);
        }
    }
}

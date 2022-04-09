using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit : MonoBehaviour
{
    public Vector2Int Index
    {
        get { return index; }
    }

    public bool IsEmpty => entity == null;

    private Color defaultCol;
    private Color highlightCol;
    private Vector2Int index;
    private SpriteRenderer spriteRenderer;
    public Entity entity;

    public void Setup(Vector2Int index_, Color defaultCol_, Color highlightCol_)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = index_;
        defaultCol = defaultCol_;
        highlightCol = highlightCol_;
        spriteRenderer.color = defaultCol_;
    }
}

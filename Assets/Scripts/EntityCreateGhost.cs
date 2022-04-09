using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCreateGhost : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Setup(EntitySO entitySO)
    {
        spriteRenderer.sprite = entitySO.sprite;
    }

    public void Hide()
    {
        spriteRenderer.gameObject.SetActive(false);
    }

    public void Show()
    {
        spriteRenderer.gameObject.SetActive(true);
    }
}

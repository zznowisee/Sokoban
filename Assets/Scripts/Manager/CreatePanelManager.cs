using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanelManager : MonoBehaviour
{

    private RectTransform canvasRectTransform;
    [SerializeField] private float buttonWidth = 180f;
    [SerializeField] private float buttonHeight = 30f;
    private RectTransform createPanel;
    private EditorManager processManager;
    [SerializeField] private Color buttonHighlightCol;
    [SerializeField] private EntityCreateButton pfEntityCreateButton;
    [SerializeField] private List<EntitySO> entitySOList;

    private void Awake()
    {
        canvasRectTransform = GetComponent<RectTransform>();
        processManager = GetComponent<EditorManager>();
        createPanel = transform.Find("entityCreatePanel") as RectTransform;
        for (int i = 0; i < entitySOList.Count; i++)
        {
            EntityCreateButton entityCreateButton = Instantiate(pfEntityCreateButton, createPanel);
            RectTransform rectTransform = entityCreateButton.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);
            entityCreateButton.Setup(this, entitySOList[i], buttonHighlightCol);
        }
        createPanel.sizeDelta = new Vector2(buttonWidth + 2 * 10f, (buttonHeight + 10f) * entitySOList.Count + 10f);
        createPanel.gameObject.SetActive(false);
    }

    public void SetCurrentEntitySO(EntitySO entitySO)
    {
        processManager.SetCurrentTrackingEntitySO(entitySO);
        DisableCreatePanel();
    }

    public void EnableCreatePanel()
    {
        //processManager.DisableCreateGhost();
        createPanel.gameObject.SetActive(true);
        Vector2 panelPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        Vector2 anchorPosition = new Vector2(0, 1);
        anchorPosition.x = panelPosition.x + createPanel.rect.width > canvasRectTransform.rect.width ? 1 : 0;
        anchorPosition.y = panelPosition.y - createPanel.rect.height < 0 ? 0 : 1;
        createPanel.pivot = anchorPosition;
        createPanel.position = Input.mousePosition;
    }

    public void DisableCreatePanel()
    {
        createPanel.gameObject.SetActive(false);
    }
}

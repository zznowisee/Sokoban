using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EntityCreateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CreatePanelManager buttonManager;
    private Color highlightCol;
    private EntitySO entitySO;
    private Button createButton;
    private Image image;
    private TextMeshProUGUI text;

    public void Setup(CreatePanelManager manager_, EntitySO entitySO_, Color highlightCol_)
    {
        createButton = GetComponent<Button>();
        text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>();
        buttonManager = manager_;
        entitySO = entitySO_;
        text.text = entitySO.name;
        highlightCol = highlightCol_;
        createButton.onClick.AddListener(() => 
        {
            buttonManager.SetCurrentEntitySO(entitySO);
            Normal();
        });
    }

    public void Highlight()
    {
        image.color = highlightCol;
    }

    public void Normal()
    {
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightCol;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}

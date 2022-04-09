using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : Singleton<EditorManager>
{
    public enum OperateState { IDLE = 0, DRAGGING, CREATING }

    public static bool TestingLevel
    {
        get { return editorState == EditorState.RUNNING; }
    }

    [HideInInspector] public List<Entity> buildEntitiesList;
    [SerializeField] private EntityCreateGhost pfEntityCreateGhost;
    [SerializeField] private OperateState operateState;
    public static EditorState editorState;
    private CreatePanelManager createPanel;
    private EntityCreateGhost createGhost;
    private EntitySO entitySO;
    private ISelectable currentSelection;

    protected override void Awake()
    {
        base.Awake();
        buildEntitiesList = new List<Entity>();
        createPanel = GetComponent<CreatePanelManager>();
        createGhost = Instantiate(pfEntityCreateGhost);
        createGhost.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (editorState)
        {
            case EditorState.EDITING: HandleEdit(); break;
            case EditorState.REPLAYING: HandleReplay(); break;
            case EditorState.RUNNING: HandleRun(); break;
        }
    }

    private void HandleEdit()
    {
        switch (operateState)
        {
            case OperateState.IDLE: SwitchState(); break;
            case OperateState.DRAGGING: Dragging(); break;
            case OperateState.CREATING: Creating(); break;
        }
    }

    private void HandleRun()
    {

    }

    private void HandleReplay()
    {

    }

    private void SwitchState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentSelection = GetEntityUnderMousePosition();
            if(currentSelection != null)
            {
                currentSelection.LeftClick();
                operateState = OperateState.DRAGGING;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            currentSelection = GetEntityUnderMousePosition();
            if(currentSelection != null)
            {
                currentSelection.RightClick();
                createPanel.DisableCreatePanel();
            }
            else
            {
                createPanel.EnableCreatePanel();
            }
        }else if (Input.GetMouseButtonUp(0))
        {
            createPanel.DisableCreatePanel();
        }
    }

    private void Dragging()
    {
        currentSelection.Dragging();
        if (Input.GetMouseButtonUp(0))
        {
            currentSelection.LeftRelease();
            currentSelection = null;
            operateState = OperateState.IDLE;
        }
    }

    private void Creating()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DisableCreateGhost();
            operateState = OperateState.IDLE;
            return;
        }

        MapUnit mapUnit = InputHelper.GetUnitUnderMousePosition();
        if(mapUnit != null)
        {
            createGhost.Show();
            createGhost.transform.position = mapUnit.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                if (mapUnit.IsEmpty)
                {
                    Entity entity = Instantiate(entitySO.prefab).GetComponent<Entity>();
                    entity.Setup(mapUnit);
                    if (!InputHelper.IsTheseKeysHeld(KeyCode.LeftControl))
                    {
                        //Reset 
                        DisableCreateGhost();
                        operateState = OperateState.IDLE;
                    }
                }
            }
        }
        else
        {
            createGhost.Hide();
        }
    }

    public void StartRunning()
    {
        editorState = EditorState.RUNNING;
        //cancel all eitor setting
        operateState = OperateState.IDLE;
        createPanel.DisableCreatePanel();
        createGhost.gameObject.SetActive(false);
        entitySO = null;
    }

    public void StopRunning()
    {
        editorState = EditorState.EDITING;
        //Reset all entity
    }

    public void StartReplaying()
    {
        editorState = EditorState.REPLAYING;
    }

    public void ClearAllBuilds()
    {
        for (int i = buildEntitiesList.Count - 1; i >= 0; i--)
            buildEntitiesList[i].Editor_CancelBuild();
    }

    public void RecordCurrentLevel()
    {

    }

    public void DisableCreateGhost()
    {
        entitySO = null;
        createGhost.Hide();
        createGhost.gameObject.SetActive(false);
    }

    public void SetCurrentTrackingEntitySO(EntitySO entitySO_)
    {
        createGhost.gameObject.SetActive(true);
        createGhost.Hide();
        operateState = OperateState.CREATING;
        entitySO = entitySO_;
        createGhost.Setup(entitySO);
    }

    private ISelectable GetEntityUnderMousePosition()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(InputHelper.MouseWorldPosition, Vector3.forward, float.MaxValue);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider)
            {
                ISelectable selectable = hits[i].collider.gameObject.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    return selectable;
                }
            }
        }
        return null;
    }
}

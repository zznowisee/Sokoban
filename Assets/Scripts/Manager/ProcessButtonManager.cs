using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProcessButtonManager : MonoBehaviour
{
    private EditorManager editorManager;
    private TextMeshProUGUI runStopBtnText;
    [SerializeField] private Button clearBtn;
    [SerializeField] private Button runStopBtn;
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button recordBtn;

    private void Awake()
    {
        editorManager = GetComponent<EditorManager>();
        runStopBtnText = runStopBtn.GetComponentInChildren<TextMeshProUGUI>();
        clearBtn.onClick.AddListener(Clear);
        runStopBtn.onClick.AddListener(RunOrStop);
        replayBtn.onClick.AddListener(Replay);
        recordBtn.onClick.AddListener(Record);
    }

    private void Clear()
    {
        editorManager.ClearAllBuilds();
    }

    private void RunOrStop()
    {
        switch (EditorManager.editorState)
        {
            case EditorState.EDITING:
                runStopBtnText.text = "Stop";

                editorManager.StartRunning();
                clearBtn.interactable = false;
                recordBtn.interactable = false;
                break;
            case EditorState.RUNNING:
                runStopBtnText.text = "Run";

                editorManager.StopRunning();
                clearBtn.interactable = true;
                recordBtn.interactable = true;
                break;
        }
        //turn RunBtn to stopBtn
    }

    private void Replay()
    {
        editorManager.StartReplaying();
    }

    private void Stop()
    {
        editorManager.StopRunning();
    }
    private void Record()
    {
        editorManager.RecordCurrentLevel();
    }
}

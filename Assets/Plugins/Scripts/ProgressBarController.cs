#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
public class ProgressBarController : MonoBehaviour
{
    public Image Image { private set; get; }

    [SerializeField] private int maxProgress = 100;
    [SerializeField] private int currentProgress = 100;

    private void OnAwake()
    {
        Image = GetComponent<Image>();
        RecalculateProgress();
    }

#if UNITY_EDITOR
    private void Reset()
    {
        Image = GetComponent<Image>();
        Image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Plugins/Images/blueBar.png");
        Image.type = Image.Type.Filled;
        Image.fillMethod = Image.FillMethod.Horizontal;
        maxProgress = 100;
        currentProgress = 100;
    }
#endif

    private void OnValidate()
    {
        Image = GetComponent<Image>();
        RecalculateProgress();
    }

    public void SetMaxProgress(int maxProgress)
    {
        this.maxProgress = maxProgress;
        RecalculateProgress();
    }

    public void SetProgress(int currentProgress)
    {
        this.currentProgress = currentProgress;
        RecalculateProgress();
    }

    private void RecalculateProgress()
    {
        Image.fillAmount = (float) currentProgress / maxProgress;
    }
}
using TMPro;
using UnityEngine;

public class TimeSurvivedText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        InvokeRepeating(nameof(UpdateText), 0f, 1f);
    }

    private void UpdateText()
    {
        textMesh.text = $"Time Survived: {TimeManager.Instance.GetTimeSurvivedString()}";
    }
}

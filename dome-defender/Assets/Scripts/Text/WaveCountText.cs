using TMPro;
using UnityEngine;

public class WaveCountText : MonoBehaviour
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

    void UpdateText()
    {
        if (WaveSystem.Instance.State == WaveSystem.SpawnState.COUNTING)
        {
            textMesh.text = $"Next Wave In: {WaveSystem.Instance.WaveCountdown:00}s";
        }
        else
        {
            textMesh.text = "Wave In Progress";
        }
    }
}

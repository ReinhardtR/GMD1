using TMPro;
using UnityEngine;

// TODO: This script will listen to changes in the player upgrades,
// which will change the speed of the player.
public class SpeedText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private PlayerController player;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player")
            .GetComponent<PlayerController>();

        UpdateText();
    }

    private void UpdateText()
    {
        if (player)
        {
            textMesh.text = $"Speed: {player.Speed}";
        }
        else
        {
            textMesh.text = "";
        }
    }
}

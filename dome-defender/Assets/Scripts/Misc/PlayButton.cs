using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        button.onClick.AddListener(OnPlay);
    }

    private void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }
}

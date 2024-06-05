using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public void GameOver()
    {
        SceneManager.LoadScene("Menu");
    }
}

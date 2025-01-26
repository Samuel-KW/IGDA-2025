using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLostHandler : MonoBehaviour
{
    public void Restart()
    {
        Debug.Log("Working");
        SceneManager.LoadScene("Scenes/intro");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

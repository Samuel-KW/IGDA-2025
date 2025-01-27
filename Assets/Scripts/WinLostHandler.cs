using System.Runtime.CompilerServices;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLostHandler : MonoBehaviour
{

    private bool loading = false;

    public void Restart()
    {
        Debug.Log("Restart");
        loading = true;
        SceneManager.LoadScene("Scenes/intro");
    }

    public void Retry()
    {
        Debug.Log("Retry");
        loading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

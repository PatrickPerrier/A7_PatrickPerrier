using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuHandler : MonoBehaviour
{
    [SerializeField]
    Canvas winScreen;

    public bool gameWon;
    
    void Start()
    {
        winScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameWon)
        {
            winScreen.gameObject.SetActive(true);            
        }
    }

    public void ReplayGame ()
    {
        SceneManager.LoadScene("RTS");
    }
    public void ExitGame ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

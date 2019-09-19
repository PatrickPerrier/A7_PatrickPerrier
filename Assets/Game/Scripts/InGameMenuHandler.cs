using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InGameMenuHandler : MonoBehaviour
{
    [SerializeField]
    Canvas winScreen;

    [SerializeField]
    Canvas lossScreen;

    public bool gameWon;

    [SerializeField]
    ObjectInfo[] allUnits;

    [SerializeField]
    List<GameObject> friendllyUnits;

    void Start()
    {
        winScreen.gameObject.SetActive(false);
        lossScreen.gameObject.SetActive(false);
        allUnits = FindObjectsOfType<ObjectInfo>();
        for (int i = 0; i < allUnits.Length; i++)
        {
            if (allUnits[i].isEnemy == false)
            {
                friendllyUnits.Add(allUnits[i].gameObject);
            }
        }
        
    }

    void Update()
    {
        friendllyUnits.RemoveAll(item => item == null);
        if (gameWon)
        {
            winScreen.gameObject.SetActive(true);            
        }
        else if (friendllyUnits.Count.Equals(0))
        {
            lossScreen.gameObject.SetActive(true);
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

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public float rColor;

    public float gColor;

    public float bColor;

    public Color tColor;

    public Image cTarget;

    private bool onMenu = true;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        tColor = new Color(rColor, gColor, bColor);
        if (onMenu)
        {
            cTarget.GetComponent<Image>().color = tColor;
        }               
    }
    public void LoadGame()
    {
        onMenu = false;
        SceneManager.LoadScene("RTS");
    }
    public void SetRedColor(float newRedColor)
    {
        rColor = newRedColor;
    }

    public void SetGreenColor(float newGreenColor)
    {
        gColor = newGreenColor;
    }

    public void SetBlueColor(float newBlueColor)
    {
        bColor = newBlueColor;
    }
}

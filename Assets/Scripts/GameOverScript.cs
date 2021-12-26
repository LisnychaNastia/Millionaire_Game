using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Text text;
    public static bool PlayerWin = true;
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Start()
    {
        if (PlayerWin)
            text.text = "You are a Millionaire";
        else
            text.text = "You've lost";
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsScript : MonoBehaviour
{
    private GameScript gameScript;
    public Button[] buttons;
    public List<Button> hintList;

    public void Start()
    {
        hintList = new List<Button>(buttons);
        GameObject go = GameObject.Find("Main Camera");
        gameScript = go.GetComponent<GameScript>();
    }

    public void Fiftyfifty()
    {
        int count = 0;
        for(int i = 0; i < gameScript.answerText.Length; i++)
        {
            if (!gameScript.IsAnswerTrue(i) && count != 2)
            {
                gameScript.answerText[i].text = string.Empty;
                count++;
            }
        }

        hintList[0].gameObject.SetActive(false);
    }

    public void Chance()
    {
        gameScript.chance = true;
        hintList[2].gameObject.SetActive(false);
    }

    public void Help()
    {
        int index = 0;
        for(int i = 0; i < 4; i++)
        {
            if (gameScript.IsAnswerTrue(i))
                index = i;
        }
        gameScript.AnswerButton(index);
        hintList[1].gameObject.SetActive(false);
    }
}

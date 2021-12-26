using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public Button[] answerButton;
    public Text questionText;
    public Text[] answerText;
    public Image arrow;

    private List<Question> questionsList;
    private int rand_Q;
    private int rand_A;
    public bool chance = false;

    private void ReadFileQuestions()
    {
        string path = @"D:\ДЗ\курсовая\millionaire/Questions1.txt";
        StreamReader file = new StreamReader(path);
        string line;
        while((line = file.ReadLine()) != null)
        {
            Question curr_Q = new Question();
            string[] lines = line.Split('/');
            curr_Q.question = lines[0];

            for(int i = 0; i < lines.Length - 1; i++)
            {
                curr_Q.answers[i] = lines[i + 1];
            }

            questionsList.Add(curr_Q);
        }
    }

    public void Start()
    {
        questionsList = new List<Question>();
        arrow.transform.localPosition = new Vector3(-125, -282, 0);
        ReadFileQuestions();
        GenerateQuestion();
    }

    private Question current_Q;

    public Question Curr_Q { get { return current_Q; } }

    void GenerateQuestion()
    {
        if(questionsList.Count > 0)
        {
            rand_Q = Random.Range(0, questionsList.Count);
            current_Q = questionsList[rand_Q];
            questionText.text = current_Q.question;
            List<string> curr_A = new List<string>(current_Q.answers);
            for (int i = 0; i < answerText.Length; i++)
            {
                rand_A = Random.Range(0, curr_A.Count);
                answerText[i].text = curr_A[rand_A];
                curr_A.RemoveAt(rand_A);
            }
        }
        else
        {
            if (!chance) 
            {
                GameOverScript.PlayerWin = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
    }

    public void AnswerButton(int id)
    {
        if (IsAnswerTrue(id))
        {
            answerButton[id].image.color = Color.green;
        }
        else
        {
            answerButton[id].image.color = Color.red;
            if (!chance) 
            {
                GameOverScript.PlayerWin = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
                
            chance = false;
        }
        StartCoroutine(ChangeColor(id));
    }

    public bool IsAnswerTrue(int id)
    {
        if (answerText[id].text == questionsList[rand_Q].answers[0]) 
        {
            arrow.transform.localPosition += new Vector3(0, 41, 0);
            return true;
        }
           
        return false;
    }

    private IEnumerator ChangeColor(int id)
    {
        yield return new WaitForSeconds(2);
        questionsList.RemoveAt(rand_Q);
        answerButton[id].image.color = new Color(255, 255, 255);
        GenerateQuestion();
    }
}

public class Question
{
    public string question;
    public string[] answers = new string[4];
}
    

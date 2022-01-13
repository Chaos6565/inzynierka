using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomaExamManager : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public GameObject questions;
    public GameObject welcomeDraw;

    public Text resultPanelText;

    public Text welcomeText;

    [Header("Pierwsze pytanie")]
    public Text firstQuestionText;
    public Toggle answer1a;
    public Text answer1aText;
    public Toggle answer1b;
    public Text answer1bText;
    public Toggle answer1c;
    public Text answer1cText;

    [Header("Drugie pytanie")]
    public Text secondQuestionText;
    public Toggle answer2a;
    public Text answer2aText;
    public Toggle answer2b;
    public Text answer2bText;
    public Toggle answer2c;
    public Text answer2cText;

    int points;
    int first;
    int second;


    List<string[]> questionsList = new List<string[]>();


    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void SetCompleted(bool state)
    {
        _complete = state;
    }




    public void OpenTask()
    {
        taskCanvas.SetActive(true);
        questions.SetActive(false);

        welcomeDraw.SetActive(true);

        welcomeText.text = "Aby wylosowaæ pytania z puli proszê wcisn¹æ strza³kê";

        questionsList.Add(new string[]{ "Ile lat trwa³y studia?", "1", "2", "3", "c"});
        questionsList.Add(new string[] { "Jakiego patrona ma ukoñczony przez Ciebie Uniwersytet?", "Miko³aja Kopernika", "Adama Mickiewicza", "Kardyna³a Stefana Wyszyñskiego", "b" });
        questionsList.Add(new string[] { "W jakim mieœcie znajduje siê UAM? ", "Poznaniu", "Warszawie", "Krakowie", "a" });
        questionsList.Add(new string[] { "Który przedmiot nie pojawi³ sie na studiach", "Analiza", "Algebra", "Geometria", "c" });
        questionsList.Add(new string[] { "Ilu ró¿nych prowadz¹cych spotka³eœ w czasie studiów", "9", "10", "11", "c" });
        questionsList.Add(new string[] { "Ile Auli posiada budynek Wydzia³u Matematyki i Informatyki UAM? ", "2", "3", "4", "b" });
        questionsList.Add(new string[] { "Na którym poziomie znajduje siê dziekanat Wydzia³u Matematyki i Informatyki UAM ? ", "Zero", "Jeden", "Dwa", "b" });
        questionsList.Add(new string[] { "Na ilu ró¿nych poziomach znajduj¹ siê sale informatyczne na Wydziale Matematyki i Informatyki UAM? ", "1", "2", "3", "c" });
        questionsList.Add(new string[] { "Na jakim poziomie rozpoczê³a siê gra?  ", "Zero", "Jeden", "Dwa", "c" });
        questionsList.Add(new string[] { "Ile poziomów ma czêœæ B Wydzia³u Matematyki i Informatyki UAM? a", "Trzy", "Cztery", "Piêæ", "c" });
        questionsList.Add(new string[] { "Ile poziomów ma czêœæ A Wydzia³u Matematyki i Informatyki UAM? ", "Dwa", "Trzy", "Cztery", "b" });
        questionsList.Add(new string[] { "Na jakim poziomie znajduje siê biblioteka Wydzia³u Matematyki i Informatyki UAM ?  ", "Zero", "Jeden", "Dwa", "b" });
        questionsList.Add(new string[] { "Na jakim poziomie znajduje siê barek / kafeteria Wydzia³u Matematyki i Informatyki UAM ?  ", "Zero", "Jeden", "Dwa", "a" });
        questionsList.Add(new string[] { "Czego uczy³ profesor Testow? ", "Analizy", "Logiki", "Algebry", "a" });
        questionsList.Add(new string[] { "Czego uczy³ profesor Implikacja? ", "Analizy", "Logiki", "Algebry", "b" });
        questionsList.Add(new string[] { "Czego uczy³ profesor Pierœcieñ? ", "Logiki", "Analizy", "Algebry",  "c" });

        points = 0;

        _complete = false;
        GameStateManager.instance.taskActive = true;
    }


    public void DrawClick()
    {
        questions.SetActive(true);
        welcomeDraw.SetActive(false);

        first = Random.Range(0, questionsList.Count);
        do
        {
            second = Random.Range(0, questionsList.Count);
        } while (second == first);


        firstQuestionText.text = questionsList[first][0];
        answer1aText.text = questionsList[first][1];
        answer1bText.text = questionsList[first][2];
        answer1cText.text = questionsList[first][3];

        secondQuestionText.text = questionsList[second][0];
        answer2aText.text = questionsList[second][1];
        answer2bText.text = questionsList[second][2];
        answer2cText.text = questionsList[second][3];
    }

    public void SubmitClick()
    {
        switch(questionsList[first][4])
        {
            case "a":
                if (answer1a.isOn && !answer1b.isOn && !answer1c.isOn) points++;
                break;

            case "b":
                if (!answer1a.isOn && answer1b.isOn && !answer1c.isOn) points++;
                break;

            case "c":
                if (!answer1a.isOn && !answer1b.isOn && answer1c.isOn) points++;
                break;
        }

        switch (questionsList[second][4])
        {
            case "a":
                if (answer2a.isOn && !answer2b.isOn && !answer2c.isOn) points++;
                break;

            case "b":
                if (!answer2a.isOn && answer2b.isOn && !answer2c.isOn) points++;
                break;

            case "c":
                if (!answer2a.isOn && !answer2b.isOn && answer2c.isOn) points++;
                break;
        }

        if (points == 2)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Uda³o ci siê, zaliczy³eœ ca³e studia informatyczne! Zosta³eœ In¿ynierem";

            _complete = true;
        }
        else
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Niestety, nie uda³o ci siê, mo¿esz spróbowaæ jeszcze raz";
        }
    }


    public void CloseClick()
    {
        background.SetActive(false);
        resultPanel.SetActive(false);

        taskCanvas.SetActive(false);

        GameStateManager.instance.taskActive = false;
        if (Complete)
        {
            Destroy(taskCanvas);
            Destroy(gameObject);
        }
    }

}

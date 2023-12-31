using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class score_display : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text_score;
    [SerializeField] GameObject back;

    
    private void Start()
    {
        SetFirstButtonSelection();
    }

    public void SetScoreToText()
    {
        text_score.text = GameManager.Instance.score.ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetFirstButtonSelection()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(back);
    }
}

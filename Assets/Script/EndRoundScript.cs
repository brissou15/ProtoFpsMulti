using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndRoundScript : MonoBehaviour
{
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject endRoundPanel;
    [SerializeField] private TMP_Text winText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActiveEndRound();
    }

    void ActiveEndRound()
    {
        if (RoundManager.instance.scores[0]>= RoundManager.instance.maxScore || RoundManager.instance.scores[1] >= RoundManager.instance.maxScore)
        {
            hudPanel.SetActive(false);
            endRoundPanel   .SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            RoundManager.instance.haveEnd = true;
            ChangeTextWin();
            Time.timeScale = 0;
        }
        else if(RoundManager.instance.roundTimer<=0)
        {
            hudPanel.SetActive(false);
            endRoundPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            RoundManager.instance.haveEnd = true;
            ChangeTextWin();
            Time.timeScale = 0;         
        }
    }

    private void ChangeTextWin()
    {
        if (RoundManager.instance.scores[0] >= RoundManager.instance.scores[1])
        {
            winText.text = "Blue Team Win";
        }
        else
        {
            winText.text = "Red Team Win";
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Cursor.lockState = CursorLockMode.Locked;
        RoundManager.instance.haveEnd = false;
    }

}

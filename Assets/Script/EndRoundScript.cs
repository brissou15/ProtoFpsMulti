using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndRoundScript : MonoBehaviour
{
    [SerializeField] private GameObject PanelHUD;
    [SerializeField] private GameObject EndRoundUI;
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
        if (RoundManager.instance.scores[0]> RoundManager.instance.maxScore || RoundManager.instance.scores[1] > RoundManager.instance.maxScore)
        {
            PanelHUD.SetActive(false);
            EndRoundUI.SetActive(true);
            ChangeTextWin();
        }
    }

    private void ChangeTextWin()
    {
        if (RoundManager.instance.scores[0] > RoundManager.instance.maxScore)
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
    }

}

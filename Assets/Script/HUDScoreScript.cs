using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScoreScript : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private GameObject blueTeamBar;
    [SerializeField] private GameObject redTeamBar;
    [SerializeField] private TMP_Text textBlueTeam;
    [SerializeField] private TMP_Text textRedTeam;
    [SerializeField] private TMP_Text textTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
        updateTime();
    }

    private void updateScore()
    {
        Vector3 currentBlueScale = blueTeamBar.transform.localScale;
        Vector3 currentRedScale = redTeamBar.transform.localScale;

        blueTeamBar.transform.localScale = new Vector3(RoundManager.instance.scores[0] / (float)RoundManager.instance.maxScore, currentBlueScale.y, currentBlueScale.z);
        redTeamBar.transform.localScale = new Vector3(RoundManager.instance.scores[1] / (float)RoundManager.instance.maxScore, currentRedScale.y, currentRedScale.z);

        textBlueTeam.text = RoundManager.instance.scores[0].ToString();
        textRedTeam.text = RoundManager.instance.scores[1].ToString();

        
      
    }
    private void updateTime()
    {
        int min = toMin(RoundManager.instance.roundTimer);
        int sec = toSecond(RoundManager.instance.roundTimer);
        textTimer.text = min.ToString() + ":" + sec.ToString();
    }
    public int toMin(float _time)
    {
        return (int)_time / 60;
    }
    public int toSecond(float _time)
    {
        return (int)_time % 60;
    }
    public float toMiliSec(float _time)
    {
        return (_time % 1) * 1000;
    }
}

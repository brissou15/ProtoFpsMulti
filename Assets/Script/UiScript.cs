using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiScript : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private PlayerScript player;
    [SerializeField] private UiGun playerUiGun;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text angleText;

    [Header("LifeBar")]
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private GameObject lifeBackground;
    [SerializeField] private TMP_Text lifeText;

    [Header("Ammo")]
    [SerializeField] private TMP_Text AmmoText;

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
        debugUI();
        UpdateHealthBar();
        updateScore();
        updateAmmo();
    }

    private void debugUI()
    {
        speedText.text = Mathf.Abs(player.m_rb.velocity.z).ToString();
    }

    private void UpdateHealthBar()
    {
        Vector3 currentScale = lifeBar.transform.localScale;
        lifeBar.transform.localScale = new Vector3(player.m_currentHealth / (float)player.m_maxHealth, currentScale.y, currentScale.z);

        lifeText.text = ((int)player.m_currentHealth).ToString()+"/"+((int)player.m_maxHealth).ToString();
    }

    private void updateAmmo()
    {
        AmmoText.text = ((int)playerUiGun.currentAmmo).ToString() + "/" + ((int)playerUiGun.MaxAmmo).ToString();
    }
    private void updateScore()
    {
        Vector3 currentBlueScale = blueTeamBar.transform.localScale;
        Vector3 currentRedScale = redTeamBar.transform.localScale;

        blueTeamBar.transform.localScale = new Vector3( RoundManager.instance.scores[0] / (float)RoundManager.instance.maxScore, currentBlueScale.y, currentBlueScale.z);
        redTeamBar.transform.localScale = new Vector3( RoundManager.instance.scores[1] / (float)RoundManager.instance.maxScore, currentRedScale.y, currentRedScale.z);

        textBlueTeam.text = RoundManager.instance.scores[0].ToString();
        textRedTeam.text = RoundManager.instance.scores[1].ToString();

        //textTimer.text = RoundManager.instance.roundTimer.ToString();
        int min = toMin(RoundManager.instance.roundTimer);
        int sec = toSecond(RoundManager.instance.roundTimer);
        textTimer.text = min.ToString()+":"+sec.ToString();
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

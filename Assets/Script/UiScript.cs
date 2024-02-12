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

        blueTeamBar.transform.localScale = new Vector3( RoundManager.instance.scores[0] / (float)player.m_maxHealth, currentBlueScale.y, currentBlueScale.z);
        blueTeamBar.transform.localScale = new Vector3( RoundManager.instance.scores[1] / (float)player.m_maxHealth, currentRedScale.y, currentRedScale.z);
    }
}

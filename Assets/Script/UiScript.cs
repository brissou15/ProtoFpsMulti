using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiScript : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private PlayerScript player;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text angleText;

    [Header("LifeBar")]
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private GameObject lifeBackground;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugUI();
        UpdateHealthBar();
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
}

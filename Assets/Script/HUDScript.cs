using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private PlayerScript player;
    [SerializeField] private UiGun playerUiGun;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text angleText;

    [Header("LifeBar")]
    [SerializeField] private GameObject lifeBar;
    //[SerializeField] private GameObject lifeBackground;
    [SerializeField] private TMP_Text lifeText;

    [Header("Ammo")]
    [SerializeField] private TMP_Text AmmoText;

   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugUI();
        UpdateHealthBar();
        
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
    

   
}

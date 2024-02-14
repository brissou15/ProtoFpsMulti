using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private GameObject PanelCountdown;
    [SerializeField] private TMP_Text textTimer;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        updatePanel();
    }

    void updateTimer()
    {
        textTimer.text  = ((int)RoundManager.instance.countdownTimer).ToString();
    }
    void updatePanel()
    {
        PanelCountdown.SetActive(!RoundManager.instance.canPlay);
    }
}

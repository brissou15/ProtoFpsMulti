using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
//using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.XInput;

public enum CONFIG
{
    DEUXMANETTEES,
    UNCLAVIERUNEMANETTE
}
public enum CONTROLER
{
    MANETTE,
    CLAVIER
}

public class Config : MonoBehaviour
{

    public CONFIG config;
    Gamepad[] gamepads;
    [SerializeField] private PlayerScript[] playerTab;
    private int saveNbControl;
    private int ManetteJ1;
    // Start is called before the first frame update
    void Start()
    {
        
        //ManetteJ1 = PlayerPrefs.GetInt("ControleScreen");//0 écran du haut et 1 écran du bas
        ManetteJ1 = 0;//0 écran du haut et 1 écran du bas

        UpdateGamepadList();

        if (PlayerPrefs.GetInt("ControleCtrl") == 1)
            config = CONFIG.DEUXMANETTEES;
        else
            config = CONFIG.UNCLAVIERUNEMANETTE;

        if (config == CONFIG.DEUXMANETTEES)
        {           
            playerTab[0].MyControler = gamepads[gamepads.Length - 2];
            playerTab[1].MyControler = gamepads[gamepads.Length - 1];
            playerTab[0].controler = CONTROLER.MANETTE;
            playerTab[1].controler = CONTROLER.MANETTE;
        }
        else
        {
            if (ManetteJ1 == 1)
            {
                playerTab[0].controler = CONTROLER.CLAVIER;
                playerTab[1].controler = CONTROLER.MANETTE;
                playerTab[1].MyControler = gamepads[gamepads.Length - 1];
            }
            else
            {
                playerTab[1].controler = CONTROLER.CLAVIER;
                playerTab[0].controler = CONTROLER.MANETTE;
                playerTab[0].MyControler = gamepads[gamepads.Length - 1];
            }
        }
        playerTab[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0f,0.5f,1f, 0.5f);
        playerTab[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0f,0f,1f, 0.5f);
        saveNbControl = gamepads.Length;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGamepadList();
        reconfigController();
    }

    private void configManette()
    {
        if (config == CONFIG.DEUXMANETTEES)
        {
            playerTab[0].MyControler = gamepads[gamepads.Length - 2];
            playerTab[1].MyControler = gamepads[gamepads.Length - 1];
        }

        if (config == CONFIG.UNCLAVIERUNEMANETTE)
        {
            if (ManetteJ1 == 1)
            {
                playerTab[1].MyControler = gamepads[gamepads.Length - 1];
            }
            else
            {
                playerTab[0].MyControler = gamepads[gamepads.Length - 1];
            }
        }
    }

    private void UpdateGamepadList()
    {
        gamepads = InputSystem.devices.OfType<Gamepad>().ToArray();
    }

    void reconfigController()
    {
        if(saveNbControl != gamepads.Length) 
        {
            configManette();
            saveNbControl = gamepads.Length;    
        }
    }
}

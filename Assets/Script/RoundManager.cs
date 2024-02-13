using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    [Header("Variables")]
    public static RoundManager instance { get; private set; }
    public int[] scores = new int[2];
    public int maxScore;
    public float roundTimer = 120;

    public GameObject Player1PreFab;

    public GameObject Player2PreFab;

    public GameObject[] BoxGun;

    
    GameObject BoxGunRecup;

    public float respawnTime = 5;
    public float timerRespawn1 = 0;
    public float timerRespawn2 = 0;

    public Transform[] posPourSpawn;


    [SerializeField] EndRoundScript endRoundScript;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        roundTimer -= Time.deltaTime;
        if (!endRoundScript.haveEnd)
        {
            SpawnSystem();
        }
        
    }

    private void SpawnSystem()
    {


        if(BoxGunRecup == null)
        {
            BoxGunRecup = Instantiate(BoxGun[Random.Range(0, 2)], posPourSpawn[Random.Range(0, 5)].position,Quaternion.identity);
        }


        if (Player2PreFab.activeSelf == false)
        {
            timerRespawn2 += Time.deltaTime;           
            if (timerRespawn2 > respawnTime)
            {
                Player2PreFab.transform.position = posPourSpawn[Random.Range(0, 5)].position;
                Player2PreFab.SetActive(true);
                Player2PreFab.GetComponent<UiGun>().ResetGun();
                Player2PreFab.GetComponent<PlayerScript>().ResetHp();
                timerRespawn2 = 0;
            }
        }

        if (Player1PreFab.activeSelf == false)
        {
            timerRespawn1 += Time.deltaTime;
            if (timerRespawn1 > respawnTime)
            {
                Player1PreFab.transform.position = posPourSpawn[Random.Range(0, 5)].position;
                Player1PreFab.SetActive(true);
                Player1PreFab.GetComponent<UiGun>().ResetGun();
                Player1PreFab.GetComponent<PlayerScript>().ResetHp();
                timerRespawn1 = 0;
            }
        }

    }

    private void updateScore()
    {

    }

    public void addScore(int team, int value)
    {
        scores[team] += value;
    }
    public RoundManager getInstance()
    {
        return instance;
    }
}

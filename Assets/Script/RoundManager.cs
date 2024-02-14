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

    GameObject BoxGun1;
    float timerRespawnGun1 = 0;
    GameObject BoxGun2;
    float timerRespawnGun2 = 0;
    GameObject BoxGun3;
    float timerRespawnGun3 = 0;
    GameObject BoxGun4;
    float timerRespawnGun4 = 0;


    public float respawnTime = 5;
    public float timerRespawn1 = 0;
    public float timerRespawn2 = 0;

    public Transform[] posPourSpawn;

    public Transform[] posSpawnBox;

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
        if(BoxGun1 == null)
        {
            timerRespawnGun1 += Time.deltaTime;
            if(timerRespawnGun1> 5)
            {
                BoxGun1 = Instantiate(BoxGun[1], posSpawnBox[0].position, Quaternion.identity);
                timerRespawnGun1 = 0;
            }
        }
        if (BoxGun2 == null)
        {
            timerRespawnGun2 += Time.deltaTime;
            if (timerRespawnGun2 > 5)
            {
                BoxGun2 = Instantiate(BoxGun[1], posSpawnBox[1].position, Quaternion.identity);
                timerRespawnGun2 = 0;
            }
        }

        if (BoxGun3 == null)
        {
            timerRespawnGun3 += Time.deltaTime;
            if (timerRespawnGun3 > 5)
            {
                BoxGun3 = Instantiate(BoxGun[0], posSpawnBox[2].position, Quaternion.identity);
                timerRespawnGun3 = 0;
            }
        }

        if (BoxGun4 == null)
        {
            timerRespawnGun4 += Time.deltaTime;
            if (timerRespawnGun4 > 5)
            {
                BoxGun4 = Instantiate(BoxGun[0], posSpawnBox[3].position, Quaternion.identity);
                timerRespawnGun4 = 0;
            }
        }


        if (Player2PreFab.activeSelf == false)
        {
            timerRespawn2 += Time.deltaTime;           
            if (timerRespawn2 > respawnTime)
            {
                Player2PreFab.transform.position = posPourSpawn[Random.Range(0, 4)].position;
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
                Player1PreFab.transform.position = posPourSpawn[Random.Range(0, 4)].position;
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

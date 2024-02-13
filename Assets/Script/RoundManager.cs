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

    [SerializeField]
    GameObject Player1PreFab;

    [SerializeField]
    GameObject Player2PreFab;


    float timerRespawn1 = 0;
    float timerRespawn2 = 0;

    public void Awake()
    {
        if(instance==null)
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
        roundTimer -=Time.deltaTime;
        SpawnSystem();
    }

    private void SpawnSystem()
    {

        if (Player2PreFab.activeSelf == false)
        {
            timerRespawn2 += Time.deltaTime;
            if (timerRespawn2 > 5)
            {
                Player2PreFab.SetActive(true);
                Player2PreFab.GetComponent<UiGun>().ResetGun();
                timerRespawn2 = 0;
            }
        }

        if (Player1PreFab.activeSelf == false)
        {
            timerRespawn1 += Time.deltaTime;
            if (timerRespawn1 > 5)
            {
                Player1PreFab.SetActive(true);
                Player1PreFab.GetComponent<UiGun>().ResetGun();
                timerRespawn1 = 0;
            }
        }

    }

    private void updateScore()
    {

    }

    public void addScore(int team,int value)
    {
        scores[team]+= value;
    }
    public RoundManager getInstance()
    {
        return instance;
    }
}

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

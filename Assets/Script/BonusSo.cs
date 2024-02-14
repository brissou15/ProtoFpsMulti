using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bonus", menuName = "SciptableObject/Bonus")]
public class BonusSo : ScriptableObject
{
    [Header("Name")]
    public string bonusName;

    [Header("Reference")]
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Values")]
    public float damegeBonus = 0;
    public float healBuff = 0;
    public float respawnTime  = 60;
    public float effectDuration = 0;

    private bool isLootable;
    private float respawnTimer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isLootable)
        {
            respawnTimer = 0;
        }
        else
        {
            respawnTimer+= Time.deltaTime;
        }
        updateVisible();
    }

    private void updateVisible()
    {
        Color currentColor = meshRenderer.material.color;
        if (isLootable)
        {
            meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 255);
        }
        else
        {
            meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bonusType
{
    Nothing,
    QuadDamage
};
public class BonusScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] protected MeshRenderer meshRenderer;

    [Header("Name")]
    [SerializeField] protected bonusType bonusType = bonusType.Nothing;
    [Header("Values")]
    [SerializeField] protected bool isLootable = true;
    [SerializeField] protected int value = 50;
    [SerializeField] protected int duration = 13;
    [SerializeField] protected float respawnTime = 5;

    protected float respawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateVisible();
        updateLootable();
    }
    private void updateLootable()
    {
        if (isLootable)
        {
            respawnTimer = 0;
        }
        else
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer > respawnTime)
            {
                isLootable = true;
                respawnTimer = 0;
            }
        }
    }
    private void updateVisible()
    {
        //Color currentColor = meshRenderer.material.color;
        if (isLootable)
        {
            //meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 255);
            meshRenderer.enabled = true;
        }
        else
        {
            //meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
            meshRenderer.enabled = false;
        }
    }
}

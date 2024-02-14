using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HealScript : BonusScript
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>() != null && isLootable)
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (player.m_currentHealth <= 100)
            {
                player.m_currentHealth += value;
                isLootable = false;
            }
        }
    }
}

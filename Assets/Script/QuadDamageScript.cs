using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class QuadDamageScript : BonusScript
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>() != null && isLootable)
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (player.bonusType==bonusType.Nothing)
            {
                GetComponent<AudioSource>().Play();

                player.bonusType = bonusType;
                player.bonusDuration = duration;
                player.damageMultiplier = value;
                isLootable = false;
            }
        }
    }
}

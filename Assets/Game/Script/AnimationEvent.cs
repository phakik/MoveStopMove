using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] Gun gun;
    public void Attack()
    {
        gun.Shoot(character.bulletType, character, character.OnHitVictim);
    }
    public void Throw()
    {
        character.playerWeapon.SetActive(false);
    }
    public void NotThrow()
    {
        character.playerWeapon.SetActive(true);

    }
}

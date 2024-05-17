using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public void Shoot(GameObject skin, Character character, Action<Character, Character> onHit)
    {
        if (character.isMoving)
        {
            return;
        }
        if (character.currentTarget != null)
        {
            GameObject bull = ObjectPooling.Instance.GetObject(skin, this.transform.position);
            bull.transform.position = this.transform.position;
            Vector3 shootDirect = (character.currentTarget.transform.position - this.transform.position).normalized;
            shootDirect.y = 0;
            bull.GetComponent<Bullet>().direct = shootDirect;
            bull.GetComponent<Bullet>().OnInit(character, onHit);
            bull.SetActive(true);
        }
    }
}


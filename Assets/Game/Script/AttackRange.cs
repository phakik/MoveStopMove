using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Character character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CONSTANT.CHARACTER_TAG) && other.gameObject != character.gameObject)
        {
            if (character.isDead == false)
            {
                character.enemyList.Add(other.gameObject.GetComponent<Character>());
                character.currentTarget = character.enemyList[0];
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CONSTANT.CHARACTER_TAG) && other.gameObject != character.gameObject)
        {
            character.enemyList.Remove(other.gameObject.GetComponent<Character>());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] public float speed;
    protected Character attacker;
    protected Action<Character, Character> onHit;
    public Vector3 direct = Vector3.up;
    public virtual void OnInit(Character attacker, Action<Character, Character> onHit)
    {
        this.attacker = attacker;
        this.onHit = onHit;
    }

    private void OnEnable()
    {
        rb.rotation = Quaternion.identity;
        StartCoroutine(DespawnTime());
    }

    void Update()
    {
        rb.velocity = speed * Time.deltaTime * direct;
        transform.Rotate(direct.normalized , 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANT.CHARACTER_TAG) && other.GetComponent<Character>() != attacker)
        {
            this.gameObject.SetActive(false);
            Character victim = Cache.GetCharacter(other);
            onHit?.Invoke(attacker, victim);
        }
    }
    private IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }


}

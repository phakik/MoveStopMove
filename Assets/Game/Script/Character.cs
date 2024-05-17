using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] protected bool isAttacking;
    [SerializeField] protected Gun gun;
    [SerializeField] protected SkinnedMeshRenderer playerButt;
    [SerializeField] protected WeaponSO weaponSO;
    [SerializeField] protected PantSO pantSO;
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected bool canAttack;
    public bool isDead;
    public Enum.WeaponType weapon;
    public Enum.PantType pant;
    Character victim;
    public List<Character> enemyList;
    public GameObject bulletType;
    public Character currentTarget;
    protected Rigidbody rb;
    public bool isMoving;
    public GameObject playerWeapon;
    string currentAnim = "IsIdle";


   

    protected void CheckMoving()
    {
        if (isDead)
        {
            SetAnim("IsDead");
        }
        else if (rb.velocity.x != 0 || rb.velocity.z != 0)
        {
            isMoving = true;
            SetAnim("IsMove");
        }
        else
        {
            if (isAttacking)
            {
                SetAnim("IsAttack");
            }
            else
            {
                isMoving = false;
                SetAnim("IsIdle");
            }
        }
    }

    public void SetAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.SetBool(currentAnim, false);
            currentAnim = animName;
            animator.SetBool(currentAnim, true);
        }
    }
    public virtual void CheckAttack()
    {
        if (isMoving)
        {
            isAttacking = false;
            return;
        }
        if (enemyList.Count != 0)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            currentTarget = null;
            return;
        }
    }

    public void OnHitVictim(Character attacker, Character victim)
    {
        this.victim = victim;
        if (victim == attacker)
        {
            return;
        }
        victim.isDead = true;
        victim.DoDead();
    }
    protected void RemoveDeadEnemy()
    {
        if (enemyList.Count >= 1)
        {
            if (enemyList[0].isDead == true)
            {
                enemyList.Remove(enemyList[0]);
            }
        }
        else
        {
            return;
        }
    }
    public virtual void GetWeapon()
    {
        for (int i = 0; i < weaponSO.weaponDataList.Count; i++)
        {
            if ((int)weapon == (int)weaponSO.weaponDataList[i].weaponName)
            {
                playerWeapon = Instantiate(weaponSO.weaponDataList[i].weaponAvatar);
                bulletType = weaponSO.weaponDataList[i].weapon;
                playerWeapon.transform.SetParent(rightHand, false);
                playerWeapon.transform.localRotation = Quaternion.Euler(363, 677, 289);
                playerWeapon.SetActive(true);
            }
        }
    }
    public virtual void GetPant()
    {
        for (int i = 0; i < pantSO.pantItemDataList.Count; i++)
        {
            if ((int)pant == (int)pantSO.pantItemDataList[i].pantName)
            {
                playerButt.material = pantSO.pantItemDataList[i].pant;
            }
        }
    }
    protected virtual void OnDespawn()
    {
        this.gameObject.SetActive(false);
    }
    protected virtual void DoDead()
    {
        SetAnim("IsDead");
        rb.velocity = Vector3.zero;
        Invoke(nameof(OnDespawn), 2f);
    }

}

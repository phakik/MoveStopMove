using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Enemy : Character
{
    [SerializeField] NavMeshAgent agent;
    IState currentState;
    public Vector3 randomPoint;
    public Transform centrePoint;
    public bool arrived = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeState(new IdleState());
        GetWeapon();
        GetPant();
    }
    public void OnInit()
    {
        isDead = false;
    }
    // Update is called once per frame
    void Update()
    {
        currentState?.OnExcute(this);
        RemoveDeadEnemy();
        CheckAttack();
    }
    public void MoveToRandomPoint()
    {
        if (isDead)
        {
            return;
        }
        if (RandomPoint(centrePoint.position, 200f, out Vector3 point))
        {
            randomPoint = point;
            agent.SetDestination(randomPoint);
            SetAnim("IsMove");
        }
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 200.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            result.y = this.transform.position.y;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    internal void StopMoving()
    {
        rb.velocity = Vector3.zero;
        agent.SetDestination(this.transform.position);
        SetAnim("IsIdle");
        return;
    }
    protected override void DoDead()
    {
        ChangeState(null);
        agent.SetDestination(this.transform.position);
        base.DoDead();
    }

    public void ChangeState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
    protected override void OnDespawn()
    {
        base.OnDespawn();
        LevelManager.Instance.CheckDeadEnemyOnMap(this);
    }
    public override void GetPant()
    {
        playerButt.material = pantSO.pantItemDataList[Random.Range(0, pantSO.pantItemDataList.Count - 1)].pant;
    }
    public override void GetWeapon()
    {
        int random = Random.Range(0, (int)Enum.WeaponType.candy_4 - 1);
        for (int i = 0; i < weaponSO.weaponDataList.Count; i++)
        {
            if (random == (int)weaponSO.weaponDataList[i].weaponName)
            {
                playerWeapon = Instantiate(weaponSO.weaponDataList[i].weaponAvatar);
                bulletType = weaponSO.weaponDataList[i].weapon;
                playerWeapon.transform.SetParent(rightHand, false);
                playerWeapon.transform.localRotation = Quaternion.Euler(363, 677, 289);
                playerWeapon.SetActive(true);
            }
        }
    }
}

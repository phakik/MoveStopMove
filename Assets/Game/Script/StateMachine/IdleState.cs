using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2, 5);
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.currentTarget != null)
        {
            enemy.ChangeState(new AttackState());
        }
        else if (timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.MoveToRandomPoint();
    }
    public void OnExcute(Enemy enemy)
    {
        if (enemy.currentTarget != null || Vector3.Distance(enemy.gameObject.transform.position, enemy.randomPoint) < 1f)
        {
            enemy.ChangeState(new IdleState());

        }

    }
    public void OnExit(Enemy enemy)
    {

    }

}

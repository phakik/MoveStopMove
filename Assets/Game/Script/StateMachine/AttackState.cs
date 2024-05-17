using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
      
    }
    public void OnExcute(Enemy enemy)
    {
        if (enemy.currentTarget != null)
        {
            Vector3 direction = enemy.currentTarget.transform.position - enemy.transform.position;
            direction.y = 0;
            enemy.transform.rotation = Quaternion.LookRotation(direction);
            enemy.StopMoving();
            enemy.CheckAttack();
            enemy.SetAnim("IsAttack");
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }
    public void OnExit(Enemy enemy) { }
}

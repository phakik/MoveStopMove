using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 startPoint;
    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    GameObject playerClone;
    GameObject enemyClone;
    List<Enemy> enemies;
    private CameraFollow cameraPlayer;

    public void OnInit()
    {
        Observer.Instance.AddListener("Lose", CheckLose);
        playerClone = ObjectPooling.Instance.GetObject(player.gameObject, startPoint);
        playerClone.transform.position = startPoint;
        playerClone.SetActive(true);
        playerClone.transform.SetParent(transform);
        cameraPlayer = FindAnyObjectByType<CameraFollow>();
        cameraPlayer.target = playerClone.transform;
        enemies = new();
        Invoke(nameof(SpawnEnemy), 0.2f);
    }
    void SpawnEnemy()
    {
        for (int i = 0; i < 15; i++)
        {
            enemyClone = ObjectPooling.Instance.GetObject(enemy.gameObject, GetRandomPointOnNavMesh());
            Vector3 vector3enemy = enemyClone.transform.position;
            vector3enemy.y = 44f;
            enemyClone.transform.position = vector3enemy;
            enemyClone.transform.SetParent(this.transform);
            enemies.Add(enemyClone.GetComponent<Enemy>());
            enemyClone.GetComponent<Enemy>().isDead = false;
            enemyClone.SetActive(true);
        }
        Observer.Instance.Notify(CONSTANT.SET_ENEMY_TEXT, enemies.Count);
    }
    public void CheckVictory()
    {
        UIManager.Instance.OpenUI<Victory>();
    }
    public void CheckLose(object delay)
    {
        UIManager.Instance.OpenUI<Lose>();
    }
    public void CheckDeadEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy) && enemy.gameObject.activeSelf == false)
        {
            enemies.Remove(enemy);
            Observer.Instance.Notify(CONSTANT.SET_ENEMY_TEXT, enemies.Count);
        }
        if (enemies.Count == 0 && playerClone.GetComponent<Character>().isDead == false)
        {
            CheckVictory();
        }
    }
    public void OnDesPawn()
    {
        gameObject.SetActive(false);
        enemies.Clear();
    }
    Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint;
        Vector3 spawnCenter = this.transform.position;
        Vector3 randomDirection = Random.insideUnitSphere * 80f;
        randomDirection += spawnCenter;
        NavMeshHit hit;
        while (!NavMesh.SamplePosition(randomDirection, out hit, 1.0f, NavMesh.AllAreas) || Vector3.Distance(hit.position, startPoint) < 10f)
        {
            randomDirection = Random.insideUnitSphere * 80f;
            randomDirection += spawnCenter;
        }
        randomPoint = hit.position;
        return randomPoint;
    }
}

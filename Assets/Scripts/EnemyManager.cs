using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform waypointParent;  // ���� ����Ʈ�� �θ� ������Ʈ.
    [SerializeField] EnemyTile enemyPrefab;     // ���� ������.
    [SerializeField] int spawnCount;            // ���� ���� ��.
    [SerializeField] float spawnRate;           // ���� ���� ��.

    Transform[] waypoints;

    void Start()
    {
        // waypointParent ������ �ڽĵ��� �̿��� �迭 �Ҵ�.
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = waypointParent.GetChild(i);

        StartCoroutine(SpawnProcess());
    }

    IEnumerator SpawnProcess()
    {
        int remainingCount = spawnCount;        // ���� ���� ��.
        while((remainingCount -= 1) >= 0)       // �� ���� �� ���� ���� 0�̻��� ���.
        {
            yield return new WaitForSeconds(spawnRate);                 // spawnRate��ŭ ���.
            EnemyTile newEnemy = Instantiate(enemyPrefab, transform);   // �� ������ ����. (���� ����)
            newEnemy.transform.position = waypoints[0].position;        // ���� ��ġ�� 0��° ���� ����Ʈ.

            newEnemy.SetDestination(waypoints);                         // ������ ������ ����.
        }
    }

}

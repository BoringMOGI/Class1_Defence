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
    System.Action OnEndPhase;

    void Start()
    {
        // waypointParent ������ �ڽĵ��� �̿��� �迭 �Ҵ�.
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = waypointParent.GetChild(i);

    }

    public void OnStartEnemy(System.Action OnEndPhase)
    {
        this.OnEndPhase = OnEndPhase;
        StartCoroutine(SpawnProcess());
    }

    IEnumerator SpawnProcess()
    {
        int remainingCount = spawnCount;        // ���� ���� ��.
        int enemyIndex = 0;                     // ���� ��ȣ.                

        GameObject[] enemyArray = new GameObject[spawnCount];
        int index = 0;

        while((remainingCount -= 1) >= 0)       // �� ���� �� ���� ���� 0�̻��� ���.
        {
            yield return new WaitForSeconds(spawnRate);                 // spawnRate��ŭ ���.
            EnemyTile newEnemy = Instantiate(enemyPrefab, transform);   // �� ������ ����. (���� ����)

            newEnemy.name = string.Concat(newEnemy.name, $"({enemyIndex++})");
            newEnemy.transform.position = waypoints[0].position;        // ���� ��ġ�� 0��° ���� ����Ʈ.
            newEnemy.SetDestination(waypoints);                         // ������ ������ ����.

            enemyArray[index++] = newEnemy.gameObject;
        }

        // ������ ���� �����ִ��� üũ.
        int enemyCount = 0;
        do
        {
            enemyCount = 0;
            for(int i = 0; i<enemyArray.Length; i++)
            {
                if (enemyArray[i] != null)
                    enemyCount += 1;
            }    

            yield return null;
        }
        while (enemyCount > 0);

        OnEndPhase?.Invoke();
    }

}

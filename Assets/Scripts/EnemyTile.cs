using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    [SerializeField] float moveSpeed;       // �̵� �ӵ�.

    Transform[] destinations;               // ������ ��ġ.
    int currentIndex;                       // ������ ��ȣ.

    bool isMoving;                          // �����̰� �ִ°�?

    public void SetDestination(Transform[] destinations)
    {
        this.destinations = destinations;
        currentIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving)
            return;

        Vector3 destination = destinations[currentIndex].position;  // ������ ��ǥ.
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if(transform.position == destination)
            InDestination();
    }

    void InDestination()
    {
        currentIndex++;
        if(currentIndex >= destinations.Length)     // ���� ���������� �����ߴ�.
        {
            OnGoal();
        }
    }
    void OnGoal()
    {
        Destroy(gameObject);
    }
}

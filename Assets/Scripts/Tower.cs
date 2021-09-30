using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] LayerMask searchMask;    

    [Header("Bullet")]
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float moveSpeed;

    [Header("Combat")]    
    [SerializeField] float attackPower;
    [SerializeField] float attackRate;
    [SerializeField] float attackRadius;

    private Transform pivot;
    private Enemy target = null;
    private float nextAttackTime = 0.0f;

    private void Start()
    {
        pivot = transform;        
    }

    void Update()
    {
        if (target == null)
            SearchEnemy();
        else
            AttackEnemy();
    }

    private void SearchEnemy()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, attackRadius, searchMask);
        if(targets.Length > 0)
        {
            Collider pick = targets[Random.Range(0, targets.Length - 1)];
            Enemy enemy = pick.GetComponent<Enemy>();

            if(enemy != null)
                target = enemy;
        }
    }
    private void AttackEnemy()
    {
        // ���� ������ Ÿ���� ����(�׾���) ���.
        if (target == null)             
            return;

        // ���� ���� ���� ���� �Ÿ��� ���� ��Ÿ����� �� ���.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > attackRadius)
        {
            target = null;
            return;
        }

        Vector3 direction = target.transform.position - pivot.position;     // (ȸ��) ���Ϸ� ����
        direction.Normalize();                                              // 0.0 ~ 1.0f ���� ������ ����ȭ.
        Quaternion lookAt = Quaternion.LookRotation(direction);             // (ȸ��) ���Ϸ� -> ���ʹϾ�
        //pivot.rotation = lookAt;

        // Lerp : ���� -> ������ ������ �ð��� ����� ���� ���� ���� �ش�.
        // Smooth rotation.
        pivot.rotation = Quaternion.Lerp(pivot.rotation, lookAt, 10f * Time.deltaTime);

        //transform.position = new Vector3(100, 100, 100);
        //transform.rotation = Quaternion.Euler(90, 90, 90);                // ���Ϸ� -> ���ʹϾ�.

        // Ÿ���� ����.
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + attackRate;
            Bullet newBullet = Instantiate(bullet);
            newBullet.transform.position = bulletPivot.position;
            newBullet.transform.rotation = bulletPivot.rotation;
            newBullet.Shoot(target, moveSpeed, attackPower);
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, searchRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackRadius);
    }

#endif
}

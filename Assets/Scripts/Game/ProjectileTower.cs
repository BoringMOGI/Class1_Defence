using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : Tower
{
    [Header("Bullet")]
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float moveSpeed;

    protected float nextAttackTime = 0.0f;

    protected override void OnUpdate()
    {
        LookTarget();

        if (target == null)
            SearchEnemy();
        else
            AttackEnemy();
    }

    protected override void AttackEnemy()
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
}

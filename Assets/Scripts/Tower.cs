using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] LayerMask searchMask;    

    [Header("Combat")]
    [SerializeField] GameObject bullet;
    [SerializeField] float attackPower;
    [SerializeField] float attackRate;
    [SerializeField] float attackRadius;

    [Header("Etc")]
    [SerializeField] Transform towerPivot;

    private Enemy target = null;
    private float nextAttackTime = 0.0f;
    
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
                                
        

        // Ÿ���� ����.
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + attackRate;
            Debug.Log($"Attack To : {target.name}");
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

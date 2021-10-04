using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    [Header("Explode")]
    [SerializeField] LayerMask explodeMask;     // ���� ����ũ.
    [SerializeField] float explodRadius;        // ���� �ݰ�.

    protected override void HitTarget()
    {
        // base.HitTarget();       // Bullet�� ���� HitTarget�� ���� ����.
        Collider[] targets = Physics.OverlapSphere(transform.position, explodRadius, explodeMask);
        foreach (Collider target in targets)
        {
            IDamaged enemy = target.GetComponent<IDamaged>();
            if (enemy != null)
                enemy.OnDamaged(power);
        }

        CreateVFX();
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, searchRadius);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, explodRadius);
    }

#endif
}

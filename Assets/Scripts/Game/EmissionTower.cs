using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EmissionTower : Tower
{
    [SerializeField] Transform beamPivot;       // ������ ������ ����.
    [SerializeField] float beamRadius;          // ������ ������.
    [SerializeField] float chargingTime;        // �������� ���� �ð�.

    LineRenderer line;

    float lookTime = 0.0f;              // ���� ��� �ð�.
    bool isLock;                        // ȸ�� ���� ����.

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = beamRadius * 2f;
        line.endWidth = beamRadius * 2f;
    }

    protected override void OnUpdate()
    {
        if (!isLock)
        {
            LookTarget();
            SearchEnemy();

            // �������� �� ��Ұ� ���� �߰����� ��.
            if (target != null)
            {
                if ((lookTime += Time.deltaTime) >= 3f)
                {
                    // �� �߻�.
                    isLock = true;
                    StartCoroutine(BeamProcess());
                }                                                
            }
        }
    }

    private IEnumerator BeamProcess()
    {
        Vector3 start = beamPivot.position;
        Vector3 end = beamPivot.position + (pivot.forward * attackRadius);

        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        float continueTime = 3.0f;
        float nextAttackTime = 0.0f;

        while((continueTime -= Time.deltaTime) > 0.0f)
        {
            if(nextAttackTime <= Time.time)
            {
                nextAttackTime = Time.time + attackRate;
                Attack();
            }

            yield return null;
        }

        line.positionCount = 0;
        isLock = false;
    }

    private void Attack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(beamPivot.position, beamRadius, pivot.forward, attackRadius, searchMask);
        foreach(RaycastHit hit in hits)
        {
            IDamaged target = hit.collider.GetComponent<IDamaged>();
            if (target != null)
                target.OnDamaged(attackPower);
        }
    }


    protected override void AttackEnemy()
    {
        
    }
}

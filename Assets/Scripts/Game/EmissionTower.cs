using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EmissionTower : Tower
{
    [SerializeField] Transform beamPivot;       // ������ ������ ����.
    [SerializeField] float chargingTime;        // �������� ���� �ð�.

    LineRenderer line;

    float lookTime = 0.0f;              // ���� ��� �ð�.
    bool isLock;                        // ȸ�� ���� ����.

    private void Start()
    {
        line = GetComponent<LineRenderer>();
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
                    CreateBeam();
                }                                                
            }
        }
        else
        {

        }
    }

    private void CreateBeam()
    {
        line.positionCount = 2;
        line.SetPosition(0, beamPivot.position);
        line.SetPosition(1, beamPivot.position + (pivot.forward * attackRadius));
    }      

    protected override void AttackEnemy()
    {
        
    }
}

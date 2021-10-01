using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum PHASE
    {
        Ready,          // �غ� ����.
        Enemy,          // �� ����.
        GameClear,      // ���� Ŭ����.
        GameOver,       // ���� ����.
    }

    [SerializeField] EnemyManager enemyManager; // �� ���� �Ŵ���.
    [SerializeField] float phaseWaitTime;       // ���� ��������� ��ٸ��� �ð�.

    float nextPhaseTime;                        // ���� ������ �ð�.
    PHASE phase;

    private void Start()
    {
        nextPhaseTime += Time.time + phaseWaitTime;
        phase = PHASE.Ready;
    }

    
    private void Update()
    {
        switch(phase)
        {
            case PHASE.Ready:
                if (nextPhaseTime <= Time.time)
                {
                    Debug.Log("Start Enemy Phase");
                    enemyManager.OnStartEnemy(OnEndEnemyPhase);
                    phase = PHASE.Enemy;
                }
                break;

            case PHASE.Enemy:

                break;
        }
    }

    private void OnEndEnemyPhase()
    {
        Debug.Log("End Enemy Phase");

        nextPhaseTime = Time.time + phaseWaitTime;

        // �¸� üũ.
        // ���� üũ.

        phase = PHASE.Ready;
    }
}

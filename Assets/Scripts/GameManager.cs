using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    private enum PHASE
    {
        Ready,          // �غ� ����.
        Enemy,          // �� ����.
        GameClear,      // ���� Ŭ����.
        GameOver,       // ���� ����.
    }

    [SerializeField] EnemyManager enemyManager; // �� ���� �Ŵ���.
    [SerializeField] float phaseWaitTime;       // ���� ��������� ��ٸ��� �ð�.
    [SerializeField] int maxHP;
    [SerializeField] int startGold;             // ���� ���.
    
    public int HP { get; private set; }
    public int Gold { get; private set; }
    public int Wave { get; private set; }

    public bool IsAlive => HP > 0;
    
    float nextPhaseTime;                        // ���� ������ �ð�.
    PHASE phase;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        nextPhaseTime += Time.time + phaseWaitTime;
        phase = PHASE.Ready;
                
        Gold = startGold;
        HP = maxHP;
    }
    private void Update()
    {
        switch(phase)
        {
            case PHASE.Ready:
                if (nextPhaseTime <= Time.time)
                {
                    OnStartEnemyPahse();                    
                }
                break;

            case PHASE.Enemy:
                break;
            case PHASE.GameClear:
                break;
            case PHASE.GameOver:
                break;
        }
    }

    private void OnStartEnemyPahse()
    {
        phase = PHASE.Enemy;
        Wave += 1;

        Debug.Log("Start Enemy Phase");
        enemyManager.OnStartEnemy(OnEndEnemyPhase);
    }
    private void OnEndEnemyPhase()
    {
        if (!IsAlive)
            return;

        Debug.Log("End Enemy Phase");

        nextPhaseTime = Time.time + phaseWaitTime;

        // �¸� üũ.
        // ���� üũ.

        phase = PHASE.Ready;
    }

    public bool OnUseGold(int useGold)
    {
        if(Gold >= useGold)
        {
            Gold -= useGold;
            return true;
        }

        return false;
    }
    public void OnGetGold(int gold)
    {
        Gold += gold;
    }

    public void OnDamagedToHp()
    {
        if (!IsAlive)
            return;

        HP -= 1;
        if(HP <= 0)
        {
            Debug.Log("GameOver");
            phase = PHASE.GameOver;
        }
    }
}

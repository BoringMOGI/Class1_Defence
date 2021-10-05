using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        None = -1,
        Standard,
        Missile,
        Laser,
    }

    [Header("Info")]
    [SerializeField] TOWER_TYPE type;
    [SerializeField] Sprite sprite;
    [SerializeField] int price;

    public TOWER_TYPE Type => type;
    public Sprite towerSprite => sprite;
    public int Price => price;

    [Header("Search")]
    [SerializeField] protected Transform pivot;
    [SerializeField] protected LayerMask searchMask;    

    [Header("Combat")]    
    [SerializeField] protected float attackPower;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackRadius;


    protected Enemy target = null;
    

    void Update()
    {
        OnUpdate();
    }

    protected abstract void OnUpdate();
    protected virtual void LookTarget()
    {
        if (target == null)
            return;

        Vector3 direction = target.transform.position - pivot.position;     // (ȸ��) ���Ϸ� ����
        direction.Normalize();                                              // 0.0 ~ 1.0f ���� ������ ����ȭ.
        Quaternion lookAt = Quaternion.LookRotation(direction);             // (ȸ��) ���Ϸ� -> ���ʹϾ�
        //pivot.rotation = lookAt;

        // Lerp : ���� -> ������ ������ �ð��� ����� ���� ���� ���� �ش�.
        // Smooth rotation.
        pivot.rotation = Quaternion.Lerp(pivot.rotation, lookAt, 10f * Time.deltaTime);

        //transform.position = new Vector3(100, 100, 100);
        //transform.rotation = Quaternion.Euler(90, 90, 90);                // ���Ϸ� -> ���ʹϾ�.
    }
    protected void SearchEnemy()
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
    protected abstract void AttackEnemy();


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

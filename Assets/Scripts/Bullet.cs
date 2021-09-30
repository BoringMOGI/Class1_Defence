using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;

    private Transform pivot;
    private Enemy target;
    private float moveSpeed;
    private float power;

    public void Shoot(Enemy target, float moveSpeed, float power)
    {
        pivot = transform;

        this.target = target;
        this.moveSpeed = moveSpeed;
        this.power = power;
    }

    private void Update()
    {
        if(target == null)
        {
            Crushed();
            return;
        }

        MoveTo();
    }

    void MoveTo()
    {
        Vector3 direction = target.transform.position - pivot.position;
        Quaternion lookAt = Quaternion.LookRotation(direction);

        pivot.position = Vector3.MoveTowards(pivot.position, target.transform.position, moveSpeed * Time.deltaTime);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, lookAt, 10f * Time.deltaTime);

        // Ÿ�ٰ� ���� �Ÿ��� (����) ����� ���ٸ�.
        if (Vector3.Distance(pivot.position, target.transform.position) <= float.Epsilon)
        {
            HitTarget();
            Crushed();
        }
    }

    void HitTarget()
    {
        target.OnDamaged(power);
        ParticleSystem newParticle = Instantiate(vfx);
        newParticle.transform.position = transform.position;
        newParticle.Play();
    }
    void Crushed()
    {
        Destroy(gameObject);
    }
}

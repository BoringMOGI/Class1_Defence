using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTile : MonoBehaviour
{
    [SerializeField] Tower setTower;      // �� ���� ��ġ�� Ÿ��.
    public bool IsSetTower => setTower != null;

    public void Set(Tower newTower)
    {
        if(IsSetTower)
        {
            Debug.Log("Ÿ���� �̹� ��ġ�Ǿ�����.");
            return;
        }

        setTower = newTower;
        newTower.transform.position = transform.position;   // ��ġ �� ����.
        newTower.transform.rotation = transform.rotation;   // ȸ�� �� ����.
    }
    public Tower Remove()
    {
        Tower removeTower = setTower;
        setTower = null;

        return removeTower;
    }

}

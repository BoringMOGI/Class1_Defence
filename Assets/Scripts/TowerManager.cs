using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] LayerMask tileMask;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // ���콺�� ���� ��ġ�� Ray�� ��ȯ.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, tileMask))
            {
                SetTile setTile = hit.collider.GetComponent<SetTile>();
                CreateTower(setTile);
            }
        }
    }

    private void CreateTower(SetTile setTile)
    {
        // ������ Ÿ���� ���ų� Ÿ�Ͽ� �̹� ��ġ�� �Ǿ��ִ� ���.
        if (setTile == null || setTile.IsSetTower)
            return;

        if (GameManager.Instance.OnUseGold(10))
        {
            Tower newTower = Instantiate(towerPrefab, transform);
            setTile.Set(newTower);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public struct TowerData
    {
        public string name;
        public Tower.TOWER_TYPE type;
        public float power;
        public float attackRate;
        public float attackRadius;
        public string bulletName;
        public float bulletSpeed;
        public float chargeTime;
    }

    [SerializeField] TextAsset data;
    [SerializeField] TowerData towerDatas;

    private void Start()
    {
        Dictionary<string, string>[] towerDatas = CSVReader.ReadCSV(data);

        for (int i = 0; i < towerDatas.Length; i++)
        {
            Dictionary<string, string> tower = towerDatas[i];

            Debug.Log($"{i}��° Ÿ���� Ÿ���� : {tower["Type"]}");
            Debug.Log($"{i}��° Ÿ���� �Ŀ��� : {tower["Power"]}");
        }

        buttonManager.Setup(towerPrefabs, OnSelectedTower);
    }












    [SerializeField] TowerButtonManager buttonManager;
    [SerializeField] Tower[] towerPrefabs;
    [SerializeField] LayerMask tileMask;

    Tower.TOWER_TYPE selectedType = Tower.TOWER_TYPE.None;

    

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && selectedType != Tower.TOWER_TYPE.None)
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

        int needGold = towerPrefabs[(int)selectedType].Price;   // ���ϴ� Ÿ���� ����.
                
        if (GameManager.Instance.OnUseGold(needGold))           // ��� �Һ� �õ�.
        {
            Tower newTower = Instantiate(towerPrefabs[(int)selectedType], transform);
            setTile.Set(newTower);

            selectedType = Tower.TOWER_TYPE.None;
        }
    }
    private void OnSelectedTower(Tower.TOWER_TYPE type)
    {
        Debug.Log($"Selected : {type}");
        selectedType = type;
    }
}

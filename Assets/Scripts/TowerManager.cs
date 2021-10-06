using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;     // Tower Ŭ������ ������ �����ϰڴ�.

public class TowerData
{
    private Dictionary<string, string> data;

    public TowerData(Dictionary<string, string> data)
    {
        this.data = data;
    }

    public string GetData(string key)
    {
        return data[key];
    }
}

public class TowerManager : MonoBehaviour
{
    // �̱��� (SingleTon)
    static TowerManager instance;
    public static TowerManager Instance => instance;

    [SerializeField] TextAsset          data;
    [SerializeField] Tower[]            towerPrefabs;
    [SerializeField] LayerMask          tileMask;

    Dictionary<TOWER_TYPE, TowerData> towerDatas;           // ������ Ÿ�� ������.
    TOWER_TYPE selectedType = TOWER_TYPE.None;              // ���� ������ Ÿ���� Ÿ��.

    private void Awake()
    {
        instance = this;

        // CSV�����͸� �츮�� ���ϴ� �����ͷ� ����.
        towerDatas = new Dictionary<TOWER_TYPE, TowerData>();
        Dictionary<string, string>[] csvDatas = CSVReader.ReadCSV(data);
        for (int i = 0; i < csvDatas.Length; i++)
        {
            TowerData newData = new TowerData(csvDatas[i]);
            TOWER_TYPE type = (TOWER_TYPE)System.Enum.Parse(typeof(TOWER_TYPE), newData.GetData(KEY_TYPE));

            towerDatas.Add(type, newData);
        }
    }

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

    public TowerData GetData(TOWER_TYPE type)
    {
        return towerDatas[type];
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
            newTower.Setup(towerDatas[newTower.Type]);
            
            setTile.Set(newTower);
            selectedType = Tower.TOWER_TYPE.None;
        }
    }
    public void OnSelectedTower(Tower.TOWER_TYPE type)
    {
        Debug.Log($"Selected : {type}");
        selectedType = type;
    }
}

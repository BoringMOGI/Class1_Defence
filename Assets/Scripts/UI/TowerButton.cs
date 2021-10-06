using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public delegate void DlSelectedTower(Tower.TOWER_TYPE type);

    [SerializeField] Image towerImage;
    [SerializeField] Text priceText;

    Tower.TOWER_TYPE type;

    public void Setup(TowerData towerData)
    {
        // �����͸� �޾ƿ� ���������� �Ľ�.
        type = (Tower.TOWER_TYPE)System.Enum.Parse(typeof(Tower.TOWER_TYPE), towerData.GetData(Tower.KEY_TYPE));

        // towerImage.sprite = tower.towerSprite;
        priceText.text = string.Format("{0:#,##0}", towerData.GetData(Tower.KEY_PRICE));

        // ��ư�� �̺�Ʈ ���.
        GetComponent<Button>().onClick.AddListener(() => TowerManager.Instance.OnSelectedTower(type));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo {
    public string itemPath;
    public int itemAmount;

    public ItemInfo(string itemPath, int itemAmount)
    {
        this.itemPath = itemPath;
        this.itemAmount = itemAmount;
    }
}
public class PlayerReward : MonoBehaviour
{
    [SerializeField] Image itemImg;
    [SerializeField] GameObject content;

    //Dictionary<string, int> items;
    //ItemInfo[] cleanedItems;
    List<ItemInfo> cleanedItems = new List<ItemInfo>();
    List<Image> initImgs = new List<Image>();

    //Dictionary<string, int> aa;
    private void Start()
    {
        gameObject.transform.SetAsLastSibling();
        
        for (int i = 0; i < BattleController.Instance.getItems.Count; i++) {
            //int temp = BattleController.Instance.getItems.FindIndex(BattleController)
            if(cleanedItems.Count == 0)
            {
                cleanedItems.Add(new ItemInfo(BattleController.Instance.getItems[i], 1));
            } else
            {
                for (int j = 0; j < cleanedItems.Count; j++)
                {
                    ItemInfo temp = new ItemInfo(BattleController.Instance.getItems[i], 1);

                    //�޾ƿ� ������ �̹����� ���� ����� �̹��� �߿� ���� ���
                    if (cleanedItems[j].itemPath.Equals(temp.itemPath))
                    {
                        cleanedItems[j].itemAmount++;
                        break;
                    }

                    //�޾ƿ� ������ �̹����� ���� ����� �̹����� ��� �ٸ� ���
                    if (j == cleanedItems.Count - 1 && !cleanedItems[j].itemPath.Equals(temp.itemPath))
                    {
                        cleanedItems.Add(temp);
                        break;
                    }
                }
            }
        }
        
        //������ �̹��� ǥ��
        for(int i = 0; i < cleanedItems.Count; i++)
        {

            initImgs.Add(Instantiate(itemImg, content.gameObject.transform));
            if (initImgs[i].TryGetComponent<Image>(out Image img))
            {
                img.sprite = Resources.Load<Sprite>(cleanedItems[i].itemPath);

                if (initImgs[i].transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI potionNameText)) {
                    potionNameText.text = GameManager.Instance.gameExcelData.PotionData.Find(x => x.PotionName.Equals(cleanedItems[i].itemPath)).PotionKoreanName;
                }

                if (initImgs[i].transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI AmountText))
                {
                    AmountText.text = cleanedItems[i].itemAmount.ToString();
                }
            }
        }
        Debug.Log(cleanedItems.Count);
        //��� �̹��� ǥ��
        initImgs.Add(Instantiate(itemImg, content.gameObject.transform));
        if(initImgs[initImgs.Count-1].TryGetComponent<Image>(out Image goldImg))
        {
            goldImg.sprite = Resources.Load<Sprite>("Gold");

            if (initImgs[initImgs.Count-1].transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI goldText))
            {
                goldText.text = BattleController.Instance.totalGold.ToString();
            }

            if (initImgs[initImgs.Count - 1].transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tempText))
            {
                tempText.text = "";
            }
        }

        //EXP �̹��� ǥ��
        initImgs.Add(Instantiate(itemImg, content.gameObject.transform));
        if (initImgs[initImgs.Count - 1].TryGetComponent<Image>(out Image expImg))
        {
            expImg.sprite = Resources.Load<Sprite>("EXP");

            if (initImgs[initImgs.Count - 1].transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI expText))
            {
                expText.text = BattleController.Instance.totalEXP.ToString();
            }

            if (initImgs[initImgs.Count - 1].transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tempText))
            {
                tempText.text = "";
            }
        }
    }

}

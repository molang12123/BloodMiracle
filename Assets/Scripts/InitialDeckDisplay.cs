using UnityEngine;
using UnityEngine.UI;

public class InitialDeckDisplay : MonoBehaviour
{
    public GameObject cardPrefab;  // 卡牌模板
    public Transform cardGrid;     // 卡牌容器

    void Start()
    {
        DisplayInitialDeck();
    }

    void DisplayInitialDeck()
    {
        //for (int i = 0; i < 12; i++)
        //{
        //    GameObject card = Instantiate(cardPrefab, cardGrid);  // 生成卡牌

        //    // 模拟数据
        //    string name = "Card " + (i + 1);
        //    string desc = "Description of card " + (i + 1);
        //    string type = (i < 5) ? "Attack" : (i < 10) ? "Defense" : "Skill";

        //    // 填充 UI 内容
        //    card.transform.Find("CardName").GetComponent<Text>().text = name;
        //    card.transform.Find("Description").GetComponent<Text>().text = desc;
        //    card.transform.Find("Type").GetComponent<Text>().text = type;

        //    Debug.Log("生成了卡牌：" + name);
        //}
    }
}

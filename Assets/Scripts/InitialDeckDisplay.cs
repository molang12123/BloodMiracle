using UnityEngine;
using UnityEngine.UI;

public class InitialDeckDisplay : MonoBehaviour
{
    public GameObject cardPrefab;  // ����ģ��
    public Transform cardGrid;     // ��������

    void Start()
    {
        DisplayInitialDeck();
    }

    void DisplayInitialDeck()
    {
        //for (int i = 0; i < 12; i++)
        //{
        //    GameObject card = Instantiate(cardPrefab, cardGrid);  // ���ɿ���

        //    // ģ������
        //    string name = "Card " + (i + 1);
        //    string desc = "Description of card " + (i + 1);
        //    string type = (i < 5) ? "Attack" : (i < 10) ? "Defense" : "Skill";

        //    // ��� UI ����
        //    card.transform.Find("CardName").GetComponent<Text>().text = name;
        //    card.transform.Find("Description").GetComponent<Text>().text = desc;
        //    card.transform.Find("Type").GetComponent<Text>().text = type;

        //    Debug.Log("�����˿��ƣ�" + name);
        //}
    }
}

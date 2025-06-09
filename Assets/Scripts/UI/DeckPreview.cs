using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class DeckPreview : MonoBehaviour
{
    public Transform cardGrid;
    public GameObject cardPrefab;

    void Start()
    {
        CharacterData selected = PlayerData.selectedCharacter;


        if (selected == null)
        {
            Debug.LogError("δ���� PlayerData.selectedCharacter��Ĭ��ʹ�� Warrior ���ݣ�");
            // ���Ϊ�գ�������Ĭ���߼�
            string selectedClass = PlayerPrefs.GetString("SelectedClass", "Warrior");
            List<CardData> fallbackCards = CardDatabase.GetInitialDeck(selectedClass);
            DisplayCards(fallbackCards);
        }
        else
        {
            DisplayCards(selected.attackCards);
            DisplayCards(selected.defenseCards);
            DisplayCards(selected.skillCards);
        }
    }

    void DisplayCards(List<CardData> cards)
    {
        foreach (CardData card in cards)
        {
            GameObject obj = Instantiate(cardPrefab, cardGrid);

            obj.transform.Find("CardName").GetComponent<Text>().text = card.cardName;
            obj.transform.Find("Description").GetComponent<Text>().text = card.description;
            obj.transform.Find("Type").GetComponent<Text>().text = card.type;
            obj.transform.Find("EnergyCostText").GetComponent<TextMeshProUGUI>().text = card.energyCost.ToString();



            Color color = Color.white;
            if (card.type == "Attack") color = Color.red;
            else if (card.type == "Defense") color = Color.blue;
            else if (card.type == "Skill") color = Color.magenta;

            obj.transform.Find("Type").GetComponent<Text>().color = color;
        }
    }

    public void OnConfirm()
    {
        // ��һ��������ǰѡ�еĽ�ɫ���� SelectedCharacter
        SelectedCharacter.Instance.characterData = PlayerData.selectedCharacter;

        if (SelectedCharacter.Instance.characterData == null)
        {
            Debug.LogError("δѡ���ɫ���޷���ʼ�� GameData��");
            return;
        }

        // �ڶ��������� GameData ��ʼ������ʱ�� SelectedCharacter �Ѿ������ݣ�
        GameData.Instance.ResetData();

        // ��־ȷ��
        Debug.Log($"GameData ��ʼ����� -> HP: {GameData.Instance.currentHP}/{GameData.Instance.maxHP}, Gold: {GameData.Instance.gold}");

        // �������������ͼ����
        SceneManager.LoadScene("Map");
    }



    public void OnBack()
    {
        SceneManager.LoadScene("ClassSelection");
    }
}

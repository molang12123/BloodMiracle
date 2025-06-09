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
            Debug.LogError("未设置 PlayerData.selectedCharacter，默认使用 Warrior 数据！");
            // 如果为空，降级回默认逻辑
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
        // 第一步：将当前选中的角色传给 SelectedCharacter
        SelectedCharacter.Instance.characterData = PlayerData.selectedCharacter;

        if (SelectedCharacter.Instance.characterData == null)
        {
            Debug.LogError("未选择角色，无法初始化 GameData！");
            return;
        }

        // 第二步：调用 GameData 初始化（这时候 SelectedCharacter 已经有数据）
        GameData.Instance.ResetData();

        // 日志确认
        Debug.Log($"GameData 初始化完成 -> HP: {GameData.Instance.currentHP}/{GameData.Instance.maxHP}, Gold: {GameData.Instance.gold}");

        // 第三步：进入地图场景
        SceneManager.LoadScene("Map");
    }



    public void OnBack()
    {
        SceneManager.LoadScene("ClassSelection");
    }
}

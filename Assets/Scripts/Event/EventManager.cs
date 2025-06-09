using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI eventNameText;
    public TextMeshProUGUI eventDescriptionText;
    public Transform choiceButtonContainer;
    public GameObject choiceButtonPrefab;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button continueButton;

    private EventData currentEvent;

    void Start()
    {
        LoadRandomEvent();
    }

    void LoadRandomEvent()
    {
        EventData[] allEvents = Resources.LoadAll<EventData>("Events");
        currentEvent = allEvents[Random.Range(0, allEvents.Length)];

        eventNameText.text = currentEvent.eventName;
        eventDescriptionText.text = currentEvent.description;

        foreach (Transform child in choiceButtonContainer)
        {
            Destroy(child.gameObject); // 清理旧按钮
        }

        foreach (EventChoice choice in currentEvent.choices)
        {
            GameObject btnObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.description;
            btnObj.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choice));
        }

        resultPanel.SetActive(false);
    }

    void OnChoiceSelected(EventChoice choice)
    {
        GameData.Instance.currentHP += choice.hpChange;
        GameData.Instance.currentHP = Mathf.Clamp(GameData.Instance.currentHP, 0, GameData.Instance.maxHP);

        if (choice.goldChange > 0)
        {
            GameData.Instance.AddGold(choice.goldChange);
        }
        else if (choice.goldChange < 0)
        {
            GameData.Instance.SpendGold(-choice.goldChange);
        }

        Debug.Log($"事件后血量：{GameData.Instance.currentHP}/{GameData.Instance.maxHP}");
        Debug.Log($"事件后金币：{GameData.Instance.gold}");

        resultPanel.SetActive(true);
        resultText.text = choice.resultMessage;

        foreach (Transform child in choiceButtonContainer)
        {
            child.gameObject.SetActive(false);
        }

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Map");
        });
    }
}

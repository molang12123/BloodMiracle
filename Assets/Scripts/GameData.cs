using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public Dictionary<int, List<int>> pathMap = new Dictionary<int, List<int>>();
    public int currentNodeIndex = 0;
    public int tempBonusDamageNextBattle = 0;

    public int maxHP;
    public int currentHP;
    public int gold = 100; // ��ʼ��ң��ɰ������


    private void InitPaths()
    {
        pathMap.Clear();
        pathMap[0] = new List<int> { 1, 2 };
        pathMap[1] = new List<int> { 3 };
        pathMap[2] = new List<int> { 4 };
        pathMap[3] = new List<int> { 5 };
        pathMap[4] = new List<int> { 6 };
        pathMap[5] = new List<int> { 7 };
        pathMap[6] = new List<int> { 8 };
        pathMap[7] = new List<int> { 9 };
        pathMap[8] = new List<int> { 10 };
        pathMap[9] = new List<int> { 11 };
        pathMap[10] = new List<int> { 11 };
        pathMap[11] = new List<int>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitPaths();
    }

    public void ResetData()
    {
        CharacterData character = SelectedCharacter.Instance.characterData;

        if (character == null)
        {
            Debug.LogError("ResetData ʱδ�ҵ���ɫ���ݣ�");
            return;
        }

        maxHP = character.maxHP;
        currentHP = character.currentHP;
        gold = character.startingGold;

        currentNodeIndex = 0; // ���õ����
        tempBonusDamageNextBattle = 0;

        InitPaths(); //���³�ʼ��·������ѡ�������ͼϵͳ��ôд��

        Debug.Log($"GameData �������: HP = {currentHP}/{maxHP}, Gold = {gold}");
    }


}
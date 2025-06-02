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
    public int gold = 100; // 初始金币，可按需调整


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
            Debug.LogError("ResetData 时未找到角色数据！");
            return;
        }

        maxHP = character.maxHP;
        currentHP = character.currentHP;
        gold = character.startingGold;

        currentNodeIndex = 0; // 重置到起点
        tempBonusDamageNextBattle = 0;

        InitPaths(); //重新初始化路径（可选，看你地图系统怎么写）

        Debug.Log($"GameData 设置完成: HP = {currentHP}/{maxHP}, Gold = {gold}");
    }


}
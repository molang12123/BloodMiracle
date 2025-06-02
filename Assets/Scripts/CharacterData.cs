using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("职业名称")]
    public string className;
    public int currentHP;
    public int maxHP;
    public int startingGold;

    // 以下字段可保留用于拓展或在编辑器中可视化
    [Header("用于显示角色介绍或图标等")]
    public Sprite characterPortrait;
    public string characterDescription;

    // 如果你仍希望支持拖入卡牌也可以保留这部分（当前不会用到）
    public List<CardData> attackCards;
    public List<CardData> defenseCards;
    public List<CardData> skillCards;
}

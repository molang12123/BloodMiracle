using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("ְҵ����")]
    public string className;
    public int currentHP;
    public int maxHP;
    public int startingGold;

    // �����ֶοɱ���������չ���ڱ༭���п��ӻ�
    [Header("������ʾ��ɫ���ܻ�ͼ���")]
    public Sprite characterPortrait;
    public string characterDescription;

    // �������ϣ��֧�����뿨��Ҳ���Ա����ⲿ�֣���ǰ�����õ���
    public List<CardData> attackCards;
    public List<CardData> defenseCards;
    public List<CardData> skillCards;
}

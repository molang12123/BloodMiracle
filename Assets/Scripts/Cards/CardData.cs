using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SkillEffectType
{
    None,
    DrawCards,
    GainEnergy,
    Heal,
    Invisibility,
    TempStrength,
    DoubleNextAttack,
    TempCardValueBoost,
    TempDefenseBoost,
}
[System.Serializable]
public class SkillEffectEntry
{
    public SkillEffectType effectType;
    public int effectValue;

}

[CreateAssetMenu(fileName = "CardData", menuName = "Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public string description;
    public string type; // Attack, Defense, Skill
    public int value;
    public int energyCost = 1;

    [SerializeField]
    public List<SkillEffectEntry> skillEffects = new List<SkillEffectEntry>();
}


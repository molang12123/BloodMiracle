using System.Collections.Generic;
using UnityEngine;

public static class CardDatabase
{
    public static List<CardData> GetInitialDeck(string className)
    {
        List<CardData> cards = new List<CardData>();

        if (className == "Warrior")
        {
            for (int i = 0; i < 5; i++)
            {
                var attack = ScriptableObject.CreateInstance<CardData>();
                attack.cardName = "Slash";
                attack.description = "Deals 4 damage";
                attack.type = "Attack";
                attack.value = 4;
                attack.energyCost = 1;
                cards.Add(attack);
            }

            for (int i = 0; i < 5; i++)
            {
                var block = ScriptableObject.CreateInstance<CardData>();
                block.cardName = "Block";
                block.description = "Gain 5 armor";
                block.type = "Defense";
                block.value = 5;
                block.energyCost = 1;
                cards.Add(block);
            }

            var berserk = ScriptableObject.CreateInstance<CardData>();
            berserk.cardName = "Berserk";
            berserk.description = "Attack and Defense cards gain +3 point for 1 turn.";
            berserk.type = "Skill";
            berserk.value = 4;
            berserk.energyCost = 1;
            berserk.skillEffects = new List<SkillEffectEntry>();

            berserk.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.TempStrength,
                effectValue = 3
            });

            berserk.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.TempDefenseBoost,
                effectValue = 3
            });

            cards.Add(berserk);

            var warCry = ScriptableObject.CreateInstance<CardData>();
            warCry.cardName = "War Cry";
            warCry.description = "Draw 2 cards";
            warCry.type = "Skill";
            warCry.value = 0;
            warCry.energyCost = 1;
            warCry.skillEffects = new List<SkillEffectEntry>();
            warCry.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.DrawCards,
                effectValue = 2
            });
            cards.Add(warCry);
        }
        else if (className == "Archer")
        {
            for (int i = 0; i < 5; i++)
            {
                var shoot = ScriptableObject.CreateInstance<CardData>();
                shoot.cardName = "Shoot";
                shoot.description = "Deals 4 damage";
                shoot.type = "Attack";
                shoot.value = 4;
                shoot.energyCost = 1;
                cards.Add(shoot);
            }

            for (int i = 0; i < 5; i++)
            {
                var dodge = ScriptableObject.CreateInstance<CardData>();
                dodge.cardName = "Dodge";
                dodge.description = "Gain 4 armor";
                dodge.type = "Defense";
                dodge.value = 4;
                dodge.energyCost = 1;
                cards.Add(dodge);
            }

            var quickDraw = ScriptableObject.CreateInstance<CardData>();
            quickDraw.cardName = "Quick Draw";
            quickDraw.description = "Draw 1 card and gain 1 energy";
            quickDraw.type = "Skill";
            quickDraw.value = 0;
            quickDraw.energyCost = 0;
            quickDraw.skillEffects = new List<SkillEffectEntry>();
            quickDraw.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.DrawCards,
                effectValue = 1
            });
            quickDraw.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.GainEnergy,
                effectValue = 1
            });
            cards.Add(quickDraw);

            var focus = ScriptableObject.CreateInstance<CardData>();
            focus.cardName = "Focus";
            focus.description = "The next attack deals double damage";
            focus.type = "Skill";
            focus.value = 0;
            focus.energyCost = 1;
            focus.skillEffects = new List<SkillEffectEntry>();
            focus.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.TempStrength,
                effectValue = 5
            });
            cards.Add(focus);
        }
        else if (className == "Assassin")
        {
            for (int i = 0; i < 5; i++)
            {
                var stab = ScriptableObject.CreateInstance<CardData>();
                stab.cardName = "Stab";
                stab.description = "Deals 5 damage";
                stab.type = "Attack";
                stab.value = 5;
                stab.energyCost = 1;
                cards.Add(stab);
            }

            for (int i = 0; i < 5; i++)
            {
                var dodge = ScriptableObject.CreateInstance<CardData>();
                dodge.cardName = "Dodge";
                dodge.description = "Gain 3 armor";
                dodge.type = "Defense";
                dodge.value = 3;
                dodge.energyCost = 1;
                cards.Add(dodge);
            }

            var backstab = ScriptableObject.CreateInstance<CardData>();
            backstab.cardName = "Backstab";
            backstab.description = "Double damage for next hit";
            backstab.type = "Skill";
            backstab.value = 0;
            backstab.energyCost = 1;
            backstab.skillEffects = new List<SkillEffectEntry>();
            backstab.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.DoubleNextAttack,
                effectValue = 0 // 不需要数值，只用类型标记
            });
            cards.Add(backstab);

            var smoke = ScriptableObject.CreateInstance<CardData>();
            smoke.cardName = "Smoke";
            smoke.description = "Invisible for one turn";
            smoke.type = "Skill";
            smoke.value = 0;
            smoke.energyCost = 1;
            smoke.skillEffects = new List<SkillEffectEntry>();
            smoke.skillEffects.Add(new SkillEffectEntry
            {
                effectType = SkillEffectType.Invisibility,
                effectValue = 0
            });
            cards.Add(smoke);
        }

        return cards;
    }
}

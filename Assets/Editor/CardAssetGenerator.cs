using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CardAssetGenerator
{
    [MenuItem("Tools/Generate Card Assets")]
    public static void GenerateCards()
    {
        string[] classNames = { "Warrior", "Archer", "Assassin" };

        foreach (string className in classNames)
        {
            List<CardData> cards = CardDatabase.GetInitialDeck(className);

            string folderPath = $"Assets/Resources/Cards/{className}";
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            if (!AssetDatabase.IsValidFolder($"Assets/Resources/Cards"))
                AssetDatabase.CreateFolder("Assets/Resources", "Cards");
            if (!AssetDatabase.IsValidFolder(folderPath))
                AssetDatabase.CreateFolder("Assets/Resources/Cards", className);

            foreach (CardData card in cards)
            {
                CardData asset = ScriptableObject.CreateInstance<CardData>();
                asset.cardName = card.cardName;
                asset.description = card.description;
                asset.type = card.type;

                string safeName = card.cardName.Trim().Replace(" ", "_"); // ����ո���ɳ�ͻ
                string assetPath = $"{folderPath}/{safeName}.asset";

                AssetDatabase.CreateAsset(asset, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("���п��������ɵ� Resources/Cards �£�");
    }
}

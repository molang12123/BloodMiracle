using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassSelection : MonoBehaviour
{
    public CharacterData warriorData;
    public CharacterData archerData;
    public CharacterData assassinData;

    public void SelectWarrior()
    {
        PlayerData.selectedCharacter = warriorData;
        PlayerPrefs.SetString("SelectedClass", "Warrior");
        GameData.Instance.maxHP = 0;
        GameData.Instance.currentHP = 0;
        SceneManager.LoadScene("DeckPreview");
    }

    public void SelectArcher()
    {
        PlayerData.selectedCharacter = archerData;
        PlayerPrefs.SetString("SelectedClass", "Archer");
        GameData.Instance.maxHP = 0;
        GameData.Instance.currentHP = 0;
        SceneManager.LoadScene("DeckPreview");
    }

    public void SelectAssassin()
    {
        PlayerData.selectedCharacter = assassinData;
        PlayerPrefs.SetString("SelectedClass", "Assassin");
        GameData.Instance.maxHP = 0;
        GameData.Instance.currentHP = 0;
        SceneManager.LoadScene("DeckPreview");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

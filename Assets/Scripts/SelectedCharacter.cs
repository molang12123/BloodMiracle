using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    public static SelectedCharacter Instance { get; private set; }

    public CharacterData characterData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

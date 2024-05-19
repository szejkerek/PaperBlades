using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamManagementInterface : Singleton<TeamManagementInterface>
{
    public Character currentCharacter;
    [SerializeField] CharacterPanel characterPanel;
    [SerializeField] List<TeamMemberInterface> charactersSlot;
    [SerializeField] TMP_Text money;
    [SerializeField] Button returnBtn;


    private void ReturnBehaviour()
    {
        SceneLoader.Instance.LoadScene(SceneConstants.ChooseMapScene);
    }
    private void Start()
    {
        money.text = SavableDataManager.Instance.data.money.ToString();
        returnBtn.onClick.AddListener(ReturnBehaviour);
        FillStartingCharacters();
    }

    private void FillStartingCharacters()
    {
        List<Character> savedCharacters = SavableDataManager.Instance.data.characters;
        for (int i = 0; i < charactersSlot.Count && i < savedCharacters.Count; i++)
        {
            charactersSlot[i].SetCharacter(savedCharacters[i]);
        }
    }

    public void SetCurrentCharacterDisplay(Character character)
    {
        currentCharacter = character;
        characterPanel.SetupView(character);
    }
}

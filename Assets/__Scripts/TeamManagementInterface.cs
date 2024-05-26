using GordonEssentials;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamManagementInterface : Singleton<TeamManagementInterface>
{
    public Character currentCharacter;
    [SerializeField] CharacterPanel characterPanel;
    [SerializeField] List<CharacterView> charactersSlot;
    [SerializeField] TMP_Text money;
    [SerializeField] Button moneyButton;
    [SerializeField] Button returnBtn;


    private void ReturnBehaviour()
    {
        SceneLoader.Instance.LoadScene(SceneConstants.ChooseLevelScene);
    }
    private void Start()
    {
        money.text = SavableDataManager.Instance.data.playerResources.Money.ToString();
        ResourcesHolder.OnResourcesUpdated += UpdateMoneyDisplay;
        returnBtn.onClick.AddListener(ReturnBehaviour);
        FillStartingCharacters();
        moneyButton.onClick.AddListener(AddMoney);
        characterPanel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ResourcesHolder.OnResourcesUpdated -= UpdateMoneyDisplay;
    }

    void UpdateMoneyDisplay()
    {
        money.text = SavableDataManager.Instance.data.playerResources.Money.ToString();
    }

    private void AddMoney()
    {
        SavableDataManager.Instance.data.playerResources.AddMoney(500);
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
        characterPanel.gameObject.SetActive(true);
    }
}
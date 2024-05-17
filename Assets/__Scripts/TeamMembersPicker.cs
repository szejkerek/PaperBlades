using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMembersPicker : MonoBehaviour
{
    public Character currentCharacter;
    [SerializeField] List<TeamMember> charactersSlot;

    private void Start()
    {
        List<Character> savedCharacters = SavableDataManager.Instance.data.characters;
        for (int i = 0; i < charactersSlot.Count && i < savedCharacters.Count; i++) {
            charactersSlot[i].SetCharacter(savedCharacters[i]);
        }

    }
}
﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class MapPlayerTracker : Singleton<MapPlayerTracker>
{
    public Action<int> OnPopupChooose;
    public bool lockAfterSelecting = false;
    public MapManager mapManager;
    public MapDrawerUI view;
    public bool locked;
    public PopupAllEventsSO events;
    public PassiveAllEffectSO passiveEffects;

    public void SendPlayerToNode(MapNodeUI mapNode)
    {
        if (locked)
        {
            return;
        }

        locked = lockAfterSelecting;
        mapManager.currentMap.path.Add(mapNode.mapNode.locationOnMap);
        SavableDataManager.Instance.data.map = mapManager.currentMap;
        SavableDataManager.Instance.Save();
        view.SetAttainableNodes();
        view.SetLineColors();

        List<Character> choices = new()
        { 
            SavableDataManager.Instance.data.team.General, 
            SavableDataManager.Instance.data.team.TeamMembers[0], 
            SavableDataManager.Instance.data.team.TeamMembers[1] };

        

        switch (mapNode.mapNode.type)
        {
            case MapNodeType.Arena:
                int sceneIndex = SceneConstants.Level_1;
                if(mapManager.currentMap.path.Count > 1)
                {
                    sceneIndex = UnityEngine.Random.Range(SceneConstants.Level_1_2, SceneConstants.Level_1_3 + 1);
                }

                SceneLoader.Instance.LoadScene(sceneIndex); //TODO: Losowanie poziomu pomiędzy dostępnymi
                break;
            case MapNodeType.Armory:
                PopupController.Instance.PopupPanel.ChooseModal(choices, TryAddNewCharacter, "Choose new ally", "Our elite squad has the opportunity to recruit a new, exceptional ally. Each candidate brings unique skills and experience that can change the course of our missions. Carefully consider their abilities and stats to choose the warrior who will best complement our team.");
                break;
            case MapNodeType.Boss:
                PopupController.Instance.PopupPanel.ShowAsEvent(events.BossEvents.SelectRandomElement());
                break;
            case MapNodeType.Experience:
                PopupController.Instance.PopupPanel.ShowAsEvent(events.ExperienceEvents.SelectRandomElement());
                break;
            case MapNodeType.Forge:
                PopupController.Instance.PopupPanel.ShowAsEvent(events.ForgeEvents.SelectRandomElement());
                break;
            case MapNodeType.Temple:
                PopupController.Instance.PopupPanel.ShowAsEvent(events.TempleEvents.SelectRandomElement());
                break;
            case MapNodeType.WeaponReroll:

                List<Character> choices2 = new()
                {
                    SavableDataManager.Instance.data.team.TeamMembers[0],
                    SavableDataManager.Instance.data.team.TeamMembers[1],
                    SavableDataManager.Instance.data.team.TeamMembers[2] };

                PassiveEffectSO passiveEffectRandom = passiveEffects.allPassiveEffectSO.SelectRandomElement();
                string passiveName = passiveEffectRandom.GetName();
                string header = String.Format("Choose ally to get <b>{0}</b> passive", passiveName);
                string content = String.Format("Now we can give <b>{0}</b> passive to one of our party memebers", passiveName);

                PopupController.Instance.PopupPanel.ChooseModal(passiveEffectRandom, choices2, TryAddPasiveToCharacter, header, content);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void TryAddNewCharacter(Character character)
    {
        if (character == null)
        {
            Debug.Log("Couldnt get Character from modal.");
            return;
        }
        SavableDataManager.Instance.data.team.AddCharacter(character);
    }

    private void TryAddPasiveToCharacter(Character character, PassiveEffectSO newPasive)
    {
        if (character == null)
        {
            Debug.Log("Couldnt get Character from modal.");
            return;
        }
        character.PassiveEffects.Add(newPasive);
    }

}

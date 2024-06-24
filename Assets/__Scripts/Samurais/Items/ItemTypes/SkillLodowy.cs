﻿using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Skill/Lodowy")]
public class SkillLodowy : ItemSO
{
    [Range(0f,1f)] public float strenght;
    public float timer;
    public float range;
    public override void Use(IUnit target, IUnit origin)
    {
        SamuraiEffectsManager manager = origin.gameObject.GetComponent<SamuraiEffectsManager>();
        if (manager == null)
            return;

        if (!Cooldown.IsOffCooldown())
            return;

        foreach (Enemy enemy in manager.Enemies)
        {
            if (!UnitInRange(origin, enemy, range))
                continue;

            StatusManager statusManager = enemy.GetComponent<StatusManager>();
            statusManager.ApplyWeakness(timer, strenght);
        }
        Cooldown.ResetTimers();
    }
}
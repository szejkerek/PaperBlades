﻿using UnityEngine;

[CreateAssetMenu(menuName = "Character/Skill/Swietlisty")]
public class SkillSwietlisty : ItemSO
{
    public int damage;
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

            enemy.TakeDamage(damage);
        }
        Cooldown.ResetTimers();
    }
}

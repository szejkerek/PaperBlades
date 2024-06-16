using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Samurai : MonoBehaviour, IUnit
{
    public bool IsAlly => true;
    public Character Character { get; private set; }

    SamuraiStylizer samuraiStylizer;
    SamuraiRenderers samuraiRenderer;
    SamuraiEffectsManager samuraiEffectsManager;

    public void SetCharacterData(Character character)
    {
        this.Character = character;
    }

    private void Start()
    {
        samuraiStylizer = GetComponent<SamuraiStylizer>();
        samuraiRenderer = GetComponentInChildren<SamuraiRenderers>();
        samuraiEffectsManager = GetComponent<SamuraiEffectsManager>();
        Character.SamuraiVisuals.Apply(samuraiStylizer.Renderers, Character);
        samuraiEffectsManager.Initialize(Character);
    }

    public void UseWeapon(IUnit target)
    {
        Character.Weapon.itemData.Execute(target);
    }

    public void UseSkill(IUnit target)
    {
        Character.Skill.itemData.Execute(target);
    }

    public void TakeDamage(int valueHP)
    {
        Character.LostHealth += valueHP;
        CharacterStats stats = Character.GetStats();
        if (stats.Health <= Character.LostHealth)
        {
            Character.LostHealth = stats.Health;
        }

        samuraiRenderer.SetDamagePercent((float)Character.LostHealth/ (float)stats.Health);

        Character.OnHealthChange?.Invoke();

    }

    public void HealUnit(int valueHP)
    {
        Character.LostHealth -= valueHP;
        if (Character.LostHealth < 0)
        {
            Character.LostHealth = 0;
            Debug.Log("I'm full healed");
        }
        Character.OnHealthChange?.Invoke();
    }

    public void HealToMax()
    {
        Character.LostHealth = 0;
        Character.OnHealthChange?.Invoke();
    }

    public int GetMaxHealth()
    {
        return Character.GetStats().Health;
    }
}

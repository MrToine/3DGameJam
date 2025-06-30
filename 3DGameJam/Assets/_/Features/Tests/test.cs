using Character.Runtime;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private CharacterStat _characterStat;

    private void Start()
    {
        if (_characterStat == null)
        {
            Debug.LogError("TestPlayerStat : CharacterStat n'est pas assigné !");
            return;
        }

        Debug.Log("▶️ Stat initiales");
        Debug.Log($"Vie : {_characterStat.CurrentHealth} / {_characterStat.GetMaxHealth()}");
        Debug.Log($"Stamina : {_characterStat.CurrentStamina}");
        Debug.Log($"XP : {_characterStat.CurrentXp}");
        Debug.Log($"Niveau : {_characterStat.CurrentLevel}");

        _characterStat.TakeDamage(20f);
        Debug.Log("💥 20 dégâts reçus");
        Debug.Log($"Vie : {_characterStat.CurrentHealth} / {_characterStat.GetMaxHealth()}");

        _characterStat.TakeXp(50);
        Debug.Log("📈 50 XP gagnés");
        Debug.Log($"XP : {_characterStat.CurrentXp}");

        _characterStat.LevelUp();
        Debug.Log("⬆️ Niveau augmenté !");
        Debug.Log($"Niveau : {_characterStat.CurrentLevel}");
    }
    
    [ContextMenu("Infliger 20 degats")]
    public void Debug_TakeDamage()
    {
        _characterStat.TakeDamage(20f);
    }
    
    [ContextMenu("Soigner de 10 PV")]
    public void Debug_TakeHealth()
    {
        _characterStat.Heal(10f);
    }
}

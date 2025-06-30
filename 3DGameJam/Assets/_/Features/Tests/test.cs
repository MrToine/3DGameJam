using Player.Runtime;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private PlayerStat _playerStat;

    private void Start()
    {
        if (_playerStat == null)
        {
            Debug.LogError("TestPlayerStat : PlayerStat n'est pas assigné !");
            return;
        }

        Debug.Log("▶️ Stat initiales");
        Debug.Log($"Vie : {_playerStat.CurrentHealth} / {_playerStat.GetMaxHealth()}");
        Debug.Log($"Stamina : {_playerStat.CurrentStamina}");
        Debug.Log($"XP : {_playerStat.CurrentXp}");
        Debug.Log($"Niveau : {_playerStat.CurrentLevel}");

        _playerStat.TakeDamage(20f);
        Debug.Log("💥 20 dégâts reçus");
        Debug.Log($"Vie : {_playerStat.CurrentHealth} / {_playerStat.GetMaxHealth()}");

        _playerStat.TakeXp(50);
        Debug.Log("📈 50 XP gagnés");
        Debug.Log($"XP : {_playerStat.CurrentXp}");

        _playerStat.LevelUp();
        Debug.Log("⬆️ Niveau augmenté !");
        Debug.Log($"Niveau : {_playerStat.CurrentLevel}");
    }
    
    [ContextMenu("Infliger 20 degats")]
    public void Debug_TakeDamage()
    {
        _playerStat.TakeDamage(20f);
    }
    
    [ContextMenu("Soigner de 10 PV")]
    public void Debug_TakeHealth()
    {
        _playerStat.Heal(10f);
    }
}

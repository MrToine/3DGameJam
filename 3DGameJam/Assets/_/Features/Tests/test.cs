using Character.Runtime;
using Core.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using WaveSystem.Runtime;

public class test : MonoBehaviour
{
    [SerializeField] private CharacterStat _characterStat;
    public WaveManager _waveManager;
    
    private void Start()
    {
        if (_characterStat == null)
        {
            Debug.LogError("TestPlayerStat : CharacterStat n'est pas assigné !");
            return;
        }
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

    [ContextMenu("Charger le loading scene")]
    public void Debug_LoadScene()
    {
        GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    [ContextMenu("Lancer une vague d'ennemies")]
    public void Debug_SpawnEnemies()
    {
        _waveManager.LaunchWaves();
    }
}

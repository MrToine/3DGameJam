using Character.Runtime;
using Core.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using WaveSystem.Runtime;

public class test : MonoBehaviour
{
    public WaveManager _waveManager;

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

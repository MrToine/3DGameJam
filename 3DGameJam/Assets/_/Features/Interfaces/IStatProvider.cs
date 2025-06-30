namespace Game.Interfaces
{
    public interface IStatProvider
    {
        float GetMaxHealth();
        float GetCurrentHealth();
        float GetAttackPower();
        float GetDefense();
        int GetLevel();
    }
}

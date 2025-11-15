using UnityEngine;

public class EnergySystem : MonoBehaviour
{
private float maxEnergy, currentEnergy;

    public EnergySystem(float _maxEnergy)
    {
        maxEnergy = _maxEnergy;
        currentEnergy = 0;
    }

    public void LoseEnergy(int amount)
    {
        currentEnergy -= amount;
        if (currentEnergy < 0) currentEnergy = 0;
    }

    public void GainEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public float GetEnergyPercentage()
    {
        return currentEnergy / maxEnergy;
    }

    public void IncreaseMaxEnergy(int amount)
    {
        maxEnergy += amount;
        currentEnergy += amount;
    }
}

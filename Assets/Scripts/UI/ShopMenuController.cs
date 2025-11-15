using TMPro;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private TextMeshProUGUI healthStat;
    [SerializeField] private TextMeshProUGUI damageStat;
    [SerializeField] private TextMeshProUGUI speedStat;
    [SerializeField] private TextMeshProUGUI fireRateStat;
    [SerializeField] private TextMeshProUGUI critChanceStat;
    [SerializeField] private TextMeshProUGUI critBonusStat;
    [SerializeField] private GameObject[] upgradeOptions;

    public void Initialize()
    {
        crystalText.text = PlanetHandler.i.GetCrystalCount().ToString();
        healthStat.text = PlanetHandler.i.GetModifierSystem().GetModifierValue(ModifierType.health).ToString();
        damageStat.text = PlayerHandler.i.GetModifierSystem().GetModifierValue(ModifierType.damage).ToString();
        speedStat.text = PlayerHandler.i.GetModifierSystem().GetModifierValue(ModifierType.speed).ToString();
        fireRateStat.text = PlayerHandler.i.GetModifierSystem().GetModifierValue(ModifierType.fireRate).ToString();
        critChanceStat.text = PlayerHandler.i.GetModifierSystem().GetModifierValue(ModifierType.critChance).ToString();
        critBonusStat.text = PlayerHandler.i.GetModifierSystem().GetModifierValue(ModifierType.critBonus).ToString();
    }

}

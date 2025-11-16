using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<WeaponSO> OnWeaponSelected;
    public static event Action<ModifierSO> OnModifierSelected;
    public static event Action<int> OnRestoreHealthSelected;
    public static event Action OnSelectionMade;
    [SerializeField] private GameObject upgradeImage;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private Sprite[] topPanelSprites;
    [SerializeField] private GameObject highLight;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private GameObject[] statUpgradeIndicators;
    [SerializeField] private Sprite restoreHealthSprite;
    [SerializeField] private Sprite restoreHealthTop;
    [SerializeField] private string restoreHealthText;
    private WeaponSO weapon;
    private ModifierSO modifier;
    private bool thisIsAModifier, thisIsHealthRestore, canSelect;

    public void InitializeModifier(ModifierSO _m)
    {
        canSelect = true;
        modifier = _m;
        topPanel.GetComponent<Image>().sprite = topPanelSprites[0];
        upgradeImage.GetComponent<Image>().sprite = modifier.modifierSprite;
        upgradeText.text = _m.description;
        thisIsAModifier = true;
    }
    public void InitializeWeapon(WeaponSO _w)
    {
        canSelect = true;
        weapon = _w;
        topPanel.GetComponent<Image>().sprite = topPanelSprites[1];
        upgradeImage.GetComponent<Image>().sprite = weapon.weaponSprite;
        upgradeText.text = _w.weaponName;
        thisIsAModifier = false;
    }

    public void InitializeRestoreHealthOption()
    {
        canSelect = true;
        topPanel.GetComponent<Image>().sprite = restoreHealthTop;
        upgradeImage.GetComponent<Image>().sprite = restoreHealthSprite;
        upgradeText.text = restoreHealthText;
        thisIsHealthRestore = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highLight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highLight.SetActive(false);
    }
    public void OnSelectedOption()
    {
        if(!canSelect) return;
        if(thisIsHealthRestore) OnRestoreHealthSelected?.Invoke((int)(PlanetHandler.i.GetHealthSystem().GetMaxHealth()*.50f));
        else if(thisIsAModifier) OnModifierSelected?.Invoke(modifier);
        else OnWeaponSelected?.Invoke(weapon);
        OnSelectionMade?.Invoke();
        canSelect = false;
    }
}

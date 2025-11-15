using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject upgradeImage;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private Sprite[] topPanelSprites;
    [SerializeField] private GameObject highLight;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private GameObject[] statUpgradeIndicators;
    private WeaponSO weapon;
    private ModifierSO modifier;

    public void InitializeModifier(ModifierSO _m)
    {
        modifier = _m;
        topPanel.GetComponent<SpriteRenderer>().sprite = topPanelSprites[0];
        upgradeText.text = _m.description;
    }
    public void InitializeWeapon(WeaponSO _w)
    {
        weapon = _w;
        topPanel.GetComponent<SpriteRenderer>().sprite = topPanelSprites[1];
        upgradeText.text = _w.weaponName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highLight.SetActive(true);
        if(modifier != null)
        {
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highLight.SetActive(false);
    }
}

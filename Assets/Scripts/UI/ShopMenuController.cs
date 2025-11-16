using System.Collections.Generic;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    private static ShopMenuController _i;
    public static ShopMenuController i { get { return _i; } }
    [SerializeField] private GameObject[] upgradeOptions;
    [SerializeField] private List<ModifierSO> modifierOptions;
    private List<ModifierSO> availableModifiers;
    private List<ModifierSO> remainingModifiers;
    private int h,d,s,f,cc,cb, cnt;
    private bool firstTime = true;

    public void Initialize()
    {
        _i = this;
        cnt = 0;
        remainingModifiers = new List<ModifierSO>();
        if(firstTime)
        {
            availableModifiers = new List<ModifierSO>();
            foreach (ModifierSO _m in modifierOptions)
            {
                availableModifiers.Add(_m);
            }
            firstTime = false;
        }
        SetupModifierOptions();
    }

    private void SetupModifierOptions()
    {
        foreach (ModifierSO _m in availableModifiers)
        {
            remainingModifiers.Add(_m);
        }

        if(remainingModifiers.Count == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                upgradeOptions[i].GetComponent<ButtonHandler>().InitializeRestoreHealthOption();
            }
        }
        else if(remainingModifiers.Count == 1)
        {
            int randomIndex1 = Random.Range(0, remainingModifiers.Count);
            upgradeOptions[0].GetComponent<ButtonHandler>().InitializeModifier(remainingModifiers[randomIndex1]);
            remainingModifiers.Remove(remainingModifiers[randomIndex1]);
            
            upgradeOptions[1].GetComponent<ButtonHandler>().InitializeRestoreHealthOption();
            upgradeOptions[2].GetComponent<ButtonHandler>().InitializeRestoreHealthOption();
        }
        else if(remainingModifiers.Count == 2)
        {
            int randomIndex1 = Random.Range(0, remainingModifiers.Count);
            upgradeOptions[0].GetComponent<ButtonHandler>().InitializeModifier(remainingModifiers[randomIndex1]);
            remainingModifiers.Remove(remainingModifiers[randomIndex1]);        

            int randomIndex2 = Random.Range(0, remainingModifiers.Count);
            upgradeOptions[1].GetComponent<ButtonHandler>().InitializeModifier(remainingModifiers[randomIndex2]);
            remainingModifiers.Remove(remainingModifiers[randomIndex2]);
            
            upgradeOptions[2].GetComponent<ButtonHandler>().InitializeRestoreHealthOption();
        }
        else
        {
            for(int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, remainingModifiers.Count);
                upgradeOptions[i].GetComponent<ButtonHandler>().InitializeModifier(remainingModifiers[randomIndex]);
                remainingModifiers.Remove(remainingModifiers[randomIndex]);
            }        
        }
        ButtonHandler.OnModifierSelected += OnModifierSelected;
        ButtonHandler.OnRestoreHealthSelected += OnRestoreHealthSelected;
        PlanetHandler.i.AddEventsForUpgrades();
        UIController.i.AddEventsForUpgrades();
    }

    public void OnModifierSelected(ModifierSO _m)
    {
        cnt++;
        if(cnt>1) return;
        switch(_m.type)
        {
            case ModifierType.health:
                h++;
                if(h == 5) availableModifiers.Remove(_m);
                break;
            case ModifierType.damage:
                d++;
                if(d == 5) availableModifiers.Remove(_m);
                break;
            case ModifierType.speed:
                s++;
                if(s == 5) availableModifiers.Remove(_m);
                break;
            case ModifierType.fireRate:
                f++;
                if(f == 5) availableModifiers.Remove(_m);
                break;
            case ModifierType.critChance:
                cc++;
                if(cc == 5) availableModifiers.Remove(_m);
                break;
            case ModifierType.critBonus:
                cb++;
                if(cb == 5) availableModifiers.Remove(_m);
                break;
            default : break;
        }
            
    }

    public void OnWeaponSelected()
    {
        
    }

    public void OnRestoreHealthSelected(int _h)
    {
        PlanetHandler.i.GetHealthSystem().RestoreHealth(_h);
    }

}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    #region Variables
    private static UIController _i;
    public static UIController i { get { return _i; } }
    [Header("STATIC MENUS")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;
    [Header("SHOP MENU")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject shopCrystals;

    [Header("GAME UI")]
    [SerializeField] private GameObject gameUIPanel;
    [SerializeField] private GameObject crystalUI;
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private GameObject leftEnergyUI;
    [SerializeField] private GameObject rightEnergyUI;
    [SerializeField] private GameObject leftHealthUI;
    [SerializeField] private GameObject rightHealthUI;
    [SerializeField] private GameObject weaponsUI;
    [SerializeField] private TextMeshProUGUI gameTimerText;
    [SerializeField] private Transform crystalPopupLocation;
    //[SerializeField] private GameObject specialWeaponUI;
    [Header("VARIABLES")]
    [SerializeField] private bool isMainMenu;
    #endregion
    #region Setup
    private void OnEnable() 
    {
        _i = this;
        if (isMainMenu) InitializeMainMenu();
    }
    private void OnDisable() 
    {

    }

    private void InitializeMainMenu()
    {
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(false);
        GetComponent<VolumeSettings>().Initialize(); 
    }
    public void Initialize()
    {
        //pauseMenu.SetActive(false);
        shopMenu.SetActive(false);
        gameUIPanel.SetActive(true);
        //creditsScreen.SetActive(false);
        //settingsMenu.SetActive(false);
        GetComponent<VolumeSettings>().Initialize();        


        PlanetHandler.OnHealthValueChange += UpdateHealthUI;
        //PlanetHandler.OnEnergyValueChange += UpdateEnergyUI;
        PlanetHandler.OnCrystalValueChanged += UpdateCrystalUI;
        PlayerHandler.OnNewWeaponEquipped += UpdateWeaponUI;
        //WeaponHandler.OnNewSpecialWeaponEquipped += UpdateSpecialWeaponUI;
        EnemySpawnSystem.OnTimerChange += UpdateGameTimerUI;
        EnemySpawnSystem.OnLevelEnd += HandleLevelEnd;

        UpdateHealthUI();
        //UpdateEnergyUI();
        UpdateCrystalUI();
        InitializeWeaponUI();
        //UpdateSpecialWeaponUI();
    }
    #endregion
    #region Menu Actions
    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.i.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameManager.i.UnPauseGame();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        GetComponent<VolumeSettings>().SettingsMenuOpened();
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCreditsScreen()
    {
        creditsScreen.SetActive(false);
    }

    public void OpenShopMenu()
    {
        gameUIPanel.SetActive(false);
        shopMenu.SetActive(true);
        shopMenu.GetComponent<ShopMenuController>().Initialize();
    }

    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        gameUIPanel.SetActive(true);
    }
    #endregion
    #region Menu Functions
    public void StartGame()
    {
        SceneController.StartGame();
    }

    public void RestartGame()
    {
        SceneController.StartGame();
    }
    public void BackToMainMenu()
    {
        SceneController.LoadMainMenu();
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }
    #endregion
    #region GameUI
    private void UpdateCrystalUI()
    {
        crystalText.text = PlanetHandler.i.GetCrystalCount().ToString();
    }
    private void UpdateEnergyUI()
    {
        //leftEnergyUI.GetComponent<Image>().fillAmount = PlanetHandler.i.GetEnergySystem().GetEnergyPercentage();
        //rightEnergyUI.GetComponent<Image>().fillAmount = PlanetHandler.i.GetEnergySystem().GetEnergyPercentage();
        
    }
    private void UpdateHealthUI()
    {
        leftHealthUI.GetComponent<Image>().fillAmount = PlanetHandler.i.GetHealthSystem().GetHealthPercentage();
        rightHealthUI.GetComponent<Image>().fillAmount = PlanetHandler.i.GetHealthSystem().GetHealthPercentage();
    }
    private void InitializeWeaponUI()
    {
        foreach(WeaponSO weapon in PlayerHandler.i.GetActiveWeapons())
        {
            GameObject newWeapon = Instantiate(GameAssets.i.pfWeaponUI, weaponsUI.transform);
            newWeapon.transform.SetParent(weaponsUI.transform);
            newWeapon.GetComponent<Image>().sprite = weapon.weaponSprite;
        }
    }
    private void UpdateWeaponUI(WeaponSO _w)
    {
        Debug.Log("Equipping New Weapon To UI");
        GameObject newWeapon = Instantiate(GameAssets.i.pfWeaponUI, weaponsUI.transform);
        newWeapon.transform.SetParent(weaponsUI.transform);
        newWeapon.GetComponent<SpriteRenderer>().sprite = _w.weaponSprite;
    }
    private void UpdateGameTimerUI(float _gt)
    {
        gameTimerText.text = _gt.ToString("###.#");
    }
    private void HandleLevelEnd()
    {
        GameManager.i.PauseGame();
        OpenShopMenu();
    }

    private void HandleLevelRestart()
    {
        CloseShopMenu();
        GameManager.i.StartNewLevel();
    }
    #endregion
    #region GetFunctions
    public Transform GetCrystalUILocation(){return crystalPopupLocation;}
    #endregion
}

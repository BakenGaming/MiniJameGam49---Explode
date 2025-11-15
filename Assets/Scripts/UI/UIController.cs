using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    //[SerializeField] private GameObject leftEnergyUI;
    //[SerializeField] private GameObject rightEnergyUI;
    [SerializeField] private GameObject leftHealthUI;
    [SerializeField] private GameObject rightHealthUI;
    [SerializeField] private GameObject weaponsUI;
    [SerializeField] private GameObject specialWeaponUI;
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
        //PlanetHandler.OnEnergyValueChange += UpdateHealthUI;
        WeaponHandler.OnNewWeaponEquipped += UpdateWeaponUI;
        WeaponHandler.OnNewSpecialWeaponEquipped += UpdateSpecialWeaponUI;

        //UpdateHealthUI();
        //UpdateEnergyUI();
        //UpdateCrystalUI();
        //UpdateWeaponUI();
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
        shopMenu.SetActive(true);
        gameUIPanel.SetActive(false);
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
    private void UpdateWeaponUI()
    {
        
    }
    private void UpdateSpecialWeaponUI()
    {
        
    }

    #endregion
}

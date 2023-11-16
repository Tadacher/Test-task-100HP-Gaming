using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoreGameplayUiContainer : MonoBehaviour
{
    public GameObject GameplayUiLayer;
    public Button 
        attackSpeedUpgrade, 
        damageUpgrade, 
        rangeUpgrade, 
        menuButton;
    public TMP_Text 
        attackspeedLvl, 
        damageLvl, 
        rangeLvl, 
        balance;

    public GameObject GameMenuLayer;
    public Button 
       ContinueGameVtn, 
       ToMenu;

    public GameObject LoseScreen;
    public Button LoseScreenToMenu;
}

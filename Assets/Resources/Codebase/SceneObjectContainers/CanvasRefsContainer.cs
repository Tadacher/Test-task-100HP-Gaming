using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasRefsContainer : MonoBehaviour
{
    public GameObject ingameLayer;
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

    public GameObject menuLayer;
    public Button 
        play, 
        restart;

    public GameObject loseScreen;
    public Button loseScreenRestart;
}

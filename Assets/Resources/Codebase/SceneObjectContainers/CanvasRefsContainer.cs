using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasRefsContainer : MonoBehaviour
{
    [SerializeField] internal GameObject ingameLayer;
    [SerializeField] internal Button attackSpeedUpgrade, damageUpgrade, rangeUpgrade, menuButton;
    [SerializeField] internal TMP_Text attackspeedLvl, damageLvl, rangeLvl, balance;

    [SerializeField] internal GameObject menuLayer;
    [SerializeField] internal Button play, restart;

    [SerializeField] internal GameObject loseScreen;
    [SerializeField] internal Button loseScreenRestart;
}

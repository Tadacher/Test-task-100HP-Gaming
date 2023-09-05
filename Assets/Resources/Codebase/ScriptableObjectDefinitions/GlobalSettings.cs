using UnityEngine;
[CreateAssetMenu(fileName = "GlobalSettings", menuName = "ScriptableObjects/GlobalSettings")]
public class GlobalSettings : ScriptableObject
{
    public UpgraderSettings upgraderSettings;
    public AsteroidSettings asteroidSettings;
    public PlayerBehaviourSettings playerBehaviourSettings;
    public MissilefactorySettings missilefactorySettings;
    public EconomicsManagerSettings economicsManagerSettings;
    public AudioSetup audioSetup;
}

using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private GameObject[] _tabs;
    [SerializeField] private Image[] _images;

    private GameObject _lastActiveTab;
    private Image _lastActiveSwitchImage;
    private static Color _fadeColor = new Color(0f, 0f, 0f, 0.5f);

    private void Awake()
    {
        foreach (var tab in _tabs)
        {
            tab.SetActive(false);
        }
        _lastActiveTab = _tabs[1];

        foreach (var image in _images)
        {
            image.color -= _fadeColor;
        }
        _lastActiveSwitchImage = _images[1];
        _lastActiveSwitchImage.color += _fadeColor;
        SwitchTab(2);
    }

    public void SwitchTab(int index)
    {
        if (index >= _tabs.Length)
            return;

        _lastActiveTab.SetActive(false);
        _lastActiveSwitchImage.color -= _fadeColor;

        _tabs[index].SetActive(true);
        _images[index].color += _fadeColor;

        _lastActiveSwitchImage = _images[index];
        _lastActiveTab = _tabs[index];
    }
}

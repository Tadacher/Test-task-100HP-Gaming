public class MetaUiService
{
    private MetaUiContainer _container;

    public MetaUiService(MetaUiContainer container)
    {
        _container = container;
    }
    public void DisableGui() 
        => _container.gameObject.SetActive(false);
}
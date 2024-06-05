public class GUIManager : Singleton<GUIManager>
{
    private bool isPopupActive = false;

    public void SetPopupActive(bool active)
    {
        isPopupActive = active;
    }

    public bool IsPopupActive()
    {
        return isPopupActive;
    }
}

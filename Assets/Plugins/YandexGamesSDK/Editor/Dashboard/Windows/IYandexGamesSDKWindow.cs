namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows
{
    public interface IYandexGamesSDKWindow
    {
        int Priority { get; }
        string WindowName { get; }
        void OnGUI();
        void OnEnable();
        void OnDisable();
    }

}
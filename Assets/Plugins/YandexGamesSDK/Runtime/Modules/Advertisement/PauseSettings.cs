using System;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Advertisement
{
    [Serializable]
    public class YGPauseSettings
    {
        public bool pauseAudio = true;
        public bool pauseTime = true;
        public bool hideCursor = true;
        public CursorLockMode cursorLockMode = CursorLockMode.None;
    }
}
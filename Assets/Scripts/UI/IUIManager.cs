using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public interface IUIManager:IMonoExposed
    {
        void Initialize(int gameDifficulty, bool soundOn, float mouseSensitivity);
        void ShowMainMenu();
        void ShowOptions();
        void ShowHelp();
        void ShowFinish(int score);
        void UpdateScoreText(int score);
        void HideMenu();
        bool isFirstStart
        {
            get;
            set;
        }

        event EventHandler onMenuCommand;

        void FireMenuCommand(String command, EventArgs eArgs);
    }
}
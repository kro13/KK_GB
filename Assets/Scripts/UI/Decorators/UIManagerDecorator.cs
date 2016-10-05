using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class UIManagerDecorator:IUIManager
    {
        private IUIManager uiManager; 
        public UIManagerDecorator(IUIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        public void AddStart(Action<MonoBehaviour> onStartAction)
        {
            uiManager.AddStart(onStartAction);
        }

        public void AddUpdate(Action onUpdateAction)
        {
            uiManager.AddUpdate(onUpdateAction);
        }

        public void Initialize(int gameDifficulty, bool soundOn, float mouseSensitivity)
        {
            uiManager.Initialize(gameDifficulty, soundOn, mouseSensitivity);
        }

        public void ShowMainMenu()
        {
            uiManager.ShowMainMenu();
        }

        public void ShowOptions()
        {
            uiManager.ShowOptions();
        }

        public void ShowHelp()
        {
            uiManager.ShowHelp();
        }

        public void ShowFinish(int score)
        {
            uiManager.ShowFinish(score);    
        }

        public void UpdateScoreText(int score)
        {
            uiManager.UpdateScoreText(score);
        }

        public void HideMenu()
        {
            uiManager.HideMenu();
        }

        public bool isFirstStart
        {
            get { return uiManager.isFirstStart; }
            set { uiManager.isFirstStart = value; }
        }

        public event EventHandler onMenuCommand
        {
            add { uiManager.onMenuCommand += value; }
            remove { uiManager.onMenuCommand -= value; }
        }

        public void FireMenuCommand(String command, EventArgs eArgs)
        {
            uiManager.FireMenuCommand(command, eArgs);
        }
    }
}
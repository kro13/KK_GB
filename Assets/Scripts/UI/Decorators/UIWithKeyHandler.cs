using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIWithKeyHandler:UIManagerDecorator
    {

        public UIWithKeyHandler(IUIManager uiManager) : base(uiManager)
        {
            uiManager.AddUpdate(UpdateInput);
        }


        private void UpdateInput()
        {
            if (Input.GetKeyDown("n"))
            {
                FireMenuCommand("NewGame", EventArgs.Empty);
            }
            if (Input.GetKeyDown("escape") || Input.GetKeyDown("p"))
            {
                FireMenuCommand("Resume", EventArgs.Empty);
            }
            if (Input.GetKeyDown("r"))
            {
                FireMenuCommand("Respawn", EventArgs.Empty);
            }
            if (Input.GetKeyDown("q"))
            {
                FireMenuCommand("Quit", EventArgs.Empty);
            }
            if (Input.GetKeyDown("o"))
            {
                FireMenuCommand("Options", EventArgs.Empty);
            }
            if (Input.GetKeyDown("h"))
            {
                FireMenuCommand("Help", EventArgs.Empty);
            }
        }
    }
}
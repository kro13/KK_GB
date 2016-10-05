using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIWithTaps : UIManagerDecorator
    {
        public UIWithTaps(IUIManager uiManager) : base(uiManager)
        {
            uiManager.AddStart(AddButtons);
        }

        private void AddButtons(MonoBehaviour uiManager)
        {
            Transform menuShortcut = uiManager.transform.FindChild("MenuShortcut");
            Button nButton = UIManagerFactory.getInstance().CreateTap();
            nButton.name = "NTap";
            nButton.GetComponentInChildren<Text>().text = "[N]";
            Button rButton = UIManagerFactory.getInstance().CreateTap();
            rButton.name = "RTap";
            rButton.GetComponentInChildren<Text>().text = "[R]";
            Button pButton = UIManagerFactory.getInstance().CreateTap();
            pButton.name = "PTap";
            pButton.GetComponentInChildren<Text>().text = "[P]";
            nButton.transform.SetParent(menuShortcut.transform, false);
            rButton.transform.SetParent(menuShortcut.transform, false);
            pButton.transform.SetParent(menuShortcut.transform, false);
            nButton.onClick.AddListener(()=>OnTapClick(nButton));
            rButton.onClick.AddListener(()=>OnTapClick(rButton));
            pButton.onClick.AddListener(()=>OnTapClick(pButton));
        }

        private void OnTapClick(Button tap)
        {
            switch (tap.name)
            {
                case "NTap":
                    FireMenuCommand("NewGame", EventArgs.Empty);

                    break;
                case "RTap":
                    FireMenuCommand("Respawn", EventArgs.Empty);
                    break;
                case "PTap":
                    FireMenuCommand("Resume", EventArgs.Empty);
                    break;
            }
        }
    }
}
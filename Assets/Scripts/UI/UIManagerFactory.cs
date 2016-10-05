using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIManagerFactory:MonoBehaviour
    {
        public UIManager uiManagerPrefab;
        public UIManager bigUiManagerPrefab;
        public Button tapPrefab;
        private static UIManagerFactory instance;
        public static UIManagerFactory getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManagerFactory>();
                if (instance == null)
                {
                    instance = new GameObject("UIManagerFactory").AddComponent<UIManagerFactory>();
                }
            }
            return instance;
        }

        public IUIManager Create()
        {
            return Instantiate(uiManagerPrefab);
        }

        public IUIManager CreateBig()
        {
            return Instantiate(bigUiManagerPrefab);
        }

        public Button CreateTap()
        {
            return Instantiate(tapPrefab);
        }
    }
}
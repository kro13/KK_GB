using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIFlexible:UIManagerDecorator
    {
        private MonoBehaviour uiManager;
        private Transform menuFrame;
        private Transform menuBack;
        private Vector2 menuItemSize;
        private RectTransform menuBackRect;

        private int menuItemsMaxCount = 6;
        public UIFlexible(IUIManager uiManager) : base(uiManager)
        {
            uiManager.AddStart(MakeFlexible);
        }

        private void MakeFlexible(MonoBehaviour uiManager)
        {
            menuFrame = uiManager.transform.Find("MenuFrame");
            menuBack = menuFrame.Find("MenuBack");

            menuBackRect = menuBack.GetComponent<RectTransform>();
      

            menuBackRect.anchorMin=new Vector2(0,0);
            menuBackRect.anchorMax=new Vector2(1,1);
            menuBackRect.offsetMin=new Vector2(0, 40);
            menuBackRect.offsetMax=new Vector2(0, -40);

            foreach (Transform menuItemsContainer in menuFrame)
            {
                MakeFlexibleMenu(menuItemsContainer);
            }

        }

        private void MakeFlexibleMenu(Transform menuItemsContainer)
        {
            if (menuItemsContainer.name != "MenuBack" &&
                menuItemsContainer.name != "HeaderText" &&
                menuItemsContainer.name != "FooterText")
            {
                Debug.Log("-"+menuItemsContainer.name);

                RectTransform menuItemsContainerRect = menuItemsContainer.GetComponent<RectTransform>();

                menuItemsContainerRect.anchorMin = new Vector2(0, 0);
                menuItemsContainerRect.anchorMax = new Vector2(1, 1);
                menuItemsContainerRect.offsetMin = new Vector2(0, 40);
                menuItemsContainerRect.offsetMax = new Vector2(0, -40);

                menuItemsContainer.gameObject.AddComponent<VerticalLayoutGroup>();
                VerticalLayoutGroup verticalLayout = menuItemsContainer.GetComponent<VerticalLayoutGroup>();
                verticalLayout.childForceExpandHeight = true;
                verticalLayout.childForceExpandWidth = true;
                verticalLayout.spacing = 10;
                verticalLayout.padding = new RectOffset(20, 20, 20, 20);
                int menuItemIndex = 0;
                foreach (Transform menuItem in menuItemsContainer)
                {
                    MakeFlexibleMenuItem(menuItem, menuItemIndex);
                    menuItemIndex++;
                }
            }
        }

        private void MakeFlexibleMenuItem(Transform menuItem, int itemIndex)
        {
            Debug.Log("---" + menuItem.name);
            RectTransform menuItemRect = menuItem.GetComponent<RectTransform>();
            menuItemRect.anchorMin = new Vector2(0, 0.5f);
            menuItemRect.anchorMax = new Vector2(1, 0.5f);
            menuItemRect.offsetMin = new Vector2(20, menuItemRect.offsetMin.y);
            menuItemRect.offsetMax = new Vector2(-20, menuItemRect.offsetMax.y);

        }

        private void SetSize(RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }
        private void SetWidth(RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(newSize, trans.rect.size.y));
        }
        private void SetHeight(RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(trans.rect.size.x, newSize));
        }
    }
}
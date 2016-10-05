using System.Collections.Generic;
using Assets.Scripts.UI;
using Assets.Scripts.Util;

namespace Assets.Scripts
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIManager : MonoBehaviour, IUIManager
    {
        public Text scoreText;
        
        public Text logText;

        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject helpMenu;
        public GameObject finishMenu;
        public GameObject menuPanel;
        public GameObject menuFrame;
        public GameObject menuShortcut;

        private Button newGame;
        private Button newGame2;
        private Button resume;
        private Button respawn;
        private Button options;
        private Button options2;
        private Button help;
        private Button quit;
        private Button quit2;
        private Button toBar;

        private Toggle sound;
        private Dropdown difficulty;
        private Slider sensitivity;
        private Button optsBack;

        private Button helpBack;

        private int initDifficulty;
        private float initSensitivity;
        private bool initSound;
        private bool _isFirstStart = true;
        public event EventHandler _onMenuCommand;
        private HashSet<Action> onUpdateActions=new HashSet<Action>();
        private Action<MonoBehaviour> onStartAction;

        //unity
        private void Start()
        {
            newGame = mainMenu.transform.Find("NewGame").GetComponent<Button>();
            resume = mainMenu.transform.Find("Resume").GetComponent<Button>();
            respawn = mainMenu.transform.Find("Respawn").GetComponent<Button>();
            options = mainMenu.transform.Find("Options").GetComponent<Button>();
            help = mainMenu.transform.Find("Help").GetComponent<Button>();
            quit = mainMenu.transform.Find("Quit").GetComponent<Button>();

            sound = optionsMenu.transform.Find("Sound").GetComponent<Toggle>();
            difficulty = optionsMenu.transform.Find("Difficulty").GetComponent<Dropdown>();
            sensitivity = optionsMenu.transform.Find("Sensitivity").GetComponent<Slider>();
            optsBack = optionsMenu.transform.Find("OptsBack").GetComponent<Button>();

            helpBack = helpMenu.transform.Find("HelpBack").GetComponent<Button>();

            newGame2 = finishMenu.transform.Find("NewGame").GetComponent<Button>();
            options2 = finishMenu.transform.Find("Options").GetComponent<Button>();
            quit2 = finishMenu.transform.Find("Quit").GetComponent<Button>();
            toBar = finishMenu.transform.Find("ToBar").GetComponent<Button>();

            newGame.onClick.AddListener(() => { OnMenuClick(newGame); });
            resume.onClick.AddListener(() => { OnMenuClick(resume); });
            respawn.onClick.AddListener(() => { OnMenuClick(respawn); });
            options.onClick.AddListener(() => { OnMenuClick(options); });
            help.onClick.AddListener(() => { OnMenuClick(help); });
            quit.onClick.AddListener(() => { OnMenuClick(quit); });
            newGame2.onClick.AddListener(() => { OnMenuClick(newGame2); });
            quit2.onClick.AddListener(() => { OnMenuClick(quit2); });
            options2.onClick.AddListener(() => { OnMenuClick(options2); });
            toBar.onClick.AddListener(() => { OnMenuClick(toBar); });

            difficulty.onValueChanged.AddListener((int val) => { OnMenuClick(difficulty); });
            sound.onValueChanged.AddListener((bool selected) => { OnMenuClick(sound); });
            sensitivity.onValueChanged.AddListener((float val) => { OnMenuClick(sensitivity); });
            optsBack.onClick.AddListener(() => { OnMenuClick(optsBack); });

            helpBack.onClick.AddListener(() => { OnMenuClick(helpBack); });
            isFirstStart = true;

            difficulty.value = initDifficulty;
            sensitivity.value = initSensitivity;
            sound.isOn = initSound;
            Debug.Log("UIManager started");
        }

        private void Update()
        {
            foreach (Action action in onUpdateActions)
            {
                action.Invoke();
            }
        }

        private void FixedUpdate()
        {
            //Log("Gyro: "+Input.gyro.enabled.ToString());
            //Log("Compass: "+Input.compass.enabled.ToString());
            
        }
        //unity

        public void AddStart(Action<MonoBehaviour> onStartAction)
        {
            this.onStartAction = onStartAction;
            onStartAction.Invoke(this);
        }

        public void AddUpdate(Action onUpdateAction)
        {
            onUpdateActions.Add(onUpdateAction);
        }

        public void Initialize(int diff, bool snd, float sens)
        {
            initDifficulty = diff;
            initSound = snd;
            initSensitivity = sens;
            if (difficulty != null)
            {
                difficulty.value = initDifficulty;
                sensitivity.value = initSensitivity;
                sound.isOn = initSound;
            }
        }

        public void ShowMainMenu()
        {
            ShowMenu();
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            helpMenu.SetActive(false);
            finishMenu.SetActive(false);
        }

        public void ShowOptions()
        {
            ShowMenu();
            mainMenu.SetActive(false);
            helpMenu.SetActive(false);
            optionsMenu.SetActive(true);
            finishMenu.SetActive(false);
        }

        public void ShowHelp()
        {
            ShowMenu();
            mainMenu.SetActive(false);
            helpMenu.SetActive(true);
            optionsMenu.SetActive(false);
            finishMenu.SetActive(false);
        }

        public void ShowFinish(int score)
        {
            ShowMenu();
            mainMenu.SetActive(false);
            helpMenu.SetActive(false);
            optionsMenu.SetActive(false);
            finishMenu.SetActive(true);
            Text finishText = finishMenu.transform.Find("FinishText").GetComponent<Text>();
            finishText.text = "You made it!\nYour total score is: " + score;
        }

        public void Log(string txt)
        {
            logText.text = txt + "\n"+logText.text;
        }

        public void UpdateScoreText(int score)
        {
            scoreText.text = "Fun: " + score;
            if (score >= 0)
            {
                scoreText.color = GameColor.Green;
            }
            else
            {
                scoreText.color = GameColor.Red;
            }
        }

        public bool isFirstStart
        {
            set
            {
                resume.interactable = !value;
                respawn.interactable = !value;
                _isFirstStart = value;
            }
            get { return _isFirstStart; }
        }

        public event EventHandler onMenuCommand
        {
            add { _onMenuCommand += value; }    
            remove { _onMenuCommand -= value; }    
        }    // the Event

        public void MakeFlexible(bool flexible)
        {
            if (flexible)
            {
                GameObject menuBack = transform.FindChild("MenuBack").gameObject;
                RectTransform menuBackTransform = menuBack.GetComponent<RectTransform>();
                menuBackTransform.anchorMin=new Vector2(0,0);
                menuBackTransform.anchorMax=new Vector2(1,1);
                menuBackTransform.pivot=new Vector2(0.5f,0.5f);
            }
        }

        private void OnMenuClick(Selectable menuItem)    // the Trigger method, called to raise the event
        {
            EventArgs eArgs = EventArgs.Empty;

            switch (menuItem.name)
            {
                case "Sound":
                    eArgs = new MenuEventArgs(((Toggle)menuItem).isOn.ToString());
                    break;
                case "Difficulty":
                    eArgs = new MenuEventArgs(((Dropdown)menuItem).value.ToString());
                    break;
                case "Sensitivity":
                    eArgs = new MenuEventArgs(((Slider)menuItem).value.ToString());
                    break;
            }

            FireMenuCommand(menuItem.name, eArgs);
        }

        public void FireMenuCommand(String command, EventArgs eArgs)
        {
            if (_onMenuCommand != null)
            {
                _onMenuCommand(command, eArgs);
            }
        }

        public void HideMenu()
        {
            menuPanel.SetActive(false);
            menuFrame.SetActive(false);
            scoreText.enabled = true;
            menuShortcut.SetActive(true);
        }

        private void ShowMenu()
        {
            scoreText.enabled = false;
            menuShortcut.SetActive(false);
            menuPanel.SetActive(true);
            menuFrame.SetActive(true);
        }

        
    }

}

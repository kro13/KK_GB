using Assets.Scripts.Controls;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Physics;
using Assets.Scripts.Score;
using System;
using Assets.Scripts.UI;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public CameraManager cameraManager;
        public IUIManager uiManager;
        public SoundManager soundManager;

        private Canyon canyon;
        private Player player;
        private FinishMarker finishMarker;

        private bool isPaused;
        private bool isFinish;
        private int difficulty;
        private int totalScore;
        private IPlayerControl control;
        private IPhysics physics;
        private IScore score;

        //unity
        private void Start()
        {
            //set init values
            difficulty = 1;
#if UNITY_WEBGL&&!UNITY_EDITOR
            float sensitivity = 0.55f;
#else
            float sensitivity = 0.5f;
#endif
            bool sound = false;

            //setup control
#if UNITY_ANDROID//&&!UNITY_EDITOR
            control = gameObject.AddComponent<GyroControl>();
            uiManager = new UIWithTaps(UIManagerFactory.getInstance().CreateBig());
#else
            control = gameObject.AddComponent<SliderControl>();
            uiManager = new UIWithKeyHandler(UIManagerFactory.getInstance().Create());
#endif
            control.SetSensitivity(sensitivity);
            //setup physics
            physics = new GamePhysics();
            physics.SetDifficulty(difficulty);
            //setup score
            score = new GameScore();
            score.SetDifficulty(difficulty);
            
            uiManager.onMenuCommand += OnMenuCommand;
            uiManager.Initialize(difficulty, sound, sensitivity);
            //uiManager.MakeFlexible(true);
            soundManager.Play(sound);

            PauseGame(true);
        }

        void Update()
        {
            UpdateText();
            //PixelPerfectScale.MakePixelPerfect(transform);
        }
        //unity

        private void BeginGame()
        {
            //create canyon
            canyon = GameObjectsFactory.getInstance().Create<Canyon>();
            canyon.Generate(difficulty);
            finishMarker = GameObjectsFactory.getInstance().Create<FinishMarker>();
            finishMarker.transform.position = canyon.GetExitPoint();
            finishMarker.transform.localScale = new Vector3(20, 1, 0);
            finishMarker.onHitFinish += OnHitFinish;

            //setup player if not yet
            if (player == null)
            {
                player = Instantiate(GameObjectsFactory.getInstance().playerPrefab);
                player.SetControl(control);
                player.SetPhysicsl(physics);
                player.SetPScore(score);
                cameraManager.SetTarget(player.gameObject);
            }

            RespawnPlayer();

            if (uiManager.isFirstStart)
            {
                uiManager.isFirstStart = false;
            }

            if (difficulty == 2)
            {
                cameraManager.PlaySnow(true);
            }
            else
            {
                cameraManager.PlaySnow(false);
            }

            isFinish = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FinishGame()
        {
            if (!isFinish)
            {
                isFinish = true;
                totalScore += score.GetScore();
                PauseGame(true);
            }
        }

        private void PauseGame(bool pause)
        {
            isPaused = pause;
            if (player)
            {
                player.SetActive(!pause);
            }

            if (!isFinish)
            {
                if (pause)
                {
                    uiManager.ShowMainMenu();
                }
                else
                {
                    uiManager.HideMenu();
                }
            }
            else
            {
                if (pause)
                {
                    uiManager.ShowFinish(totalScore);
                }
                else
                {
                    uiManager.HideMenu();
                }
            }

            Cursor.visible = pause;
            if (!pause)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void RestartGame()
        {
            if (canyon)
            {
                Destroy(canyon.gameObject);
            }
            if (finishMarker)
            {
                if (finishMarker.onHitFinish != null)
                {
                    finishMarker.onHitFinish -= OnHitFinish;
                }
                Destroy(finishMarker.gameObject);
            }
            BeginGame();
        }

        private void RespawnPlayer()
        {
            score.Reset();
            player.Reset();
            player.transform.position = canyon.GetEnterPoint();
            PauseGame(false);
        }

        private void UpdateText()
        {
            uiManager.UpdateScoreText(score.GetScore());
        }

        private void OnHitFinish(object sender, EventArgs e)
        {
            FinishGame();
        }

        private void OnMenuCommand(object sender, EventArgs e)
        {
            String command = sender.ToString();
            if (e != EventArgs.Empty)
            {
                ExecuteMenuCommand(command, ((MenuEventArgs)e).GetArgs());
            }
            else
            {
                ExecuteMenuCommand(command);
            }
        }

        private void ExecuteMenuCommand(string command, params string[] args)
        {
            Debug.Log("Menu: " + command);
            switch (command)
            {
                case "NewGame":
                    RestartGame();
                    soundManager.SetTrack("Normal");
                    break;
                case "Resume":
                    if (!uiManager.isFirstStart)
                    {
                        PauseGame(!isPaused);
                    }
                    break;
                case "Respawn":
                    if (!uiManager.isFirstStart && !isFinish)
                    {
                        RespawnPlayer();
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    break;
                case "Options":
                    PauseGame(true);
                    uiManager.ShowOptions();
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "Help":
                    PauseGame(true);
                    uiManager.ShowHelp();
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "OptsBack":
                    if (isFinish)
                    {
                        uiManager.ShowFinish(totalScore);
                    }
                    else
                    {
                        uiManager.ShowMainMenu();
                    }
                    break;
                case "HelpBack":
                    if (isFinish)
                    {
                        uiManager.ShowFinish(totalScore);
                    }
                    else
                    {
                        uiManager.ShowMainMenu();
                    }
                    break;
                case "ToBar":
                    cameraManager.PlaySnow(true);
                    PauseGame(false);
                    player.SetActive(false);
                    soundManager.SetTrack("Bar");
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "Quit":
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Application.Quit();
                    break;
                case "Sound":
                    soundManager.Play(bool.Parse(args[0]));
                    break;
                case "Difficulty":
                    physics.SetDifficulty(int.Parse(args[0]));
                    score.SetDifficulty(int.Parse(args[0]));
                    difficulty = int.Parse(args[0]);
                    uiManager.isFirstStart = true;
                    break;
                case "Sensitivity":
                    control.SetSensitivity(float.Parse(args[0]));
                    break;
            }
        }
    }


}

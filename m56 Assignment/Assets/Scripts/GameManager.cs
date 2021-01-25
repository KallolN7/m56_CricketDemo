using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{
    /// <summary>
    ///  Controls the entire Game Flow
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private BowlerController bowlerController;
        [SerializeField]
        private StumpsController stumps_C;
        [SerializeField]
        private BatsmanController batsmanController;

        public static GameManager instance;

        #region Public Region

        /// <summary>
        /// Updates scores and score UI
        /// </summary>
        public void UpdateScore()
        {
            UpdateBalls();
            UpdateWicketHit();
            ScoreboardController.instance.UpdateScoreTexts(Config.wicketHitCount, Config.ballsBowledCount);
        }

        /// <summary>
        /// Updates the bowler type- Spinner/Pacer
        /// </summary>
        public void UpdateBowlerType()
        {
            if (Config.IS_BOWLER_SPINNER == 0)
                Config.IS_BOWLER_SPINNER = 1;
            else
                Config.IS_BOWLER_SPINNER = 0;

            BowlSpinSwingController.instance.UpdateSwingSpinTypeText();
            ScoreboardController.instance.UpdateBowlerText();
            bowlerController.SetDefaultPos();
            Debug.Log("GameManager, IsSpinner: " + Config.IS_BOWLER_SPINNER);
        }

        /// <summary>
        /// Get BowlerController object reference
        /// </summary>
        /// <returns></returns>
        public BowlerController GetBowlerController()
        {
            return bowlerController;
        }

        /// <summary>
        /// Get BatsmanController object reference
        /// </summary>
        /// <returns></returns>
        public BatsmanController GetBatsmanController()
        {
            return batsmanController;
        }

        /// <summary>
        /// Restarts Game after a ball reaches its final position
        /// </summary>
        public void RestartGame()
        {
            UpdateScore();
            Invoke(nameof(SetDefaultState), 2);
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            instance = this; //Assigning Singleton
        }

        private void Start()
        {
            StartGame();
        }

        /// <summary>
        /// Starts Game very first time.  
        /// </summary>
        private void StartGame()
        {
            ResetGame();
            BowlSpinSwingController.instance.StartSliderAnim();
        }

        /// <summary>
        /// Resets Game variables and gameobjets for first game start
        /// </summary>
        private void ResetGame()
        {
            batsmanController.InitBatsman();
            Config.wicketHitCount = 0;
            Config.ballsBowledCount = 0;
            ScoreboardController.instance.ResetScoreboard();
            BowlSpinSwingController.instance.UpdateSwingSpinTypeText();
            BallPitchController.instance.SetDefaultPos();
            Config.didHitWicket = false;
            Config.canBowl = true;
            Config.InputIndex = 0;
            ScoreboardController.instance.UpdateInputText("Press Spacebar To select Spin/Swing");
            ScoreboardController.instance.UpdateBowlerTextState(true);
        }

        /// <summary>
        ///  Set default state to gameobjects and variables after ball reaches final position
        /// </summary>
        private void SetDefaultState()
        {
            BowlSpinSwingController.instance.StartSliderAnim();
            stumps_C.ResetStumps();
            bowlerController.SetDefaultPos();
            batsmanController.InitBatsman();
            Config.didHitWicket = false;
            BallPitchController.instance.SetDefaultPos();
            Config.canBowl = true;
            Config.InputIndex = 0;
            ScoreboardController.instance.UpdateInputText("Press Spacebar To select Spin/Swing");
            ScoreboardController.instance.UpdateBowlerTextState(true);
        }

        /// <summary>
        ///  Increments Config.wicketHitCount by 1, if wicket is hit
        /// </summary>
        private void UpdateWicketHit()
        {
            if (Config.didHitWicket)
                Config.wicketHitCount++;
        }

        /// <summary>
        ///  Increments  Config.ballsBowledCount by 1, after ball is bowled
        /// </summary>
        private void UpdateBalls()
        {
            Config.ballsBowledCount++;
        }

        #endregion
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deftouch.Asc.OnBoarding;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private OnboardingBowlerController bowlerController;
    [SerializeField]
    private OnboardingStumps_C stumps_C;
    public static GameManager instance;

    #region Public Region

    public void UpdateScore()
    {
        UpdateBalls();
        UpdateWicketHit();
        ScoreboardController.instance.UpdateScoreTexts(Config.wicketHitCount, Config.ballsBowledCount);
    }

    public void UpdateBowlerType()
    {
        if(Config.IS_BOWLER_SPINNER == 0)
        Config.IS_BOWLER_SPINNER = 1;
        else
            Config.IS_BOWLER_SPINNER = 0;

        BowlSpinSwingController.instance.UpdateSwingSpinTypeText();
        bowlerController.SetDefaultPos();
        Debug.Log("GameManager, IsSpinner: " + Config.IS_BOWLER_SPINNER);
    }

    public OnboardingBowlerController GetBowlerController()
    {
        return bowlerController;
    }
    
    public void RestartGame()
    {
        UpdateScore();
        Invoke(nameof(SetDefaultState), 2);
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        ResetGame();
        BowlSpinSwingController.instance.StartSliderAnim();
    }

    private void EndGame()
    {
       
    }

    private void ResetGame()
    {
        Config.wicketHitCount = 0;
        Config.ballsBowledCount = 0;
        ScoreboardController.instance.ResetScoreboard();
        BowlSpinSwingController.instance.UpdateSwingSpinTypeText();
        Config.didHitWicket = false;
        Config.canBowl = true;
        Config.InputIndex = 0;
        ScoreboardController.instance.UpdateInputText("Press Spacebar To select Spin/Swing");
        ScoreboardController.instance.UpdateBowlerTextState(true);
    }

    private void SetDefaultState()
    {
        BowlSpinSwingController.instance.StartSliderAnim();
        stumps_C.ResetStumps();
        bowlerController.SetDefaultPos();
        Config.didHitWicket = false;
        BallPitchController.instance.SetDefaultPos();
        Config.canBowl = true;
        Config.InputIndex = 0;
        ScoreboardController.instance.UpdateInputText("Press Spacebar To select Spin/Swing");
        ScoreboardController.instance.UpdateBowlerTextState(true);
    }

    private void UpdateWicketHit()
    {
        if(Config.didHitWicket)
        Config.wicketHitCount++;
    }

    private void UpdateBalls()
    {
        Config.ballsBowledCount++;
    }

    #endregion
}

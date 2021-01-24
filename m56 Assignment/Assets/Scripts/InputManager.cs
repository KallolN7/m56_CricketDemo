using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deftouch.Asc.OnBoarding;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Config.InputIndex == 0)
            {
                AnimateSpinSwingSlider();
            }
            else if (Config.InputIndex == 1)
            {
                AnimateBounceSlider();
            }
            else if(Config.InputIndex == 2 && Config.canBowl )
            {
                BowlBall();
            }
        }

        if(Config.InputIndex == 2)
        BallPitchController.instance.MovePitchIndicator();

        if (Input.GetKeyDown(KeyCode.LeftShift) && Config.canBowl)
            ScoreboardController.instance.SwitchBowler();
    }


    private void AnimateSpinSwingSlider()
    {
        BowlSpinSwingController.instance.StopSlider();
        BounceController.instance.StartSliderAnim();
        ScoreboardController.instance.UpdateInputText("Press Spacebar To select Bounce");
        Config.InputIndex++;
    }

    private void AnimateBounceSlider()
    {
        BowlSpinSwingController.instance.StopSlider();
        BounceController.instance.StopSlider();
        ScoreboardController.instance.UpdateInputText("Press Arrow Keys To Navigate Ball Pitch Position. \n Press Spacebar To Bowl. ");
        Config.InputIndex++;
    }

    private void BowlBall()
    {
        BowlSpinSwingController.instance.StopSlider();
        BounceController.instance.StopSlider();
        GameManager.instance.GetBowlerController().StartBowling();
        ScoreboardController.instance.UpdateInputText("");
        ScoreboardController.instance.UpdateBowlerTextState(false);
        AssignBatsmanShot();
        Config.canBowl = false;
    }



    private void AssignBatsmanShot()
    {
        if (Config.IS_BOWLER_SPINNER == 1)
            Invoke(nameof(PlayFakeBatsmanShot), Config.shotDelayForSpinner);
        else
            Invoke(nameof(PlayFakeBatsmanShot), Config.shotDelayForPacer);
    }

    private void PlayFakeBatsmanShot()
    {
        GameManager.instance.GetBatsmanController().PlayShot();
    }
}

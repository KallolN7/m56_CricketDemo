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
                BowlSpinSwingController.instance.StopSlider();
                BounceController.instance.StartSliderAnim();
                ScoreboardController.instance.UpdateInputText("Press Spacebar To select Bounce");
                Config.InputIndex++;
            }
            else if (Config.InputIndex == 1)
            {
                BowlSpinSwingController.instance.StopSlider();
                BounceController.instance.StopSlider();
                ScoreboardController.instance.UpdateInputText("Press Arrow Keys To Navigate Ball Pitch Position. \n Press Spacebar To Bowl. ");
                Config.InputIndex++;
            }
            else if(Config.InputIndex == 2 && Config.canBowl )
            {
                BowlSpinSwingController.instance.StopSlider();
                BounceController.instance.StopSlider();
                GameManager.instance.GetBowlerController().StartBowling();
                ScoreboardController.instance.UpdateInputText("");
                ScoreboardController.instance.UpdateBowlerTextState(false);
                Config.canBowl = false;
            }
        }

        if(Config.InputIndex == 2)
        BallPitchController.instance.MovePitchIndicator();


        //if (Input.GetKeyDown(KeyCode.Alpha1))4
        //    BowlSpinSwingController.instance.StopSlider();

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    BounceController.instance.StopSlider();

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    GameManager.instance.RestartGame();

        if (Input.GetKeyDown(KeyCode.LeftShift) && Config.canBowl)
            ScoreboardController.instance.SwitchBowler();
    }
}

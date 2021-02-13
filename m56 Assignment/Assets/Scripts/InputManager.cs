using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{
    /// <summary>
    /// Controls all User inputs 
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        // Config.InputIndex = 0 -> spin/swing Selection
        // Config.InputIndex = 1 -> bounce Selection
        // Config.InputIndex = 2 -> ball pitch area Selection + bowl ball 
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
                else if (Config.InputIndex == 2 && Config.canBowl)
                {
                    BowlBall();
                }
            }

            if (Config.InputIndex == 2)
                BallPitchController.instance.MovePitchIndicator();

            if (Input.GetKeyDown(KeyCode.RightShift) && Config.canBowl)
                GameManager.instance.UpdateBowlerType();
        }

        /// <summary>
        /// Indicates time Select Spin/Swing
        /// </summary>
        private void AnimateSpinSwingSlider()
        {
            BowlSpinSwingController.instance.StopSlider();
            BounceController.instance.StartSliderAnim();
            ScoreboardController.instance.UpdateInputText("Press Spacebar To select Bounce");
            Config.InputIndex++;
        }

        /// <summary>
        /// Indicates time Select Bounce value
        /// </summary>
        private void AnimateBounceSlider()
        {
            BowlSpinSwingController.instance.StopSlider();
            BounceController.instance.StopSlider();
            ScoreboardController.instance.UpdateInputText("Press Arrow Keys To Navigate Ball Pitch Position. \n Press Spacebar To Bowl. ");
            Config.InputIndex++;
        }

        /// <summary>
        /// Indicates time to select ball landing position, and bowl when ready 
        /// </summary>
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


        /// <summary>
        /// Trigger batsman animation after ball is delivered after delay of Config.shotDelayForSpinner/Config.shotDelayForPacer
        /// </summary>
        private void AssignBatsmanShot()
        {
            if (Config.IS_BOWLER_SPINNER == 1)
                Invoke(nameof(PlayFakeBatsmanShot), Config.shotDelayForSpinner);
            else
                Invoke(nameof(PlayFakeBatsmanShot), Config.shotDelayForPacer);
        }

        /// <summary>
        /// Plays fake batsman shot animation 
        /// </summary>
        private void PlayFakeBatsmanShot()
        {
            GameManager.instance.GetBatsmanController().PlayShot();
        }
    }
}


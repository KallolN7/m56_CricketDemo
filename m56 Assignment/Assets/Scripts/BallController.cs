using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace m56
{
    /// <summary>
    /// Controls the ball movement
    /// </summary>
    public class BallController : MonoBehaviour
    {
        [SerializeField]
        private Transform ballTransform;
        [SerializeField]
        private Vector3 ballDefaultPos;
        [SerializeField]
        private Vector3 ballPitchPos;
        [SerializeField]
        private Vector3 ballEndPos;

        private Sequence ballSequence;
        public static BallController instance;

        #region Public 

        /// <summary>
        /// Starts animating the ball from bowler's hand to final position
        /// </summary>
        public void DeliverBall()
        {
            ballTransform.gameObject.SetActive(true);
            Resetball();
            CalculatePitchPos();
            CalculateSpinSwing();
            AnimateBallPitch();
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            instance = this; //Assigning Singleton
        }

        /// <summary>
        /// Calculates the pitch position where the ball will land after coming out of Bowler's hand
        /// </summary>
        private void CalculatePitchPos()
        {
            ballPitchPos = BallPitchController.instance.GetPitchIndicatorPosition();
            ballEndPos.y = BounceController.instance.GetSliderValue();
        }

        /// <summary>
        /// Calculates the spin/swing deviation of ball based on spin/swing UI scale
        /// </summary>
        private void CalculateSpinSwing()
        {
            Debug.Log("BallController, CalculateSpinSwing, ballEndPos.x : " + ballEndPos.x);
            float deviation = BowlSpinSwingController.instance.GetSliderValue();
            deviation *= BallAnimationData.deviationFactor;
            deviation = BallAnimationData.deviationConstant - deviation;
            ballEndPos.x = deviation + BallPitchController.instance.GetPitchIndicatorDeltaPosX();
            Debug.Log("BallController, CalculateSpinSwing, deviation : " + deviation + " | BallPitchController.instance.GetPitchIndicatorDeltaPosX : " + BallPitchController.instance.GetPitchIndicatorDeltaPosX());
            Debug.Log("BallController, CalculateSpinSwing, ballEndPos.x : " + ballEndPos.x);
        }

        /// <summary>
        /// Animates the ball frm Bowler's hand to landing area on pitch 
        /// </summary>
        private void AnimateBallPitch()
        {
            if (Config.IS_BOWLER_SPINNER == 1)
            {
                ballSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
                ballSequence.Append(ballTransform.DOMove(ballPitchPos, BallAnimationData.ballPitchTimeForSpinner).SetEase(Ease.Linear));
                ballSequence.OnComplete(() =>
                {
                    AnimateBallToEndPos();
                });
            }
            else
            {
                ballSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
                ballSequence.Append(ballTransform.DOMove(ballPitchPos, BallAnimationData.ballPitchTimeForFastBowler).SetEase(Ease.Linear));
                ballSequence.OnComplete(() =>
                {
                    AnimateBallToEndPos();
                });
            }

        }

        /// <summary>
        /// Animates the ball from the pitch to final position
        /// </summary>
        private void AnimateBallToEndPos()
        {
            if (Config.IS_BOWLER_SPINNER == 1)
            {
                ballSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
                ballSequence.Append(ballTransform.DOMove(ballEndPos, BallAnimationData.ballEndTimeForSpinner).SetEase(Ease.Linear));
                ballSequence.OnComplete(() =>
                {
                    GameManager.instance.RestartGame();
                });
            }
            else
            {
                ballSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
                ballSequence.Append(ballTransform.DOMove(ballEndPos, BallAnimationData.ballEndTimeForFastBowler).SetEase(Ease.Linear));
                ballSequence.OnComplete(() =>
                {
                    GameManager.instance.RestartGame();
                });
            }


        }

        /// <summary>
        /// Resets ball to it's default position
        /// </summary>
        private void Resetball()
        {
            ballTransform.position = ballDefaultPos;
        }

        #endregion
    }

}

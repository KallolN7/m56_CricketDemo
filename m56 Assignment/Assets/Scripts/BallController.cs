using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    #region Public Methods

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
        instance = this;
    }


    private void CalculatePitchPos()
    {
        ballPitchPos = BallPitchController.instance.GetPitchIndicatorPosition();
        ballEndPos.y = BounceController.instance.GetSliderValue();
    }

    private void CalculateSpinSwing()
    {
        Debug.Log("BallController, CalculateSpinSwing, ballEndPos.x : " + ballEndPos.x);
        float deviation = BowlSpinSwingController.instance.GetSliderValue();
        deviation *=BallAnimationData.deviationFactor;
        deviation = BallAnimationData.deviationConstant - deviation;
        ballEndPos.x = deviation + BallPitchController.instance.GetPitchIndicatorDeltaPosX();
        Debug.Log("BallController, CalculateSpinSwing, deviation : " + deviation + " | BallPitchController.instance.GetPitchIndicatorDeltaPosX : " + BallPitchController.instance.GetPitchIndicatorDeltaPosX());
        Debug.Log("BallController, CalculateSpinSwing, ballEndPos.x : " + ballEndPos.x);
    }

    private void AnimateBallPitch()
    {
        if(Config.IS_BOWLER_SPINNER==1)
        {
            ballSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
            ballSequence.Append(ballTransform.DOMove(ballPitchPos,BallAnimationData.ballPitchTimeForSpinner).SetEase(Ease.Linear));
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

    private void AnimateBallToEndPos()
    {
        if(Config.IS_BOWLER_SPINNER == 1)
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

    private void Resetball()
    {
        ballTransform.position = ballDefaultPos;
    }

    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI wicketsHitText;
    [SerializeField]
    private TextMeshProUGUI ballsBowledText;
    [SerializeField]
    private TextMeshProUGUI bowlerTypeText;
    [SerializeField]
    private TextMeshProUGUI inputText;
    [SerializeField]
    private GameObject bowlerSwitchTextObj;
    [SerializeField]
    private RectTransform BowledTextRect;

    private Sequence bowledTxtSequence;

    public static ScoreboardController instance;
    #region Public Methods

    public void SwitchBowler()
    {
        GameManager.instance.UpdateBowlerType();
        UpdateBowlerText();
        Debug.Log("ScoreboardController, OnClickSwitchBowler: " + Config.IS_BOWLER_SPINNER);
    }


    public void ResetScoreboard()
    {
        UpdateBowlerText();
        wicketsHitText.text = "Wickets taken: " + Config.wicketHitCount;
        ballsBowledText.text = "Bowls Bowled: " + Config.ballsBowledCount;
    }

    public void UpdateScoreTexts(int wickets, int balls)
    {
        wicketsHitText.text = "Wickets taken: " + wickets;
        ballsBowledText.text = "Bowls Bowled: " + balls;
    }

    public void AnimateBowledText()
    {
        if (bowledTxtSequence != null)
            bowledTxtSequence.Kill();

        ShowBowledText();
        Invoke(nameof(HideBowledText), 1);
    }

    public void UpdateInputText(string text)
    {
        inputText.text = text;
    }

    public void UpdateBowlerTextState(bool state)
    {
        bowlerSwitchTextObj.SetActive(state);
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        instance = this;
    }

    private void UpdateBowlerText()
    {
        if (Config.IS_BOWLER_SPINNER == 1)
            bowlerTypeText.text = "Bowler Type \n Spinner";
        else
            bowlerTypeText.text = "Bowler Type \n Fast Bowler";
    }

    private void ShowBowledText()
    {
        bowledTxtSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
        bowledTxtSequence.Append(BowledTextRect.DOAnchorPosY(1000, 0.5f).SetEase(Ease.OutBack));
    }

    private void HideBowledText()
    {
        bowledTxtSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
        bowledTxtSequence.Append(BowledTextRect.DOAnchorPosY(-200, 0.5f).SetEase(Ease.InBack));
    }
    #endregion
}

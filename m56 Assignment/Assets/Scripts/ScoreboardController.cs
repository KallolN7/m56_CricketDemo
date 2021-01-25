using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace m56
{
    /// <summary>
    /// Controls scoreboard UI and all UI based on balls events
    /// </summary>
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

        /// <summary>
        /// Resets scoreboard UI
        /// </summary>
        public void ResetScoreboard()
        {
            UpdateBowlerText();
            wicketsHitText.text = "Wickets taken: " + Config.wicketHitCount;
            ballsBowledText.text = "Bowls Bowled: " + Config.ballsBowledCount;
        }

        /// <summary>
        /// Updates score UI
        /// </summary>
        /// <param name="wickets"></param>
        /// <param name="balls"></param>
        public void UpdateScoreTexts(int wickets, int balls)
        {
            wicketsHitText.text = "Wickets: " + wickets;
            ballsBowledText.text = "Balls: " + balls;
        }

        /// <summary>
        /// Shows "BOWLED!!" text whenever wicket is hit
        /// </summary>
        public void AnimateBowledText()
        {
            if (bowledTxtSequence != null)
                bowledTxtSequence.Kill();

            ShowBowledText();
            Invoke(nameof(HideBowledText), 1);
        }

        /// <summary>
        /// Shows text to guide player with Input
        /// </summary>
        /// <param name="text"></param>
        public void UpdateInputText(string text)
        {
            inputText.text = text;
        }

        /// <summary>
        /// Disables bowler change guide text when ball is being delivered, enables when bowler is idle. 
        /// </summary>
        /// <param name="state"></param>
        public void UpdateBowlerTextState(bool state)
        {
            bowlerSwitchTextObj.SetActive(state);
        }

        /// <summary>
        /// Updates bowler type text based on current bowler type
        /// </summary>
        public void UpdateBowlerText()
        {
            if (Config.IS_BOWLER_SPINNER == 1)
                bowlerTypeText.text = "Bowler Type \n Spinner";
            else
                bowlerTypeText.text = "Bowler Type \n Fast Bowler";
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            instance = this; //Assigning Singleton
        }

        /// <summary>
        /// Animates "BOWLED!!" text
        /// </summary>
        private void ShowBowledText()
        {
            bowledTxtSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
            bowledTxtSequence.Append(BowledTextRect.DOAnchorPosY(1000, 0.5f).SetEase(Ease.OutBack));
        }

        /// <summary>
        ///  Hides "BOWLED!!" text 
        /// </summary>
        private void HideBowledText()
        {
            bowledTxtSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
            bowledTxtSequence.Append(BowledTextRect.DOAnchorPosY(-200, 0.5f).SetEase(Ease.InBack));
        }

        #endregion
    }
}


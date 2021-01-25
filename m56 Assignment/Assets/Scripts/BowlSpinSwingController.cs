using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace m56
{
    /// <summary>
    /// Controls the ball Spin/Swing degree scale UI
    /// </summary>
    public class BowlSpinSwingController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private float animDuration;
        [SerializeField]
        private float sliderEndValue;
        [SerializeField]
        private float sliderDefaultValue;
        [SerializeField]
        private TextMeshProUGUI offSpin_InSwingText;
        [SerializeField]
        private TextMeshProUGUI legSpin_OutSwingText;

        private bool isPointerMoving;
        private Sequence sliderSequence;

        public static BowlSpinSwingController instance;

        #region Public Methods


        /// <summary>
        ///  Get current pointer value on scale
        /// </summary>
        /// <returns></returns>
        public float GetSliderValue()
        {
            return slider.value;
        }

        /// <summary>
        /// Starts animating the slider
        /// </summary>
        public void StartSliderAnim()
        {
            if (!isPointerMoving)
            {
                ResetSlider();
                AnimateSlider();
            }
        }

        /// <summary>
        /// Stops the slider animation
        /// </summary>
        public void StopSlider()
        {
            StopSliderAnimation();
        }

        /// <summary>
        /// Updates Spin/Swing type texts based on bowler type
        /// </summary>
        public void UpdateSwingSpinTypeText()
        {
            if (Config.IS_BOWLER_SPINNER == 1)
            {
                offSpin_InSwingText.text = "Off Spin";
                legSpin_OutSwingText.text = "Leg Spin";
            }
            else
            {
                offSpin_InSwingText.text = "In Swing";
                legSpin_OutSwingText.text = "Out Swing";
            }
        }

        #endregion


        #region Private Methods

        private void Awake()
        {
            instance = this; //Assigning Singleton
        }

        /// <summary>
        /// Animates the slider
        /// </summary>
        private void AnimateSlider()
        {
            sliderSequence = DOTween.Sequence().SetRecyclable(true).SetAutoKill(false);
            sliderSequence.Append(slider.DOValue(sliderEndValue, animDuration).SetEase(Ease.Linear));
            sliderSequence.SetLoops(-1, LoopType.Yoyo);
            sliderSequence.Restart();
            isPointerMoving = true;
        }

        /// <summary>
        /// Stops the slider animation
        /// </summary>
        private void StopSliderAnimation()
        {
            if (sliderSequence != null)
                sliderSequence.Kill();
            Debug.Log("SliderValue: " + slider.value);
            isPointerMoving = false;
        }

        /// <summary>
        /// Resets the slider to default position
        /// </summary>
        private void ResetSlider()
        {
            slider.value = sliderDefaultValue;
        }

        #endregion


    }
}


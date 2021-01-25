using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace m56
{
    /// <summary>
    /// Controls the bounce indicator scale UI 
    /// </summary>
    public class BounceController : MonoBehaviour
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

        private bool isPointerMoving;
        private Sequence sliderSequence;

        public static BounceController instance;

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
            sliderEndValue = slider.maxValue;
            sliderDefaultValue = slider.minValue;

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


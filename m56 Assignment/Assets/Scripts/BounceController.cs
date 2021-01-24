using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void StartSliderAnim()
    {
        if (!isPointerMoving)
        {
            ResetSlider();
            AnimateSlider();
        }
    }

    public void StopSlider()
    {
        StopSliderAnimation();
    }


    #endregion


    #region Private Methods

    private void Awake()
    {
        instance = this;
    }

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

    private void StopSliderAnimation()
    {
        if (sliderSequence != null)
            sliderSequence.Kill();
        Debug.Log("SliderValue: " + slider.value);
        isPointerMoving = false;
    }

    private void ResetSlider()
    {
        slider.value = sliderDefaultValue;
    }

    #endregion
}

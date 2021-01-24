using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BatsmanAnimHolder", menuName = "ScriptableObjects/BatsmanAnimHolder")]
public class OnboardingBatsmanAnimHolder : ScriptableObject
{
    [Header(" Batsman Shot Animations ")]
    public AnimationClip[] batsmanShotAnimations;
    public AnimationClip[] batShotAnimations;

    [Header(" Intro Onboarding Batsman Shot Animations ")]
    public AnimationClip[] onBoardingIntroGoodBatsmanShotAnimations;
    public AnimationClip[] onBoardingIntroGoodBatShotAnimations;
    public AnimationClip[] onBoardingIntroBadBatsmanShotAnimations;
    public AnimationClip[] onBoardingIntroBadBatShotAnimations;

    [Header("Batsman EndGame Animations")]
    public AnimationClip batsmanVictory;
    public AnimationClip batsmanDefeat;

    [Header("Bat EndGame Animations")]
    public AnimationClip batVictory;
    public AnimationClip batDefeat;


    #region Batsman and Bat Method

    public int GetAnimationCount()
    {
        return batsmanShotAnimations.Length;
    }
    public AnimationClip GetBatsmanShotAnimations(int animIndex)
    {
        return batsmanShotAnimations[animIndex];
    }
    public AnimationClip GetBatShotAnimations(int animIndex)
    {
        return batShotAnimations[animIndex];
    }
    public AnimationClip GetIntroGoodBatsmanShot(int animIndex)
    {
        return onBoardingIntroGoodBatsmanShotAnimations[animIndex];
    }
    public AnimationClip GetIntroBadBatsmanShot(int animIndex)
    {
        return onBoardingIntroBadBatsmanShotAnimations[animIndex];
    }
    public AnimationClip GetIntroGoodBatShotAnimations(int animIndex)
    {
        return onBoardingIntroGoodBatShotAnimations[animIndex];
    }
    public AnimationClip GetIntroBadBatShotAnimations(int animIndex)
    {
        return onBoardingIntroBadBatShotAnimations[animIndex];
    }
    public AnimationClip GetBatDefeatAnim()
    {
        return batDefeat;
    }
    public AnimationClip GetBatsmanDefeatAnim()
    {
        return batsmanDefeat;
    }
    public AnimationClip GetBatVictoryAnim()
    {
        return batVictory;
    }
    public AnimationClip GetBatsmanVictoryAnim()
    {
        return batsmanVictory;
    }
    #endregion
}


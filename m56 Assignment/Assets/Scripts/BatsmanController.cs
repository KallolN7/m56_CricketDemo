using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{ 
    /// <summary>
    /// Controls batsman animation
    /// </summary>
    public class BatsmanController : MonoBehaviour
    {
        [Header("Animation References")]
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private Animator animBat;
        [SerializeField]
        private Animator animHelmet;
        [SerializeField]
        private Vector3 startPosition;
        [SerializeField]
        private Transform batsmanHips;

        private AnimatorOverrideController _animatorOverrideController;
        private AnimatorOverrideController _animatorBatOverrideController;

        private Vector3 batsmanHipsPositionOnStart = new Vector3(0f, 0f, 0f);

        [SerializeField]
        private int shotSectorIndex;
        [SerializeField]
        private float animationSpeed = 1;
        [SerializeField]
        private OnboardingBatsmanAnimHolder batsmanAnimHolder;

        #region Public Methods

        /// <summary>
        /// Sets the batsman object to it's default values. Resets it to it's idle Animation
        /// </summary>
        public void InitBatsman()
        {
            GetAnimatorStateReferences();
            ResetToIdleAnimation();
        }

        /// <summary>
        /// Plays Batsman Shot animation
        /// </summary>
        public void PlayShot()
        {
            PlayBatsManAnimation(BatsmanAnimData.triggerShot, animationSpeed);
        }


        /// <summary>
        /// Resets batsman to it's idle Animation
        /// </summary>
        public void ResetToIdleAnimation()
        {
            ResetAllTriggers();
            transform.position = startPosition;
            batsmanHips.position = batsmanHipsPositionOnStart;
            PlayBatsManAnimation(BatsmanAnimData.triggerIdle, 1);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Plays Batsman animation based on trigger and animation speed
        /// </summary>
        /// <param name="animationTrigger"></param>
        /// <param name="animationSpeed"></param>
        private void PlayBatsManAnimation(int animationTrigger, float animationSpeed)
        {
            AssignCorrectShot();
            if (anim != null && animBat != null)
            {
                anim.speed = animBat.speed = animationSpeed;
                anim.SetTrigger(animationTrigger);
                animBat.SetTrigger(animationTrigger);

            }
            else
            {
                Debug.Log("BatsmanController,Is anim null ? " + (anim == null) + " is animBat null ? " + (animBat == null));
            }
        }


        /// <summary>
        /// Assigns random Shot animation clip animator. 
        /// </summary>
        private void AssignCorrectShot()
        {
            GetAnimatorStateReferences();
            int random = Random.Range(0, batsmanAnimHolder.GetAnimationCount());
            _animatorOverrideController[BatsmanAnimData.STATE_SHOT] = batsmanAnimHolder.GetBatsmanShotAnimations(random);
            _animatorBatOverrideController[BatsmanAnimData.STATE_SHOT] = batsmanAnimHolder.GetBatShotAnimations(random);
        }

        /// <summary>
        /// Resets all animator Triggers
        /// </summary>
        private void ResetAllTriggers()
        {
            anim.ResetTrigger(BatsmanAnimData.triggerIdle);
            animBat.ResetTrigger(BatsmanAnimData.triggerIdle);

            anim.ResetTrigger(BatsmanAnimData.triggerShot);
            animBat.ResetTrigger(BatsmanAnimData.triggerShot);
        }

        /// <summary>
        /// Gets Animator State Refernces
        /// </summary>
        private void GetAnimatorStateReferences()
        {
            //Batsman
            _animatorOverrideController = (AnimatorOverrideController)anim.runtimeAnimatorController;

            //Bat
            _animatorBatOverrideController = (AnimatorOverrideController)animBat.runtimeAnimatorController;
        }

        #endregion


    }
}


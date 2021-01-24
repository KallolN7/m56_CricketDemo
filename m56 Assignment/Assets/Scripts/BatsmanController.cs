using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{
    public class BatsmanController : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private Animator animBat;
        [SerializeField]
        private Animator animHelmet;

        private AnimatorOverrideController _animatorOverrideController;
        private AnimatorOverrideController _animatorBatOverrideController;

        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Transform batsmanHips;
        private Vector3 batsmanHipsPositionOnStart = new Vector3(0f, 0f, 0f);

        [SerializeField]
        private int shotSectorIndex;
        [SerializeField]
        private float animationSpeed = 1;
        [SerializeField]
        private OnboardingBatsmanAnimHolder batsmanAnimHolder;

        #region Public Methods

        public void InitBatsman()
        {
            GetAnimatorStateReferences();
            ResetToIdleAnimation();
        }

        public void PlayShot()
        {
            PlayBatsManAnimation(BatsmanAnimData.triggerShot, animationSpeed);
        }


        public void ResetToIdleAnimation()
        {
            ResetAllTriggers();
            transform.position = startPosition;
            batsmanHips.position = batsmanHipsPositionOnStart;
            PlayBatsManAnimation(BatsmanAnimData.triggerIdle, 1);
        }

        #endregion


        #region Private Methods
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

        private void AssignCorrectShot()
        {
            GetAnimatorStateReferences();
            int random = Random.Range(0, batsmanAnimHolder.GetAnimationCount());
            _animatorOverrideController[BatsmanAnimData.STATE_SHOT] = batsmanAnimHolder.GetBatsmanShotAnimations(random);
            _animatorBatOverrideController[BatsmanAnimData.STATE_SHOT] = batsmanAnimHolder.GetBatShotAnimations(random);
        }

        private void ResetAllTriggers()
        {
            anim.ResetTrigger(BatsmanAnimData.triggerIdle);
            animBat.ResetTrigger(BatsmanAnimData.triggerIdle);

            anim.ResetTrigger(BatsmanAnimData.triggerShot);
            animBat.ResetTrigger(BatsmanAnimData.triggerShot);
        }


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


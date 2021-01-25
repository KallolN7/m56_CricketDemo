using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{
    /// <summary>
    /// Controls the pitch indicator mvement
    /// </summary>
    public class BallPitchController : MonoBehaviour
    {
        public static BallPitchController instance;

        #region Public Methods

        /// <summary>
        /// Moves the pitch Indicator object on pitch based on user's input. 
        /// </summary>
        public void MovePitchIndicator()
        {
            float zMove = Input.GetAxis("Vertical") * Time.deltaTime * PitchIndicatorData.pitchIndicatorSpeed;
            float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * PitchIndicatorData.pitchIndicatorSpeed;
            transform.Translate(xMove, 0, zMove);
            Vector3 clampedPosition = transform.localPosition;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, PitchIndicatorData.xMinClampPos, PitchIndicatorData.xMaxClampPos);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, PitchIndicatorData.zMinClampPos, PitchIndicatorData.zMaxClampPos);
            transform.localPosition = clampedPosition;
        }

        /// <summary>
        /// Sets the pitch indicator to it's default position
        /// </summary>
        public void SetDefaultPos()
        {
            transform.localPosition = PitchIndicatorData.defaultPos;
        }


        /// <summary>
        /// Get current position of pitch indicator
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPitchIndicatorPosition()
        {
            return this.transform.position;
        }


        /// <summary>
        /// Get current Z position of pitch indicator
        /// </summary>
        /// <returns></returns>
        public float GetPitchIndicatorPosZ()
        {
            return this.transform.position.z;
        }

        /// <summary>
        /// Get current delta/deviation in X position from default  X position of pitch indicator
        /// </summary>
        /// <returns></returns>
        public float GetPitchIndicatorDeltaPosX()
        {
            float delta = transform.localPosition.x - PitchIndicatorData.defaultPos.x;
            return delta;
        }

        #endregion

        
        #region Private Methods

        private void Awake()
        {
            instance = this; //Assigning Singleton
        }

        #endregion

    }
}


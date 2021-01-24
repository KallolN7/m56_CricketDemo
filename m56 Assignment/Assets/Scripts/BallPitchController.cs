using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPitchController : MonoBehaviour
{
    public static BallPitchController instance;

    #region Public Methods

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

    public void SetDefaultPos()
    {
        transform.localPosition = PitchIndicatorData.defaultPos;
    }


    public Vector3 GetPitchIndicatorPosition()
    {
        return this.transform.position;
    }

    public float GetPitchIndicatorPosZ()
    {
        return this.transform.position.z;
    }

    public float GetPitchIndicatorDeltaPosX()
    {
        float delta = transform.localPosition.x - PitchIndicatorData.defaultPos.x;
        return delta;
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        instance = this;
    }

    #endregion

}

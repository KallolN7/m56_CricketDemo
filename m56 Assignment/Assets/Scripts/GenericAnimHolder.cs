using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericAnimHolder", menuName = "ScriptableObjects/GenericAnimHolder")]
public class GenericAnimHolder : ScriptableObject {

	public AnimationClip[] clips;
    public AnimationClip[] keeperCollectClips;
}

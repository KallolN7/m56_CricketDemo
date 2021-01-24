using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This Script will Hold All bowling Animations 
/// </summary>

[CreateAssetMenu(fileName ="BowlerAnimHolder", menuName ="ScriptableObjects/BowlerAnimHolder")]
public class BowlerAnimHolder : ScriptableObject
{
	public AnimationClip[] bowlerIdleAnimations;
	public BowlerAnimations[] bowlerAnimations;
	public AnimationClip[] bowlerBackToWicketAnimations; // These are Once Bowler releases the Ball bowler needs to come back to wickets position.

	public AnimationClip bowlerDejectedAnimation; // When Batsman Misses the ball Play this Animation.
	public AnimationClip GetBowlerAnim(int animSet, int animIndex)
	{
		return bowlerAnimations[animSet].animationClips[0]; // now using only one animation previously we had 10 animations for each bowler.
	}

	public AnimationClip GetIdleAnim()
	{
		return bowlerIdleAnimations[bowlerIdleAnimations.Length - 1];
	}

	public AnimationClip GetRandomAnimIdleClip()
	{
		return bowlerIdleAnimations[UnityEngine.Random.Range(0, bowlerIdleAnimations.Length - 1)];
	}

	public AnimationClip GetBackToWicketAnimationsClip(int animCount)
	{
		return bowlerBackToWicketAnimations[animCount];
	}

	public AnimationClip GetBowlerDejectedAnimationClip()
	{
		return bowlerDejectedAnimation;
	}
}

[System.Serializable]
public class BowlerAnimations
{
	public string bowlerName;
	public AnimationClip[] animationClips;
}

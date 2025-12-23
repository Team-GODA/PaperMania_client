using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	[SerializeField] private List<Effect> effects;
}

[System.Serializable]
public class Effect
{
	public GameObject EffectObj;
	public float Delay;
}

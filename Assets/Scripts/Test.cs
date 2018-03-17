using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedData;

public class Test : MonoBehaviour 
{
	[SharedVariable("vitality")]
	public FloatVariable life;

	public FloatVariable variable;
	public FloatReference reference;

	public VariableSet stats;

	private void Start()
	{
//		foreach (SharedVariable sv in stats) 
//		{
//			Debug.Log ( sv.GetObjectValue() );
//		}
	}

}

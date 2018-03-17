using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedData;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Slider))]
public class SliderSetter : MonoBehaviour
{
	[Tooltip("Slider to set by variable." )]
	public Slider slider;

	[Tooltip("Value to use as the current ")]
	public FloatVariable variable;

	private void OnEnable()
	{
		if (!this.slider)
			this.slider = GetComponent<Slider> ();
	}

	private void Update()
	{
		if( this.slider && this.variable )
			this.slider.value = variable;
	}
}

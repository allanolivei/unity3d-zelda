using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharedData;

public class ImageFillSetter : MonoBehaviour
{
	[Tooltip("Value to use as the current ")]
	public FloatVariable variable;

	[Tooltip("Min value that Variable to have no fill on Image.")]
	public float min;

	[Tooltip("Max value that Variable can be to fill Image.")]
	public float max;

	[Tooltip("Image to set the fill amount on." )]
	public Image Image;

	private void Update()
	{
		Image.fillAmount = Mathf.Clamp01(
			Mathf.InverseLerp(min, max, variable));
	}
}

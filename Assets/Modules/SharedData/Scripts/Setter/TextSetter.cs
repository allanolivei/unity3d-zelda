using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedData;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class TextSetter : MonoBehaviour
{
	[Tooltip("Text to set by variable." )]
	public Text text;

	[Tooltip("Value to use as the current ")]
	public FloatVariable variable;

	private void OnEnable()
	{
		if (!this.text)
			this.text = GetComponent<Text> ();
	}

	private void Update()
	{
		if( this.text && this.variable )
			this.text.text = variable;
	}
}

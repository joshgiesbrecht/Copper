using UnityEngine;
using System.Collections;

public class ScrollingContentResizer : MonoBehaviour {

	public RectTransform container;
	public RectTransform thingContained;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RectTransform c = (RectTransform)container.transform;
		RectTransform tc = (RectTransform)thingContained.transform;
		Vector2 newSD = new Vector2 (c.sizeDelta.x, tc.sizeDelta.y);
		c.sizeDelta = newSD;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.Runtime;

public class Main : MonoBehaviour {

	public VerticalLayoutGroup contentVlg;
	public LayoutGroup choiceLg;
	public ScrollRect scrollRect;
	public Text maintext;


	// Prefabs
//	public GameObject storychunkPrefab;
	public Button buttonPrefab;

	public InkScript inkScript;

	// tweakable thingies
	public int textDelay;

	// private variables
	private string nextLine;
	private bool waiting;
	private int delay;
	private bool typing;
	private GameObject nextStoryChunk;

	// Use this for initialization
	void Start () {
		nextLine = "";
		waiting = false;
		delay = 0;
		typing = false;
		textDelay = 1;

//		nextStoryChunk = Instantiate (storychunkPrefab);
//		nextStoryChunk.transform.SetParent (contentVlg.transform, false);
	}

	void Update() {
		if (!waiting) {
			if (typing) {
				delay++;
				if (delay > textDelay) {
					delay = 0;
					if (nextLine.Length > 0) {
//						nextStoryChunk.GetComponent<Text>().text = string.Concat (nextStoryChunk.GetComponent<Text>().text, nextLine[0]);
						maintext.text = string.Concat (maintext.text, nextLine[0]);
						nextLine = nextLine.Substring (1);
					} else {
						typing = false;
					}
				}
			} else if (inkScript.CanContinue ()) {
				typing = true;
				RemoveChoices ();

				nextLine = inkScript.Continue ();
//				nextStoryChunk = Instantiate (storychunkPrefab);
//				nextStoryChunk.transform.SetParent (contentVlg.transform, false);
//
//
//				RectTransform cv = (RectTransform)contentVlg.transform;
//				RectTransform st = (RectTransform)nextStoryChunk.transform;
//				Vector2 tmp = new Vector2(cv.sizeDelta.x ,cv.sizeDelta.y + st.sizeDelta.y + 10);
//				cv.sizeDelta = tmp;

				maintext.text = string.Concat (maintext.text, System.Environment.NewLine);

			} else {
				// check for choices!
				int numChoices = inkScript.choiceCount();
				if (numChoices > 0) {
					for (int i = 0; i < numChoices; i++) {
						Debug.Log ("adding button");
						UnityEngine.UI.Button nextButton = Instantiate (buttonPrefab);
						nextButton.transform.SetParent (choiceLg.transform, false);
						Text choiceText = nextButton.GetComponentInChildren<Text> ();
						choiceText.text = inkScript.getChoice (i);

						int tmp = i;
						nextButton.onClick.AddListener (delegate {
							makeChoice (tmp);
						});

					}
					waiting = true;
				} else {
					// I guess we're at the end of the story??!??!?!
				}
			}
		}
	}

	public void makeChoice(int i) {
		Debug.Log ("hi there " + i);
		inkScript.chooseChoice (i);
		waiting = false;
	}

	void RemoveChoices () {
		int childCount = choiceLg.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (choiceLg.transform.GetChild (i).gameObject);
		}
	}

}

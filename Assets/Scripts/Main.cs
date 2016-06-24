using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Ink.Runtime;

public class Main : MonoBehaviour {

	public Text maintext;

	public Text choice1;
	public Text choice2;
	public Text choice3;
	public Text choice4;

	public Button b1;
	public Button b2;
	public Button b3;
	public Button b4;

	public InkScript inkScript;

	public int textDelay;

	private string nextLine;
	private bool waiting;
	private int delay;
	private bool typing;
	private int lineCount;

	// Use this for initialization
	void Start () {
		nextLine = "";
		maintext.text = "";
		waiting = false;
		delay = 0;
		typing = false;
		lineCount = 0;
	}

	void Update() {
		if (!waiting) {
			if (typing) {
				delay++;
				if (delay > textDelay) {
					delay = 0;
					if (nextLine.Length > 0) {
						maintext.text = string.Concat (maintext.text, nextLine[0]);
						nextLine = nextLine.Substring (1);
					} else {
						typing = false;
						if (lineCount > 5) {
							// trim the top line off hopefully
							maintext.text = maintext.text.Substring (
								maintext.text.IndexOf (System.Environment.NewLine));
						}

					}
				}
			} else if (inkScript.CanContinue ()) {
				typing = true;
				b1.interactable = false;
				b2.interactable = false;
				b3.interactable = false;
				b4.interactable = false;
				choice1.text = "";
				choice2.text = "";
				choice3.text = "";
				choice4.text = "";
				nextLine = inkScript.Continue ();
				maintext.text = string.Concat (maintext.text, System.Environment.NewLine);
				lineCount++;
			} else {
				// check for choices?
				int numChoices = inkScript.choiceCount();
				if (numChoices > 0) {
					b1.interactable = true;
					waiting = true;
					choice1.text = inkScript.getChoice (0);
				}
				if (numChoices > 1) {
					b2.interactable = true;
					choice2.text = inkScript.getChoice (1);
				}
				if (numChoices > 2) {
					b3.interactable = true;
					choice3.text = inkScript.getChoice (2);
				}
				if (numChoices > 3) {
					b4.interactable = true;
					choice4.text = inkScript.getChoice (3);
				}
				if (numChoices > 4) {
					Debug.Log ("WE NEED MOAR BUTTONS");
				}
			}
		}
	}

	public void makeChoice(int i) {
		inkScript.chooseChoice (i);
		waiting = false;
	}

}

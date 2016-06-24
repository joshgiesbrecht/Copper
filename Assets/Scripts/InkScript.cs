using UnityEngine;
using System.Collections;
using Ink.Runtime;

public class InkScript : MonoBehaviour {

	public TextAsset inkAsset;

	Story _inkStory;

	void Awake() {
		_inkStory = new Story (inkAsset.text);
	}
	
	public bool CanContinue() {
		return _inkStory.canContinue;
	}

	public string Continue() {
		return _inkStory.Continue();
	}

	public int choiceCount() {
		return _inkStory.currentChoices.Count;
	}

	public string getChoice(int i) {
		return _inkStory.currentChoices [i].text;
	}

	public void chooseChoice(int i) {
		_inkStory.ChooseChoiceIndex (i);
	}

	public void test() {
		while (_inkStory.canContinue) {
			Debug.Log (_inkStory.Continue ());
		}
	}
}

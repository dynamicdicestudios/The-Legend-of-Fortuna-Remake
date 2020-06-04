using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;
	bool triggered;
	
	public void TriggerDialogue() {
		if (!triggered) {
			FindObjectOfType<DialogManager>().StartDialogue(dialogue);
			triggered = true;
		} else
			FindObjectOfType<DialogManager>().DisplayNextSentence();
	}
}

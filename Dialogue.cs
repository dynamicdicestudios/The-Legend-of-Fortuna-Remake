using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{	
	public AudioClip[] sounds;
	public Sprite[] characters;
	public string[] names;
	[TextArea(3, 10)]
    public string[] sentences;
}

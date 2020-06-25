using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	public AudioSource audioSource;
	public string animation;
	public Image characterImage;
	public Text nameText;
	public Text dialogueText;
	
	private AudioClip lastSound;
	
    private Queue<AudioClip> sounds;
	private Queue<Sprite> characters;
	private Queue<string> names;
	private Queue<string> sentences;
	
	// Start is called before the first frame update
    void Start()
    {
		sounds = new Queue<AudioClip>();
		characters = new Queue<Sprite>();
		names = new Queue<string>();
		sentences = new Queue<string>();
    }
	
	public void StartDialogue(Dialogue dialogue) {		
		characters.Clear();
		names.Clear();
		sentences.Clear();
		
		foreach (AudioClip sound in dialogue.sounds) {
			sounds.Enqueue(sound);
		}
		
		foreach (Sprite character in dialogue.characters) {
			characters.Enqueue(character);
		}
		
		foreach (string name in dialogue.names) {
			names.Enqueue(name);
		}
		
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}
		
		DisplayNextSentence();
	}
	
	public void DisplayNextSentence() {
			
		if (sentences.Count == 0) {
			FindObjectOfType<LevelChanger>().FadeToLevel(animation);
			return;
		}
		
		AudioClip sound = sounds.Dequeue();
		Sprite character = characters.Dequeue();
		string name = names.Dequeue();
		string sentence = sentences.Dequeue();
		
		if (lastSound != sound) {
			audioSource.clip = sound;
			audioSource.Play();
			lastSound = sound;
		}
		characterImage.GetComponent<Image>().sprite = character;
		nameText.text = name;
		dialogueText.text = sentence;
	}	
}

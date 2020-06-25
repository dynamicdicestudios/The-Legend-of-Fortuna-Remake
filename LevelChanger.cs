using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelChanger : MonoBehaviour
{
    public Animator animator;
	//public AudioSource audioSource;
	private float delay = 1f;
	
	public void FadeToLevel(string animation) {
		animator.Play(animation);
	}
	
	/*IEnumerator FadeMusic() {
		float elapsedTime = 0;
        float currentVolume = audioSource.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
	}*/
	
	public void nextScene(string scene) {
		
		//StartCoroutine(FadeMusic());
		
		//SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("Assets/Scenes/" + scene + ".unity");
	}
}

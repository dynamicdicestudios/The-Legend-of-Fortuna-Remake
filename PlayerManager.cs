using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public GameObject avatar1, avatar2;
	
	Vector3 current_pos;
	int avatarOn = 1;
	
    // Start is called before the first frame update
    void Start()
    {
		avatar1.GetComponent<PlayerController>().Activate();
		avatar2.GetComponent<PlayerController>().Deactivate();
    }
	
	public void SwitchAvatar() {
		
		switch (avatarOn) {
			
		case 1: 
			avatarOn = 2;
			
			current_pos = avatar1.gameObject.transform.position;
			avatar1.GetComponent<PlayerController>().Deactivate();
			
			avatar2.GetComponent<PlayerController>().Activate();
			avatar2.gameObject.transform.position = current_pos;
			
			break;
			
		case 2:
			avatarOn = 1;
			
			current_pos = avatar2.gameObject.transform.position;
			avatar2.GetComponent<PlayerController>().Deactivate();
			
			avatar1.GetComponent<PlayerController>().Activate();
			avatar1.gameObject.transform.position = current_pos;
			
			break;
		}
	}
	
    public int GetAvatarOn()
    {
        return avatarOn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{	
	[SerializeField]
	float timeOffset = 1;
	
	[SerializeField]
	Vector2 posOffset = new Vector2 (0.5f, 0.5f);

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position;
		Vector3 endPos = FindObjectOfType<PlayerController>().transform.position;
		endPos.x += posOffset.x;
		endPos.y += posOffset.y;
		endPos.z = -10;
		
		transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
		
		
		
		transform.position = new Vector3(FindObjectOfType<PlayerController>().transform.position.x, FindObjectOfType<PlayerController>().transform.position.y, -10); 
		
		
		
    }
}

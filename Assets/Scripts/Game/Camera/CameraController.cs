using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float maxHeight;

    private void Start()
    {
        maxHeight = target.transform.position.y;
    }

    private void LateUpdate()
    {
        // If we're reaching new heights, update maxHeight and score
        if (target.transform.position.y > maxHeight){
            maxHeight = target.transform.position.y;
            ScoreManager.Instance.AddScore(1);
        }
        // Follow player vertically, but not back below at maxHeight
        transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z); 
    }
}

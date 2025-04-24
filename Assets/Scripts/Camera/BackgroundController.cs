using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject camera;
    public float parallaxEffect; //background speed relative to camera

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void FixedUpdate()
    {
        //calculate distance background move based on cam movement
        float distance = camera.transform.position.x * parallaxEffect;// 0 = move with cam || 1 = not moving || 0.5 = half
        float movement = camera.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        //infinite scroller
        if(movement > startPos + length)
        {
            startPos += length;
        }
        else if(movement < startPos - length)
        {
            startPos -= length;
        }
    }
}

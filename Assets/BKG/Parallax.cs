using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float parallaxEffect;
    [SerializeField]
    new private Transform camera;

    private ParallaxController controller;

    private float length, startpos;
    
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        parallaxEffect = 1 - (this.transform.position.z / 10);
        controller = GetComponentInParent<ParallaxController>();
    }

    void Update()
    {
        //Guard clause checking parent's IsStarted state
        if (!controller.isStarted)
        {
            return; 
        }

        float temp = camera.position.x * (1 - parallaxEffect);
        float dist = (camera.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}

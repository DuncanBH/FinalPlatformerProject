using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    bool doStartLine;
    [SerializeField]
    Transform startLinePosition;

    public bool isStarted = false;

    int _layerMask;

    void Awake()
    {
         _layerMask = LayerMask.GetMask("Player");
    }
    void Update()
    {
        //if startline is enabled and hasn't started yet
        if (doStartLine && !isStarted)
        {
            RaycastHit2D raycast = Physics2D.Raycast(startLinePosition.position, Vector2.up, 10, _layerMask);

            if (raycast)
            {
                isStarted = true;
            }
        }
        //if no startline, just start it.
        else if (!doStartLine)
        {
            isStarted = true;
        }
    }
}

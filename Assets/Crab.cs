using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Enemy
{
    void Start()
    {
        if (Random.Range(1, 100) > 95)
        {
            this.transform.localScale = new Vector3(10, 10, 1);
        }
    }

    void Update()
    {
        
    }
}

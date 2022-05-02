using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flutterby : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField]
    GameObject point;
    [SerializeField]
    int direction;


    public float angleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }
    // Update is called once per frame
    void Update()
    {
        /*transform.RotateAround(point.transform.position, new Vector3(0, 0, 1), angleSpeed*Time.deltaTime);
        var dir = -(point.transform.position - transform.position);//you might not need the - in the beginning depending how your scene is setup
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
        var rotation = transform.rotation;
        transform.RotateAround(point.transform.position, Vector3.forward*direction, angleSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
    
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Enemy
{
    [SerializeField]
    private bool StartGiant = false;
    private static bool playingMusic;
    private float jumpTimer;

    new Rigidbody2D rigidbody;
    private bool ready = false;

    void Start()
    {
        if (Random.Range(1, 100) > 90 || StartGiant)
        {
            this.transform.localScale = new Vector3(10, 10, 1);
            Debug.Log("Giant enemy crab! pos: " + transform.position + " scale: " + transform.localScale);
            if (!playingMusic)
            {
                GetComponent<AudioSource>().enabled = true;
                playingMusic = true;
            }
        }
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.up * 0.5f, ForceMode2D.Impulse);

        jumpTimer = Random.Range(0, 10f);

        StartCoroutine(ReadyUp());
    }

    private IEnumerator ReadyUp()
    {
        yield return new WaitForSeconds(1f);
        ready = true;
    }

    void Update()
    {
        if (jumpTimer > 10f)
        {
            rigidbody.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
            jumpTimer = 0.0f;
        }
        jumpTimer += Time.deltaTime;
    }
    public override void takeDamage(int damage)
    {
        if (!ready) { return; }

        this.Health -= damage;

        if (Health < 0)
        {
            //Play Death animation
            animator.Play("Base Layer.EnemyDie", 0);
        }
    }
}

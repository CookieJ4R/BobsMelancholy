using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{

    public GameObject fallingObject;

    public Vector3 fallTowards;
    public float speed = 0.5f;
    public float secondDelay = 0;

    bool falling = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("TriggerDrop", secondDelay);
        }
    }


    private void Update()
    {
        if (falling)
            fallingObject.transform.position = Vector3.MoveTowards(fallingObject.transform.position, fallTowards, speed);


        if (fallingObject.transform.position.y == fallTowards.y - 0.1f)
            falling = false;

    }

    void TriggerDrop()
    {
        falling = true;
        for (int i = 0; i < fallingObject.transform.childCount; i++)
        {
            fallingObject.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}

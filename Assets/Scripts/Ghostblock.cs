using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostblock : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Animation>().Play();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGoal : MonoBehaviour
{

    GameObject player;

    Vector3 endPoint;

    private void Start()
    {
         endPoint = new Vector3(this.transform.position.x, this.transform.position.y - 1.25f, this.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.Find("Player");
        player.GetComponent<Player>().canMove = false;
        player.GetComponent<Player>().prohibitAnyMovement = true;
        
    }

    private void Update()
    {
        if (player != null)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, endPoint, 0.1f);

            if (player.transform.position == endPoint)
            {
                GetComponent<Animation>().Play();
                player = null;
            }
        }
    }

}

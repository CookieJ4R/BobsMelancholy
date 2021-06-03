using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public Vector2[] blockPositions;
    public GameObject block;

    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !triggered)
        {
            foreach (Vector2 pos in blockPositions)
                Instantiate(block, pos, Quaternion.identity).transform.parent = this.gameObject.transform.parent;

            triggered = true;
        }
    }

}

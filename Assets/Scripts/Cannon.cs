using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject bullet;

    public bool ghostShots;

    public bool alternateGhostShots;

    public float initialDelay;
    public float repeatDelay;

    public float bulletDestroyTime;
    public Vector2 bulletSpeed;

    int shots = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", initialDelay, repeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        shots++;
        GameObject bulletGO = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletGO.transform.parent = this.gameObject.transform.parent;
        bulletGO.name = "bullet";
        bulletGO.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        if (ghostShots)
        {
            if(!alternateGhostShots || (alternateGhostShots && shots % 2 == 0))
                bulletGO.GetComponent<BoxCollider2D>().enabled = false;
        }
        Destroy(bulletGO, bulletDestroyTime);

        if (shots == 25)
            shots = -1;

    }

}

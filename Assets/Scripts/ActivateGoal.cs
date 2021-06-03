using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGoal : MonoBehaviour
{

    public GameObject toActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        toActivate.GetComponent<PlatformController>().enabled = true;
        toActivate.GetComponent<PlatformController>().speed = 25f;
        Invoke("slowSpeed", 5f);
        Invoke("disableScript", 14f);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }


    public void slowSpeed()
    {
        toActivate.GetComponent<PlatformController>().speed = 2.5f;
    }

    void disableScript()
    {
        toActivate.GetComponent<PlatformController>().enabled = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public int id;
    public GameObject audioSource;

    private void OnEnable()
    {
        audioSource.GetComponent<ISoundPlayer>().PlaySound(id);
    }
}

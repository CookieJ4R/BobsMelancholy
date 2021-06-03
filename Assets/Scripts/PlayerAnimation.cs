using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public GameObject dashEffect;

    public GameObject deathEffect;

    AudioManager audioManager;

    Vector2 input;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(input.x != 0)
            spriteRenderer.flipX = (input.x == -1) ? true : false;

    }

    public void playDashAnimation()
    {
        audioManager.PlayDashSound();
        Camera.main.GetComponent<ShakeBehaviour>().TriggerShake(0.15f, 0.1f);
        StartCoroutine(spawnPlayerShade());

    }

    public void playDeathAnimation() {
        audioManager.PlayDeathSound();
        GameObject go = Instantiate(deathEffect);
        Camera.main.GetComponent<ShakeBehaviour>().TriggerShake(0.1f, 0.2f);
        go.transform.position = transform.position;
        Destroy(this.gameObject);
        Destroy(go, 1f);

    }

    IEnumerator spawnPlayerShade()
    {
        GameObject go = Instantiate(dashEffect, this.transform.position, Quaternion.identity);
        Destroy(go, 1f);
        yield return new WaitForSeconds(0.05f);
        GameObject go1 = Instantiate(dashEffect, this.transform.position, Quaternion.identity);
        Destroy(go1, 1f);
        yield return new WaitForSeconds(0.05f);
        GameObject go2 = Instantiate(dashEffect, this.transform.position, Quaternion.identity);
        Destroy(go2, 1f);
    }

}

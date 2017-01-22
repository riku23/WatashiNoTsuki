using UnityEngine;
using System.Collections;

public class CloudPlatformScript : MonoBehaviour
{
    public float timeForDestroy, timeForRegenerate;
    BoxCollider2D boxCollider;
    // SpriteRenderer spriteRenderer;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("COLLIDE");
        if (other.gameObject.tag == "Player")
        {
            Invoke("DestroyPlatform", timeForDestroy);
            Invoke("RegeneratePlatform", timeForRegenerate);
        }
      
    }

   

    void DestroyPlatform()
    {
        anim.SetBool("Destroy", true);
        //spriteRenderer.color = Color.red;
        //gameObject.SetActive(false);
        boxCollider.enabled = false;
    }

    void RegeneratePlatform()
    {
        anim.SetBool("Destroy", false);
        //spriteRenderer.color = Color.white;
        //gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

}

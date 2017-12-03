using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxEffect : MonoBehaviour
{
    private Random rnd = new Random();
    private Player player;
    
    public GameObject slow;
    public GameObject fast;

    // Use this for initialization.
    void Start()
    {
        player = GetComponent<Player>();
    }
    
    // Update is called once per frame.
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MysteryBox")
        {
            int effect = (int)Random.Range(0, 1.9f);

            switch (effect)
            {
                // Slow the player.
                case 0:
                    player.multiplier = Random.Range(0.5f, 0.9f);
                    player.multiplierDuration = 10f;
                    slow.SetActive(true);
                    StartCoroutine(DisableEffect(player.multiplierDuration, slow));
                    break;

                // Accelerate the player.
                case 1:
                    player.multiplier = Random.Range(1.1f, 1.5f);
                    player.multiplierDuration = 10f;
                    fast.SetActive(true);
                    StartCoroutine(DisableEffect(player.multiplierDuration, fast));
                    break;

                default:
                    break;
            }
        }
    }

    IEnumerator DisableEffect(float waitTime, GameObject effect)
    {
        yield return new WaitForSeconds(waitTime);
        effect.SetActive(false);
        player.multiplier = 1;
    }
}

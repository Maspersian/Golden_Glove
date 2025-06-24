using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trap : MonoBehaviour
{
    public int key = 0;
    public int coin = 0;


   
    [SerializeField] Animator player;
    [SerializeField] Animator chest;
    [SerializeField] Animator cup;
    [SerializeField] Animator finish;
    [SerializeField] Animator respawn;
    [SerializeField] Animator enemy;
    [SerializeField] TextMeshProUGUI coinTM;
    [SerializeField] TextMeshProUGUI keyTM;
    [SerializeField] GameObject gameOverPannel;
    [SerializeField] GameObject gameFinishPannel;
    [SerializeField] GameObject[] healthArray;
    [SerializeField] int health;
    [SerializeField] bool checkPointed;
    [SerializeField] Transform checkPoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip keySfx;
    [SerializeField] AudioClip coinSfx;
    [SerializeField] AudioClip hitSfx;
    [SerializeField] AudioClip respawnSfx;
    [SerializeField] AudioClip worldComSfx;
    [SerializeField] AudioClip waterOverSfx;
    [SerializeField] AudioClip finishSfx;
    [SerializeField] AudioClip trapSfx;
    [SerializeField] AudioClip enemyKillSfx;
    [SerializeField] AudioClip enemyKillSfx2;

    // Start is called before the first frame update
    void Start()
    {
        health = 6;
    }

    // Update is called once per frame
    void Update()
    {
        Healthv();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            health -= 1;
            audioSource.PlayOneShot(trapSfx);
        }
        if (collision.gameObject.tag == "Water")
        {
            gameObject.SetActive(false);
            gameOverPannel.SetActive(true);
            audioSource.PlayOneShot(waterOverSfx);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            player.SetBool("Hit", true);
            Debug.Log("Hit");
            health -= 1;
            audioSource.PlayOneShot(hitSfx);
        }

        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("collected");
            key += 1;
            keyTM.text = key.ToString();
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(keySfx);
        }
        if (key == 1)
        {
            if (collision.gameObject.tag == "Chest")
            {
                chest.SetBool("Open", true);
                Debug.Log("chest open");
                coin += 10;
                audioSource.PlayOneShot(keySfx);

            }
        }
        if (collision.gameObject.tag == "Cup")
        {
            cup.SetBool("Finish", true);
            Debug.Log("Finish");
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            player.SetBool("Hit", false);
            Debug.Log("Hitachi");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            finish.SetBool("Flaping", true);
            enemy.SetBool("Hit", true);
            Invoke("Finish", 5f);
            
        }
        if (collision.gameObject.tag == "Respawn")
        {
            respawn.SetBool("Flaping", true);
            checkPointed = true;
        }
        if (collision.gameObject.tag == "Coin")
        {
            coin += 1;
            coinTM.text = coin.ToString();
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinSfx);
        }
    }
    void Finish()
    {
        gameFinishPannel.SetActive(true);
        audioSource.clip = worldComSfx;
        audioSource.Play();
        
    }
    void Healthv()
    {
        if (health == 6)
        {
            healthArray[6].SetActive(true);
            healthArray[5].SetActive(false);
            healthArray[4].SetActive(false);
            healthArray[3].SetActive(false);
            healthArray[2].SetActive(false);
            healthArray[1].SetActive(false);
            healthArray[0].SetActive(false);

        }
        if (health == 5)
        {
            healthArray[6].SetActive(false);
            healthArray[5].SetActive(true);
            healthArray[4].SetActive(false);
            healthArray[3].SetActive(false);
            healthArray[2].SetActive(false);
            healthArray[1].SetActive(false);
            healthArray[0].SetActive(false);

        }
        if (health == 4)
        {
            healthArray[6].SetActive(false);
            healthArray[5].SetActive(false);
            healthArray[4].SetActive(true);
            healthArray[3].SetActive(false);
            healthArray[2].SetActive(false);
            healthArray[1].SetActive(false);
            healthArray[0].SetActive(false);

        }
        if (health == 3)
        {
            healthArray[6].SetActive(false);
            healthArray[5].SetActive(false);
            healthArray[4].SetActive(false);
            healthArray[3].SetActive(true);
            healthArray[2].SetActive(false);
            healthArray[1].SetActive(false);
            healthArray[0].SetActive(false);

        }
        if (health == 2)
        {
            healthArray[6].SetActive(false);
            healthArray[5].SetActive(false);
            healthArray[4].SetActive(false);
            healthArray[3].SetActive(false);
            healthArray[2].SetActive(true);
            healthArray[1].SetActive(false);
            healthArray[0].SetActive(false);

        }
        if (health == 1)
        {
            healthArray[6].SetActive(false);
            healthArray[5].SetActive(false);
            healthArray[4].SetActive(false);
            healthArray[3].SetActive(false);
            healthArray[2].SetActive(false);
            healthArray[1].SetActive(true);
            healthArray[0].SetActive(false);

        }
        if (health == 0)
        {
            healthArray[6].SetActive(false);
            healthArray[5].SetActive(false);
            healthArray[4].SetActive(false);
            healthArray[3].SetActive(false);
            healthArray[2].SetActive(false);
            healthArray[1].SetActive(false);
            healthArray[0].SetActive(true);

        }
        if (health == 0)
        {
            Time.timeScale = 0;
            audioSource.PlayOneShot(enemyKillSfx);
            Invoke("EnemyKill", 2f);
           // gameOverPannel.SetActive(true);

        }

    }
    public void Respawn()
    {
        if (checkPointed)
        {
            transform.position = checkPoint.position;
            gameObject.SetActive(true);
            health = 6;
            gameOverPannel.SetActive(false);
            audioSource.PlayOneShot(respawnSfx);
        }
    }
    private void EnemyKill()
    {
        gameOverPannel.SetActive(true);
        audioSource.PlayOneShot(enemyKillSfx2);
    }

}

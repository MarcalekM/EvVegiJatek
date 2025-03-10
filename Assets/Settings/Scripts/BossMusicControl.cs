using System.Collections;
using System.Linq;
using UnityEngine;

public class BossMusicControl : MonoBehaviour
{
    [SerializeField] public AudioSource bossMusic1;
    [SerializeField] public AudioSource bossMusic2;
    [SerializeField] public BossStage2 bossStage2;

    [SerializeField] public GameObject blockGoingBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!bossStage2.Stage2 && bossMusic1.isPlaying) SwapMusic();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            blockGoingBack.GetComponent<Collider2D>().enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.tag == "Player")
        {
            bossMusic1.Play();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.FindObjectsOfType<Ghost>(true).ToList().ForEach(g => g.gameObject.SetActive(true));
            FindObjectOfType<Boss>()?.ActivateBoss();
        }
        //this.gameObject.SetActive(false);
    }

    public void SwapMusic()
    {
        StopAllCoroutines();

        StartCoroutine(FadeMusic());
    }

    private IEnumerator FadeMusic()
    {
        float timeToFade = 2.5f;
        float timeElapsed = 0;
        bossMusic2.Play();
        while (timeElapsed < timeToFade)
        {
            bossMusic1.volume = Mathf.Lerp(0.75f, 0, timeElapsed / timeToFade);
            bossMusic2.volume = Mathf.Lerp(0, 0.75f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bossMusic1.Stop();
    }
}
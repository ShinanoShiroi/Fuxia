using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 startPosition;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        AudioManager.instance.StopPlaying("IntroMusic");
        AudioManager.instance.Play("InGameMusic");
    }

    void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //detect collision with trigger
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "death")
        {
            player.transform.position = startPosition;
            player.transform.rotation = startRotation;
            Physics.SyncTransforms();

            Debug.Log("Fall!");

            AudioManager.instance.Play("PlayerVoidFall");

        }
        else if (col.tag == "checkpoint")
        {
            startPosition = col.transform.position;
            startRotation = col.transform.rotation;

            Destroy(col.gameObject);
            AudioManager.instance.Play("CheckpointSound");


        }
        else if (col.tag == "endlevel")
        {
            if (CoinScoreScript.coinAmount == 6)
            {
                Destroy(col.gameObject);
                GetComponent<Animator>().Play("WIN00", -1, 0f);
                Invoke("nextLevel", 2f);
                AudioManager.instance.Play("LevelupSound");
            }
            else
            {
                AudioManager.instance.Play("NotAllCoinsSound");
            }

        }
    }
}

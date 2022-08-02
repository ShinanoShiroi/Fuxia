using UnityEngine;

public class PlatformAttach : MonoBehaviour
{

    public GameObject Player;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Player.transform.parent = null;
        }
    }
}

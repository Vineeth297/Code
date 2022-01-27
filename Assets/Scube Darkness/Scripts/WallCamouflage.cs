using UnityEngine;

public class WallCamouflage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.tag = "Hidden";
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Hidden")) return;
        other.tag = "Player";
    }
}

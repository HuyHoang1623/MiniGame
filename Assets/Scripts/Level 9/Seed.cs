using UnityEngine;

public class Seed : MonoBehaviour
{
    private bool _hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_hasTriggered) return;

        if (collider.CompareTag("Fork"))
        {
            _hasTriggered = true;
            Done();

            DFManager.Instance.MarkSeedAsTriggered(this);
        }
    }

    private void Done()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
using UnityEngine;

public class TokenControl : MonoBehaviour
{
    [SerializeField] private CoinsManager coinsManager;  // Reference to the CoinsManager component

    void Update()
    {
        transform.Rotate(0, 0, 0.2f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
            if(coinsManager != null) 
            {
                coinsManager.CoinCollected(); // Call the function directly on the referenced component
            }
            else
            {
                Debug.LogError("CoinsManager reference is null in TokenControl.");
            }
        }
    }
}

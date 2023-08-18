using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject obstaclePrefab; 
        [SerializeField] private GameObject coinPrefab; 


    [SerializeField] private float spawnSpacing = 100;
    private float roadLength;
    private float roadWidth;
  
  private void Start() {
BoxCollider boxCollider = this.GetComponent<BoxCollider>();
if (boxCollider != null)
{
    Vector3 size = boxCollider.size;
    Vector3 worldSize = Vector3.Scale(size, transform.localScale);
    roadLength = worldSize.x;
    roadWidth = worldSize.z;
    
        SpawnObstacles();

}

  }
    void SpawnObstacles()
{
    // Calculate the number of obstacles
    int numberOfObstacles = Mathf.FloorToInt(roadLength / spawnSpacing);
   
    for (int i = 2; i < numberOfObstacles; i++)
    {
        float zPos = i * spawnSpacing;
        int[] numbers = { 5, -5, 0 }; 
        float xPos = numbers[Random.Range(0, 3)];

       if (i % 5 == 0)
    {   
              Vector3 spawnPosition = new Vector3(xPos, 1.3f, zPos);

    // Rotation Quaternion for the ramp
        Quaternion rampRotation = Quaternion.Euler(90, 0, 0);
        Instantiate(coinPrefab, spawnPosition, rampRotation);
    } 
        else {
                        Vector3 spawnPosition = new Vector3(xPos, 0, zPos);

    Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
       
       

        // Combine the randomized X and Z position with the constant Y position (assuming Y = 0)

        // Instantiate the obstacle at the calculated position
        
        }
        
    
}
}

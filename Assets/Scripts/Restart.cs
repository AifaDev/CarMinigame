using UnityEngine;
using UnityEngine.SceneManagement;

class Restart : MonoBehaviour
{
public void RestartScene()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
}
}

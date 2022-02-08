using UnityEngine;
using UnityEngine.SceneManagement;

public class burgier_death : MonoBehaviour
{
    public void Death()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

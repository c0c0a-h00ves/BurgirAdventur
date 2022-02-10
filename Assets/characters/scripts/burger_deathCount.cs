using UnityEngine;
using UnityEngine.UI;

public class burger_deathCount : MonoBehaviour
{
    public Text deathCount;

    private static int _death;

    
    // Update is called once per frame
    void Update()
    {
        ShowDeathCounter();
    }

    public void ShowDeathCounter()
    {
        deathCount.text = _death.ToString();
    }

    public void addDeath()
    {
        _death++;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

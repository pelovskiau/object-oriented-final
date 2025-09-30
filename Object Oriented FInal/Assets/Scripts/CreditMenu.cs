using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditMenu : MonoBehaviour
{
    private Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = transform.Find("BacktoMenu").GetComponent<Button>();
    }

    // Update is called once per frame
    public void Leave()
    {
        SceneManager.LoadScene(0);
    }
}

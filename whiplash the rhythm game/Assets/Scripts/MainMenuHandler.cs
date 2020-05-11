using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuPannel;
    [SerializeField]
    private GameObject SwitchPannel;
    [SerializeField]
    private GameObject AboutPannel;

    public void NewGame()
    {
        MainMenuPannel.gameObject.SetActive(false);
        SwitchPannel.gameObject.SetActive(true);
    }
    public void About()
    {
        MainMenuPannel.gameObject.SetActive(false);
        AboutPannel.gameObject.SetActive(true);
    }
    public void Back()
    {
        MainMenuPannel.gameObject.SetActive(true);
        SwitchPannel.gameObject.SetActive(false);
        AboutPannel.gameObject.SetActive(false);
    }

    public void Song1()
    {
        SceneManager.LoadScene("lvl0");
    }

    public void Song2()
    {
        SceneManager.LoadScene("lvl1");
    }
    public void Song3()
    {
        SceneManager.LoadScene("lvl2");
    }
    public void Exit()
    {
        Application.Quit();
    }
}

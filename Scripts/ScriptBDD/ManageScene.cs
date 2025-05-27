using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public static void LoadS_Inscrire()
    {
        SceneManager.LoadScene("S_Inscrire");
    }
    public static void LoadSeConnecter()
    {
        SceneManager.LoadScene("Se_Connecter");
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quitter()
    {
        Application.Quit();
        //EditorApplication.ExitPlaymode();
    }
}

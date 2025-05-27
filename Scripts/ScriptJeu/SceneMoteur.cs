using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoteur : MonoBehaviour
{

    [SerializeField] GameObject appuyerSurG;

    public GameObject pere;
    GameObject fils;
    InventoryItem test;

    [SerializeField] GameObject afficherObjet;

    bool voirSlot = false;

    [SerializeField] GameObject loadSceneMoteur;


    //enregistrer position joueur
    Transform joueur;
    [HideInInspector] public static Vector3 positionJoueur;
    [HideInInspector] public static bool entrer=false;




    private void Start()
    {
        joueur = GameObject.Find("Ch17_nonPBR").transform;
        fils = pere.transform.GetChild(0).gameObject;
        test = fils.GetComponent<InventoryItem>();
        //
        VoirSlot();
    }

    private void Update()
    {
        VoirSlot();
    }

    private void OnTriggerEnter(Collider other)
    {
        entrer = true;

        if (voirSlot && test.nameO == "Thermometre")
        {
            appuyerSurG.SetActive(true);
        }
        else
        {
            afficherObjet.SetActive(true);
            afficherObjet.GetComponent<TextMeshProUGUI>().SetText("Il faut un Thermometre");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(joueur.position);
        if (Input.GetKeyDown(KeyCode.G) && appuyerSurG.active)
        {
            StartCoroutine(ChargerScene());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        entrer = false;
        appuyerSurG.SetActive(false);
        afficherObjet.SetActive(false);
    }

    IEnumerator ChargerScene()
    {
        positionJoueur = joueur.position;
        loadSceneMoteur.SetActive(true);

        loadSceneMoteur.GetComponent<Animator>().Play("Debut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Moteur");

    }


    public void VoirSlot()
    {
        try
        {
            //Debug.Log("try");
            voirSlot = true;
            fils = pere.transform.GetChild(0).gameObject;
            if (fils != null)
            {
                test = fils.GetComponent<InventoryItem>();
            }
        }
        catch
        {
            //Debug.Log("catch");
            voirSlot = false;
        }
    }
}

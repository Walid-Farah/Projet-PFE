using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vannes : MonoBehaviour
{
    float tournerX, tournerY;
    private Camera mainCamera;

    [SerializeField] GameObject manometre;


    [SerializeField] TMP_Text objetToucher;
    [SerializeField] TMP_Text points;
    int nbrpoints = 200;
    //
    bool verifierPoint = true;



    void Start()
    {
        mainCamera = Camera.main;
        DataBase.CreationLignePE(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (!PauseMenu.gamePaused)
        {
            Tourner();
            VerifierPoints();
        }
        
    }

    void Tourner()
    {
        
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            tournerX += Input.GetAxis("Mouse X") * 5f;
            tournerY -= Input.GetAxis("Mouse Y") * 5f;
            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y - tournerX, gameObject.transform.rotation.z + tournerY);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && verifierPoint)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Vanne")
                {
                    objetToucher.SetText("Presque");
                    nbrpoints = nbrpoints - 50;
                }
                else
                {
                    verifierPoint = false;
                    objetToucher.SetText("Bravo");
                    DataBase.PointEssaie(SceneManager.GetActiveScene().buildIndex, nbrpoints);

                    DataBase.AjouterQuizOuPression(DataBase.idPersonne.ToString(), 0);
                    
                    manometre.SetActive(true);
                    manometre.GetComponent<Animator>().Play("PlacerMano");
                }
            }
            else
            {
                objetToucher.SetText("Non");
                nbrpoints = nbrpoints - 100;
            }
        }
    }

    void VerifierPoints()
    {
        if (nbrpoints < 0)
        {
            verifierPoint = false;
            points.SetText("Nombre de Points: 0");

            objetToucher.color = Color.red;
            objetToucher.SetText("Echec");

            StartCoroutine(RetournerMoteur());
        }
        else
        {
            points.SetText("Nombre de Points: " + nbrpoints);
        }
    }    

    public IEnumerator RetournerMoteur()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Moteur");
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraGroupe : MonoBehaviour
{

    private float zoom;
    private float zoomMultiplier = 19f;
    private float minZoom = 12f;
    private float maxZoom = 70f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    [SerializeField] private Camera cam;

    float avancerZ, avancerX, avancerY;
    Vector3 moveDirection;
    float tournerX,tournerY;

    Vector3 conserverPos, conserverRota;

    private Camera mainCamera;

    [SerializeField] TMP_Text textObjet;
    [SerializeField] TMP_Text textTemperature;

    [SerializeField] GameObject accederVanne;
    Button btnVanne;

    [HideInInspector] public static string nomObjetMoteur;

    private void Start()
    {
        btnVanne = accederVanne.GetComponent<Button>();

        zoom = cam.fieldOfView;
        avancerZ = transform.position.z;
        avancerX = transform.position.x;
        avancerY = transform.position.y;

        conserverPos = transform.position;
        conserverRota = transform.localEulerAngles;

        mainCamera = Camera.main;

        Debug.Log("Script CameraGroupe: "+SceneMoteur.positionJoueur);

    }

    private void Update()
    {
        if (!PauseMenu.gamePaused)
        {
            Mouvement();
            Zoom();
            DetecterObjets();
        }

        //accederVanne.SetActive()
    }



    void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, zoom, ref velocity, smoothTime);
    }

    void Mouvement()
    {
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            tournerX = Input.GetAxis("Mouse X");
            tournerY = Input.GetAxis("Mouse Y");

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - tournerY, transform.localEulerAngles.y + tournerX, transform.localEulerAngles.z);

        }

        float moveZ = Input.GetAxis("Vertical") * Time.timeScale;
        float moveX = Input.GetAxis("Horizontal") * Time.timeScale;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            avancerY = Mathf.Clamp(transform.position.y + moveZ,0,15);
            transform.position = new Vector3(avancerX, avancerY, avancerZ);
        }

        else
        {
            moveDirection = transform.forward * moveZ + transform.right * moveX;

            avancerX = Mathf.Clamp(transform.position.x + moveDirection.x, -25, 0);
            avancerZ = Mathf.Clamp(transform.position.z + moveDirection.z, 40, 70);

            transform.position = new Vector3(avancerX, avancerY, avancerZ);
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.position = conserverPos;
            transform.localEulerAngles = conserverRota;
        }
    }



    void DetecterObjets()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Etiquette" || hit.collider.tag == "Vanne")
                {
                    Debug.Log(hit.collider.name);
                    nomObjetMoteur = hit.collider.name;
                    textObjet.SetText("Nom Objet: " + hit.collider.name);
                    textTemperature.SetText("Lo");

                    accederVanne.SetActive((hit.collider.tag == "Vanne" && ActiverQuiz.activerAccederBouton) ? true : false);
                    btnVanne.onClick.AddListener(() => ChoixScene(hit.collider.name));
                }

                else if (hit.collider.tag == "Temperature")
                {
                    Debug.Log(hit.collider.name);
                    textTemperature.SetText(hit.collider.name);
                    textObjet.SetText("");
                }
            }
        }
    }

    void ChoixScene(string nomVanne)
    {
        switch (nomVanne)
        {
            case "Vanne_De_Service_Aspiration":
                SceneManager.LoadScene("Vanne_De_Service_Aspiration"); break;
            case "Vanne_De_Service_Refoulement":
                SceneManager.LoadScene("Vanne_De_Service_Refoulement"); break;
            case "Vanne_De_Service_BRL":
                SceneManager.LoadScene("Vanne_De_Service_BRL"); break;
        }

    }



}


////////////////


//if (hit.collider.tag == "Etiquette")
//{
//    Debug.Log(hit.collider.name);
//    textObjet.SetText("Nom Objet: " + hit.collider.name);
//    textTemperature.SetText("Lo");
//}
//else
//{

//    if (hit.collider.tag == "Temperature")
//    {
//        Debug.Log(hit.collider.name);
//        textTemperature.SetText(hit.collider.name);
//        textObjet.SetText("");
//    }
//    else
//    {
//        if (hit.collider.tag == "Tuyau")
//        {
//            Debug.Log(hit.collider.name);
//            textTemperature.SetText("Lo");
//            textObjet.SetText("");
//        }
//        else
//        {
//            if (hit.collider.tag == "Vanne")
//            {
//                Debug.Log(hit.collider.name);
//                textObjet.SetText("Nom Objet: " + hit.collider.name + " Vanne");
//                textTemperature.SetText("Lo");
//                btnVanne.onClick.AddListener(() => ChoixScene(hit.collider.name));
//            }

//        }
//    }
//}

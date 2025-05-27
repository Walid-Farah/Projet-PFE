using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalculerPtQuiz : MonoBehaviour
{

    [SerializeField] Renderer enregistrer;
    [SerializeField] Color color;

    [HideInInspector] public static Transform objetClick = null;

    Button btnClickPrecedentFaux = null;

    public static int repVrai = 0;
    public static int repFaux = 0;

    //int score = 11;

    //int nbrEssai;


    private void Start()
    {
        //nbrEssai = DataBase.NbrEssaieQuiz();


        Debug.Log("scorePersonee");
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        SelectionnerObjet(ray);
        Verifier();

        //Debug.Log($"1: {ActiverQuiz.nbrEssaieQuiz > -1}");
        //Debug.Log($"2: {repVrai + repFaux != 11}");
        //Debug.Log($"3: {objetClick != null}");
    }


    void Verifier()
    {
        if (ActiverQuiz.nbrEssaieQuiz > -1)
        {
            if (repVrai + repFaux != 11)
            {
                GameObject boutonGO = EventSystem.current.currentSelectedGameObject;


                if (boutonGO != null)
                {
                    //if ((boutonGO.name.Equals("ComQuiz")) || (boutonGO.name.Equals("ArretQuiz")))
                    //{

                        Button btnAppuyer = boutonGO.GetComponent<Button>();
                        ColorBlock couleur = btnAppuyer.colors;


                        if (objetClick != null)
                        {
                            if (btnClickPrecedentFaux != null)
                            {
                                couleur.normalColor = color;
                                btnClickPrecedentFaux.colors = couleur;
                            }

                            if (objetClick.name.Equals(boutonGO.name))
                            {

                                couleur.disabledColor = Color.green;
                                btnAppuyer.colors = couleur;
                                btnAppuyer.interactable = false;

                                objetClick.gameObject.layer = 0;
                                objetClick = null;
                                repVrai++;
                            }
                            else
                            {


                                couleur.normalColor = Color.red;
                                btnAppuyer.colors = couleur;

                                GameObject myEvent = GameObject.Find("EventSystem");
                                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

                                btnClickPrecedentFaux = btnAppuyer;
                                repFaux++;

                            }
                        }
                    //}
                }
            }
            else
            {
                DataBase.AjouterPointQZ(repVrai);
                ActiverQuiz.activerAccederBouton = true;
                Debug.Log("Fin Quiz");
            }
        }

    }



    void SelectionnerObjet(Ray ray)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == 1 )
                {
                    if (objetClick != null)
                    {
                        objetClick.GetComponent<Renderer>().material = enregistrer.material;

                    }

                    objetClick = hitInfo.transform;
                    objetClick.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
    }
}

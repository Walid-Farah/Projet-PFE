using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventairePersonne : MonoBehaviour
{
    public GameObject pere;
    GameObject fils;
    InventoryItem test;

    [SerializeField] GameObject afficherObjet;

    bool voirSlot=false;
    void Start()
    {
        fils = pere.transform.GetChild(0).gameObject;
        test = fils.GetComponent<InventoryItem>();
        //Debug.Log(test.nameO);

    }

    void Update()
    {
        try
        {
            voirSlot=true;
            fils = pere.transform.GetChild(0).gameObject;
            if (fils != null)
            {
                test = fils.GetComponent<InventoryItem>();
            }
        }
        catch
        {
            voirSlot = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (voirSlot)
        {
            AfficherObjet();
        }
        else
        {
            afficherObjet.GetComponent<TextMeshProUGUI>().SetText("Vide");
        }
        afficherObjet.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        afficherObjet.SetActive(false);
        
    }


    //void Tester()
    //{
    //    if (test.nameO == "Marteau")
    //    {
    //        //afficherObjet.SetText("ici se trouve un marteau");
    //        Debug.Log("ici se trouve un Marteau");
    //    }
    //    else
    //    {
    //        if (test.nameO == "Pied_De_Biche")
    //        {
    //            //afficherObjet.SetText("Ici se trouve un Pied De Biche");
    //            Debug.Log("Ici se trouve un Pied De Biche");
    //        }

    //    }
    //}

    void AfficherObjet()
    {
        afficherObjet.GetComponent<TextMeshProUGUI>().SetText(test.nameO);
    }
}


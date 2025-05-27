using TMPro;
using UnityEngine;

public class AfficherPNom : MonoBehaviour
{

    [SerializeField] TMP_Text nomP;
    [SerializeField] TMP_Text niveauP;
    void Start()
    {

        nomP.SetText(DataBase.nomPersonne+" "+DataBase.prenomPersonne);
        int niveau = DataBase.GetBDDNiveau(DataBase.idPersonne.ToString());
        niveauP.SetText(niveau.ToString()+"/2");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

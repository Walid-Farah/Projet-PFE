using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AfficherScorePer : MonoBehaviour,IPointerDownHandler
{

    Transform entryContainer;
    Transform entryTemplate;

    string idPersonneClick=null;

    Camera mainCam;


    [SerializeField] GameObject boutonSupp;
    Transform objetSelectionne=null;


    [SerializeField]Color enregistrerCouleur;


    private void Awake()
    {
        mainCam = Camera.main;
        AfficherPersonnesNbrPoint();
        AddPhysics();

    }

    


    public void AfficherPersonnesNbrPoint()
    {
        Debug.Log(transform.name);
        entryContainer = transform.Find("ContainerScore");
        entryTemplate = entryContainer.Find("ScorePersonne");


        Vector2 offset=new Vector2(0,600);
        entryContainer.GetComponent<RectTransform>().offsetMin=offset;


        for (int enfant = 1; enfant < entryContainer.childCount; enfant++)
        {
            Destroy(entryContainer.GetChild(enfant).gameObject);
        }

        float templateHeight = -100f;
        int i = 0;

        using (IDbConnection connection = new SqliteConnection("URI = file:Users.s3db"))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                //command.CommandText = "SELECT Personne.nom,Personne.prenom,SUM(NbrPoints) as NbrPoints,SUM(NbrEssaie) AS NbrEssaie,Personne.PointQuiz FROM Personne,Piece,PointEssaie WHERE PointEssaie.idPersonne=Personne.id and PointEssaie.idPiece=Piece.id GROUP BY Personne.id ORDER BY NbrPoints DESC, NbrEssaie ASC";
                command.CommandText = "SELECT Personne.nom,Personne.prenom,SUM(PointEssaie.NbrPoints) as NbrPoints,SUM(NbrEssaie) AS NbrEssaie,Personne.PointQuiz,Personne.id  FROM Personne ,Piece LEFT JOIN PointEssaie ON Personne.id=PointEssaie.idPersonne AND Piece.id=PointEssaie.idPiece GROUP BY Personne.id ORDER BY NbrPoints DESC, PointQuiz DESC,NbrEssaie ASC";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.Log(reader.IsDBNull(2));
                        //Debug.Log(reader["NbrPoints"]==DBNull.Value);

                        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                        entryTransform.Find("NomP").GetComponent<TextMeshProUGUI>().SetText($"{reader.GetString(0)}");
                        entryTransform.Find("PrenomP").GetComponent<TextMeshProUGUI>().SetText($"{reader.GetString(1)}");
                        entryTransform.Find("ScoreP").GetComponent<TextMeshProUGUI>().SetText(reader.IsDBNull(2) ? "Pas Fait": reader.GetInt16(2).ToString());
                        entryTransform.Find("NbrEssaieP").GetComponent<TextMeshProUGUI>().SetText(reader.IsDBNull(3) ? "Pas Fait" : reader.GetInt16(3).ToString());
                        entryTransform.Find("PtQuiz").GetComponent<TextMeshProUGUI>().SetText((reader.GetInt16(4) == -1) ? "Pas Fait" : reader.GetInt16(4).ToString());
                        entryTransform.Find("Id").gameObject.name=reader.GetInt16(5).ToString();

                        offset=entryContainer.GetComponent<RectTransform>().offsetMin;
                        offset.y += templateHeight;
                        entryContainer.GetComponent<RectTransform>().offsetMin=offset;



                        entryTransform.localPosition = new Vector3(0, templateHeight * i, 0);
                        i++;

                    }
                    
                }
            }
        }

        Destroy(entryTemplate.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (objetSelectionne != null)
        {
            objetSelectionne.GetComponent<Image>().color = enregistrerCouleur;
        }
        
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.transform.parent.GetChild(5).name);
        boutonSupp.SetActive(true);
        idPersonneClick = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetChild(5).name;

        eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Image>().color = Color.red;



        objetSelectionne= eventData.pointerCurrentRaycast.gameObject.transform.parent;

    }


    void AddPhysics()
    {
        Physics2DRaycaster physics2DRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physics2DRaycaster != null)
        {
            Debug.Log("f");
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void SupprimerPersonne()
    {
        string[] personne = DataBase.RecupererNomPrenomPersonne(idPersonneClick);

        bool oui = EditorUtility.DisplayDialog("Avertissement", $"Etes Vous sur de supprimer {personne[0]} {personne[1]}!", "Oui", "Non");
        if (oui)
        {
            DataBase.SupprimerApprenant(idPersonneClick);
            //AfficherPersonnesNbrPoint();
            boutonSupp.SetActive(false);

            Vector2 offset = entryContainer.GetComponent<RectTransform>().offsetMin;
            offset.y += 100f;
            entryContainer.GetComponent<RectTransform>().offsetMin = offset;
        }
    }

    
}

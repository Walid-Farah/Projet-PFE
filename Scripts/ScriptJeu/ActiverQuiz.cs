using UnityEngine;
using UnityEngine.UI;

public class ActiverQuiz : MonoBehaviour
{
    [SerializeField] GameObject evaluation;
    [SerializeField] GameObject accederVanne;

    [SerializeField] GameObject arretQuiz;
    [SerializeField] GameObject debutQuiz;

    [SerializeField] GameObject objetPourEvaluation;
    [SerializeField] Renderer recupMaterial;

    public static bool activerAccederBouton=true;

    public static int nbrEssaieQuiz;



    static Button sauvegardeBouton;
    private void Awake()
    {
        nbrEssaieQuiz = DataBase.NbrEssaieQuiz();


        if (gameObject.name!= "ArretQuiz") {
            debutQuiz.SetActive(nbrEssaieQuiz > -1 ? true : false);
            sauvegardeBouton=evaluation.transform.GetChild(1).gameObject.GetComponent<Button>();
        }
    }

   

    public void CommencerQuiz()
    {
        evaluation.SetActive(true);
        gameObject.SetActive(false);
        activerAccederBouton = false;
        accederVanne.SetActive(false);
        arretQuiz.SetActive(true);

        nbrEssaieQuiz--;
        DataBase.AjoutNbrEssaieQuiz(nbrEssaieQuiz);

    }

    public void ArreterQuiz()
    {
        for (int i = 0; i < evaluation.transform.childCount; i++)
        {
            evaluation.transform.GetChild(i).GetComponent<Button>().colors = sauvegardeBouton.colors;
            evaluation.transform.GetChild(i).GetComponent<Button>().interactable = true;

        }

        evaluation.SetActive(false);
        gameObject.SetActive(false);
        activerAccederBouton = true;

         
        debutQuiz.SetActive(nbrEssaieQuiz>=0 ? true:false);

        DataBase.AjouterPointQZ(CalculerPtQuiz.repVrai);
        CalculerPtQuiz.repVrai = 0;
        CalculerPtQuiz.repFaux = 0;

        DataBase.AjouterQuizOuPression(DataBase.idPersonne.ToString(), 1);

        for (int i = 0; i < objetPourEvaluation.transform.childCount; i++)
        {
            objetPourEvaluation.transform.GetChild(i).gameObject.layer = 1;
            objetPourEvaluation.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = recupMaterial.material;
        }
        CalculerPtQuiz.objetClick = null;
    }
}

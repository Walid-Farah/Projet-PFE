using UnityEngine;
using UnityEngine.UI;

public class DetectionObjet : MonoBehaviour
{

    public GameObject ShowInventoryButton;


    [SerializeField] Button afficherInventaire;
    [SerializeField] Button masquerInventaire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        afficherInventaire.onClick.AddListener(AnimationOuvrir);
        masquerInventaire.onClick.AddListener (AnimationFermer);
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowInventoryButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ShowInventoryButton.SetActive(false);
    }

    void AnimationOuvrir()
    {
        gameObject.GetComponent<Animator>().Play("OuvrirBoite");
    }
    void AnimationFermer()
    {
        gameObject.GetComponent<Animator>().Play("FermerBoite");
    }
   
}

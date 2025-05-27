using UnityEngine;

public class OuvrirPortail : MonoBehaviour
{
    public GameObject portail1;
    public GameObject portail2;

    private bool Action = false;
    private bool Ouvert = true;


    void Start()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Action = true;
            if (Ouvert)
            {

                portail1.GetComponent<Animator>().Play("OuvrirPortail");
                portail2.GetComponent<Animator>().Play("OuvrirPortail2");
                Ouvert = false;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Action = false;
        if(!Ouvert)
        {
            Ouvert = true;
            portail1.GetComponent<Animator>().Play("FermerPortail");
            portail2.GetComponent<Animator>().Play("FermerPortail2");
        }
    }


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (Action == true)
        //    {
        //        if (Ouvert)
        //        {

        //            portail1.GetComponent<Animator>().Play("OuvrirPortail");
        //            portail2.GetComponent<Animator>().Play("OuvrirPortail2");
        //            Ouvert = false;
        //        }
        //        else
        //        {
        //            Ouvert = true;
        //            portail1.GetComponent<Animator>().Play("FermerPortail");
        //            portail2.GetComponent<Animator>().Play("FermerPortail2");
        //        }

        //    }
        //}

    }
}

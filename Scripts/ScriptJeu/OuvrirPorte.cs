using UnityEngine;

public class OuvrirPorte : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject porte1;
    public GameObject porte2;

    private bool Action = false;
    private bool Ouvert = true;


    void Start()
    {
        Instruction.SetActive(false);

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }  
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                if (Ouvert)
                {
                    
                    Instruction.SetActive(false);
                    porte1.GetComponent<Animator>().Play("OuvrirPorte");
                    if(porte2 != null)
                    {
                        porte2.GetComponent<Animator>().Play("OuvrirPorte2");
                    }
                    Ouvert = false;
                }
                else
                {
                    Instruction.SetActive(false);
                    Ouvert =true;
                    porte1.GetComponent<Animator>().Play("FermerPorte");
                    if (porte2 != null)
                    {
                        porte2.GetComponent<Animator>().Play("FermerPorte2");
                    }
                }
                
            }
        }

    }
}

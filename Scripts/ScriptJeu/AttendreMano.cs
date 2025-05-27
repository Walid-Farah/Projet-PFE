using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AttendreMano : MonoBehaviour
{
    [SerializeField] GameObject text;
    //Vector3 positionInitJauge;
    private void Start()
    {
        //positionInitJauge = transform.GetChild(1).localEulerAngles;
        //Debug.Log(positionInitJauge);

        Debug.Log("hello");
    }

    void Update()
    {

    }

    public IEnumerator Attendre()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
        text.SetActive(false);
    }
    
}

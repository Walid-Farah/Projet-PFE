using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public MonObjet objet;
    [HideInInspector] public string nameO; 

    [Header("UI")]
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;


    void Start()
    {
        Initialiser(objet);
    }

    public void Initialiser(MonObjet newObjet)
    {
        image.sprite = newObjet.image;
        nameO= newObjet.name;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
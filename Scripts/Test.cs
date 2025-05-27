using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class Test : MonoBehaviour
{

    //[SerializeField] GameObject GameObject;

    public AnimationClip clip;


    private void Awake()
    {
        Keyframe[] keys = new Keyframe[2];
        keys[0] = new Keyframe(0f, -90f);
        keys[1] = new Keyframe(1f, -70f);

        AnimationCurve curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.x", curve);
    }

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    //[MenuItem("Tools/Add Keyframe Dynamically")]
    //static void AddKeyframe()
    //{
    //    // Create a new AnimationClip
    //    AnimationClip clip = new AnimationClip();
    //    clip.name = "GeneratedClip";

    //    // Define a curve for the position.x property
    //    AnimationCurve curve = new AnimationCurve();

    //    // Add keyframes to the curve
    //    curve.AddKey(new Keyframe(0f, 0f));
    //    curve.AddKey(new Keyframe(1f, 5f));
    //    curve.AddKey(new Keyframe(2f, 0f));

    //    // Assign curve to the clip (transform.localPosition.x of GameObject)
    //    clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

    //    // Save the clip as an asset
    //    AssetDatabase.CreateAsset(clip, "Assets/GeneratedClip.anim");
    //    AssetDatabase.SaveAssets();

    //    Debug.Log("Animation clip with keyframes created.");
    //}




}
    //AfficherScorePer t;

    //private void Start()
    //{
    //    AddPhysics();

    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("Clicked: " +eventData.pointerCurrentRaycast.gameObject.transform.parent.GetChild(5).name);
    //    bool oui=EditorUtility.DisplayDialog("Avertissement", "Test", "Oui","Non");
    //    if (oui)
    //    {
    //        DataBase.SupprimerApprenant(eventData.pointerCurrentRaycast.gameObject.transform.parent.GetChild(5).name);
    //        t = new AfficherScorePer();
    //        t.AfficherPersonnesNbrPoint();
    //    }
    //}

    //void AddPhysics()
    //{
    //    Physics2DRaycaster physics2DRaycaster = FindObjectOfType<Physics2DRaycaster>();
    //    if (physics2DRaycaster != null)
    //    {
    //        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
    //    }
    //}


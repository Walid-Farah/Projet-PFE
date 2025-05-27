using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationVanne : MonoBehaviour
{
    public AnimationClip clip;


    private void Awake()
    {
        if(SceneManager.GetActiveScene().name== "Vanne_De_Service_Aspiration")
        {
            Debug.Log("kje");
            Keyframe[] keys = new Keyframe[2];
            keys[0] = new Keyframe(0f, -90f);
            keys[1] = new Keyframe(1f, -80f);

            AnimationCurve curve = new AnimationCurve(keys);
            clip.SetCurve("Pressure gauge.Pointer", typeof(Transform), "localEulerAngles.x", curve);
        }
        else if(SceneManager.GetActiveScene().name == "Vanne_De_Service_BRL" )
        {
            Keyframe[] keys = new Keyframe[2];
            keys[0] = new Keyframe(0f, -90f);
            keys[1] = new Keyframe(1f, -60f);

            AnimationCurve curve = new AnimationCurve(keys);
            clip.SetCurve("Pressure gauge.Pointer", typeof(Transform), "localEulerAngles.x", curve);
        }
        else if(SceneManager.GetActiveScene().name == "Vanne_De_Service_Refoulement")
        {
            Keyframe[] keys = new Keyframe[2];
            keys[0] = new Keyframe(0f, -90f);
            keys[1] = new Keyframe(1f, -40f);

            AnimationCurve curve = new AnimationCurve(keys);
            clip.SetCurve("Pressure gauge.Pointer", typeof(Transform), "localEulerAngles.x", curve);
        }
    }


}

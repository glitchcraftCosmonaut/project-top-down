using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minY, maxY;// The Limitation of the environment

    //ZOOM IN AND OUT

    //MARKER Camera SHAKE SHAKE
    private float shakeAmplitude;//How much Camera would shake
    private Vector3 shakeActive;//Camera Shake Position

    //public bool isShaked;//MARKER Once this Boolean value is true, the CAMERA will SHAKE SHAKE. This variable will be used on other scripts
    
    
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;


    private void Start()
    {
        if (target == null) return;

        offset = transform.position - target.position;
    }

    private void OnEnable()
    {
        EventSystem.cameraShakeEvent += CameraShake;
    }

    private void OnDisable()
    {
        EventSystem.cameraShakeEvent -= CameraShake;
    }

    private void Update()
    {
        // RestrictCamera();
        ShakeCamera();
         if (target == null) return;

        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }

    //MARKER Limit the Camera Range according to the environment
    // private void RestrictCamera()
    // {
    //     transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
    //                                      Mathf.Clamp(transform.position.y, minY, maxY),
    //                                      transform.position.z);
    // }

    //MARKER CAMERA SHAKE SHAKE 
    private void ShakeCamera()
    {
        if (shakeAmplitude > 0)
        {
            shakeActive = new Vector3(Random.Range(-shakeAmplitude, shakeAmplitude), Random.Range(-shakeAmplitude, shakeAmplitude), 0);
            shakeAmplitude -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
        }

        transform.position += shakeActive;
    }

    //CORE This Function will be called On EventSystem Delegate Pattern
    public void CameraShake(float _shakeAmount)
    {
        shakeAmplitude = _shakeAmount;
    }


}

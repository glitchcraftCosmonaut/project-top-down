using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = {"Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE"};
    public string[] runDirections = {"Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE"};
    int lastDirection;
    private void Awake() 
    {
        anim = GetComponent<Animator>();

        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        Debug.Log("R1 " + result1);

        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
        Debug.Log("R2 " + result2);

        float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
        Debug.Log("R3 " + result3);
    }
    
    //MARKER setiap arah akan disesuaikan dengan salah satu elemen string
    //MARKER Gunakan arah untuk menentukan animasi
    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;
        if(_direction.magnitude < 0.01f)// karakter static dan velocity mendekati 0
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(_direction); //MARKER mendapatkan index dari potongan arah vektor
        }
        
        anim.Play(directionArray[lastDirection]);
    }
    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 normalizeDirection = _direction.normalized;
        float step = 360 / 8;// satu lingkaran 45 dan 8 potongan
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normalizeDirection);// mengembalikan nilai dari sudut yang terdeteksi antara A dan B
        angle += offset;
        if(angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        
        return Mathf.FloorToInt(stepCount);

    }
}

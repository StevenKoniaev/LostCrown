using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxScript : MonoBehaviour
{
    private Transform camTrans;
    private Vector3 lastCameraPos;
    public Vector2 parralaxVal;
    private float textureUnitSizeX; 
    private void Start()
    {
        camTrans = Camera.main.transform;
        lastCameraPos = camTrans.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
    }

    private void Update() 
    {
        Vector3 deltaMovement = camTrans.position - lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * parralaxVal.x, deltaMovement.y * parralaxVal.y);
        lastCameraPos = camTrans.position;
    
    if (Mathf.Abs(camTrans.position.x - transform.position.x) >= textureUnitSizeX) 
        {
            float offSetX = (camTrans.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTrans.position.x + offSetX, transform.position.y);
        }
    }
}
 
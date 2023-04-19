using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Takes texture and applies it to map, modified from:
// https://www.youtube.com/watch?v=WP-Bm65Q-1Y&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3&index=3&ab_channel=SebastianLague
public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    // Takes in noise map and applies it to a texture
    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, texture.height, 1);
    }
}

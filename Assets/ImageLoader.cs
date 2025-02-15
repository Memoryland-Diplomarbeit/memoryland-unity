using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;



public class ImageLoader : MonoBehaviour
{

    public int urlID = 0;
    public Renderer thisRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // the following will be called even before the load finished
        thisRenderer.material.color = Color.blue;

        StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently
    }

    /*
     * get image.width && image.height 
     * 
     * Canvas canvas = FindObjectOfType<Canvas>();
     *
     * float h = canvas.GetComponent<RectTransform>().rect.height;
     * float w = canvas.GetComponent<RectTransform>().rect.width;
     * 
     * 
     * thisRenderer.transform.localScale = new Vector3();
     */

    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine()
    {
	string thisPhoto = null;
	
        IEnumerator tgt = FindAnyObjectByType<SceneChanger>().GetOnePhoto(urlID);
        while (tgt.MoveNext()) 
        {
        	thisPhoto = (string)tgt.Current;
        	yield return thisPhoto;
        }
        
        if (thisPhoto == null) {
        	yield return null;
        }

	if (thisPhoto == null) {
            	thisRenderer.material.color = Color.red;          // set red
		Debug.Log("Image NotFound!");
		yield return null;
	}
	else 
	{
		
		UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(thisPhoto);
		yield return imageRequest.SendWebRequest();

		Debug.Log("Loaded");

		if (imageRequest.result != UnityWebRequest.Result.Success) {
		    Debug.Log(imageRequest.error);
		    thisRenderer.material.color = Color.red;          // set white
		}
		else {
		    thisRenderer.material.color = Color.white;          // set white
		    Texture myTexture = DownloadHandlerTexture.GetContent(imageRequest);

		    float scale = 0.3f;

		    //thisRenderer.gameObject.
		    if (myTexture.height > myTexture.width)
		    {
		        //float f = ((float)myTexture.height * (float)0.5) / ((float)myTexture.width * (float)0.5);
		        float f = (float)myTexture.height / (float)myTexture.width * scale;
		        thisRenderer.transform.localScale = new Vector3(scale, scale, f);
		        //thisRenderer.material.SetTextureScale("_MainTex", new Vector2(1f, 0.5f));

		    }
		    else
		    {
		        //float f = ((float)myTexture.width * (float)0.5) / ((float)myTexture.height * (float)0.5);
		        float f = (float)myTexture.width / (float)myTexture.height * scale;
		        thisRenderer.transform.localScale = new Vector3(f, scale, scale);
		        //thisRenderer.material.SetTextureScale("_MainTex", new Vector2(0.5f, 1f));

		    }

		    //myTexture.
		    //thisRenderer.material.SetShaderPassEnabled("RayTracingPrepass", false);
		    //thisRenderer.material.SetShaderPassEnabled
		    thisRenderer.material.mainTexture = myTexture ;  // set loaded image
		}
	}
    }
}

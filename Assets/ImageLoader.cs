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

    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine()
    {
	Texture myTexture = null;
	
        IEnumerator tgt = FindAnyObjectByType<SceneChanger>().GetOneTexture(urlID);
        while (tgt.MoveNext()) 
        {
        	myTexture = (Texture)tgt.Current;
        	yield return myTexture;
        }
        
        if (myTexture == null) {
        	yield return null;
        }

	if (myTexture == null) {
            	thisRenderer.material.color = Color.red;          // set red
		Debug.Log("Image NotFound!");
		yield return null;
	}
	else 
	{
		Debug.Log("Loaded");

		thisRenderer.material.color = Color.white;          // set white

		float scale = 0.3f;

		//thisRenderer.gameObject.
		if (myTexture.height > myTexture.width)
		{
			float f = (float)myTexture.height / (float)myTexture.width * scale;
			thisRenderer.transform.localScale = new Vector3(scale, scale, f);
		}
		else
		{
			float f = (float)myTexture.width / (float)myTexture.height * scale;
			thisRenderer.transform.localScale = new Vector3(f, scale, scale);
		}

		thisRenderer.material.mainTexture = myTexture ;  // set loaded image
	}
    }
}

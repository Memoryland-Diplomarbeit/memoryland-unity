using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartupDocument : MonoBehaviour
{
    private Button _start;
    private Label _loading;
    private string chosenScene = null;
    
    
    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _start = uiDocument.rootVisualElement.Q("start") as Button;
        _start.SetEnabled(false);
        _loading = uiDocument.rootVisualElement.Q("loading") as Label;
        _start.RegisterCallback<ClickEvent>(PrintClickMessage);
        
	StartCoroutine(LoadFromLikeCoroutine());
    }

    private IEnumerator LoadFromLikeCoroutine()
    {
    	
        
        while (chosenScene == null) {
		IEnumerator tgt = FindAnyObjectByType<SceneChanger>().IsValid();
		while (tgt.MoveNext()) 
		{
			chosenScene = (string)tgt.Current;
			yield return chosenScene;
		}
        }
        
        
        if (chosenScene != "ERROR") {
        	Debug.Log("Finished!");
        	_loading.text = "Finished Loading " + chosenScene;
        	_start.SetEnabled(true);
        	FindAnyObjectByType<SceneChanger>().StartThisNow();
        }
        else 
        {
	        _loading.text = "Sorry, ERROR Loading this Memoryland. Possibly Token invalid!";
	        Debug.Log("ERROR loading");
        }
    }
    
    private void OnDisable()
    {
        _start.UnregisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
	FindAnyObjectByType<SceneChanger>().StartThisNow();
	
        Debug.Log($"{"start"} was clicked!");
    }
}



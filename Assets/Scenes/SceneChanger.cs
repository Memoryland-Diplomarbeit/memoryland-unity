using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UIElements;



public class CoroutineWithData {
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;

    public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
	    this.target = target;
	    this.coroutine = owner.StartCoroutine(Run());
    }

	private IEnumerator Run() {
        while(target.MoveNext()) {
            result = target.Current;
            yield return result;
        }
    }
}


[System.Serializable]
public class MemorylandType
{
	public string name;
	public int photoAmount;
}

[System.Serializable]
public class MemorylandConfiguration
{
	public int position;
	public string photo;
}


[System.Serializable]
public class Memoryland
{
    public long id;
    public string name;
    public MemorylandType memorylandType;
    public List<MemorylandConfiguration> memorylandConfigurations;

    public static Memoryland CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Memoryland>(jsonString);
    }
}


public class SceneChanger : MonoBehaviour
{
	private string chosenScene = null;
	private string token = null;
	private string server = "localhost";
	private string fallback = "yes";
	private Dictionary<int, string> allUrls = null;

	public void ChangeScene(string sceneName)
	{
		if (SceneManager.GetActiveScene().name == sceneName) 
		{
			Debug.Log("Scene " + sceneName + " already active!");
		}
		else 
		{
			Debug.Log("Change Scene to " + sceneName);
			SceneManager.LoadScene (sceneName);
		}
	}
	
	public void Exit()
	{
		Application.Quit ();
	}


	public IEnumerator GetOnePhoto(int position)
	{
		if (allUrls == null) // not finished call to backend
			yield return null;
			
		if (allUrls.ContainsKey(position)) {
			yield return allUrls[position];
		}
		else 
		{
			Debug.Log(position.ToString() + " NOT FOUND in MemoryLand!");
			yield return null;
		}
	}


	// Start is called before the first frame update
	IEnumerator Start()
	{
		// first load the given token
		Dictionary<string,string> paras =  URLParameters.GetSearchParameters();
		this.token = paras.GetString("token", "empty");
		this.server = paras.GetString("server", "http://localhost:5202");
		
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			this.fallback = paras.GetString("fallback", "no");
		}
		else
		{
			Debug.LogError("USING FALLBACK => NO WEBGL!");
			this.fallback = paras.GetString("fallback", "yes");
		}
		
		
		Debug.Log("Parameter Value token = "+this.token);
		Debug.Log("Parameter Value server = "+this.server);
		Debug.Log("Parameter Value fallback = "+this.fallback);
		// ....
		string response = @"{
			""name"": ""abc"",
			""id"": 12344,
			""memorylandType"" : {
				""name"" : ""island"",
				""photoAmount"" : 10
				} ,
			""memorylandConfigurations"" : [
				{
					""position"" : 0,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/20190215_064143.jpg?sp=r&st=2024-10-13T17:36:59Z&se=2025-10-14T01:36:59Z&spr=https&sv=2022-11-02&sr=b&sig=%2B15dkN7t51JvBwQ54KD7DzU%2FqSD7OpHkGlYLGqLQJeI%3D""
				},
				{
					""position"" : 1,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/20190703_110349.jpg?sp=r&st=2024-10-13T17:38:06Z&se=2025-10-14T01:38:06Z&spr=https&sv=2022-11-02&sr=b&sig=81ztImGypP3D%2FTE2C6X7lFGpc6k6%2BN41%2B8GdNQ6fB7c%3D""
				},
				{
					""position"" : 2,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/20190703_110436.jpg?sp=r&st=2024-10-13T17:38:24Z&se=2025-10-14T01:38:24Z&spr=https&sv=2022-11-02&sr=b&sig=xKhBM%2B3LyCN0cBV7zJDxfIlsEfgXyaC9IwTNCVTumKQ%3D""
				},
				{
					""position"" : 3,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/20190703_133415.jpg?sp=r&st=2024-10-13T17:39:02Z&se=2025-10-14T01:39:02Z&spr=https&sv=2022-11-02&sr=b&sig=YJimVGYzPV1bKWT0pgFiZ%2FKeDdiwImysYb1DuiFBTM8%3D""
				},
				{
					""position"" : 4,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/20190726_073125.jpg?sp=r&st=2024-10-13T17:39:19Z&se=2025-10-14T01:39:19Z&spr=https&sv=2022-11-02&sr=b&sig=iZ%2BWRRs%2FoV%2FKh8YsTNNCz6x8eaS7dAyWs4ZBymWV%2FBQ%3D""
				},
				{
					""position"" : 5,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/DSC01308.JPG?sp=r&st=2025-01-06T13:38:07Z&se=2026-01-06T21:38:07Z&spr=https&sv=2022-11-02&sr=b&sig=ynfEyPLYybwVSrj%2FlF8xGqLMrpnw4lmlLDqTuFUOTuo%3D""
				},
				{
					""position"" : 6,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/DSC01311.JPG?sp=r&st=2025-01-06T13:38:46Z&se=2026-01-06T21:38:46Z&spr=https&sv=2022-11-02&sr=b&sig=NocvXYMp8ErVtj6ITNahUpuNTzetcd%2BIh9Pm27DyMfE%3D""
				},
				{
					""position"" : 7,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/DSC01345.JPG?sp=r&st=2025-01-06T13:39:08Z&se=2026-01-06T21:39:08Z&spr=https&sv=2022-11-02&sr=b&sig=nOgNHGHQld5AgttgaA3OihoBoyqpccDoprA25G6JyHU%3D""
				},
				{
					""position"" : 8,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/DSC01368.JPG?sp=r&st=2025-01-06T13:39:40Z&se=2026-01-06T21:39:40Z&spr=https&sv=2022-11-02&sr=b&sig=mm%2FgsyiQ204OjVJjfBFtQo%2BYLEPVhJOShZa5czHmZ5U%3D""
				},
				{
					""position"" : 9,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/DSC01372.JPG?sp=r&st=2025-01-06T13:41:10Z&se=2026-01-06T21:41:10Z&spr=https&sv=2022-11-02&sr=b&sig=bjJPHSTPq5Rm0X2DqpNkLSQkaItU6lVbVmiTbNUsTu4%3D""
				},
				{
					""position"" : 10,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/IMG_0257.JPG?sp=r&st=2025-01-06T13:40:37Z&se=2026-01-06T21:40:37Z&spr=https&sv=2022-11-02&sr=b&sig=2N5v3HkOhk4LmK1dgsfI1xJks7%2BB2xf%2BjzZVytOmI7A%3D""
				},
				{
					""position"" : 11,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/IMG_0303.JPG?sp=r&st=2025-01-06T13:41:41Z&se=2026-01-06T21:41:41Z&spr=https&sv=2022-11-02&sr=b&sig=K7Zg7WxdKsOwSh3a1pqMnk46wGX4KI8BL7LWV05f4xQ%3D""
				},
				{
					""position"" : 12,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/IMG_0361.JPG?sp=r&st=2025-01-06T13:42:07Z&se=2026-01-06T21:42:07Z&spr=https&sv=2022-11-02&sr=b&sig=cKQLURivRnzKM2QwalUoRsWAexh7JP26gjj0hO%2BR6zc%3D""
				},
				{
					""position"" : 13,
					""photo"" : ""https://isabel0unity0storage.blob.core.windows.net/images/somefoto/IMG_0398.JPG?sp=r&st=2025-01-06T13:42:30Z&se=2026-01-06T21:42:30Z&spr=https&sv=2022-11-02&sr=b&sig=2JN4aI5D35cTqJbO7FwcXNLuDCjqimux3OdjAwUiW0E%3D""
				}
			]
			}";
		
		if (this.fallback == "yes")
		{
			SetInternals(response);
			Debug.LogError("Fallback is set => Using simulation init parameters to start with!");
		}
		
		// missing call to the url/backend
		string uri = server+"/api/Memoryland?token=" + this.token;
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
		    yield return webRequest.SendWebRequest();
		
		    switch (webRequest.result)
		    {
			case UnityWebRequest.Result.ConnectionError:
			case UnityWebRequest.Result.DataProcessingError:
			    Debug.LogError(server + ": Error: " + webRequest.error);
			    break;
			case UnityWebRequest.Result.ProtocolError:
			    Debug.LogError(server + ": HTTP Error: " + webRequest.error);
			    break;
			case UnityWebRequest.Result.Success:
			    Debug.Log(server + ":\nReceived: " + webRequest.downloadHandler.text);
			    response = webRequest.downloadHandler.text;
			    SetInternals(response);
			    break;
			default:
			    Debug.LogError(server + ": unknown " + webRequest.result.ToString());
			    break;
		    }
    		}
		

		// now set the scene
		if (chosenScene == null) {
			chosenScene = "ERROR";
		}
		
		// now change scene
		ChangeScene(chosenScene);
	}


	private void SetInternals(string response) 
	{
		if (response != null) {
		
			Memoryland ml = Memoryland.CreateFromJSON(response);
		
			if (ml.memorylandType != null && ml.memorylandType.name != null ) {
				chosenScene = ml.memorylandType.name + "Scene";
				Debug.Log("Got Scene from response: " + chosenScene);
			}
			
			if (ml.memorylandConfigurations != null) {
				allUrls = new Dictionary<int, string>();
				foreach (MemorylandConfiguration item in ml.memorylandConfigurations) {
					allUrls[item.position] = item.photo;
				}
			}
		}
		
		if (allUrls != null) 
		{
			Debug.Log(string.Join(Environment.NewLine, allUrls));
		} 
		else 
		{
			Debug.Log("no URLs loaded!");
		}	
	}

	
	// Update is called once per frame
	void Update()
	{

	}
}

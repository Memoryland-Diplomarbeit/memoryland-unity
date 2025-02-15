# memoryland-unity
Unity Project of Memoryland

ATTENTION: need to set git config for large images:
    git config http.postBuffer 524288000


## Scenes

Each MemoryLand Type in the Backend/Frontend needs to be a seperate Scene named <typeName>Scene.

* "forestScene" is a scene in a forest containing placeholders for 10 pictures

* "islandScene" is a scene on an tropical island (palms) containing placeholders for 10 pictures


## Special Scripts / Enhancements
### URLParser

Taken from the Project:
https://github.com/Bunny83/UnityWebExamples/blob/master/Mandelbrot/URLParameters.cs 

This parser helps to get the Parameters from the index.html page into the unity Project. We use that to get the MemoryLand's token from the frontend into the WebGL Project of Unity. Then we use the "server" parameter to configure the backends-address.

### SceneChooser

The Project starts in the initial Scene ("startupScene"), where the SceneChooser Object is loaded and gets the token and the server parameter. These two are used to load the MemoryLand-Type and the set of pictures from the backend-api.

No login is necessary because we need to be able to view memorylands even without logins.

using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GPS_Script : MonoBehaviour // adapted from https://nosuchstudio.medium.com/how-to-access-gps-location-in-unity-521f1371a7e3 
{


    public TMP_Text gpsText;
    public TMP_Text distanceText;
    public DistanceScript distanceScript;
    public float distance = 100;
    public float previousDist;
    public Image hotcoldIndicator;

    public UI_Script uiScript;

    float playerLat;
    float playerLong;
    float targetLat, targetLong;
    float pos1Lat, pos1Long, pos2Lat, pos2Long, pos3Lat, pos3Long, pos4Lat, pos4Long;
    float timer = 0;
    private bool gpsOn = false;

    public enum NavigationState
    {
        Target1,
        Target2,
        Target3,
        Target4
    }
    public NavigationState state = NavigationState.Target1;

    private void Start()
    {
        gpsText.text = "Lat: , Long: ";
        distanceText.text = "0 km";

        pos1Lat = 51.474622f;
        pos1Long = -0.037363f;
        pos2Lat = 51.474020f;
        pos2Long = -0.037147f;
        pos3Lat = 51.474497f;
        pos3Long = -0.036498f;
        pos4Lat = 51.474741f; 
        pos4Long = -0.037885f; 



        distance = 100;
    }


    private void Update()
    {

        gpsText.text = state + "<br>Lat: " + targetLat + ", Long: " + targetLong;
        distanceText.text = distance + " metres";

        if (distance < 10)
        {            
            uiScript.ShowClueText();
            gpsOn = false;
            uiScript.ToggleGPSUI();
            distance = 100;
        }

        if (gpsOn)
        {

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                StartLocation();
                timer = 3;
            }


        }

    }

    public void SkipGPS()
    {
        distance = 0;

    }


 /*   public void GPSOn()
    {
        gpsOn = true;

    }

    public void GPSOff()
    {
        gpsOn = false;

    }
 */

    public void SetTarget1()
    {
        uiScript.ToggleGPSUI();
        state = NavigationState.Target1;
        gpsOn = true;
    }

    public void SetTarget2()
    {
        uiScript.ToggleGPSUI();
        state = NavigationState.Target2;
        gpsOn = true;
    }

    public void SetTarget3()
    {
        uiScript.ToggleGPSUI();
        state = NavigationState.Target3;
        gpsOn = true;
    }    
    
    public void SetTarget4()
    {
        uiScript.ToggleGPSUI();
        state = NavigationState.Target4;
        gpsOn = true;
    }



    public void StartLocation()
    {
        previousDist = distance;


        switch (state)
        {
            case NavigationState.Target1:
                targetLat = pos1Lat;
                targetLong = pos1Long;

                break;
            case NavigationState.Target2:
                targetLat = pos2Lat;
                targetLong = pos2Long;

                break;
            case NavigationState.Target3:
                targetLat = pos3Lat;
                targetLong = pos3Long;

                break;
            case NavigationState.Target4:
                targetLat = pos4Lat;
                targetLong = pos4Long;

                break;
        }


        StartCoroutine(LocationCoroutine());
    }





    IEnumerator LocationCoroutine() {




    // Uncomment if you want to test with Unity Remote
/*#if UNITY_EDITOR
        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(5f);
#endif*/
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            gpsText.text = "Permission not enabled.";
            yield break;
        }

#elif UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            yield break;
        }
#endif
        // Start service before querying location
        Input.location.Start(5, 5);
                
        // Wait until service initializes
        int maxWait = 30;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            gpsText.text = "status: " + Input.location.status;
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 30;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1) {
            // TODO Failure
            Debug.LogFormat("Timed out");
            gpsText.text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running) {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            gpsText.text = "location failed";
            yield break;
        } else {
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            Debug.LogFormat("Location: " 
                + UnityEngine.Input.location.lastData.latitude + " " 
                + UnityEngine.Input.location.lastData.longitude + " " 
                + UnityEngine.Input.location.lastData.altitude + " " 
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " " 
                + UnityEngine.Input.location.lastData.timestamp);

            playerLat = UnityEngine.Input.location.lastData.latitude;
            playerLong = UnityEngine.Input.location.lastData.longitude;
            // TODO success do something with location

            gpsText.text = state + "<br>Lat: " + targetLat + ", Long: " + targetLong;

            distance = distanceScript.Calculate_Distance(playerLong, playerLat, targetLong, targetLat) * 1000;


            if(distance - previousDist < 0)//if distance is decreasing, i.e. player is getting closer to target
            {
                hotcoldIndicator.color = new Color32(100,255,100,255);
            }
            else
            {
                hotcoldIndicator.color = new Color32(255, 100, 100,255);
            }

        }

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
    }
}
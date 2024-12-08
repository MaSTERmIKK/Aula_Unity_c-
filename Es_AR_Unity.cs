using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System.Collections;

public class LocationBasedMarker : MonoBehaviour
{
    public GameObject markerPrefab;
    public Vector2 targetLocation; // Latitudine e Longitudine target
    public float activationRadius = 50f; // Raggio di attivazione in metri
    private GameObject markerInstance;

    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Servizio di localizzazione non abilitato.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timeout dell'avvio del servizio di localizzazione.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Impossibile determinare la posizione.");
            yield break;
        }
        else
        {
            // Iniziare ad aggiornare la posizione
            StartCoroutine(UpdateMarkerPosition());
        }
    }

    IEnumerator UpdateMarkerPosition()
    {
        while (true)
        {
            Vector2 userLocation = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            float distance = CalculateDistance(userLocation, targetLocation);

            if (distance <= activationRadius && markerInstance == null)
            {
                // Istanziare il marker
                markerInstance = Instantiate(markerPrefab, Vector3.zero, Quaternion.identity);
                // Posizionare il marker in base alla direzione e distanza
                Vector3 position = GetPositionInAR(userLocation, targetLocation, distance);
                markerInstance.transform.position = position;
            }
            else if (distance > activationRadius && markerInstance != null)
            {
                // Rimuovere il marker se esiste e fuori dal raggio
                Destroy(markerInstance);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    float CalculateDistance(Vector2 pos1, Vector2 pos2)
    {
        float R = 6371000; // Raggio della Terra in metri
        float lat1 = pos1.x * Mathf.Deg2Rad;
        float lat2 = pos2.x * Mathf.Deg2Rad;
        float deltaLat = (pos2.x - pos1.x) * Mathf.Deg2Rad;
        float deltaLon = (pos2.y - pos1.y) * Mathf.Deg2Rad;

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
                  Mathf.Cos(lat1) * Mathf.Cos(lat2) *
                  Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        float distance = R * c;
        return distance;
    }

    Vector3 GetPositionInAR(Vector2 userLoc, Vector2 targetLoc, float distance)
    {
        // Calcolare la direzione verso il target
        Vector2 direction = targetLoc - userLoc;
        float bearing = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ottieni l'orientamento del dispositivo
        float deviceBearing = Input.compass.trueHeading;

        // Calcola l'angolo relativo
        float relativeBearing = bearing - deviceBearing;

        // Converti la distanza in unità Unity (es. 1 metro = 1 unità)
        float unityDistance = distance / 1f;

        // Calcola la posizione in AR
        float rad = relativeBearing * Mathf.Deg2Rad;
        float x = unityDistance * Mathf.Cos(rad);
        float z = unityDistance * Mathf.Sin(rad);

        return new Vector3(x, 0, z);
    }

    void OnEnable()
    {
        Input.compass.enabled = true;
    }

    void OnDisable()
    {
        Input.location.Stop();
        Input.compass.enabled = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationProviderInfo : MonoBehaviour {
    private List<Text> texts = new List<Text>();
    private ARLocationProvider locationProvider;
    private LoadingBar accuracyBar;

    // Use this for initialization
    void Start () {
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Provider").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Latitude").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Longitude").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Altitude").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Time").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Status").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas Right/DistanceWalked").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas Right/CameraPosition").GetComponent<Text>());
        texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas Right/MagneticSensor").GetComponent<Text>());

        locationProvider = ARLocationProvider.Instance;

        accuracyBar = GameObject.Find(gameObject.name + "/Panel/Canvas/LoadingBar").GetComponent<LoadingBar>();
    }
	
	// Update is called once per frame
	void Update () {
        texts[0].text = "Provider: " + locationProvider.provider.name;
        texts[1].text = "Latitude: " + locationProvider.currentLocation.latitude;
        texts[2].text = "Longitude: " + locationProvider.currentLocation.longitude;
        texts[3].text = "Altitude: " + locationProvider.currentLocation.altitude;

        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long currentEpochTime = (long) ((DateTime.UtcNow - epochStart).TotalSeconds * 1000.0);
        texts[4].text = "Time Since Last (ms): " +  (currentEpochTime - locationProvider.currentLocation.timestamp);

        texts[5].text = "Status: " + locationProvider.provider.GetStatusString();
        texts[6].text = "Distance Walked: " + locationProvider.provider.distanceFromStartPoint;
        texts[7].text = "Camera Pos: " + GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        texts[8].text = "MagneticSensor: " + locationProvider.provider.isCompassEnabled;

        var accuracy = locationProvider.currentLocation.accuracy;

        accuracyBar.fillPercentage = Mathf.Min(1, (float) accuracy / 25.0f);
        accuracyBar.text = "" + (float)locationProvider.currentLocation.accuracy;
    }
}

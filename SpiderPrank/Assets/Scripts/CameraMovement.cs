using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform follower;
    [SerializeField] private Vector2 followingOffset;

    private Camera mainCamera;
    [SerializeField] private Camera followupCamera;
    [SerializeField] private Camera upfrontCamera;

    [SerializeField] private BoxCollider2D boundingBox;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (!followupCamera || !upfrontCamera)
        {
            Camera[] cameras = mainCamera.GetComponentsInChildren<Camera>();
            followupCamera = cameras[0];
            upfrontCamera = cameras[1];
        }

    }

    private void FixedUpdate()
    {
        GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            follower = players[0].transform;
            Vector3 pos = new Vector2(follower.transform.position.x, follower.transform.position.y) + followingOffset;
            mainCamera.transform.position = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
        }
        upfrontCamera.transform.position = mainCamera.transform.position + new Vector3(0, boundingBox.bounds.size.y);
        followupCamera.transform.position = mainCamera.transform.position - new Vector3(0, boundingBox.bounds.size.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControllerScript : MonoBehaviour
{
    [SerializeField]
    private Transform Player;

    [SerializeField]
    private CamBounds[] camBounds;

    [SerializeField]
    [Tooltip("Put scene's default camera here")]
    private CinemachineVirtualCamera activeCamera;

    private int _layerMask;

    void Awake()
    {
        //_layerMask = LayerMask.GetMask("Player");

        //Make sure all cameras except main are off
        foreach (CamBounds camBound in camBounds)
        {
            if (!(camBound.Camera.GetInstanceID() == activeCamera.GetInstanceID()))
            {
                camBound.Camera.Priority = 1;
            }
        } 
    }

    void Update()
    {
        //For each camera cached
        foreach (CamBounds cambound in camBounds)
        {
            //Get collider and player pos
            Bounds bounds = cambound.ColliderBounds;

            //Check if player is in bounds -> set cam priority to high
            if (bounds.Contains(Player.position))
            {
                cambound.Camera.Priority = 10;
            }
            //if not, set cam priority to low
            else
            {
                cambound.Camera.Priority = 1;
            }
        }
    }

    [Serializable]
    private class CamBounds
    {
        [SerializeField]
        private CinemachineVirtualCamera camera;
        [SerializeField]
        private Collider2D collider;

        public CinemachineVirtualCamera Camera
        {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            }
        }
        public Bounds ColliderBounds
        {
            get
            {
                return collider.bounds;
            }
        }
    }
}
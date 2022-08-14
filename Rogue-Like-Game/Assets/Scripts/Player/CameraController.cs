using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    public class CameraController : MonoBehaviour
    {
        private static CameraController instance { get; set; }
        
        public Transform target;

        public Vector3 offset;

        public float pitch = 2f;

        private float currentZoom = 10f;


        public float zoomSpeed = 4f;
        public float minZoom = 5f;
        public float maxZoom = 15f;


        private float yawInput;
        public float yawSpeed = 100f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }


        private void Update()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Math.Clamp(currentZoom, minZoom, maxZoom);

           yawInput -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        }

        private void LateUpdate()
        {
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);
            
            transform.RotateAround(target.position, Vector3.up, yawInput);
        }
    }

    
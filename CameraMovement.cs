//	Created by: Sunny Valley Studio 
//	https://svstudio.itch.io

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{

    public class CameraMovement : MonoBehaviour
    {
        public Camera gameCamera;
        public float cameraMovementSpeed = 5;

        private void Start()
        {
            gameCamera = GetComponent<Camera>();
        }
        public void MoveCamera(Vector3 inputVector)
        {
            var movementVector = Quaternion.Euler(0,30,0) * inputVector;
            gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
        }
    }
}
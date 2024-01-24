using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSBR
{
    public class CarCameraFollower : MonoBehaviour
    {
        public Transform carTransform;
        public Vector3 offset = new Vector3(0f, 2f, -5f);
        public float followSpeed = 10f;
        public float rotationSpeed = 5f;
        public float lookaheadDistance = 5f;

        void LateUpdate()
        {
            if (carTransform == null)
            {
                return;
            }

            // Calculate a target position by adding offset and a lookahead component
            Vector3 targetPosition = carTransform.position + offset + carTransform.forward * lookaheadDistance;

            // Smoothly interpolate the camera's position towards the desired position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

            // Look at the car smoothly
            Quaternion targetRotation = Quaternion.LookRotation(carTransform.position - transform.position + carTransform.forward * lookaheadDistance, carTransform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}

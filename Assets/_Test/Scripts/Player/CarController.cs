using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSBR
{
    public class CarController : NetworkBehaviour
    {
        [Header("Car Setting")]
        [SerializeField] private Rigidbody rbody;
        [SerializeField] private float motorTorque = 1000f;
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private float brakeTorque = 5000f;
        [SerializeField] private float steerAngle = 30f;

        [Header("Wheel Colliders")]
        [SerializeField] private WheelCollider frontLeftWheel;
        [SerializeField] private WheelCollider frontRightWheel;
        [SerializeField] private WheelCollider rearLeftWheel;
        [SerializeField] private WheelCollider rearRightWheel;

        [Header("Inputs")]
        private float _horizontalInput;
        private float _verticalInput;
        private bool isBreaking;
        private bool isReseting;

        [Header("Tire Rotation Transforms")]
        [SerializeField] private Transform frontLeftTireTransform;
        [SerializeField] private Transform frontRightTireTransform;
        [SerializeField] private Transform rearLeftTireTransform;
        [SerializeField] private Transform rearRightTireTransform;
        [Networked] CarInputData carInputData { get; set; }
        private float motor;
        private float steering;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();


            isBreaking = carInputData.IsBreaking;
            isReseting = carInputData.IsReseting;
            CarMovement();
            SetBrakesAccordingly(isBreaking ? brakeTorque : 0);
            ResetCarPosition();
            LimitSpeed();
            RotateTires();

        }

        private void ResetCarPosition()
        {
            if (isReseting)
            {
                transform.SetLocalPositionAndRotation(transform.position, Quaternion.identity);
                rbody.velocity = Vector3.zero;
                rbody.angularVelocity = Vector3.zero;
                SetBrakesAccordingly(brakeTorque);
            }
        }


        private void CarMovement()
        {
            _horizontalInput = carInputData.Direction.x;
            _verticalInput = carInputData.Direction.z;

            Debug.Log($"Horizontal {_horizontalInput}");
            motor = _verticalInput * motorTorque;
            steering = _horizontalInput * steerAngle;

            frontLeftWheel.motorTorque = motor;
            frontRightWheel.motorTorque = motor;

            frontLeftWheel.steerAngle = steering;
            frontRightWheel.steerAngle = steering;
        }

        private void SetBrakesAccordingly(float brakeTorque)
        {
            frontLeftWheel.brakeTorque = brakeTorque;
            frontRightWheel.brakeTorque = brakeTorque;
            rearLeftWheel.brakeTorque = brakeTorque;
            rearRightWheel.brakeTorque = brakeTorque;
        }

        private void RotateTires()
        {
            RotateSingleTire(frontLeftTireTransform, frontLeftWheel);
            RotateSingleTire(frontRightTireTransform, frontRightWheel);
            RotateSingleTire(rearLeftTireTransform, rearLeftWheel);
            RotateSingleTire(rearRightTireTransform, rearRightWheel);
        }
        private void RotateSingleTire(Transform tireTransform, WheelCollider wheelCollider)
        {
            wheelCollider.GetWorldPose(out _, out Quaternion rotation);
            tireTransform.rotation = rotation;
        }

        private void LimitSpeed()
        {
            if (rbody.velocity.magnitude > maxSpeed)
            {
                rbody.velocity = rbody.velocity.normalized * maxSpeed;
            }
        }
        public void SetInputData(CarInputData data)
        {
            carInputData = data;
        }
    }

}

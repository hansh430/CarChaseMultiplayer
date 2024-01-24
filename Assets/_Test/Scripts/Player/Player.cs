using Fusion;
using UnityEngine;

namespace TPSBR
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private CarController carController;
        [SerializeField] private GameObject localComponent;
        [Networked] public CarInputData inputData { get; set; }
        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                localComponent.SetActive(true);
            }
            else
            {
                localComponent.SetActive(false);
            }
        }
        public override void FixedUpdateNetwork()
        {
            if (GetInput(out CarInputData data))
            {
                inputData = data;
            }
            carController.SetInputData(inputData);

        }
    }
}

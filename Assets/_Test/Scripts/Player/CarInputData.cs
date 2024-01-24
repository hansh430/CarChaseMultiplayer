using Fusion;
using System;
using UnityEngine;

namespace TPSBR
{
    [Serializable]
    public struct CarInputData : INetworkInput
    {
        public Vector3 Direction;
        public bool IsBreaking;
        public bool IsReseting;
    }
}

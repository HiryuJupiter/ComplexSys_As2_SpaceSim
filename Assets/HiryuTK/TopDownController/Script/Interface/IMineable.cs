using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    /// <summary>
    /// Interface for objects that can be mined
    /// </summary>
    public interface IMineable
    {
        void Mine(float amount);
    }
}
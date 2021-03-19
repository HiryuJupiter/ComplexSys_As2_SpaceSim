﻿using UnityEngine;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-1000)]
    public class GameInput_TopDownController : MonoBehaviour
    {
        public static GameInput_TopDownController Instance;
        public static float MoveX { get; set; }
        public static float MoveY { get; set; }
        public static bool JumpBtnDown { get; set; }
        public static bool JumpBtn { get; set; }
        public static bool JumpBtnUp { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            DirectionInputUpdate();
            ActionInputUpdate();
        }

        private void DirectionInputUpdate()
        {
            //LEFT - RIGHT
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                MoveX = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                MoveX = 1f;
            }
            else
            {
                MoveX = 0f;
            }

            //UP - DOWN
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                MoveY = 1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                MoveY = -1f;
            }
            else
            {
                MoveY = 0f;
            }
        }

        private void ActionInputUpdate()
        {
            JumpBtnDown = Input.GetKeyDown(KeyCode.Space);
            JumpBtn = Input.GetKey(KeyCode.Space);
            JumpBtnUp = Input.GetKeyUp(KeyCode.Space);
        }
    }
}

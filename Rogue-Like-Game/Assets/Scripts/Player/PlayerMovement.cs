using System;
using UnityEngine;



    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        public float SprintSpeed = 5.335f;
        
        
        public float RotationSmoothTime = 0.12f;
        
        public bool Grounded = true;
        private Animator _animator;

        protected bool canMove = true;
        [SerializeField] private float rotationSpeed = 10;
        private CharacterController controller;

        private float targetRot;

        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
            controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveInput();
        }

        private void LateUpdate()
        {
            UpdateAnimations();
        }

        void MoveInput()
        {
            if(!canMove)
                return;
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector3 moveDir = new Vector3(input.x, 0, input.y).normalized;
            
            if (moveDir != Vector3.zero)
            {
                targetRot = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
               
            }
            
            
            controller.SimpleMove(moveDir * speed);

            
        }

        void UpdateAnimations()
        {
            
        }
    }

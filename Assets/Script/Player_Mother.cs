using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LensFlare.InGame
{
    public class Player_Mother : MorningBird.SubMonoClass.SubMonoClassCore
    {
        [FoldoutGroup("PreDefine")]
        [FoldoutGroup("PreDefine/Move")]
        [SerializeField] Rigidbody _playerRigidBody;
        [FoldoutGroup("PreDefine/Move")]
        [SerializeField] float _maxVelocity;
        [FoldoutGroup("PreDefine/Move")]
        [SerializeField] float _moveSpeed;
        [FoldoutGroup("PreDefine/Move")]
        [SerializeField] float _jumpSpeed;

        InputAction _moveAction;
        InputAction _jumpAction;
        InputAction _interectAction;
        InputAction _onCameraStateAction;
        InputAction _onCameraSelfiAction;
        InputAction _offCameraStateAction;
        InputAction _onCameraShutterAction;

        Camera _mainCamera;

        protected override void Start()
        {
            _mainCamera = Camera.main;

            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
            _interectAction = InputSystem.actions.FindAction("Interact");


        }

        protected override void Update()
        {
            // Declare variable
            Vector3 inputVelocity = new Vector3();
            
            // Input and Move
            {

                if (Input.GetKey(KeyCode.W) == true)
                {
                    inputVelocity += _mainCamera.transform.forward;
                }

                if (Input.GetKey(KeyCode.S) == true)
                {
                    inputVelocity += _mainCamera.transform.forward * -1f;
                }

                if (Input.GetKey(KeyCode.D) == true)
                {
                    inputVelocity += _mainCamera.transform.right;
                }

                if (Input.GetKey(KeyCode.A) == true)
                {
                    inputVelocity += _mainCamera.transform.right * -1f;
                }

                _playerRigidBody.AddForce(inputVelocity * Time.deltaTime * _moveSpeed);

                // Limit max velocity
                if (_playerRigidBody.linearVelocity.sqrMagnitude > _maxVelocity)
                {
                    _playerRigidBody.linearVelocity *= 0.98f;
                }

                // Set Visual Looking Direction
                if (_playerRigidBody.linearVelocity.sqrMagnitude > 0.1f)
                {
                    transform.rotation = Quaternion.LookRotation(_playerRigidBody.linearVelocity);
                }


            }




            if (Input.GetKey(KeyCode.Space) == true)
            {

            }

            if (Input.GetKey(KeyCode.E) == true)
            {

            }
        }

        protected override void OnDisable()
        {

        }

        protected override void OnEnable()
        {

        }

        protected override void OnDestroy()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            
        }

    } 
}

using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        class CameraState
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float x;
            public float y;
            public float z;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
                
                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, roll);
                t.position = new Vector3(x, y, z);
            }
        }
        
        CameraState m_TargetCameraState = new CameraState();
        CameraState m_InterpolatingCameraState = new CameraState();

        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;

        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;

        [Header("Zoom Settings")]
        public float minFov = 15f;
        public float maxFov = 90f;
        public float sensitivity = 10f;   

        [Header("Star Panel")]
        public StarInformationPanel starInformationPanel;

        [Header("Audio")]
        public AudioManager audioManager;
        
        [Header("Star Names Manager")]
        public StarNamesManager starNamesManager;
        public Challenge challenge;
        public ConstellationsLinesManager constellationsLinesManager;

        [HideInInspector]
        public bool canRaycast = true;
        [Header("UI Changes")]
        public Transform lensImageTransform;

        void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);
        }
        
        void Update()
        {
            // Change the camera FOV based on the scrollwheel and the keyboard inputs
            float fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;            

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, minFov, Time.deltaTime);    
                lensImageTransform.localScale = new Vector3(1, Mathf.Lerp(lensImageTransform.localScale.y, 1 + (maxFov - fov)/100, Time.deltaTime), 1);
                
                if (!audioManager.sfxSource.isPlaying && audioManager.sfxSource.clip != audioManager.zoomInSound)
                {
                    audioManager.sfxSource.clip = audioManager.zoomInSound;
                    audioManager.PlayZoomIn();
                }              
            }

            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, maxFov, Time.deltaTime); 
                lensImageTransform.localScale = new Vector3(1, Mathf.Lerp(lensImageTransform.localScale.y, 1 + (maxFov - fov)/100, Time.deltaTime), 1); 

                if (!audioManager.sfxSource.isPlaying && audioManager.sfxSource.clip != audioManager.zoomOutSound)
                {
                    audioManager.sfxSource.clip = audioManager.zoomOutSound;
                    audioManager.PlayZoomOut();
                }             
            }

            // Hide and lock cursor when right mouse button pressed
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Unlock and show cursor when right mouse button released
            if (Input.GetMouseButtonUp(1))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            // Rotation
            if (Input.GetMouseButton(1))
            {
                var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));
                var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);
                m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
                m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
            }
            

            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / 0.2f) * Time.deltaTime);
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);
            m_InterpolatingCameraState.UpdateTransform(transform);


            // Raycast setup, here we check if we can raycast to get the star information panel
            if(starInformationPanel.gameObject.activeInHierarchy == false)
            {
                canRaycast = true;
            }

            if(canRaycast){
                    // Check for planets
                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)
                        && Camera.main.fieldOfView <= 2)
                    {

                        if(starInformationPanel.gameObject.activeInHierarchy == false)
                        {
                            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.red);
                            Debug.Log("Did Hit");

                            // We check if the planet is already discovered
                            if(hit.collider.GetComponentInParent<StarDisplay>().discovered == false)
                            {
                                hit.collider.GetComponentInParent<StarDisplay>().discovered = true;
                                audioManager.PlayDiscoverSound();
                                starNamesManager.UpdateStarsNameVisibility();
                                challenge.UpdateChallenge();
                                constellationsLinesManager.UpdateLinesVisibility();
                            }

                            if(starInformationPanel.star != hit.collider.GetComponentInParent<StarDisplay>().star)
                            {
                                starInformationPanel.star = hit.collider.GetComponentInParent<StarDisplay>().star;
                                starInformationPanel.UpdateInformation(); 
                            }

                            starInformationPanel.gameObject.SetActive(true);
                            audioManager.PlayAppearSound();
                        }

                    }
                    else
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.green);
                        Debug.Log("Did not Hit");

                        if(starInformationPanel.gameObject.activeInHierarchy == true)
                        {
                            canRaycast = false;
                            StartCoroutine(starInformationPanel.Disappear());
                            audioManager.PlayDisappearSound();
                        }
                    }
            }

        }
    }
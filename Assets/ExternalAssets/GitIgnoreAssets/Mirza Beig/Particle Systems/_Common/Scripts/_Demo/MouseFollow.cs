
// =================================	
// Namespaces.
// =================================

using UnityEngine;

// =================================	
// Define namespace.
// =================================

namespace MirzaBeig
{

    namespace ParticleSystems
    {

        namespace Demos
        {

            // =================================	
            // Classes.
            // =================================
            
            public class MouseFollow : MonoBehaviour
            {
                // =================================	
                // Nested classes and structures.
                // =================================

                // ...

                // =================================	
                // Variables.
                // =================================

                // ...

                [SerializeField] private OVRSkeleton _Skeleton;
                public float speed = 8.0f;
                public float distanceFromCamera = 5.0f;

                public bool ignoreTimeScale;

                // =================================	
                // Functions.
                // =================================

                // ...

                void Awake()
                {

                }

                // ...

                void Start()
                {

                }

                // ...

                void Update()
                {
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = distanceFromCamera;

                    //Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
                    var mouseScreenToWorld = _Skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;

                    float deltaTime = !ignoreTimeScale ? Time.deltaTime : Time.unscaledDeltaTime;
                    Vector3 position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * deltaTime));

                    transform.position = position;
                }

                // ...

                void LateUpdate()
                {

                }

                // =================================	
                // End functions.
                // =================================

            }

            // =================================	
            // End namespace.
            // =================================

        }

    }

}

// =================================	
// --END-- //
// =================================

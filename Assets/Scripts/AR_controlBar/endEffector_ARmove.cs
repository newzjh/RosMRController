// using UnityEngine;
// using MixedReality.Toolkit.Input;

// public class endEffector_ARmove : MonoBehaviour, IMixedRealityPointerHandler
// {
//     public bool movePositiveX = false;
//     public bool moveNegativeX = false;
//     public float speed = 0.05f; // 5 cm/s

//     void Update()
//     {
//         if (movePositiveX)
//         {
//             transform.Translate(speed * Time.deltaTime, 0, 0);
//         }
//         else if (moveNegativeX)
//         {
//             transform.Translate(-speed * Time.deltaTime, 0, 0);
//         }
//     }

//     public void OnPointerDown(MixedRealityPointerEventData eventData)
//     {
//         if (eventData.Pointer.Result.CurrentPointerTarget.name == "MovePositiveX")
//         {
//             movePositiveX = true;
//             moveNegativeX = false;
//         }
//         else if (eventData.Pointer.Result.CurrentPointerTarget.name == "MoveNegativeX")
//         {
//             moveNegativeX = true;
//             movePositiveX = false;
//         }
//     }

//     public void OnPointerUp(MixedRealityPointerEventData eventData)
//     {
//         movePositiveX = false;
//         moveNegativeX = false;
//     }

//     // Implement other interface methods, but you can leave them empty
//     public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
//     public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
// }
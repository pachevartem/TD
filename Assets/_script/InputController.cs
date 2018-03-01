using UnityEngine;

namespace ArtelVR
{
    
    
    /// <summary>
    /// Класс отвечает за интерфейс взаимодействия игрока с игровым миром. 
    /// </summary>
    public class InputController: MonoBehaviour
    {
        //событие нажатия на ячейку. 
        public static event ClickCell OnClickCell;    
        
        void Update()
        {
            Raycasting();
        }

        void Raycasting()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000, GameController.Instance.CellLayerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.magenta);

                if (UIController.Bank<100)  //TODO: пахнет гавном
                {
                    return;
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    OnClickCell (hit.collider.gameObject.GetComponent<CellController>());
                }
            }
        }
    }
}
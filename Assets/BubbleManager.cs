    using UnityEngine;
    using System.Collections.Generic;

    public static class BubbleManager
    {
        public static List<GameObject> allBubbleList;
        public static List<GameObject> playerBubbleList;
        public static List<BubbleTaskable> bubbleTaskList;
        public static void AddBubble(GameObject bubbleRef){
            if(playerBubbleList == null){
                playerBubbleList = new List<GameObject>();
            }
            if(allBubbleList == null){
                allBubbleList = new List<GameObject>();
            }


            playerBubbleList.Add(bubbleRef);
            if(allBubbleList.FindIndex(bubbleRef => bubbleRef.gameObject == bubbleRef) == -1){
                allBubbleList.Add(bubbleRef);
            }

            Debug.Log(playerBubbleList.Count);
        }

        public static void RemoveBubble(GameObject bubbleRef){
            Debug.Log(playerBubbleList.FindIndex(b => b == bubbleRef));
            playerBubbleList.RemoveAt(playerBubbleList.FindIndex(b => b == bubbleRef));
            Debug.Log(playerBubbleList.Count);
        }

        public static void AssignBubble(BubbleTaskable task, GameObject bubbleRef){
            if(bubbleTaskList == null){
                bubbleTaskList = new List<BubbleTaskable>();
            }
            RemoveBubble(bubbleRef);
            if(bubbleTaskList.FindIndex(b => b == task) != -1){
                bubbleTaskList.Add(task);
                task.AttachBubbleToTask(bubbleRef);
            }
            else{
                task.AttachBubbleToTask(bubbleRef);
            }
        }

        public static Vector3 GetMousePos(){
            var mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 10.0f;
            return Camera.main.ScreenToWorldPoint(mouseWorldPos);
        }
    }

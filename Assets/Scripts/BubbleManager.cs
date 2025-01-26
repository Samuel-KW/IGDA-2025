    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;
    using System.Collections;
    using Microsoft.CSharp;
    using System.Dynamic;

    public static class BubbleManager
    {
        public static GameObject audioManagerObject;
        public static AudioManager audioManager;
        public static List<GameObject> allBubbleList;
        public static List<GameObject> playerBubbleList;
        public static ArrayList bubbleTaskList;
        public static void AddBubble(GameObject bubbleRef){
            if(playerBubbleList == null){
                playerBubbleList = new List<GameObject>();
            }
            if(allBubbleList == null){
                allBubbleList = new List<GameObject>();
            }

            playerBubbleList.Add(bubbleRef);
            if (!allBubbleList.Contains(bubbleRef))
            {
                allBubbleList.Add(bubbleRef);
            }

            if(audioManager == null){
                // Place the prefab in a Resources folder (e.g., Assets/Resources/Prefabs/)
                string prefabPath = "Prefabs/AudioManager";
                GameObject loadedPrefab = Resources.Load<GameObject>(prefabPath);
                
                if(loadedPrefab != null){
                    audioManagerObject = GameObject.Instantiate(loadedPrefab, Vector3.zero, Quaternion.identity);
                    audioManager = audioManagerObject.GetComponent<AudioManager>();
                } else {
                    Debug.LogError("AudioManager prefab not found in Resources folder");
                }
            }
        audioManager.SetMusicPowerNum(allBubbleList.Count);
        }

        public static void RecallAllBubbles(){
            //Debug.Log(allBubbleList.Count);
            foreach(GameObject bubble in allBubbleList){
                //Debug.Log("Reached Here");
                Minion minion = bubble.GetComponent<Minion>();
                if(minion.task != null){
                    //Debug.Log("Found Bubble In Task");
                    minion.task.DetachBubblesFromTask();
                }
            }
        }// 

        public static void RemoveBubble(GameObject bubbleRef){
            Debug.Log("Removed Bubble");
            //Debug.Log(bubbleRef);
            int index = playerBubbleList.FindIndex(b => b == bubbleRef);
            //Debug.Log(index);
            if(index != -1)
            playerBubbleList.RemoveAt(index);
            index = allBubbleList.FindIndex(b => b == bubbleRef);
            //Debug.Log(index);
            if(index != -1)
            allBubbleList.RemoveAt(index);
            
        }

        public static void AssignBubble(dynamic task, GameObject bubbleRef){ 
            if(bubbleTaskList == null){ 
                bubbleTaskList = new ArrayList(); 
            }
            int index = playerBubbleList.FindIndex(b => b == bubbleRef);
            //Debug.Log(index);
            if(index != -1)
            playerBubbleList.RemoveAt(index); 
            if(FindTaskIndex(bubbleTaskList, task) == -1){ 
                bubbleTaskList.Add(task); 
            }
            task.AttachBubbleToTask(bubbleRef); 
            //Debug.Log(playerBubbleList.Count); 
        }

        public static Vector3 GetMousePos(){
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter)){
                Vector3 worldPosition = ray.GetPoint(enter);
                return worldPosition;
            // Now you have the world position you wanted.
            }
            return Vector3.zero;
        }

        public static int FindTaskIndex(ArrayList bubbleTaskList, dynamic task){
            for (int i = 0; i < bubbleTaskList.Count; i++)
            {
                if (bubbleTaskList[i] == task)
                {
                    return i;
                }
            }
            return -1;
        }
    }

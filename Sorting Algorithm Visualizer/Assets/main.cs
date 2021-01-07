using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    public int size;
    public Slider sizeSlider;
    public Button genbutt, runButt;
    public Dropdown algoDrop, speedDrop;
    public List<int> arr;
    public List<GameObject> bars;
    public GameObject bar, runMenu, editMenu;
    public int running = 0;
    float startTime, stepSpeed = 0;
    int step = 0, lastStep = 0;
    int nextI = 0, nextJ = 0, nextK = 0;
    public Text rText, lText;
    public Color visited, visiting, normal;
    LinkedList<int[]> queue;
    int[] left ;
    int[] right ;
    int min = -1;
    public static Text label;
    public static float lFade;
    


    // Start is called before the first frame update
    void Start()
    {
        lFade = 0;
        label = lText;
        UpdateSizeandCreateBars();
        queue = new LinkedList<int[]>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (algoDrop.value == 4)
        {
            arr[size - 1] = 0;
            UpdateBars();
        }

        Camera.main.rect = new Rect(0, 0, 1, (Screen.height - 100) / (float)Screen.height);
        lFade -= (float).02;
        lText.color = new Color(0, 0, 0, lFade);
        editMenu.SetActive(running == 0);
        runMenu.SetActive(running != 0);
        if (running == 1 && Time.time - startTime > step * stepSpeed)
        {
            step++;
            rText.text = algoDrop.options[algoDrop.value].text + " Algorithm      Operations: " + step;
            
        }
        if (step > lastStep)
        {
            lastStep = step;
            runAlgo();

        }
        


    }
    public void speedChange()
    {
        if (speedDrop.value == 0)
        {
            stepSpeed = 0;
        }
        else
        {
            stepSpeed = (float).2;
        }
        
    }
    public void UpdateSizeandCreateBars()
    {
        

        size = (int)sizeSlider.value;
        arr.Clear();
        for (int i = 0; i < size; i++)
        {
            arr.Add((int)(Random.value * 100));
        }
        UpdateBars();
        

    }

    public void runAlgo()
    {
        if (algoDrop.value == 2)
        {
            BubbleSort(nextI,nextJ);
        }else if (algoDrop.value == 0)
        {
            Merge(queue.First.Value[0], queue.First.Value[1], queue.First.Value[2], nextI, nextJ, nextK);
        }
        else if (algoDrop.value == 3)
        {
            QuickSortPartition(queue.First.Value[0], queue.First.Value[1], nextJ, nextI);
        }
        else if (algoDrop.value == 4)
        {
            insertionSort(nextI, nextJ);
        }
        else if (algoDrop.value == 1)
        {
            SelectionSort( nextI, nextJ);
        }

        UpdateBars();
    }
    public void changeAlgorithm()
    {

        if (algoDrop.value == 4)
        {
            UpdateSizeandCreateBars();
            arr[size - 1] = 0;
            UpdateBars();
        }
    }


    public void UpdateBars()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        
        bars.Clear();
        float radius = 5;
        float w = (radius * 2) / size;
        for (int i = 0; i < size; i++)
        {
            GameObject current = Instantiate(bar, new Vector3(0, 0, 0), Quaternion.identity, transform);
            current.transform.localPosition = new Vector3(-radius + w * i, 0, 0);
            current.transform.localScale = new Vector3(w / 2, arr[i] / (float)20, 1);
            current.GetComponent<SpriteRenderer>().color = normal;
            bars.Add(current);

        }

        if (running != 0)
        {
            if (algoDrop.value == 2)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i > size - 1 - nextI)
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visited;
                    }

                    if (i == nextJ || i == nextJ+1)
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visiting;
                    }

                }
            }else
            if (algoDrop.value == 0 && queue.Count > 0)
            {
                int a = queue.First.Value[0];
                int q = queue.First.Value[1];
                int b = queue.First.Value[2];
                for (int i = 0; i < size; i++)
                {
                    

                    if (i >= a && i <= b)
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visited;
                    }
                    if (i == a + nextI || i == q + nextJ)
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visiting;
                    }

                }
            }else
            if (algoDrop.value == 3 && queue.Count > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i >= queue.First.Value[0] && i <= queue.First.Value[1])
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visiting;
                    }

                    if (i == nextJ || i == nextI)
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visited;
                    }

                }
            }
            else
            if (algoDrop.value == 1 || algoDrop.value == 4)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i < nextI )
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visited;
                    }

                    if (i == nextJ)
                    {
                        bars[i].GetComponent<SpriteRenderer>().color = visiting;
                    }

                }
            }
        }

    }

    public void pushRun()
    {
        if (running == 0)
        {
            nextI = 0;
            nextJ = 0;
            nextK = 0;
            running = 1;
            step = 0;
            lastStep = 0;
            startTime = Time.time;
            queue.Clear();
            min = 0;
            
            

            if (algoDrop.value == 0)
            {
                MergeSort(0, size - 1);
                int leftLen = queue.First.Value[1] - queue.First.Value[0] + 1;
                int rightLen = queue.First.Value[2] - queue.First.Value[1];

                left = new int[leftLen];
                right = new int[rightLen];

                for (int p = 0; p < leftLen; ++p)
                {
                    left[p] = arr[queue.First.Value[0] + p];
                    
                }
                for (int p = 0; p < rightLen; ++p)
                {
                    right[p] = arr[queue.First.Value[1] + p + 1];
                    
                }
            }else if (algoDrop.value == 3)
            {
                int[] temp = { 0, size-1 };
                queue.AddLast(temp);
                nextJ = -1;
            }
            else if (algoDrop.value == 4)
            {
                nextI = 1;
                nextJ = 0;
                
            }

            runButt.GetComponentInChildren<Text>().text = "Done";
        }
        else
        {
            running = 0;
            runButt.GetComponentInChildren<Text>().text = "Run Algorithm";
        }
        UpdateBars();
        
    }

    public void BubbleSort(int i, int j)
    {

        if (arr[j] > arr[j+1])
        {
            int temp = arr[j];
            arr[j] = arr[j+1];
            arr[j+1] = temp;
        }


        j++;


        if (j >= size- i - 1)
        {
            j = 0;
            i++;
        }

        if (i >= size)
        {
            running = 2;
        }

        nextI = i;
        nextJ = j;
    }

    public void MergeSort(int a, int b) 
    {
        if (a < b)
        {
            int q = (a + b) / 2;
            MergeSort(a, q);
            MergeSort(q + 1, b);
            int[] temp = { a, q, b };
            queue.AddLast(temp);


        }
    }
    public void Merge(int a, int q, int b, int i, int j, int k)
    {
        
        int leftLen = q - a + 1;
        int rightLen = b - q;








        print("5s");
        if (i < leftLen && j < rightLen)
        {
            print("yessir i =" + i + "  j=" + j + "  k = " + k);
            print("ye " + a + "  " + q + "  " + b);
            if (left[i] <= right[j])
            {
                arr[k] = left[i];
                i++;
            }
            else
            {
                arr[k] = right[j];
                j++;
            }
            k++;
            

            nextI = i;
            nextJ = j;
            nextK = k;
        }
        else
        {
            print("4s");
            while (i < leftLen)
            {
                arr[k] = left[i];
                i++;
                k++;
            }

            print("3s");
            while (j < rightLen)
            {
                arr[k] = right[j];
                j++;
                k++;
            }

            print(queue.First.Value[0] + "  " + queue.First.Value[1] + "  " + queue.First.Value[2]);
            queue.RemoveFirst();


            if (queue.Count == 0)
            {
                print("done");
                running = 2;
            }
            else
            {
                nextI = 0;
                nextJ = 0;
                nextK = queue.First.Value[0];

                leftLen = queue.First.Value[1] - queue.First.Value[0] + 1;
                rightLen = queue.First.Value[2] - queue.First.Value[1];

                left = new int[leftLen];
                right = new int[rightLen];

                for (int p = 0; p < leftLen; ++p)
                {
                    print("2s");
                    left[p] = arr[queue.First.Value[0] + p];
                    print("L" + p + "  " + left[p]);
                }
                for (int p = 0; p < rightLen; ++p)
                {
                    print("1s " + rightLen + "  " + queue.First.Value[0] + "  " + queue.First.Value[1] + "  " + queue.First.Value[2]);
                    right[p] = arr[queue.First.Value[1] + p + 1];
                    print("R" + p + "  " + right[p]);
                }
            }   
        }
    }

    public void QuickSortPartition(int low, int high, int j, int i)
    {
        
        int pivot = arr[high];
        if (j == -1)
        {
            
            j = low;
            i = (low - 1);
            
        }

        // index of smaller element 
        
        if ( j < high ) 
        {
            
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            j++;
            nextJ = j;
            nextI = i;
        }
        else
        {
            
            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            nextJ = -1;
            if (i + 2 < high)
            {
                int[] temp2 = { i + 2, high };
                queue.AddAfter(queue.First, temp2);
            }
            if (low < i)
            {
                int[] temp = { low, i };
                queue.AddAfter(queue.First, temp);
            }
            queue.RemoveFirst();
            

            if (queue.Count == 0)
            {
                print("done");
                running = 2;
            }



        }

    }

    public void SelectionSort(int i, int j)
    {

        if (i < size - 1)
        {
            if (j < size)
            {
                if (arr[j] < arr[min])
                    min = j;

                j++;
            }
            else
            {
                int temp = arr[min];
                arr[min] = arr[i];
                arr[i] = temp;
                i++;
                j = i+1;
                min = i;
            }
            nextI = i;
            nextJ = j;

        }
        else
        {
            print("done");
            running = 2;
        }
    }
    public void insertionSort(int i, int j)
    {
        
        if ( i < size)
        {
            int key = arr[i];
            

            if (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            else
            {

                arr[j + 1] = key;
                j = i - 1;
                i++;
            }
            

            nextI = i;
            nextJ = j;

        }
        else
        {
            arr[size - 1] = 0;
            print("done");
            running = 2;
        }
    }







}

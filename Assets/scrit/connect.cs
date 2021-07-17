using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class connect : MonoBehaviour
{
    public GameObject btnprefab;
    public Transform parentobj;
    int noliyo = 0;
    int value = 0;
    public Sprite[] emojisprite;
    public int row;
    public int colum;
    int i;
    int j;
    // int Xvalue;
    public List<GameObject> btnlist = new List<GameObject>(); //levelbtnlist

    public List<GameObject> livelist = new List<GameObject>();

    public List<Vector2> destroylist = new List<Vector2>(); // destroy savelist.

   public LineRenderer lineRenderer;




    public static connect fairy;

    private void Start()
    {
      //  lineRenderer = GetComponent<LineRenderer>();
    }

    private void Awake()
    {
        fairy = this;
    }
    public void playbtnclick()
    {
       // if (btnlist.Count == 2)
      //  {
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < colum; j++)
                {
                    // Debug.Log("done");

                    GameObject krishna = Instantiate(btnprefab, parentobj.transform);

                    krishna.GetComponent<RectTransform>().anchoredPosition = new Vector3(j * 120, i * 120, 0);

                    noliyo++;
                    krishna.gameObject.name = (i + "_" + j).ToString(); // parent text
                    krishna.GetComponentInChildren<Text>().text = (i + "_" + j).ToString();

                    btnlist.Add(krishna);

                    btnlog.Game.Xvalue = i;
                    btnlog.Game.Yvalue = j;


                }
            }
        //}
        for(i = -1; i<= colum; i++)
        {
            destroylist.Add(new Vector2(-1f, i));
            destroylist.Add(new Vector2(row, i));
        }
        for(i = 0; i < row; i++)
        {
            destroylist.Add(new Vector2(i, -1f));
            destroylist.Add(new Vector2(i, colum));
        }

        for (int k = 0; k < btnlist.Count; k++)
        {
            int R = Random.Range(0, emojisprite.Length);
            btnlist[k].transform.GetChild(0).GetComponent<Image>().sprite = emojisprite[R];
        }
    }

    IEnumerator LineDraw(Vector3 a, Vector3 b)
    {
        lineRenderer.SetPosition(0,a);
         lineRenderer.SetPosition(1,b);
        yield return new WaitForSeconds(2);    // change it 

        lineRenderer.positionCount = 0;
       
    }

    private void Update()
    {
        if (livelist.Count == 2)
        {
            if (livelist[0].transform.GetChild(0).GetComponent<Image>().sprite == livelist[1].transform.GetChild(0).GetComponent<Image>().sprite)
            {
                Debug.Log(livelist[0].gameObject.GetComponent<RectTransform>().localPosition);
                Debug.Log(livelist[1].gameObject.GetComponent<RectTransform>().localPosition);

                lineRenderer.positionCount = 2;

                StartCoroutine(LineDraw(livelist[0].gameObject.GetComponent<RectTransform>().localPosition, livelist[1].gameObject.GetComponent<RectTransform>().localPosition));

                //  Debug.Log("same images");

                int firstX = livelist[0].gameObject.GetComponent<btnlog>().Xvalue;
                int firstY = livelist[0].gameObject.GetComponent<btnlog>().Yvalue;
                int secoundX = livelist[1].gameObject.GetComponent<btnlog>().Xvalue;
                int secoundY = livelist[1].gameObject.GetComponent<btnlog>().Yvalue;

                Debug.Log(firstX + " : " + secoundX + " : " + firstY + ":" + secoundY);

                if (Mathf.Abs(firstX - secoundX) == 1 && firstY == secoundY)  // horizontal destroy
                {
                    remove();
                }
                else if (firstX == secoundX && Mathf.Abs(firstY - secoundY) == 1)  //ertical destroy
                {
                    remove();
                }


                // speace log and center destroy //   
                else if ((firstY == secoundY && secoundX != firstX))  //  down-up  conditon   (1)
                {
                   if(secoundX < firstX)
                    {
                        int abc = firstX;
                        firstX = secoundX;
                        secoundX = abc;
                        abc = firstY;
                        firstY = secoundY;
                        secoundY = abc;
                        Debug.Log("swap");
                    }
                    Debug.Log("riddhi");
                    List<Vector2> TempA = new List<Vector2>();  // down to up  
                    for (int i = firstX + 1; i < secoundX; i++)
                    {
                        //  Debug.Log(i + "," + FirstY);

                        TempA.Add(new Vector2(i, firstY));
                    }
                    bool delete = newfun(TempA, (secoundX - firstX)-1);
                    if (delete)
                        remove();
                    else
                    {
                        Vector2 p1 = new Vector2(firstX, firstY);
                        Vector2 p2 = new Vector2(secoundX, secoundY);
                        int x = clist(TempA,p1,p2,new Vector2(0,1),0);
                        if(x>0)
                        {
                            remove();
                            Debug.Log("right c = " + x);
                        }

                        Debug.Log("left");
                        int y = clist(TempA,p1,p2,new Vector2(0,-1),0);
                        if (y > 0)
                        {
                            remove();
                            Debug.Log("left c = " + x);
                        }  
                    }
                    livelist.Clear();
                  
                }
                else if (firstX == secoundX && firstY != secoundY)//  (3)   right to left 
                {
                    if(firstY < secoundY)
                    {
                        int abc = firstX;
                        firstX = secoundX;
                        secoundX = abc;
                        abc = firstY;
                        firstY = secoundY;
                        secoundY = abc;
                        Debug.Log("swap");
                    }

                  //  Debug.Log("right to left");
                    
                    List<Vector2> TempB = new List<Vector2>();  // right to left
                    for (int i = secoundY + 1; i < firstY; i++)
                    {
                          TempB.Add(new Vector2(firstX, i));
                    }
                    bool delete = newfun(TempB, (firstY - secoundY)-1);
                    if (delete)
                        remove();

                    else
                    {
                        Vector2 p1 = new Vector2(firstX, firstY);
                        Vector2 p2 = new Vector2(secoundX, secoundY);
                        List<Vector2> list1 = TempB;
                        Vector2 adder = new Vector2(1, 0);
                        int q = clist(list1, p1, p2, adder, 0);
                        if (q > 0)
                        {
                            remove();
                            Debug.Log("up c = " + q);
                        }
                        Debug.Log("left" + TempB.Count);
                        List<Vector2> list2 = TempB;
                        adder = new Vector2(-1, 0);
                        int w = clist(list2, p1, p2, adder, 0);
                        if (w > 0)
                        {
                            remove();
                            Debug.Log("down c = " + w);
                        }
                        livelist.Clear();
                    }  
                }
                //********    5    ********// 
                else if ((secoundX > firstX && secoundY > firstY) || (secoundX < firstX && secoundY < firstY))
                    //firstx > secoundx&& firsty<secoundy 
                {
                    
                    Debug.Log("xyz");
                    if(secoundX < firstX && secoundY < firstY)
                    {
                        int abc = firstX;
                        firstX = secoundX;
                        secoundX = abc;
                        abc = firstY;
                        firstY = secoundY;
                        secoundY = abc;
                        Debug.Log("swap");
                    }
                   
                        List<Vector2> h1 = new List<Vector2>();
                        List<Vector2> h2 = new List<Vector2>();
                        List<Vector2> v1 = new List<Vector2>();
                        List<Vector2> v2 = new List<Vector2>();

                    for (int i = firstY + 1; i <= secoundY; i++)
                    {
                        h1.Add(new Vector2(firstX, i));
                        Debug.Log("h1 : " + new Vector2(firstX, i));
                    }
                    Debug.Log(h1.Count);
                    for (int i = firstX; i < secoundX; i++)
                    {
                        v1.Add(new Vector2(i, secoundY));
                        Debug.Log("v1 : " + new Vector2(i,secoundY));

                    }
                    Debug.Log(v1.Count);


                    for (int i = firstY; i < secoundY; i++)
                    {
                        h2.Add(new Vector2(secoundX, i));
                        Debug.Log("h2 : " + new Vector2(secoundX, i));

                    }
                    Debug.Log(h2.Count);

                    for (int i = firstX + 1; i <= secoundX; i++)
                    {
                        v2.Add(new Vector2(i,firstY));
                        Debug.Log("v2 : " + new Vector2(i,firstY));

                    }
                    Debug.Log(v2.Count);

                    // v1  
                    bool deleteA = newfun(h1, (secoundY - firstY));     // h1
                   bool deleteB = newfun(v1, (secoundX - firstX));   // v1    
                    bool deleteC = newfun(h2, (secoundY - firstY));  //h2
                    bool deleteD = newfun(v2, (secoundX - firstX));  //v2

                    if (deleteA && deleteB)
                    {
                        remove();
                        Debug.Log("noliyoA");
                    }

                    else  if (deleteC && deleteD)
                    {
                        remove();
                        Debug.Log("noliyoB");
                    }
                    else
                    {
                        Vector2 p1 = new Vector2(firstX, firstY);
                        Vector2 p2 = new Vector2(secoundX, secoundY);
                        
                        Vector2 addar = new Vector2(0, 1);
                        int N = listupdater(h2, v2, p1, p2, addar, 0);
                         if(N>0)
                         {
                            remove();
                            Debug.Log("z=" + N);
                         }
                        
                        addar = new Vector2(1, 0);
                        int M = listupdater(v1,h1, p1, p2, addar, 0);
                        if (M > 0)
                        {
                            remove();
                            Debug.Log("R=" + M);
                        }

                    }
                 
                    livelist.Clear();
                }
                //********    6    ********// 

                else if (firstX < secoundX && firstY> secoundY|| firstX > secoundX && firstY < secoundY)
                {
                    Debug.Log("janvi");
                    if (secoundX < firstX && secoundY > firstY)
                    {
                        int xyz = firstX;
                        firstX = secoundX;
                        secoundX = xyz;
                        xyz = firstY;
                        firstY = secoundY;
                        secoundY = xyz;
                        Debug.Log("swaping");
                    }
                    List<Vector2> h1 = new List<Vector2>();
                    List<Vector2> h2 = new List<Vector2>();
                    List<Vector2> v1 = new List<Vector2>();
                    List<Vector2> v2 = new List<Vector2>();

                    for (int i = secoundY; i < firstY; i++)
                    {
                        h1.Add(new Vector2(firstX,i));
                    }
                    Debug.Log(h1.Count);
                    for (int i = firstX; i < secoundX; i++)
                    {
                        v1.Add(new Vector2( i,secoundY));
                    }
                    Debug.Log(v1.Count);

                    for (int i = secoundY+1; i <= firstY; i++)
                    {
                        h2.Add(new Vector2(secoundX, i ));
                    }
                    Debug.Log(h2.Count);

                    for (int i = firstX+1 ; i <= secoundX; i++)
                    {
                        v2.Add(new Vector2( i, firstY));
                    }
                    Debug.Log(v2.Count);

                    bool selectA = newfun(h1, (firstY - secoundY));
                    bool selectD = newfun(v1, (secoundX - firstX));                    
                    bool selectC = newfun(h2, (firstY - secoundY));  
                    bool selectB = newfun(v2, (secoundX - firstX));
                    if (selectA && selectD)
                    {
                        remove();
                        Debug.Log("yesA");
                    }
                    else if (selectC && selectB)
                    {
                        remove();
                        Debug.Log("yesB");
                    }
                    Vector2 p1 = new Vector2(firstX, firstY);
                    Vector2 p2 = new Vector2(secoundX, secoundY);
                   
                    Vector2 addar = new Vector2(0, -1);
                    int N = listupdater(h2, v2 , p1, p2, addar, 0);
                    Debug.Log("hello wolrd");
                    if (N > 0)
                    {
                        remove();
                        Debug.Log("U=" + N);
                    }
                 
                    addar = new Vector2(1, 0);
                    int Y = listupdater(v1, h1, p1, p2, addar, 0);
                    if (Y > 0)
                    {
                        remove();
                        Debug.Log("W=" + Y);
                    }

                    livelist.Clear();
                }


                else
                {
                    livelist.Remove(livelist[0]);
                }
            }
            else
            {
                livelist.Remove(livelist[0]);
            }
        }
        void remove()
        {
            btnlist.Remove(livelist[0]);
            btnlist.Remove(livelist[1]);

            destroylist.Add(new Vector2(livelist[0].GetComponent<btnlog>().Xvalue, livelist[0].GetComponent<btnlog>().Yvalue));
            destroylist.Add(new Vector2(livelist[1].GetComponent<btnlog>().Xvalue, livelist[1].GetComponent<btnlog>().Yvalue));

            Destroy(livelist[0]);

            Destroy(livelist[1]);

            livelist.Clear();
        }

    }

    int listupdater(List<Vector2> h1, List<Vector2> v1, Vector2 p1, Vector2 p2, Vector2 adder, int turn)     // z condition 
    {
        turn++;
        Debug.Log(turn);
        for (int i = 0; i < h1.Count; i++)
        {
            h1[i] += adder;
            if(h1[i]==p2)
            {
                h1[i] = p1 + adder;
            }
            Debug.Log(h1[i]);
        }
        for (int i = 0; i < v1.Count; i++)
        {
            v1[i] += adder;
            Debug.Log(v1[i]);
        }
        
        float dif1 = Mathf.Abs(p2.x - p1.x);
         int diff1 = (int)dif1;
        
        float dif2 = Mathf.Abs(p2.y - p1.y);
        int diff2 = (int)dif2;
       
        if(adder.x == 0)
        {
            bool b = newfun(v1, diff1);
            bool a = newfun(h1, diff2);
            if (a && b)
                return turn;
            else
            {
                if(turn<diff2-1)
                {
                    turn = listupdater(h1, v1, p1, p2, adder, turn);
                    return turn;
                }
                else
                {
                    return -1;
                }
            }
        }
        else
        {
            bool b = newfun(v1, diff2);
            bool a = newfun(h1, diff1);
            if (a && b)
                return turn;
            else
            {
                if (turn < diff2 - 1)
                {
                    turn = listupdater(h1, v1, p1, p2, adder, turn);
                    return turn;
                }
                else
                {
                    return -1;
                }
            }
        }
       
    }
    int clist(List<Vector2> lst, Vector2 p1, Vector2 p2, Vector2 adder, int turn)     // z condition 
    {
        turn++;
        Debug.Log(turn);
        lst.Add(p1);
        lst.Add(p2);
        for (int i = 0; i < lst.Count; i++)
        {
            lst[i] += adder;
            Debug.Log(lst[i]);
        }
        Debug.Log(lst.Count);
        int limit = 0;
        int xy = 0;
        int dir = 0;
        if (adder.x == 0)
        {
            limit = colum;
            xy = (int)p1.y;
            dir = (int)adder.y;
        }
        else
        {
            limit = row;
            xy = (int)p1.x;
            dir = (int)adder.x;
        }
        dir = (dir + 1) / 2;
        int possibility = Mathf.Abs(limit * dir - xy) - dir;
        bool a = newfun(lst, lst.Count);
        if (a)
            return turn;
        else
        {
            if (turn <= possibility)
            {
                turn = clist(lst, p1, p2, adder, turn);
                return turn;
            }
            else
            {
                lst.Clear();
                Debug.Log("end");
                return -1;
            }
        }
    }
    public void refreshbtnclick()
    {
        for (int i = 0; i < btnlist.Count; i++)
        {
            int R = Random.Range(0, emojisprite.Length);
            btnlist[i].transform.GetChild(0).GetComponent<Image>().sprite = emojisprite[R];
        }
    } 
    
    bool newfun(List<Vector2> checklist, int diff)
    {
        bool delete = false;
        int counter = 0;
        for (int i = 0; i < checklist.Count; i++)
        {
            if (destroylist.Contains(checklist[i]))
            {
               // Debug.Log(checklist[i]);
                counter++;
            }
        }
        Debug.Log("counter" + counter + " : " + diff);
        if (counter == diff)
        {
            delete = true;
            return delete;
        }
        else
            return delete;
    }
    
}
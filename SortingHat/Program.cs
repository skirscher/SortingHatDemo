using System;

namespace SortingHat
{
    /************************ Sorting Hat Project ************************************/
    /** Functionality / Purpose                                                     **/
    /**                                                                             **/
    /** This program performs all of the 'sorting' patterns used in object oriented **/
    /** programming: Bubble, Selection and Merge. It originally used a pre-defined  **/
    /** unsorted array as the intial list of numerical values to be sorted. After   **/
    /** I completed the functions for each sort type, I expanded upon the original. **/
    /** I added a menu to allow the program to repeatedly sort based upon input     **/
    /** of which sort type to use from the user. Once this was working properly, I  **/
    /** wanted to see if I could use user input to determine the size of the array. **/
    /** After I got that working, I decided to put my skills to the test further by **/
    /** adding a random number generating functionality to the program. The program **/
    /** now creates a dynamically sized array with random generated numbers stored. **/
    /** The array is cleared & re-initialized for each sort & the user is prompted  **/
    /** to enter an array size each time they select one of the menu options. As it **/
    /** sorts, the program outputs the array's content to keep track of the sorting **/
    /** progress. The randomizer is also scaled based upon the array size.          **/
    /*********************************************************************************/
    
    class Program
    {
        static int Main(string[] args)
        {
            //            Console.WriteLine("Hello World!");

            int[] unsorted = new int[] { }; // { 1, 5, 3, 7, 6, 12, 44, 63, 17, 83, 69, 13, 30, 25, 10, 27, 14, 23, 97, 32};
            int[] sortme = new int[] { };

            int inLen = 0;

            Console.WriteLine("Please enter length of sorting array (1-10,000): ");
            string resp = Console.ReadLine();
            inLen = Convert.ToInt32(resp);

            if ((inLen < 10001) && (inLen > 1))
            {
                //                int inArrayNum = 0;

                Array.Resize(ref unsorted, inLen); // Resize unsorted to the array size inputed by user
                Array.Resize(ref sortme, inLen); // Resize sortme to the array size inputed by user
                GenerateUnsorted(unsorted, inLen); // generates randomized unsorted numerical array
                int inResponse = 0;
                string[] promptArray = new string[] { "Bubble", "Selection", "Merge" };

                while (inResponse < promptArray.Length + 1)
                {
                    Console.WriteLine("\n\nCurrent array: ");
                    PrintArray(unsorted);
                    PrintPrompt(promptArray);
                    string response = Console.ReadLine();
                    inResponse = Convert.ToInt32(response);

                    Array.Resize(ref sortme, unsorted.Length);
                    unsorted.CopyTo(sortme, 0);

                    if (inResponse < promptArray.Length)
                    {
                        Console.WriteLine("Starting " + promptArray[inResponse - 1] + " Sort...");
                    }
                    switch (inResponse)
                    {
                        case 1:
                            BubbleSort(sortme);
                            break;
                        case 2:
                            SelectSort(sortme);
                            break;
                        case 3:
                            MergeSort(sortme, 0, unsorted.Length - 1);
                            Console.Write("Final Array: ");
                            PrintArray(sortme);
                            break;
                        default:
                            Console.WriteLine("Exiting... Goodbye!");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input, exiting...");
            }
            return 0;
        }

        public static void PrintArray(int[] arrIn)
        {
            for (int i = 0; i < arrIn.Length; i++)
            {
                Console.Write(arrIn[i] + " ");
            }
            Console.Write("\n");
        }

        public static void GenerateUnsorted(int[] unsorted, int inLength)
        {
            int arrLoopX = 0;
            bool bExists = false;
            while (arrLoopX < unsorted.Length)
            {
                int inArrayNum = 0;
                Random randoNum = new Random();
                //Console.Write("Enter a number 0-9999:");
                //resp = Console.ReadLine();

                // Scale randomizer according to user input for array length
                int endNum = 0;
                if (inLength < 101)
                {
                    endNum = 500;
                }
                else if ((inLength > 101) && (inLength < 1001))
                {
                    endNum = 1500;
                }
                else if ((inLength > 1001) && (inLength <= 10000))
                {
                    endNum = 10500;
                }

                inArrayNum = randoNum.Next(0, endNum);
                //Convert.ToInt32(resp);
                //if ((inArrayNum >= 0) && (inArrayNum <= 9999))
                //{
                for (int curIndx = 0; curIndx < unsorted.Length; curIndx++)
                {
                    if (unsorted[curIndx] == inArrayNum)
                    {
                        bExists = true;
                    }
                }

                if (!bExists)
                {
                    unsorted[arrLoopX] = inArrayNum;
                    Console.WriteLine("Inserted " + inArrayNum + " into unsorted[" + arrLoopX + "]");
                    arrLoopX++;
                }
                else
                {
                    Console.WriteLine("Value " + inArrayNum + " exists, trying again...");
                    //    Console.WriteLine("Invalid input, try again...");
                    bExists = false;
                }
            }

        }

        public static void BubbleSort(int[] arrInput)
        {
            for (int i = 0; i < arrInput.Length - 1; i++)
            {
                for (int x = 0; x < arrInput.Length - i - 1; x++)
                {
                    int temp = 0;
                    if (arrInput[x] > arrInput[x + 1])
                    {
                        temp = arrInput[x];
                        Console.WriteLine("Switching arrInput[" + x + "]: " + arrInput[x] + " with arrInput[" + (x + 1) + "]: " + arrInput[x + 1]);
                        arrInput[x] = arrInput[x + 1];
                        arrInput[x + 1] = temp;
                    }
                }
            }
            Console.Write("Final Array: ");
            PrintArray(arrInput);
        }

        public static void SelectSort(int[] arrInput)
        {
            PrintArray(arrInput);
            for (int i = 0; i < arrInput.Length - 1; i++)
            {
                int min = i;
                for (int x = i + 1; x < arrInput.Length; x++)
                {
                    if (arrInput[x] < arrInput[min])
                    {
                        min = x;
                    }
                }
                if (min != i)
                {
                    Console.WriteLine("Swapping " + i + ":" + arrInput[i] + " with " + min + ":" + arrInput[min] + "\n");
                    int temp = arrInput[i];
                    arrInput[i] = arrInput[min];
                    arrInput[min] = temp;
                }
                Console.Write("Current Array: ");
                PrintArray(arrInput);
            }
            Console.Write("Final Array: ");
            PrintArray(arrInput);
        }

        public static void MergeArray(int[] arrInput, int indxL, int indxM, int indxR)
        {
            int i, x, y = 0;

            int n1 = indxM - indxL + 1;
            int n2 = indxR - indxM;

            int[] arrLeft = new int[n1];
            int[] arrRight = new int[n2];

            for (i = 0; i < n1; i++)
            {
                arrLeft[i] = arrInput[indxL + i];
            }

            for (x = 0; x < n2; x++)
            {
                arrRight[x] = arrInput[indxM + 1 + x];
            }

            i = 0;
            x = 0;
            y = indxL;

            while (i < n1 && x < n2)
            {
                if (arrLeft[i] <= arrRight[x])
                {
                    arrInput[y] = arrLeft[i];
                    i++;
                }
                else
                {
                    arrInput[y] = arrRight[x];
                    x++;
                }
                y++;
            }

            while (i < n1)
            {
                arrInput[y] = arrLeft[i];
                i++;
                y++;
            }

            while (x < n2)
            {
                arrInput[y] = arrRight[x];
                x++;
                y++;
            }
        }

        public static void MergeSort(int[] arrIn, int indxL, int indxR)
        {
            Console.WriteLine("Sorting array: ");
            PrintArray(arrIn);
            if (indxL < indxR)
            {
                int indxM = indxL + (indxR - indxL) / 2;
                MergeSort(arrIn, indxL, indxM);
                MergeSort(arrIn, indxM + 1, indxR);
                MergeArray(arrIn, indxL, indxM, indxR);
            }
            Console.WriteLine("Sorted array: ");
            PrintArray(arrIn);
        }

        /*        static void CopyArray(int[] inArray, int[] outArray)
                {
                    for (int i = 0; i < inArray.Length; i++)
                    {
                        outArray.Add(inArray[i]);
                    }
                }*/

        static void PrintPrompt(string[] promptArr)
        {
            Console.WriteLine("Please make your selection:");
            for (int i = 0; i < promptArr.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + promptArr[i] + " Sort");
            }
            //           Console.WriteLine("2. Selection Sort");
            //           Console.WriteLine("3. Merge Sort");
            Console.WriteLine((promptArr.Length + 1) + ". Exit");
        }
    }
}

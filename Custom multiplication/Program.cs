using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine(); // Assume input to be two string-integer separated by a space
        string[] multiplyInput = input.Split(' ');
        List<int> output = Multiply(IntToDigList(multiplyInput[0]), IntToDigList(multiplyInput[1]));
        // Print out the result of the multiplication
        foreach(int integer in output)
        {
            Console.Write(integer.ToString());
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Perform multiplication between 2 integers.
    /// </summary>
    /// <param name="x">List representing integer 1</param>
    /// <param name="y">List representing integer 2</param>
    /// <returns>Multiplication result in a list</returns>
    static List<int> Multiply(List<int> x, List<int> y)
    {
        x.Reverse();
        y.Reverse();

        List<int> result = new List<int>();
        int tempAddCount = 0;

        // x * y, need to loop through x for every digit in y
        for (int ctrY = 0; ctrY < y.Count; ctrY++)
        {
            tempAddCount = ctrY;

            for (int ctrX = 0; ctrX < x.Count; ctrX++)
            {
                string product = (x.ElementAt(ctrX) * y.ElementAt(ctrY)).ToString();
                char[] currentProduct = product.ToCharArray();

                int AddCO = 0;
                string addition;
                int coCount = tempAddCount;

                if (currentProduct.Length > 1) // Assuming 2-digit product
                {
                    // Check if the result buffer contains any value
                    if (result.Count == 0 || result.Count <= coCount) 
                    {
                        result.Insert(coCount, (int)Char.GetNumericValue(currentProduct[1])); // Result buffer is empty
                    }
                    else
                    {
                        // Result buffer is not empty
                        addition = ((int)Char.GetNumericValue(currentProduct[1]) + result.ElementAt(coCount)).ToString();
                        processAddition(ref result, ref AddCO, addition, coCount, false);
                    }

                    coCount++; // Increase x-position by 1

                    if (result.Count == 0 || result.Count <= coCount)
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[0]) + AddCO).ToString();
                        processAddition(ref result, ref AddCO, addition, coCount, true);
                    }
                    else
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[0]) + AddCO + result.ElementAt(coCount)).ToString();
                        processAddition(ref result, ref AddCO, addition, coCount, false);
                    }

                    while (AddCO > 0) // Continue to move x-position until there is no carry over (addition)
                    {
                        coCount++;

                        if (result.Count == 0 || result.Count <= coCount)
                        {
                            result.Insert(coCount, AddCO);
                            AddCO = 0;
                        }
                        else
                        {
                            addition = (AddCO + result.ElementAt(coCount)).ToString();
                            processAddition(ref result, ref AddCO, addition, coCount, false);
                        }
                    }
                }
                else // 1-digit product
                {
                    if (result.Count == 0 || result.Count <= coCount)
                    {
                        result.Insert(coCount, (int)Char.GetNumericValue(currentProduct[0]));
                    }
                    else
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[0]) + result.ElementAt(coCount)).ToString();
                        processAddition(ref result, ref AddCO, addition, coCount, false);
                    }

                    while (AddCO > 0)
                    {
                        coCount++;

                        if (result.Count == 0 || result.Count <= coCount)
                        {
                            result.Insert(coCount, AddCO);
                            AddCO = 0;
                        }
                        else
                        {
                            addition = (AddCO + result.ElementAt(coCount)).ToString();
                            processAddition(ref result, ref AddCO, addition, coCount, false);
                        }
                    }

                }
                tempAddCount += 1;
            }
        }

        result.Reverse();
        return result;
    }

    /// <summary>
    /// Process the addition result.
    /// </summary>
    /// <param name="result">A list to store the current multiplication result</param>
    /// <param name="carryOver">An int object </param>
    /// <param name="additionResult">The addition result to be processed</param>
    /// <param name="isInsert">true: Insert into result, false: Replace result</param>
    static void processAddition(ref List<int>result, ref int carryOver, string additionResult, int currentPosition, bool isInsert)
    {
        if (additionResult.Length > 1)
        {
            char[] currentAddition = additionResult.ToCharArray();
            result[currentPosition] = (int)Char.GetNumericValue(currentAddition[1]);
            carryOver = (int)Char.GetNumericValue(currentAddition[0]);
        }
        else
        {
            if (isInsert)
                result.Insert(currentPosition, Int32.Parse(additionResult));
            else
                result[currentPosition] = Int32.Parse(additionResult);
            carryOver = 0;
        }
    }

    /// <summary>
    /// Convert integer into a list.
    /// </summary>
    /// <param name="strNum">Integer in string format</param>
    /// <returns>List representing a integer</returns>
    static List<int> IntToDigList(string strNum)
    {
        List<int> digList = new List<int>();
        for (int ctr = 0; ctr < strNum.Length; ctr++)
        {
            digList.Add((int)Char.GetNumericValue(strNum[ctr]));
        }
        return digList;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        string[] multiplyInput = input.Split(' ');
        List<int> output = Multiply(IntToDigList(multiplyInput[0]), IntToDigList(multiplyInput[1]));
        Console.ReadLine();
    }

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
                //product = (Int32.Parse(product) + tempCO).ToString();

                char[] currentProduct = product.ToCharArray();
                //tempCO = (int)Char.GetNumericValue(currentProduct[0]); // Store the carry over for the multiplication

                int AddCO = 0;
                string addition;
                int coCount = tempAddCount;

                if (currentProduct.Length > 1) //Assuming 2 digits product
                {
                    if (result.Count == 0 || result.Count <= coCount)
                    {
                        result.Insert(coCount, (int)Char.GetNumericValue(currentProduct[1]));
                    }
                    else
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[1]) + result.ElementAt(coCount)).ToString();

                        if (addition.Length > 1)
                        {
                            char[] currentAddition = addition.ToCharArray();
                            result[coCount] = (int)Char.GetNumericValue(currentAddition[1]);
                            AddCO = (int)Char.GetNumericValue(currentAddition[0]);
                        }
                        else
                        {
                            result[coCount] = Int32.Parse(addition);
                        }
                    }

                    coCount++;

                    if (result.Count == 0 || result.Count <= coCount)
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[0]) + AddCO).ToString();
                        if (addition.Length > 1)
                        {
                            char[] currentAddition = addition.ToCharArray();
                            result.Insert(coCount, (int)Char.GetNumericValue(currentAddition[1]));
                            AddCO = (int)Char.GetNumericValue(currentAddition[0]);
                        }
                        else
                        {
                            result.Insert(coCount, Int32.Parse(addition));
                            AddCO = 0;
                        }
                    }
                    else
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[0]) + AddCO + result.ElementAt(coCount)).ToString();
                        if (addition.Length > 1)
                        {
                            char[] currentAddition = addition.ToCharArray();
                            result[coCount] = (int)Char.GetNumericValue(currentAddition[1]);
                            AddCO = (int)Char.GetNumericValue(currentAddition[0]);
                        }
                        else
                        {
                            result[coCount] = Int32.Parse(addition);
                            AddCO = 0;
                        }
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
                            if (addition.Length > 1)
                            {
                                char[] currentAddition = addition.ToCharArray();
                                result[coCount] = (int)Char.GetNumericValue(currentAddition[1]);
                                AddCO = (int)Char.GetNumericValue(currentAddition[0]);
                            }
                            else
                            {
                                result[coCount] = Int32.Parse(addition);
                                AddCO = 0;
                            }
                        }
                    }

                }
                else
                {
                    if (result.Count == 0 || result.Count <= coCount)
                    {
                        result.Insert(coCount, (int)Char.GetNumericValue(currentProduct[1]));
                    }
                    else
                    {
                        addition = ((int)Char.GetNumericValue(currentProduct[0]) + result.ElementAt(coCount)).ToString();

                        if (addition.Length > 1)
                        {
                            char[] currentAddition = addition.ToCharArray();
                            result[coCount] = (int)Char.GetNumericValue(currentAddition[1]);
                            AddCO = (int)Char.GetNumericValue(currentAddition[0]);
                        }
                        else
                        {
                            result[coCount] = Int32.Parse(addition);
                        }
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
                            if (addition.Length > 1)
                            {
                                char[] currentAddition = addition.ToCharArray();
                                result[coCount] = (int)Char.GetNumericValue(currentAddition[1]);
                                AddCO = (int)Char.GetNumericValue(currentAddition[0]);
                            }
                            else
                            {
                                result[coCount] = Int32.Parse(addition);
                                AddCO = 0;
                            }
                        }
                    }

                }


                tempAddCount += 1;
            }
        }

        result.Reverse();
        return result;
    }

    static List<int> Add(int[] x, int[] y)
    {
        List<int> sum = new List<int>();
        int maxDigLength = Math.Max(x.Length, y.Length);
        int rem = 0;
        for (int ctr = 0; ctr < maxDigLength; ctr++)
        {
            int xDig = ctr < x.Length ? x[ctr] : 0;
            int yDig = ctr < y.Length ? y[ctr] : 0;
            int digSum = xDig + yDig + rem;
            sum.Add(digSum % 10);
            rem = digSum / 10;
        }
        if (rem > 0)
        {
            sum.Add(rem);
        }
        return sum;
    }

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

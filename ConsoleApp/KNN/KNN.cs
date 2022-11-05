namespace ConsoleApp.KNN;

public class KNN
{
    public KNN()
    {

    }

    public double TestKNN(List<string> trainDataset, List<string> testDataset, int K = 1, bool useWeights = false)
    {
        double sumOfTrueDecisions = 0;
        double testDataCount = 1000;
        for (int i = 1; i < testDataCount; i++)
        {
            List<Neighbour> neighbourList = new List<Neighbour>();
            List<string> testColumns = testDataset[i].Split(",").ToList();
            Dictionary<string, double> neighbourFreq = new Dictionary<string, double>();
            string neighborName = "";
            for (int j = 1; j < trainDataset.Count() - 1; j++)
            {
                List<string> trainColumns = trainDataset[j].Split(",").ToList();
                neighborName = trainColumns[trainColumns.Count() - 1];
                double sumOfSquares = 0;
                for (int k = 0; k < testColumns.Count() - 1; k++)
                {
                    double testValue = Double.Parse(testColumns[k].Replace(".", ","));
                    double trainValue = Double.Parse(trainColumns[k].Replace(".", ","));
                    sumOfSquares += Math.Pow((trainValue - testValue), 2);
                }
                double value = Math.Sqrt(sumOfSquares);
                if (useWeights)
                    value = (double)1 / (double)Math.Pow(value, 2);
                neighbourList.Add(new Neighbour(neighborName, value));
                // System.Console.WriteLine();
            }

            if (useWeights)
                neighbourList = neighbourList.OrderByDescending(x => x.Value).ToList();
            else
                neighbourList = neighbourList.OrderBy(x => x.Value).ToList();


            // Karar Verildi
            for (int j = 0; j < K; j++)
            {
                if (useWeights)
                {
                    if (neighbourFreq.ContainsKey(neighbourList[j].Name))
                    {
                        neighbourFreq[neighbourList[j].Name] += neighbourList[j].Value;
                    }
                    else
                    {
                        neighbourFreq.Add(neighbourList[j].Name, neighbourList[j].Value);
                    }
                }
                else
                {
                    if (neighbourFreq.ContainsKey(neighbourList[j].Name))
                    {
                        neighbourFreq[neighbourList[j].Name] += 1;
                    }
                    else
                    {
                        neighbourFreq.Add(neighbourList[j].Name, 1);
                    }
                }
            }
            neighbourFreq = neighbourFreq.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            string decision = neighbourFreq.First().Key;
            if (decision.Equals(testColumns[testColumns.Count() - 1]))
            {
                sumOfTrueDecisions++;
            }
            // System.Console.WriteLine();
        }
        System.Console.WriteLine();
        return (double)sumOfTrueDecisions / (double)testDataCount;
    }
}

public class Neighbour
{
    public string Name;
    public double Value;
    public Neighbour(string Name, double Value)
    {
        this.Name = Name;
        this.Value = Value;
    }
}
namespace NonTransitiveDiceGame;

public class ProbabilityCalculator
{
    public double CalculateWinProbability(Dice dieA, Dice dieB)
    {
        var wins = dieA.Faces
            .SelectMany(_ => dieB.Faces, (faceA, faceB) => new { faceA, faceB })
            .Count(pair => pair.faceA > pair.faceB);

        // The total number of possible outcomes is the product of the number of faces.
        var totalOutcomes = dieA.Faces.Count * dieB.Faces.Count;

        // Probability = (Favorable Outcomes) / (Total Possible Outcomes)
        return (double)wins / totalOutcomes;
    }
}
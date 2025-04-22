using TLPractice.Models.Races;

namespace Fighters
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gameManager = new GameManager();

            var winner = gameManager.Play(
                new Knight("Tom", new Human()),
                new Knight("Bob", new Human()));
            Console.WriteLine($"Winner: {winner.Name}");
        }
    }
}
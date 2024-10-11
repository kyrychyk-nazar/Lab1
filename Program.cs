using System;
using System.Collections.Generic;

namespace Лаб1
{
    public class Game
    {
        public string OpponentName { get; set; }
        public int Rating { get; set; }
        public bool IsWin { get; set; }
        public Guid GameId { get; private set; }

        public Game(string opponentName, int rating, bool isWin)
        {
            if (rating <= 0)
            {
                throw new ArgumentException("Рейтинг повинен бути вище 0");
            }
            OpponentName = opponentName;
            Rating = rating;
            IsWin = isWin;
            GameId = Guid.NewGuid();
        }
    }

    public class GameAccount
    {
        public string Username { get; set; }
        public int CurrentRating { get; set; }
        public int GameCounts { get; private set; }

        private List<Game> gamesHistory = new List<Game>();

        public GameAccount(string username, int pochatRating)
        {
            if (pochatRating < 1)
            {
                throw new ArgumentException("Рейтинг має бути хоча б 1.");
            }
            Username = username;
            CurrentRating = pochatRating;
            GameCounts = 0;
        }

        public void WinGame(string opponentName, int rating)
        {
            CurrentRating += rating;
            GameCounts++;
            gamesHistory.Add(new Game(opponentName, rating, true));
            Console.WriteLine($"Win against {opponentName} with {rating}. Current rating: {CurrentRating}");
        }

        public void LoseGame(string opponentName, int rating)
        {
            if (CurrentRating - rating < 1)
            {
                Console.WriteLine("Рейтинг не може бути менше одного, ваш рейтинг залишається поточним.");
                rating = 0;
            }
            CurrentRating -= rating;
            GameCounts++;
            gamesHistory.Add(new Game(opponentName, rating, false));
            Console.WriteLine($"Lose against {opponentName} with {rating}. Current rating: {CurrentRating}");
        }

        public void GetStats()
        {
            Console.WriteLine($"\nСтатистика ігор гравця {Username}:");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("| Противник     | Результат  | Рейтинг | Game ID      |");
            Console.WriteLine("-------------------------------------------------");

            foreach (var game in gamesHistory)
            {
                string result = game.IsWin ? "Win" : "Lose";
                Console.WriteLine($"| {game.OpponentName,-12} | {result,-7} | {game.Rating,-6} | {game.GameId,-10} |");
            }
            Console.WriteLine("-------------------------------------------------");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            GameAccount player1 = new GameAccount("Player1", 100);
            GameAccount player2 = new GameAccount("Player2", 80);
            
            player1.WinGame("Player2", 20);
            player2.LoseGame("Player1", 20);

            player1.LoseGame("Player3", 15);
            player2.WinGame("Player3", 25);
            
            player1.GetStats();
            player2.GetStats();
        }
    }
}
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Reverse UNO Game! The goal is to have the most cards.");
        
        // Initialize deck, players, and starting hands
        List<string> deck = InitializeDeck();
        ShuffleDeck(deck);

        List<string> player1Hand = DrawCards(deck, 7);
        List<string> player2Hand = DrawCards(deck, 7);

        string currentCard = deck[0];
        deck.RemoveAt(0);
        
        Console.WriteLine($"Starting card: {currentCard}\n");

        bool player1Turn = true;
        while (deck.Count > 0)
        {
            if (player1Turn)
            {
                Console.WriteLine("Player 1's turn:");
                PlayTurn(player1Hand, player2Hand, ref currentCard, deck);
            }
            else
            {
                Console.WriteLine("Player 2's turn:");
                PlayTurn(player2Hand, player1Hand, ref currentCard, deck);
            }
            player1Turn = !player1Turn;
        }

        Console.WriteLine("\nThe deck is empty! Time to count cards.");
        Console.WriteLine($"Player 1 has {player1Hand.Count} cards.");
        Console.WriteLine($"Player 2 has {player2Hand.Count} cards.");

        if (player1Hand.Count > player2Hand.Count)
            Console.WriteLine("Player 1 wins with the most cards!");
        else if (player2Hand.Count > player1Hand.Count)
            Console.WriteLine("Player 2 wins with the most cards!");
        else
            Console.WriteLine("It's a tie!");
    }

    static List<string> InitializeDeck()
    {
        List<string> deck = new List<string>();
        string[] colors = { "Red", "Yellow", "Green", "Blue" };
        for (int i = 1; i <= 9; i++)
        {
            foreach (string color in colors)
            {
                deck.Add($"{color} {i}");
            }
        }
        return deck;
    }

    static void ShuffleDeck(List<string> deck)
    {
        Random rng = new Random();
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            string temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    static List<string> DrawCards(List<string> deck, int count)
    {
        List<string> hand = new List<string>();
        for (int i = 0; i < count && deck.Count > 0; i++)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
        return hand;
    }

    static void PlayTurn(List<string> currentPlayerHand, List<string> otherPlayerHand, ref string currentCard, List<string> deck)
    {
        Console.WriteLine($"Current card: {currentCard}");
        Console.WriteLine("Your hand: " + string.Join(", ", currentPlayerHand));

        // Attempt to play a card
        for (int i = 0; i < currentPlayerHand.Count; i++)
        {
            string card = currentPlayerHand[i];
            if (card.Split(' ')[0] == currentCard.Split(' ')[0] ||
                card.Split(' ')[1] == currentCard.Split(' ')[1])
            {
                currentCard = card;
                currentPlayerHand.RemoveAt(i);
                Console.WriteLine($"Played {card}\n");
                return;
            }
        }

        // If no playable card
        if (currentPlayerHand.Count > 0)
        {
            string givenCard = currentPlayerHand[0];
            currentPlayerHand.RemoveAt(0);
            otherPlayerHand.Add(givenCard);
            Console.WriteLine($"No playable card. Gave {givenCard} to the other player.");

            // Other player plays any card of their choice
            if (otherPlayerHand.Count > 0)
            {
                Console.WriteLine("Other player gets to play any card of their choice.");
                string chosenCard = otherPlayerHand[0];
                otherPlayerHand.RemoveAt(0);
                currentCard = chosenCard;
                Console.WriteLine($"Other player played {chosenCard}.");

                // Other player draws 2 more cards
                List<string> extraCards = DrawCards(deck, 2);
                otherPlayerHand.AddRange(extraCards);
                Console.WriteLine($"Other player drew 2 more cards: {string.Join(", ", extraCards)}\n");
            }
        }
    }
}

using System;
using System.Threading.Channels;


Console.WriteLine();
Card card1 = new Card(3, 4);
GameBoard board = new GameBoard();
card1.RandomCard();
foreach (var item in card1.Deck)
{
    Console.Write($"{item} ");
}
Console.WriteLine();
board.ShowBoard(card1);


class Card
{
    public string[] Deck { get; set; }
    public int Row { get; }
    public int Col { get; }

    public Card(int scale1, int scale2)
    {
        Deck = new string[scale1 * scale2];
        Row = scale1;
        Col = scale2;
        for (int i = 0; i < Deck.Length / 2; i++)
        {
            Deck[i * 2] = $"{i + 1}";
            Deck[i * 2 + 1] = $"{i + 1}";
        }
    }
    public void RandomCard()
    {
        //랜덤으로 카드 섞기 기능
        Random random = new Random();
        for (int i = 0; i < Deck.Length; i++)
        {
            int index = random.Next(0, Deck.Length);
            string temp = Deck[i];
            Deck[i] = Deck[index];
            Deck[index] = temp;
        }
    }
}

class GameBoard
{

    public void ShowBoard(Card card)
    {
        for (int i = 0; i < card.Row; i++)
        {
            for (int j = 0; j < card.Col; j++)
            {
                Console.Write(card.Deck[i]);
            }
            Console.WriteLine();
        }
    }
}







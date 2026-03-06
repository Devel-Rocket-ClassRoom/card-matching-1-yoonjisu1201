using System;
using System.Threading.Channels;
Card card = new Card();
card.CardSetting();
card.ShowBoard();


class Card
{
    public int[] cardDeck;
    public int[] cardDeck2;

    Random rnd = new Random();
    public void CardSetting()
    {
        cardDeck = new int[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        cardDeck2 = new int[16];

        for (int i = 0; i < 16; i++)
        {
            int index = rnd.Next(0, 16);
            int temp = cardDeck[i];
            cardDeck[i] = cardDeck[index];
            cardDeck[index] = temp;
        }
    }
    public void ShowBoard()
    {
        Console.WriteLine($"\t1열\t2열\t3열\t4열");
        for (int i = 0; i < cardDeck.Length; i++)
        {
            if (cardDeck[i] == cardDeck2[i])
            {
                Console.Write($"\t[{cardDeck[i]}]");
            }
            else
            {
                Console.Write($"\t**");
            }

            if ((i + 1) % 4 == 0)
            {
                Console.WriteLine();
            }
        }
    }

   public void CardSelect()
    {
        while (true)
        {
            Console.Write("첫 번째 카드를 선택하세요 (행 열): ");
            string value = Console.ReadLine();
            if (Convert.ToInt32(value) >= 10)
            {
                Console.WriteLine("행과 열을 공백으로 구분하여 입력하세요. (예: 1 3)");
                continue;
            }
            else if (true)
            {
                
            }
        }
    }
}







using System;
using System.Threading.Channels;

PlayManager playManager = new PlayManager(4, 4);
playManager.Playing();



class Card   // 카드 정보 클래스
{
    public string[] Deck { get; set; }
    public bool[] isFlipped { get; set; }
    public int Row { get; }
    public int Col { get; }

    public Card(int scale1, int scale2)
    {
        Deck = new string[scale1 * scale2];
        isFlipped = new bool[scale1 * scale2];
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
        Console.WriteLine("카드를 섞는 중 . . .");
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

class GameBoard   // 게임보드 출력 클래스
{
    public void ShowBoard(Card card)
    {
        Console.WriteLine($"\t1열\t2열\t3열\t4열");
        for (int i = 0; i < card.Row; i++)
        {
            Console.Write($"{i + 1}행\t");
            for (int j = 0; j < card.Col; j++)
            {
                int index = i * 4 + j;
                if (card.isFlipped[index] == true)
                {
                    Console.Write($" [{card.Deck[index]}]\t");
                }
                else
                {
                    Console.Write($" **\t");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

class PlayManager : GameBoard   // 게임 플레이 관련 : 카드 뽑기 메서드,  시도횟수 카운트변수, 찾은쌍 카운트변수 ,  카드 클래스 객체생성, 게임보드 객체생성, 뽑은카드 저장할 변수 2개
{
    private int TryCount;
    private int MatchCount;
    private int Max_TryCount;
    private int Max_MatchCount;
    public int FirstCard_index { get; private set; }
    public int SecondCard_index { get; private set; }
    public string FirstCard { get; private set; }
    public string SecondCard { get; private set; }
    private Card card;
    private GameBoard board;

    public PlayManager(int scale1, int scale2)
    {
        card = new Card(scale1, scale2);
        board = new GameBoard();
        card.RandomCard();
        Max_MatchCount = card.Deck.Length / 2;
    }
    public void Playing()
    {
        //현재 진행상황 테스트 출력중
        while (true)
        {
            board.ShowBoard(card);
            FlipCard(card);
            CheckForMatch();
            if (TryCount >= 10)
            {
                break;
            }
            else if (MatchCount == Max_MatchCount)
            {
                break;
            }
   

        }
    }


    public void FlipCard(Card card)   // 카드 두장 뒤집는 기능
    {

        for (int i = 0; i < 2; i++)
        {
            while (true)
            {
                if (i == 0)
                {
                    Console.Write("첫 번째 카드를 선택하세요 (행 열): ");
                }
                else
                {
                    Console.Write("두 번째 카드를 선택하세요 (행 열): ");
                }
                string input = Console.ReadLine();
                string[] value = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (value.Length == 2 && int.TryParse(value[0], out int row) && int.TryParse(value[1], out int col))
                {
                    if ((row <= card.Row && row >= 1) || (col <= card.Col && col >= 1))
                    {
                        int index = (row - 1) * 4 + (col - 1);
                        if (i == 0)
                        {
                            FirstCard = card.Deck[index];
                            card.isFlipped[index] = true;
                            board.ShowBoard(card);
                            Console.WriteLine($"시도 횟수: {TryCount}/{Max_TryCount} | 찾은 쌍: {MatchCount}/{Max_MatchCount}");

                        }
                        else
                        {
                            SecondCard = card.Deck[index];
                            card.isFlipped[index] = true;
                            board.ShowBoard(card);
                            Console.WriteLine($"시도 횟수: {TryCount}/{Max_TryCount} | 찾은 쌍: {MatchCount}/{Max_MatchCount}");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"행은 1~{card.Col}, 열은 1~{card.Col} 범위로 입력하세요.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("행과 열을 공백으로 구분하여 입력하세요. (예: 1 3)");
                    continue;
                }
            }

        }
    }
    public void CheckForMatch()
    {
        bool isMatch = FirstCard == SecondCard;
        if (isMatch)
        {
            TryCount++;
            MatchCount++;
            Console.WriteLine("짝을 찾았습니다!");
        }
        else
        {
            TryCount++;
            card.isFlipped[FirstCard_index] = false;
            card.isFlipped[SecondCard_index] = false;
            Console.WriteLine("짝이 맞지 않습니다!");
        }
        FirstCard = string.Empty;
        SecondCard = string.Empty;
    }


}







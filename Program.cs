using System;

namespace TicTacToeGame

{
    //יש לפתח משחק בסביבת קונסול המאפשר ל2 שחקנים לשחק אחד נגד השני איקס עיגול
    //על המשחק לזהות אוטומטית כאשר נגמר המשחק
    //אם יש מנצח על יש להציג את שמו על המסך
    //אתגר - יש לאפשר לשחק נגד המחשב
    //חשוב:
    //יש לפתח את המשחק בקונספקט של OOP
    //יש לעשות שימוש ביכולות שנלמדו במהלך המולול

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WindowWidth = 145;
            Console.WindowHeight = 45;
            while (true)//In an infinite loop
            {
                Battle newBattle = new Battle(UIManager.SelectBattleType());//create new battle
                do
                {                 
                    newBattle.CurrentGame.Play();//and play new game
                } while (!UIManager.IsSelectedNewBattle()); //until a new battle is selected
            }                                    
        }//Main               
    }//Program
}//TicTacToeGame

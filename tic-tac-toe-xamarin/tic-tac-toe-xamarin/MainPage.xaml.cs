using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tic_tac_toe_xamarin
{
    public partial class MainPage : ContentPage
    {
        private int[,] gameBoard = new int[3, 3];
        private int currentPlayer = 1;
        private bool isGameOver = false;
        private int player1Score = 0;
        private int player2Score = 0;


        public MainPage()
        {
            InitializeComponent();

        }
        private void ResetGame()
        {
            // Reset the game board
            gameBoard = new int[3, 3];

            // Reset the box views
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    BoxView boxView = board.Children
                        .Cast<BoxView>()
                        .First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);

                    boxView.Color = Color.Gray;
                }
            }

            // Reset game over flag
            isGameOver = false;
        }

        private void BoxView_Tapped(object sender, EventArgs e)
        {
            BoxView boxView = (BoxView)sender;
            int row = Grid.GetRow(boxView);
            int column = Grid.GetColumn(boxView);

            if (gameBoard[row, column] == 0 && !isGameOver)
            {
                if (currentPlayer == 1)
                {
                    boxView.Color = Color.Blue;
                    gameBoard[row, column] = 1;
                    currentPlayer = 2;
                }
                else
                {
                    boxView.Color = Color.Red;
                    gameBoard[row, column] = 2;
                    currentPlayer = 1;
                }

                // Check for winner or draw
                int winner = CheckForWinner();
                if (winner != 0)
                {
                    isGameOver = true;
                    DisplayAlert("Game Over", $"Player {winner} wins! \nPlayer 1: {player1Score} \nPlayer 2: {player2Score}", "OK");

                }
                else if (IsBoardFull())
                {
                    isGameOver = true;
                    DisplayAlert("Game Over", "The game is a draw.", $"\nPlayer 1: {player1Score} \nPlayer 2: {player2Score}", "OK");
                }
            }
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (gameBoard[row, col] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        private int CheckForWinner()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (gameBoard[row, 0] != 0 && gameBoard[row, 0] == gameBoard[row, 1] && gameBoard[row, 1] == gameBoard[row, 2])
                {
                    if (gameBoard[row, 0] == 1)
                    {
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
                    }
                    return gameBoard[row, 0];
                }
            }

            // Check columns
            for (int column = 0; column < 3; column++)
            {
                if (gameBoard[0, column] != 0 && gameBoard[0, column] == gameBoard[1, column] && gameBoard[1, column] == gameBoard[2, column])
                {
                    if (gameBoard[0, column] == 1)
                    {
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
                    }
                    return gameBoard[0, column];
                }
            }

            // Check diagonals
            if (gameBoard[0, 0] != 0 && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
            {
                if (gameBoard[0, 0] == 1)
                {
                    player1Score++;
                }
                else
                {
                    player2Score++;
                }
                return gameBoard[0, 0];
            }
            if (gameBoard[0, 2] != 0 && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
            {
                if (gameBoard[0, 2] == 1)
                {
                    player1Score++;
                }
                else
                {
                    player2Score++;
                }
                return gameBoard[0, 2];
            }

            // No winner
            return 0;
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}

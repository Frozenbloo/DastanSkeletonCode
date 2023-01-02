using System;
using System.Collections.Generic;

namespace DastanSkeletonCode
{
        class Program
        {

            static void Main(string[] args)
            {
                Dastan ThisGame = new Dastan(6, 6, 4);
                ThisGame.PlayGame();
                Console.WriteLine("Goodbye!");
                Console.ReadLine();
            }
        }

        class Dastan
        {
            protected List<Square> Board;
            protected int NoOfRows, NoOfColumns, MoveOptionOfferPosition;
            protected List<Player> Players = new List<Player>();
            protected List<string> MoveOptionOffer = new List<string>();
            protected Player CurrentPlayer;
            protected Random RGen = new Random();

            public Dastan(int R, int C, int NoOfPieces)
            {
                Players.Add(new Player("Player One", 1));
                Players.Add(new Player("Player Two", -1));
                CreateMoveOptions();
                NoOfRows = R;
                NoOfColumns = C;
                MoveOptionOfferPosition = 0;
                CreateMoveOptionOffer();
                CreateBoard();
                CreatePieces(NoOfPieces);
                CurrentPlayer = Players[0];
            }

            /// <summary>
            /// <c>DisplayBoard</c> draws the board.
            /// </summary>
            private void DisplayBoard()
            {
                Console.Write(Environment.NewLine + "   ");
                for (int Column = 1; Column <= NoOfColumns; Column++)
                {
                    Console.Write(Column.ToString() + "  ");
                }
                Console.Write(Environment.NewLine + "  ");
                for (int Count = 1; Count <= NoOfColumns; Count++)
                {
                    Console.Write("---");
                }
                Console.WriteLine("-");
                for (int Row = 1; Row <= NoOfRows; Row++)
                {
                    Console.Write(Row.ToString() + " ");
                    for (int Column = 1; Column <= NoOfColumns; Column++)
                    {
                        int Index = GetIndexOfSquare(Row * 10 + Column);
                        Console.Write("|" + Board[Index].GetSymbol());
                        Piece PieceInSquare = Board[Index].GetPieceInSquare();
                        if (PieceInSquare == null)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write(PieceInSquare.GetSymbol());
                        }
                    }
                    Console.WriteLine("|");
                }
                Console.Write("  -");
                for (int Column = 1; Column <= NoOfColumns; Column++)
                {
                    Console.Write("---");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            private void DisplayState()
            {
                DisplayBoard();
                Console.WriteLine("Move option offer: " + MoveOptionOffer[MoveOptionOfferPosition]);
                Console.WriteLine();
                Console.WriteLine(CurrentPlayer.GetPlayerStateAsString());
                Console.WriteLine("Turn: " + CurrentPlayer.GetName());
                Console.WriteLine();
            }

            /// <summary>
            /// Calculates the index of any Square.
            /// </summary>
            /// <param name="SquareReference">The Square you want to calculate the index of.</param>
            /// <returns>The index of a Square.</returns>
            private int GetIndexOfSquare(int SquareReference)
            {
                int Row = SquareReference / 10;
                int Col = SquareReference % 10;
                return (Row - 1) * NoOfColumns + (Col - 1);
            }
            
            /// <summary>
            /// Checks to see if the Square is in the bounds of the game board.
            /// </summary>
            /// <param name="SquareReference">The Square you want to check.</param>
            /// <returns>True if the square is in the bounds, and false if it is out of bounds</returns>
            private bool CheckSquareInBounds(int SquareReference)
            {
                int Row = SquareReference / 10;
                int Col = SquareReference % 10;
                if (Row < 1 || Row > NoOfRows)
                {
                    return false;
                }
                else if (Col < 1 || Col > NoOfColumns)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            /// <summary>
            /// Checks to see if the Square is a valid move
            /// </summary>
            /// <param name="SquareReference">The Square the player wants to move to</param>
            /// <param name="StartSquare">The Square the piece is currently in</param>
            /// <returns>True if the move is valid, false if it is invalid</returns>
            private bool CheckSquareIsValid(int SquareReference, bool StartSquare)
            {
                if (!CheckSquareInBounds(SquareReference))
                {
                    return false;
                }
                Piece PieceInSquare = Board[GetIndexOfSquare(SquareReference)].GetPieceInSquare();
                if (PieceInSquare == null)
                {
                    if (StartSquare)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (CurrentPlayer.SameAs(PieceInSquare.GetBelongsTo()))
                {
                    if (StartSquare)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (StartSquare)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            /// <summary>
            /// Checks to see if the game is over
            /// </summary>
            /// <returns>Returns true if the game is over, or false if it is still ongoing./returns>
            private bool CheckIfGameOver()
            {
                bool Player1HasMirza = false;
                bool Player2HasMirza = false;
                foreach (var S in Board)
                {
                    Piece PieceInSquare = S.GetPieceInSquare();
                    if (PieceInSquare != null)
                    {
                        if (S.ContainsKotla() && PieceInSquare.GetTypeOfPiece() == "mirza" && !PieceInSquare.GetBelongsTo().SameAs(S.GetBelongsTo()))
                        {
                            return true;
                        }
                        else if (PieceInSquare.GetTypeOfPiece() == "mirza" && PieceInSquare.GetBelongsTo().SameAs(Players[0]))
                        {
                            Player1HasMirza = true;
                        }
                        else if (PieceInSquare.GetTypeOfPiece() == "mirza" && PieceInSquare.GetBelongsTo().SameAs(Players[1]))
                        {
                            Player2HasMirza = true;
                        }
                    }
                }
                return !(Player1HasMirza && Player2HasMirza);
            }

            private int GetSquareReference(string Description)
            {
                int SelectedSquare;
                Console.Write("Enter the square " + Description + " (row number followed by column number): ");
                SelectedSquare = Convert.ToInt32(Console.ReadLine());
                return SelectedSquare;
            }

            private void UseMoveOptionOffer()
            {
                int ReplaceChoice;
                Console.Write("Choose the move option from your queue to replace (1 to 5): ");
                ReplaceChoice = Convert.ToInt32(Console.ReadLine());
                CurrentPlayer.UpdateMoveOptionQueueWithOffer(ReplaceChoice - 1, CreateMoveOption(MoveOptionOffer[MoveOptionOfferPosition], CurrentPlayer.GetDirection()));
                CurrentPlayer.ChangeScore(-(10 - (ReplaceChoice * 2)));
                MoveOptionOfferPosition = RGen.Next(0, 5);
            }

            private int GetPointsForOccupancyByPlayer(Player CurrentPlayer)
            {
                int ScoreAdjustment = 0;
                foreach (var S in Board)
                {
                    ScoreAdjustment += (S.GetPointsForOccupancy(CurrentPlayer));
                }
                return ScoreAdjustment;
            }

            private void UpdatePlayerScore(int PointsForPieceCapture)
            {
                CurrentPlayer.ChangeScore(GetPointsForOccupancyByPlayer(CurrentPlayer) + PointsForPieceCapture);
            }

            private int CalculatePieceCapturePoints(int FinishSquareReference)
            {
                if (Board[GetIndexOfSquare(FinishSquareReference)].GetPieceInSquare() != null)
                {
                    return Board[GetIndexOfSquare(FinishSquareReference)].GetPieceInSquare().GetPointsIfCaptured();
                }
                return 0;
            }

            public void PlayGame()
            {
                bool GameOver = false;
                while (!GameOver)
                {
                    DisplayState();
                    bool SquareIsValid = false;
                    int Choice;
                    do
                    {
                        Console.Write("Choose move option to use from queue (1 to 3) or 9 to take the offer: ");
                        Choice = Convert.ToInt32(Console.ReadLine());
                        if (Choice == 9)
                        {
                            UseMoveOptionOffer();
                            DisplayState();
                        }
                    }
                    while (Choice < 1 || Choice > 3);
                    int StartSquareReference = 0;
                    while (!SquareIsValid)
                    {
                        StartSquareReference = GetSquareReference("containing the piece to move");
                        SquareIsValid = CheckSquareIsValid(StartSquareReference, true);
                    }
                    int FinishSquareReference = 0;
                    SquareIsValid = false;
                    while (!SquareIsValid)
                    {
                        FinishSquareReference = GetSquareReference("to move to");
                        SquareIsValid = CheckSquareIsValid(FinishSquareReference, false);
                    }
                    bool MoveLegal = CurrentPlayer.CheckPlayerMove(Choice, StartSquareReference, FinishSquareReference);
                    if (MoveLegal)
                    {
                        int PointsForPieceCapture = CalculatePieceCapturePoints(FinishSquareReference);
                        CurrentPlayer.ChangeScore(-(Choice + (2 * (Choice - 1))));
                        CurrentPlayer.UpdateQueueAfterMove(Choice);
                        UpdateBoard(StartSquareReference, FinishSquareReference);
                        UpdatePlayerScore(PointsForPieceCapture);
                        Console.WriteLine("New score: " + CurrentPlayer.GetScore() + Environment.NewLine);
                    }
                    if (CurrentPlayer.SameAs(Players[0]))
                    {
                        CurrentPlayer = Players[1];
                    }
                    else
                    {
                        CurrentPlayer = Players[0];
                    }
                    GameOver = CheckIfGameOver();
                }
                DisplayState();
                DisplayFinalResult();
            }

            private void UpdateBoard(int StartSquareReference, int FinishSquareReference)
            {
                Board[GetIndexOfSquare(FinishSquareReference)].SetPiece(Board[GetIndexOfSquare(StartSquareReference)].RemovePiece());
            }

            private void DisplayFinalResult()
            {
                if (Players[0].GetScore() == Players[1].GetScore())
                {
                    Console.WriteLine("Draw!");
                }
                else if (Players[0].GetScore() > Players[1].GetScore())
                {
                    Console.WriteLine(Players[0].GetName() + " is the winner!");
                }
                else
                {
                    Console.WriteLine(Players[1].GetName() + " is the winner!");
                }
            }

            private void CreateBoard()
            {
                Square S;
                Board = new List<Square>();
                for (int Row = 1; Row <= NoOfRows; Row++)
                {
                    for (int Column = 1; Column <= NoOfColumns; Column++)
                    {
                        if (Row == 1 && Column == NoOfColumns / 2)
                        {
                            S = new Kotla(Players[0], "K");
                        }
                        else if (Row == NoOfRows && Column == NoOfColumns / 2 + 1)
                        {
                            S = new Kotla(Players[1], "k");
                        }
                        else
                        {
                            S = new Square();
                        }
                        Board.Add(S);
                    }
                }
            }

            private void CreatePieces(int NoOfPieces)
            {
                Piece CurrentPiece;
                for (int Count = 1; Count <= NoOfPieces; Count++)
                {
                    CurrentPiece = new Piece("piece", Players[0], 1, "!");
                    Board[GetIndexOfSquare(2 * 10 + Count + 1)].SetPiece(CurrentPiece);
                }
                CurrentPiece = new Piece("mirza", Players[0], 5, "1");
                Board[GetIndexOfSquare(10 + NoOfColumns / 2)].SetPiece(CurrentPiece);
                for (int Count = 1; Count <= NoOfPieces; Count++)
                {
                    CurrentPiece = new Piece("piece", Players[1], 1, "\"");
                    Board[GetIndexOfSquare((NoOfRows - 1) * 10 + Count + 1)].SetPiece(CurrentPiece);
                }
                CurrentPiece = new Piece("mirza", Players[1], 5, "2");
                Board[GetIndexOfSquare(NoOfRows * 10 + (NoOfColumns / 2 + 1))].SetPiece(CurrentPiece);
            }

            private void CreateMoveOptionOffer()
            {
                MoveOptionOffer.Add("jazair");
                MoveOptionOffer.Add("chowkidar");
                MoveOptionOffer.Add("cuirassier");
                MoveOptionOffer.Add("ryott");
                MoveOptionOffer.Add("faujdar");
            }

            private MoveOption CreateRyottMoveOption(int Direction)
            {
                MoveOption NewMoveOption = new MoveOption("ryott");
                Move NewMove = new Move(0, 1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, -1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(1 * Direction, 0);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(-1 * Direction, 0);
                NewMoveOption.AddToPossibleMoves(NewMove);
                return NewMoveOption;
            }

            private MoveOption CreateFaujdarMoveOption(int Direction)
            {
                MoveOption NewMoveOption = new MoveOption("faujdar");
                Move NewMove = new Move(0, -1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, 1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, 2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, -2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                return NewMoveOption;
            }

            private MoveOption CreateJazairMoveOption(int Direction)
            {
                MoveOption NewMoveOption = new MoveOption("jazair");
                Move NewMove = new Move(2 * Direction, 0);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(2 * Direction, -2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(2 * Direction, 2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, 2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, -2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(-1 * Direction, -1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(-1 * Direction, 1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                return NewMoveOption;
            }

            private MoveOption CreateCuirassierMoveOption(int Direction)
            {
                MoveOption NewMoveOption = new MoveOption("cuirassier");
                Move NewMove = new Move(1 * Direction, 0);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(2 * Direction, 0);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(1 * Direction, -2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(1 * Direction, 2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                return NewMoveOption;
            }

            private MoveOption CreateChowkidarMoveOption(int Direction)
            {
                MoveOption NewMoveOption = new MoveOption("chowkidar");
                Move NewMove = new Move(1 * Direction, 1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(1 * Direction, -1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(-1 * Direction, 1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(-1 * Direction, -1 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, 2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                NewMove = new Move(0, -2 * Direction);
                NewMoveOption.AddToPossibleMoves(NewMove);
                return NewMoveOption;
            }

            private MoveOption CreateMoveOption(string Name, int Direction)
            {
                if (Name == "chowkidar")
                {
                    return CreateChowkidarMoveOption(Direction);
                }
                else if (Name == "ryott")
                {
                    return CreateRyottMoveOption(Direction);
                }
                else if (Name == "faujdar")
                {
                    return CreateFaujdarMoveOption(Direction);
                }
                else if (Name == "jazair")
                {
                    return CreateJazairMoveOption(Direction);
                }
                else
                {
                    return CreateCuirassierMoveOption(Direction);
                }
            }

            private void CreateMoveOptions()
            {
                Players[0].AddToMoveOptionQueue(CreateMoveOption("ryott", 1));
                Players[0].AddToMoveOptionQueue(CreateMoveOption("chowkidar", 1));
                Players[0].AddToMoveOptionQueue(CreateMoveOption("cuirassier", 1));
                Players[0].AddToMoveOptionQueue(CreateMoveOption("faujdar", 1));
                Players[0].AddToMoveOptionQueue(CreateMoveOption("jazair", 1));
                Players[1].AddToMoveOptionQueue(CreateMoveOption("ryott", -1));
                Players[1].AddToMoveOptionQueue(CreateMoveOption("chowkidar", -1));
                Players[1].AddToMoveOptionQueue(CreateMoveOption("jazair", -1));
                Players[1].AddToMoveOptionQueue(CreateMoveOption("faujdar", -1));
                Players[1].AddToMoveOptionQueue(CreateMoveOption("cuirassier", -1));
            }
        }

        class Piece
        {
            protected string TypeOfPiece, Symbol;
            protected int PointsIfCaptured;
            protected Player BelongsTo;

            public Piece(string T, Player B, int P, string S)
            {
                TypeOfPiece = T;
                BelongsTo = B;
                PointsIfCaptured = P;
                Symbol = S;
            }

            public string GetSymbol()
            {
                return Symbol;
            }

            public string GetTypeOfPiece()
            {
                return TypeOfPiece;
            }

            public Player GetBelongsTo()
            {
                return BelongsTo;
            }

            public int GetPointsIfCaptured()
            {
                return PointsIfCaptured;
            }
        }

        class Square
        {
            protected string Symbol;
            protected Piece PieceInSquare;
            protected Player BelongsTo;

            public Square()
            {
                PieceInSquare = null;
                BelongsTo = null;
                Symbol = " ";
            }

            public virtual void SetPiece(Piece P)
            {
                PieceInSquare = P;
            }

            public virtual Piece RemovePiece()
            {
                Piece PieceToReturn = PieceInSquare;
                PieceInSquare = null;
                return PieceToReturn;
            }

            public virtual Piece GetPieceInSquare()
            {
                return PieceInSquare;
            }

            public virtual string GetSymbol()
            {
                return Symbol;
            }

            public virtual int GetPointsForOccupancy(Player CurrentPlayer)
            {
                return 0;
            }

            public virtual Player GetBelongsTo()
            {
                return BelongsTo;
            }

            public virtual bool ContainsKotla()
            {
                if (Symbol == "K" || Symbol == "k")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        class Kotla : Square
        {
            public Kotla(Player P, string S) : base()
            {
                BelongsTo = P;
                Symbol = S;
            }

            public override int GetPointsForOccupancy(Player CurrentPlayer)
            {
                if (PieceInSquare == null)
                {
                    return 0;
                }
                else if (BelongsTo.SameAs(CurrentPlayer))
                {
                    if (CurrentPlayer.SameAs(PieceInSquare.GetBelongsTo()) && (PieceInSquare.GetTypeOfPiece() == "piece" || PieceInSquare.GetTypeOfPiece() == "mirza"))
                    {
                        return 5;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (CurrentPlayer.SameAs(PieceInSquare.GetBelongsTo()) && (PieceInSquare.GetTypeOfPiece() == "piece" || PieceInSquare.GetTypeOfPiece() == "mirza"))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        class MoveOption
        {
            protected string Name;
            protected List<Move> PossibleMoves;

            public MoveOption(string N)
            {
                Name = N;
                PossibleMoves = new List<Move>();
            }

            public void AddToPossibleMoves(Move M)
            {
                PossibleMoves.Add(M);
            }

            public string GetName()
            {
                return Name;
            }

            public bool CheckIfThereIsAMoveToSquare(int StartSquareReference, int FinishSquareReference)
            {
                int StartRow = StartSquareReference / 10;
                int StartColumn = StartSquareReference % 10;
                int FinishRow = FinishSquareReference / 10;
                int FinishColumn = FinishSquareReference % 10;
                foreach (var M in PossibleMoves)
                {
                    if (StartRow + M.GetRowChange() == FinishRow && StartColumn + M.GetColumnChange() == FinishColumn)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        class Move
        {
            protected int RowChange, ColumnChange;

            public Move(int R, int C)
            {
                RowChange = R;
                ColumnChange = C;
            }

            public int GetRowChange()
            {
                return RowChange;
            }

            public int GetColumnChange()
            {
                return ColumnChange;
            }
        }

        class MoveOptionQueue
        {
            private List<MoveOption> Queue = new List<MoveOption>();

            public string GetQueueAsString()
            {
                string QueueAsString = "";
                int Count = 1;
                foreach (var M in Queue)
                {
                    QueueAsString += Count.ToString() + ". " + M.GetName() + "   ";
                    Count += 1;
                }
                return QueueAsString;
            }

            public void Add(MoveOption NewMoveOption)
            {
                Queue.Add(NewMoveOption);
            }

            public void Replace(int Position, MoveOption NewMoveOption)
            {
                Queue[Position] = NewMoveOption;
            }

            public void MoveItemToBack(int Position)
            {
                MoveOption Temp = Queue[Position];
                Queue.RemoveAt(Position);
                Queue.Add(Temp);
            }

            public MoveOption GetMoveOptionInPosition(int Pos)
            {
                return Queue[Pos];
            }
        }

        class Player
        {
            private string Name;
            private int Direction, Score;
            private MoveOptionQueue Queue = new MoveOptionQueue();

            public Player(string N, int D)
            {
                Score = 100;
                Name = N;
                Direction = D;
            }

            public bool SameAs(Player APlayer)
            {
                if (APlayer == null)
                {
                    return false;
                }
                else if (APlayer.GetName() == Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public string GetPlayerStateAsString()
            {
                return Name + Environment.NewLine + "Score: " + Score.ToString() + Environment.NewLine + "Move option queue: " + Queue.GetQueueAsString() + Environment.NewLine;
            }

            public void AddToMoveOptionQueue(MoveOption NewMoveOption)
            {
                Queue.Add(NewMoveOption);
            }

            public void UpdateQueueAfterMove(int Position)
            {
                Queue.MoveItemToBack(Position - 1);
            }

            public void UpdateMoveOptionQueueWithOffer(int Position, MoveOption NewMoveOption)
            {
                Queue.Replace(Position, NewMoveOption);
            }

            public int GetScore()
            {
                return Score;
            }

            public string GetName()
            {
                return Name;
            }

            public int GetDirection()
            {
                return Direction;
            }

            public void ChangeScore(int Amount)
            {
                Score += Amount;
            }

            public bool CheckPlayerMove(int Pos, int StartSquareReference, int FinishSquareReference)
            {
                MoveOption Temp = Queue.GetMoveOptionInPosition(Pos - 1);
                return Temp.CheckIfThereIsAMoveToSquare(StartSquareReference, FinishSquareReference);
            }
        }
    }


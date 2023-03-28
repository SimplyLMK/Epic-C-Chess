using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace custom_projD
{

    public static class AI
    {
        /*
        private static List<((int, int), (int, int))> openingBook = new List<((int, int), (int, int))>
        {
            ((1, 6), (2, 6)),
            ((0, 6), (2, 5)),
            ((0, 5), (1, 6)),
            ((0, 4), (0, 6)),
        };

        private static ((int, int), (int, int)) GetOpeningMove(Board board)
        {
          
            foreach (var openingMove in openingBook)
            {
                var start = openingMove.Item1;
                var end = openingMove.Item2;

                if (board.pos[start.Item1, start.Item2] != null && board.pos[start.Item1, start.Item2].PieceColor == board.Current_Turn &&
                    board.pos[start.Item1, start.Item2].Is_Valid_Move(end.Item1, end.Item2, board))
                {
                    return openingMove;
                }
            }

            return ((-1, -1), (-1, -1)); // If no opening move is found
        }

        private static bool IsOpeningPhase(Board board)
        {
           
            int totalPieces = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board.pos[row, col] != null)
                    {
                        totalPieces++;
                    }
                }
            }
            return totalPieces > 28;
        }

        private static int EvaluateOpening(Board board, Piece.Color maximizingColor)
        {
            int whiteCenterControl = 0;
            int blackCenterControl = 0;
            int whiteDevelopment = 0;
            int blackDevelopment = 0;
            int[,] centerSquares = { { 3, 3 }, { 3, 4 }, { 4, 3 }, { 4, 4 } };

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board.pos[row, col];
                    if (piece != null)
                    {
                        for (int i = 0; i < centerSquares.GetLength(0); i++)
                        {
                            int targetRow = centerSquares[i, 0];
                            int targetCol = centerSquares[i, 1];

                            if (piece.Is_Valid_Move(targetRow, targetCol, board))
                            {
                                if (piece.PieceColor == Piece.Color.White)
                                {
                                    whiteCenterControl++;
                                }
                                else
                                {
                                    blackCenterControl++;
                                }
                            }
                        }

                        if (piece.PieceType == Piece.Type.Pawn )
                        {
                            
                            if(piece.PieceColor == Piece.Color.White & ((col == 3) || (col == 4)) && (row < 6) )
                            {
                                whiteDevelopment++;
                            }
                            else if (piece.PieceColor == Piece.Color.Black & ((col == 3) || (col == 4)) && (row < 1))
                            {

                                blackDevelopment++;
                            }

                        }


                        if (piece.PieceType == Piece.Type.Knight || piece.PieceType == Piece.Type.Bishop)
                        {
                            if (piece.PieceColor == Piece.Color.White && row < 6)
                            {
                                whiteDevelopment++;
                            }
                            else if (piece.PieceColor == Piece.Color.Black && row > 1)
                            {
                                blackDevelopment++;
                            }
                        }
                    }
                }
            }

            int centerControlScore = maximizingColor == Piece.Color.White ? whiteCenterControl - blackCenterControl : blackCenterControl - whiteCenterControl;
            int developmentScore = maximizingColor == Piece.Color.White ? whiteDevelopment - blackDevelopment : blackDevelopment - whiteDevelopment;

            float centerControlWeight = 0.5f;
            float developmentWeight = 1 - centerControlWeight;

            return (int)(centerControlScore * centerControlWeight + developmentScore * developmentWeight);
        }

        public static int Evaluate(Board board, Piece.Color maximizingColor, int depth)
        {
            bool isOpening = IsOpeningPhase(board);
            int openingScore = isOpening ? EvaluateOpening(board, maximizingColor) : 0;
            int materialScore = !isOpening ? EvaluateMaterial(board, maximizingColor) : 0;

            float openingWeight = 0.5f;
            float materialWeight = 1 - openingWeight;

            return (int)(openingScore * openingWeight + materialScore * materialWeight);
        }

        public static int EvaluateMaterial(Board board, Piece.Color maximizingColor)
        {
            int materialScore = 0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board.pos[row, col];
                    if (piece != null)
                    {
                        int pieceValue = piece.GetValue();
                        if (piece.PieceColor == maximizingColor)
                        {
                            materialScore += pieceValue;
                        }
                        else
                        {
                            materialScore -= pieceValue;
                        }
                    }
                }
            }

            return materialScore;
        }




        /*
                public static (int, ((int, int), (int, int))) Minimax(Board board, int depth, bool maximizingPlayer, Piece.Color maximizingColor)
                {
                    if (depth == 0 || board.GameOver())
                    {
                        int score = Evaluate(board, maximizingColor, depth);
                        return (score, ((-1, -1), (-1, -1)));
                    }


                    List<((int, int), (int, int))> moves = board.GetMoves();
                    ((int, int), (int, int)) bestMove = moves[new Random().Next(moves.Count)];
                    int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;

                    foreach (((int, int) start, (int, int) end) move in moves)
                    {
                        Piece capturedPiece = board.pos[move.end.Item1, move.end.Item2];
                        board.AI_Mov(move.start.Item1, move.start.Item2, move.end.Item1, move.end.Item2);
                        (int score, ((int, int), (int, int))) currentMove = Minimax(board, depth - 1, !maximizingPlayer, maximizingColor);
                        board.AI_Undo(move.start, move.end, capturedPiece);

                        if (maximizingPlayer)
                        {
                            if (currentMove.score > bestScore)
                            {
                                bestScore = currentMove.score;
                                bestMove = move;
                            }
                        }
                        else
                        {
                            if (currentMove.score < bestScore)
                            {
                                bestScore = currentMove.score;
                                bestMove = move;
                            }
                        }
                    }

                    return (bestScore, bestMove);
                }
            }

        */

        // Reference for alpha beta prunning algorithm code https://www.youtube.com/watch?v=l-hh51ncgDI
        /*
        public static (int, ((int, int), (int, int))) AlphaBeta(Board board, int depth, int alpha, int beta, bool maximizingPlayer, Piece.Color maximizingColor)
        {

            var openingMove = GetOpeningMove(board);
            if (openingMove != ((-1, -1), (-1, -1)))
            {
                // If an opening move is found, return it without going through the algorithm
                return (0, openingMove);
            }

            if (depth == 0 || board.GameOver())
            {
                int score = Evaluate(board, maximizingColor, depth);
                return (score, ((-1, -1), (-1, -1)));
            }

            List<((int, int), (int, int))> moves = board.GetMoves();
            ((int, int), (int, int)) bestMove = moves[new Random().Next(moves.Count)];

            foreach (((int, int) start, (int, int) end) move in moves)
            {
                Piece capturedPiece = board.pos[move.end.Item1, move.end.Item2];
                board.AI_Mov(move.start.Item1, move.start.Item2, move.end.Item1, move.end.Item2);
                (int score, ((int, int), (int, int))) currentMove = AlphaBeta(board, depth - 1, alpha, beta, !maximizingPlayer, maximizingColor);
                board.AI_Undo(move.start, move.end, capturedPiece);

                if (maximizingPlayer)
                {
                    if (currentMove.score > alpha)
                    {
                        alpha = currentMove.score;
                        bestMove = move;
                    }
                }
                else
                {
                    if (currentMove.score < beta)
                    {
                        beta = currentMove.score;
                        bestMove = move;
                    }
                }

                if (alpha >= beta)
                {
                    break; // Pruning the unneccessary branch
                }
            }

            return (maximizingPlayer ? alpha : beta, bestMove);
        }

    }
    */

        private static List<((int, int), (int, int))> open_mov = new List<((int, int), (int, int))>
        {
            ((1, 6), (2, 6)),
            ((0, 6), (2, 5)),
            ((0, 5), (1, 6)),          
            ((0, 4), (0, 6)),
        };

        private static int cur_open_Movs = 0;

        public static ((int, int), (int, int)) GetOpeningMove()
        {
            if (cur_open_Movs < open_mov.Count)
            {
                ((int, int), (int, int)) move = open_mov[cur_open_Movs];
                cur_open_Movs++;
                return move;
            }
            else
            {
                return ((-1, -1), (-1, -1));
            }
        }


        public static int Evaluate(Board board, Piece.Color maximizingColor)
        {

            int whiteScore = board.Def_Score_Initial_Value(Piece.Color.White);
            int blackScore = board.Def_Score_Initial_Value(Piece.Color.Black);


            if (maximizingColor == Piece.Color.White)
            {
                return whiteScore - blackScore;
            }
            else
            {
                return blackScore - whiteScore;
            }
        }

        public static (int, ((int, int), (int, int))) Minimax(Board board, int depth, bool maximizingPlayer, Piece.Color maximizingColor)
        {
            if (depth == 0 || board.GameOver())
            {
                int score = Evaluate(board, maximizingColor);
                return (score, ((-1, -1), (-1, -1)));
            }

            List<((int, int), (int, int))> moves = board.GetMoves();
            ((int, int), (int, int)) bestMove = moves[new Random().Next(moves.Count)];
            int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;

            foreach (((int, int) start, (int, int) end) move in moves)
            {
                Piece capturedPiece = board.pos[move.end.Item1, move.end.Item2];
                board.AI_Mov(move.start.Item1, move.start.Item2, move.end.Item1, move.end.Item2);
                (int score, ((int, int), (int, int))) currentMove = Minimax(board, depth - 1, !maximizingPlayer, maximizingColor);
                board.AI_Undo(move.start, move.end, capturedPiece);

                if (maximizingPlayer)
                {
                    if (currentMove.score > bestScore)
                    {
                        bestScore = currentMove.score;
                        bestMove = move;
                    }
                }
                else
                {
                    if (currentMove.score < bestScore)
                    {
                        bestScore = currentMove.score;
                        bestMove = move;
                    }
                }
            }

            return (bestScore, bestMove);
        }
    }





}


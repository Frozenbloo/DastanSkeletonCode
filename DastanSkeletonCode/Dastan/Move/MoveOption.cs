using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
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
}

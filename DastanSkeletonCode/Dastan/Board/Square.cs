using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
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
}

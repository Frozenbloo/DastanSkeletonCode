using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
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
}

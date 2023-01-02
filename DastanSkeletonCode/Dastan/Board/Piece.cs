using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
	class Piece
	{
		protected string TypeOfPiece, Symbol;
		protected int PointsIfCaptured;
		protected Player BelongsTo;

		/// <summary>
		/// The default constructor for the Piece Class
		/// </summary>
		/// <param name="T">The type of piece</param>
		/// <param name="B">Who the piece belongs to</param>
		/// <param name="P">The points if the piece is captured</param>
		/// <param name="S">The Piece Symbol</param>
		public Piece(string T, Player B, int P, string S)
		{
			TypeOfPiece = T;
			BelongsTo = B;
			PointsIfCaptured = P;
			Symbol = S;
		}

		/// <summary>
		/// Symbol Getter
		/// </summary>
		/// <returns>The piece symbol</returns>
		public string GetSymbol()
		{
			return Symbol;
		}

		/// <summary>
		/// Piece Type Getter
		/// </summary>
		/// <returns>The type of Piece</returns>
		public string GetTypeOfPiece()
		{
			return TypeOfPiece;
		}

		/// <summary>
		/// BelongsTo Getter
		/// </summary>
		/// <returns>The Player the Piece belongs to</returns>
		public Player GetBelongsTo()
		{
			return BelongsTo;
		}

		/// <summary>
		/// PointsIfCaptured Getter
		/// </summary>
		/// <returns>The points awarded if the piece is captured</returns>
		public int GetPointsIfCaptured()
		{
			return PointsIfCaptured;
		}
	}
}

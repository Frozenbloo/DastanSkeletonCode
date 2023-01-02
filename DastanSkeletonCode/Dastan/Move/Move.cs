using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
	class Move
	{
		protected int RowChange, ColumnChange;

		/// <summary>
		/// The default constructor for the move class
		/// <para>
		/// When a new move is played
		/// a new move object is created.
		/// </para>
		/// </summary>
		/// <param name="R">The Row Change</param>
		/// <param name="C">The Column Change</param>
		public Move(int R, int C)
		{
			RowChange = R;
			ColumnChange = C;
		}

		/// <summary>
		/// Row Change Getter
		/// </summary>
		/// <returns>The Row Change</returns>
		public int GetRowChange()
		{
			return RowChange;
		}

		/// <summary>
		/// Column Change Getter
		/// </summary>
		/// <returns>The Column Change</returns>
		public int GetColumnChange()
		{
			return ColumnChange;
		}
	}
}

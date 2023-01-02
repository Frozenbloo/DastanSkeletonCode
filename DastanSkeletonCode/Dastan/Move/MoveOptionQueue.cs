using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
	class MoveOptionQueue
	{
		/// <summary>
		/// The move option queue
		/// </summary>
		private List<MoveOption> Queue = new List<MoveOption>();

		/// <summary>
		/// Converts the queue into a string
		/// </summary>
		/// <returns>The Queue as a string</returns>
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

		/// <summary>
		/// Adds a move option to the queue
		/// </summary>
		/// <param name="NewMoveOption">The move to add to the queue</param>
		public void Add(MoveOption NewMoveOption)
		{
			Queue.Add(NewMoveOption);
		}

		/// <summary>
		/// Replaces items within the queue
		/// </summary>
		/// <param name="Position">The index to replace</param>
		/// <param name="NewMoveOption">The new move option</param>
		public void Replace(int Position, MoveOption NewMoveOption)
		{
			Queue[Position] = NewMoveOption;
		}

		/// <summary>
		/// Moves an item from any position to the back of the queue
		/// </summary>
		/// <param name="Position">The position of the item to move to the back</param>
		public void MoveItemToBack(int Position)
		{
			MoveOption Temp = Queue[Position];
			Queue.RemoveAt(Position);
			Queue.Add(Temp);
		}

		/// <summary>
		/// Gets the move option
		/// </summary>
		/// <param name="Pos">The position of the move option</param>
		/// <returns>The move option</returns>
		public MoveOption GetMoveOptionInPosition(int Pos)
		{
			return Queue[Pos];
		}
	}
}

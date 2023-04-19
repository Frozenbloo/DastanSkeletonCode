using System;
using System.Collections.Generic;
using System.Text;

namespace DastanSkeletonCode
{
    class Ta_ziz : Square
    {
        private int CampedTurns;
        private Player ownerShip = null;

        public Ta_ziz(string S) : base()
        {
            this.CampedTurns = 0;
            this.ownerShip = null;
            Symbol = S;
        }

        public override void SetPiece(Piece P)
        {
            if (P.GetBelongsTo().GetName() == "Player One")
            {
                this.Symbol = "A";
                this.PieceInSquare = P;
                ownerShip = P.GetBelongsTo();
            }
            else if (P.GetBelongsTo().GetName() == "Player Two")
            {
                this.Symbol = "a";
                this.PieceInSquare = P;
                ownerShip = P.GetBelongsTo();
            }
        }

        public override Piece RemovePiece()
        {
            this.Symbol = "x";
            CampedTurns = 0;
            ownerShip = null;
            Piece PieceToReturn = this.GetPieceInSquare();
            PieceInSquare = null;
            return PieceToReturn;
        }

        public override bool GetCampedTwoTurns()
        {
            if (CampedTurns == 4)
            {
                CampedTurns = 0;
                return true;
            } else
            {
                if (PieceInSquare != null) CampedTurns++;
                return false;
            }
        }
    }
}

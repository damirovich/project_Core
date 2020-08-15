using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;
namespace SUBDCORE.Repository
{
    public class Positionrepository : ICrudable<Position>
    {
        SQLSpAdapter sqladapter;
        List<Position> positions = new List<Position>();
        public IEnumerable<Position> GetList()
        {
            sqladapter = new SQLSpAdapter("GetPosition");
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage)
            {
                positions.Add(new Position()
                {
                    IdPosition = Convert.ToByte(row[0]),
                    Position1 = row[1].ToString()
                });
            }
            return positions;
        }
        public void Create(Position position)
        {
            sqladapter = new SQLSpAdapter("AddPosition");
            sqladapter.AddSqlParameter("@PositionName", position.Position1);
            sqladapter.ExecNonQuery();
        }
        public void Update(Position position)
        {
            sqladapter = new SQLSpAdapter("UpPosition");
            sqladapter.AddSqlParameter("@idposition", position.IdPosition);
            sqladapter.AddSqlParameter("@PositionName", position.Position1);
            sqladapter.ExecNonQuery();
        }
        public void Delete(int id)
        {
            sqladapter = new SQLSpAdapter("DelPosition");
            sqladapter.AddSqlParameter("@idposition", id);
            sqladapter.ExecNonQuery();
        }
    }
}

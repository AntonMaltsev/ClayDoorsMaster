using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.DAL.DTO;

namespace Api.DAL.Repository
{
    public class ClayDoorRepository : BaseRepository<ClayDoor>
    {
        public IQueryable<ClayDoor> GetDoorQuery()
        {
            return All();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL;

namespace DAL
{
    class DBManager
    {
        private static ILNZU_dbContext db;

        public DBManager(ILNZU_dbContext dbContext)
        {
            db = dbContext;
        }

        public List<int> getUsers(int meetingRoomId)
        {
            return (from pairs in db.UserMemberOfMeetingRoom
                    where pairs.MeetingRoomId == meetingRoomId
                    select pairs.UserId).ToList();
        }
    }
}

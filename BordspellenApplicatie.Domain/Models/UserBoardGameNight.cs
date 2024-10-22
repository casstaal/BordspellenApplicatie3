using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Models
{
    public class UserBoardGameNight
    {
        public int Id { get; set; }
        public int BoardGameNightId { get; set; }
        public virtual BoardGameNight GameNight { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
